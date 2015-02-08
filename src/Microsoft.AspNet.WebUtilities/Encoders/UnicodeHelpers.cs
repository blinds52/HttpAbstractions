// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.AspNet.WebUtilities.Encoders
{
    /// <summary>
    /// Contains helpers for dealing with Unicode code points.
    /// </summary>
    internal unsafe static class UnicodeHelpers
    {
        /// <summary>
        /// Used for invalid Unicode sequences or other unrepresentable values.
        /// </summary>
        private const char UNICODE_REPLACEMENT_CHAR = '\uFFFD';
        
        private static uint[] _definedCharacterBitmap;

        /// <summary>
        /// Helper method which creates a bitmap of all characters which are
        /// defined per version 7.0.0 of the Unicode specification.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static uint[] CreateDefinedCharacterBitmap()
        {
            // The stream should be exactly 8KB in size.
            var stream = typeof(UnicodeHelpers).GetTypeInfo().Assembly.GetManifestResourceStream("compiler/resources/unicode-7.0.0-defined-characters.bin");
            if (stream.Length != 8 * 1024)
            {
                Environment.FailFast("Corrupt data detected.");
            }

            // Read everything in as raw bytes.
            byte[] rawData = new byte[8 * 1024];
            for (int numBytesReadTotal = 0; numBytesReadTotal < rawData.Length; )
            {
                int numBytesReadThisIteration = stream.Read(rawData, numBytesReadTotal, rawData.Length - numBytesReadTotal);
                if (numBytesReadThisIteration == 0)
                {
                    Environment.FailFast("Corrupt data detected.");
                }
                numBytesReadTotal += numBytesReadThisIteration;
            }

            // Finally, convert the byte[] to a uint[].
            // The incoming bytes are little-endian.
            uint[] retVal = new uint[2 * 1024];
            for (int i = 0; i < retVal.Length; i++)
            {
                retVal[i] = (((uint)rawData[4 * i + 3]) << 24)
                    | (((uint)rawData[4 * i + 2]) << 16)
                    | (((uint)rawData[4 * i + 1]) << 8)
                    | (uint)rawData[4 * i];
            }

            // And we're done!
            Volatile.Write(ref _definedCharacterBitmap, retVal);
            return retVal;
        }

        /// <summary>
        /// Returns a bitmap of all characters which are defined per version 7.0.0
        /// of the Unicode specification.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static uint[] GetDefinedCharacterBitmap()
        {
            return Volatile.Read(ref _definedCharacterBitmap) ?? CreateDefinedCharacterBitmap();
        }

        /// <summary>
        /// Given a UTF-16 character stream, reads the next scalar value from the stream.
        /// Set 'endOfString' to true if 'pChar' points to the last character in the stream.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int GetScalarValueFromUtf16(char* pChar, bool endOfString)
        {
            // This method is marked as AggressiveInlining to handle the common case of a non-surrogate
            // character. The surrogate case is handled in the slower fallback code path.
            char thisChar = *pChar;
            return (Char.IsSurrogate(thisChar)) ? GetScalarValueFromUtf16Slow(pChar, endOfString) : thisChar;
        }

        private static int GetScalarValueFromUtf16Slow(char* pChar, bool endOfString)
        {
            char firstChar = pChar[0];

            if (!Char.IsSurrogate(firstChar))
            {
                Debug.Fail("This case should've been handled by the fast path.");
                return firstChar;
            }
            else if (Char.IsHighSurrogate(firstChar))
            {
                if (endOfString)
                {
                    // unmatched surrogate - substitute
                    return UNICODE_REPLACEMENT_CHAR;
                }
                else
                {
                    char secondChar = pChar[1];
                    if (Char.IsLowSurrogate(secondChar))
                    {
                        // valid surrogate pair - extract codepoint
                        return GetScalarValueFromUtf16SurrogatePair(firstChar, secondChar);
                    }
                    else
                    {
                        // unmatched surrogate - substitute
                        return UNICODE_REPLACEMENT_CHAR;
                    }
                }
            }
            else
            {
                // unmatched surrogate - substitute
                Debug.Assert(Char.IsLowSurrogate(firstChar));
                return UNICODE_REPLACEMENT_CHAR;
            }
        }

        private static int GetScalarValueFromUtf16SurrogatePair(char highSurrogate, char lowSurrogate)
        {
            Debug.Assert(Char.IsHighSurrogate(highSurrogate));
            Debug.Assert(Char.IsLowSurrogate(lowSurrogate));

            // See http://www.unicode.org/versions/Unicode6.2.0/ch03.pdf, Table 3.5 for the
            // details of this conversion. We don't use Char.ConvertToUtf32 because its exception
            // handling shows up on the hot path, and our caller has already sanitized the inputs.
            return (lowSurrogate & 0x3ff) | (((highSurrogate & 0x3ff) + (1 << 6)) << 10);
        }
        
        /// <summary>
        /// Returns a value stating whether a character is defined per version 7.0.0
        /// of the Unicode specification. Certain classes of characters (control chars,
        /// private use, surrogates, some whitespace) are considered "undefined" for
        /// our purposes.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsCharacterDefined(char c)
        {
            uint codePoint = (uint)c;
            int index = (int)(codePoint >> 5);
            int offset = (int)(codePoint & 0x1FU);
            return ((GetDefinedCharacterBitmap()[index] >> offset) & 0x1U) != 0;
        }

        /// <summary>
        /// Determines whether the given scalar value is in the supplementary plane and thus
        /// requires 2 characters to be represented in UTF-16 (as a surrogate pair).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsSupplementaryCodePoint(int scalar)
        {
            return ((scalar & ~((int)Char.MaxValue)) != 0);
        }
    }
}

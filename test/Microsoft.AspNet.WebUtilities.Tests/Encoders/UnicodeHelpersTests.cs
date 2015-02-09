// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace Microsoft.AspNet.WebUtilities.Encoders
{
    public unsafe class UnicodeHelpersTests
    {
        private const int UnicodeReplacementChar = '\uFFFD';

        private static readonly UTF8Encoding _utf8EncodingThrowOnInvalidBytes = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

        [Fact]
        public void GetDefinedCharacterBitmap_ReturnsSingletonInstance()
        {
            // Act
            uint[] retVal1 = UnicodeHelpers.GetDefinedCharacterBitmap();
            uint[] retVal2 = UnicodeHelpers.GetDefinedCharacterBitmap();

            // Assert
            Assert.Same(retVal1, retVal2);
        }

        [Theory]
        [InlineData("a", (int)'a')] // normal BMP char, end of string
        [InlineData("ab", (int)'a')] // normal BMP char, not end of string
        [InlineData("\uDFFF", UnicodeReplacementChar)] // trailing surrogate, end of string
        [InlineData("\uDFFFx", UnicodeReplacementChar)] // trailing surrogate, not end of string
        [InlineData("\uD800", UnicodeReplacementChar)] // leading surrogate, end of string
        [InlineData("\uD800x", UnicodeReplacementChar)] // leading surrogate, not end of string, followed by non-surrogate
        [InlineData("\uD800\uD800", UnicodeReplacementChar)] // leading surrogate, not end of string, followed by leading surrogate
        [InlineData("\uD800\uDFFF", 0x103FF)] // leading surrogate, not end of string, followed by trailing surrogate
        public void GetScalarValueFromUtf16(string input, int expectedResult)
        {
            fixed (char* pInput = input)
            {
                Assert.Equal(expectedResult, UnicodeHelpers.GetScalarValueFromUtf16(pInput, endOfString: (input.Length == 1)));
            }
        }

        [Fact]
        public void GetUtf8RepresentationForScalarValue()
        {
            for (int i = 0; i <= 0x10FFFF; i++)
            {
                if (i <= 0xFFFF && Char.IsSurrogate((char)i))
                {
                    continue; // no surrogates
                }

                // Arrange
                byte[] expectedUtf8Bytes = _utf8EncodingThrowOnInvalidBytes.GetBytes(Char.ConvertFromUtf32(i));

                // Act
                List<byte> actualUtf8Bytes = new List<byte>(4);
                uint asUtf8 = (uint)UnicodeHelpers.GetUtf8RepresentationForScalarValue((uint)i);
                do
                {
                    actualUtf8Bytes.Add((byte)asUtf8);
                } while ((asUtf8 >>= 8) != 0);

                // Assert
                Assert.Equal(expectedUtf8Bytes, actualUtf8Bytes);
            }
        }

        [Fact]
        public void IsCharacterDefined()
        {
            // Arrange
            bool[] definedChars = ReadListOfDefinedCharacters();

            // Act & assert
            for (int i = 0; i <= Char.MaxValue; i++)
            {
                bool expected = definedChars[i];
                bool actual = UnicodeHelpers.IsCharacterDefined((char)i);
                Assert.Equal(expected, actual);
            }
        }

        private static bool[] ReadListOfDefinedCharacters()
        {
            HashSet<string> bannedCategories = new HashSet<string>() { "Cc", "Cs", "Co", "Cn", "Zl", "Zp" };
            bool[] retVal = new bool[0x10000];
            var stream = typeof(UnicodeHelpersTests).GetTypeInfo().Assembly.GetManifestResourceStream("../../encoder-resources/UnicodeData.txt");

            var streamReader = new StreamReader(stream);
            while (true)
            {
                string line = streamReader.ReadLine();
                if (line == null)
                {
                    break;
                }

                string[] splitLine = line.Split(';');
                uint codePoint = UInt32.Parse(splitLine[0], NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                if (codePoint >= retVal.Length)
                {
                    continue; // don't care about supplementary chars
                }

                string category = splitLine[2];
                if (bannedCategories.Contains(category))
                {
                    continue; // skip this character
                }

                retVal[codePoint] = true; // defined!
            }
            return retVal;
        }
    }
}

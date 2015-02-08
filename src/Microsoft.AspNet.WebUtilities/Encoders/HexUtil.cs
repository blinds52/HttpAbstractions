// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.AspNet.WebUtilities.Encoders
{
    /// <summary>
    /// Contains helpers for dealing with byte-hex char conversions.
    /// </summary>
    internal static class HexUtil
    {
        /// <summary>
        /// Converts a number 0 - 15 to its associated hex character '0' - 'F'.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static char IntToChar(uint i)
        {
            Debug.Assert(i < 16);
            return (i < 10) ? (char)('0' + i) : (char)('A' + (i - 10));
        }
    }
}

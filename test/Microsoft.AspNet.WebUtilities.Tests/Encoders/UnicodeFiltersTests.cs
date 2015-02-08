// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Microsoft.AspNet.WebUtilities.Encoders
{
    public class UnicodeFiltersTests
    {
        [Fact]
        public void Range_None()
        {
            // Act & assert
            int[] codePoints = UnicodeFilters.None.GetAllowedCodePoints().ToArray();
            Assert.Empty(codePoints);
        }

        [Fact]
        public void Range_All()
        {
            RunRangeTestImpl(UnicodeFilters.All, first: '\u0000', last: '\uFFFF');
        }

        [Fact]
        public void Range_BasicLatin()
        {
            RunRangeTestImpl(UnicodeFilters.BasicLatin, first: '\u0000', last: '\u007F');
        }

        [Fact]
        public void Range_Latin1Supplement()
        {
            RunRangeTestImpl(UnicodeFilters.Latin1Supplement, first: '\u0080', last: '\u00FF');
        }

        [Fact]
        public void Range_LatinExtendedA()
        {
            RunRangeTestImpl(UnicodeFilters.LatinExtendedA, first: '\u0100', last: '\u017F');
        }

        [Fact]
        public void Range_LatinExtendedB()
        {
            RunRangeTestImpl(UnicodeFilters.LatinExtendedB, first: '\u0180', last: '\u024F');
        }

        [Fact]
        public void Range_IPAExtensions()
        {
            RunRangeTestImpl(UnicodeFilters.IPAExtensions, first: '\u0250', last: '\u02AF');
        }

        [Fact]
        public void Range_SpacingModifierLetters()
        {
            RunRangeTestImpl(UnicodeFilters.SpacingModifierLetters, first: '\u02B0', last: '\u02FF');
        }

        [Fact]
        public void Range_CombiningDiacriticalMarks()
        {
            RunRangeTestImpl(UnicodeFilters.CombiningDiacriticalMarks, first: '\u0300', last: '\u036F');
        }

        [Fact]
        public void Range_GreekandCoptic()
        {
            RunRangeTestImpl(UnicodeFilters.GreekandCoptic, first: '\u0370', last: '\u03FF');
        }

        [Fact]
        public void Range_Cyrillic()
        {
            RunRangeTestImpl(UnicodeFilters.Cyrillic, first: '\u0400', last: '\u04FF');
        }

        [Fact]
        public void Range_CyrillicSupplement()
        {
            RunRangeTestImpl(UnicodeFilters.CyrillicSupplement, first: '\u0500', last: '\u052F');
        }

        [Fact]
        public void Range_Armenian()
        {
            RunRangeTestImpl(UnicodeFilters.Armenian, first: '\u0530', last: '\u058F');
        }

        [Fact]
        public void Range_Hebrew()
        {
            RunRangeTestImpl(UnicodeFilters.Hebrew, first: '\u0590', last: '\u05FF');
        }

        [Fact]
        public void Range_Arabic()
        {
            RunRangeTestImpl(UnicodeFilters.Arabic, first: '\u0600', last: '\u06FF');
        }

        [Fact]
        public void Range_Syriac()
        {
            RunRangeTestImpl(UnicodeFilters.Syriac, first: '\u0700', last: '\u074F');
        }

        [Fact]
        public void Range_ArabicSupplement()
        {
            RunRangeTestImpl(UnicodeFilters.ArabicSupplement, first: '\u0750', last: '\u077F');
        }

        [Fact]
        public void Range_Thaana()
        {
            RunRangeTestImpl(UnicodeFilters.Thaana, first: '\u0780', last: '\u07BF');
        }

        [Fact]
        public void Range_NKo()
        {
            RunRangeTestImpl(UnicodeFilters.NKo, first: '\u07C0', last: '\u07FF');
        }

        [Fact]
        public void Range_Samaritan()
        {
            RunRangeTestImpl(UnicodeFilters.Samaritan, first: '\u0800', last: '\u083F');
        }

        [Fact]
        public void Range_Mandaic()
        {
            RunRangeTestImpl(UnicodeFilters.Mandaic, first: '\u0840', last: '\u085F');
        }

        [Fact]
        public void Range_ArabicExtendedA()
        {
            RunRangeTestImpl(UnicodeFilters.ArabicExtendedA, first: '\u08A0', last: '\u08FF');
        }

        [Fact]
        public void Range_Devanagari()
        {
            RunRangeTestImpl(UnicodeFilters.Devanagari, first: '\u0900', last: '\u097F');
        }

        [Fact]
        public void Range_Bengali()
        {
            RunRangeTestImpl(UnicodeFilters.Bengali, first: '\u0980', last: '\u09FF');
        }

        [Fact]
        public void Range_Gurmukhi()
        {
            RunRangeTestImpl(UnicodeFilters.Gurmukhi, first: '\u0A00', last: '\u0A7F');
        }

        [Fact]
        public void Range_Gujarati()
        {
            RunRangeTestImpl(UnicodeFilters.Gujarati, first: '\u0A80', last: '\u0AFF');
        }

        [Fact]
        public void Range_Oriya()
        {
            RunRangeTestImpl(UnicodeFilters.Oriya, first: '\u0B00', last: '\u0B7F');
        }

        [Fact]
        public void Range_Tamil()
        {
            RunRangeTestImpl(UnicodeFilters.Tamil, first: '\u0B80', last: '\u0BFF');
        }

        [Fact]
        public void Range_Telugu()
        {
            RunRangeTestImpl(UnicodeFilters.Telugu, first: '\u0C00', last: '\u0C7F');
        }

        [Fact]
        public void Range_Kannada()
        {
            RunRangeTestImpl(UnicodeFilters.Kannada, first: '\u0C80', last: '\u0CFF');
        }

        [Fact]
        public void Range_Malayalam()
        {
            RunRangeTestImpl(UnicodeFilters.Malayalam, first: '\u0D00', last: '\u0D7F');
        }

        [Fact]
        public void Range_Sinhala()
        {
            RunRangeTestImpl(UnicodeFilters.Sinhala, first: '\u0D80', last: '\u0DFF');
        }

        [Fact]
        public void Range_Thai()
        {
            RunRangeTestImpl(UnicodeFilters.Thai, first: '\u0E00', last: '\u0E7F');
        }

        [Fact]
        public void Range_Lao()
        {
            RunRangeTestImpl(UnicodeFilters.Lao, first: '\u0E80', last: '\u0EFF');
        }

        [Fact]
        public void Range_Tibetan()
        {
            RunRangeTestImpl(UnicodeFilters.Tibetan, first: '\u0F00', last: '\u0FFF');
        }

        [Fact]
        public void Range_Myanmar()
        {
            RunRangeTestImpl(UnicodeFilters.Myanmar, first: '\u1000', last: '\u109F');
        }

        [Fact]
        public void Range_Georgian()
        {
            RunRangeTestImpl(UnicodeFilters.Georgian, first: '\u10A0', last: '\u10FF');
        }

        [Fact]
        public void Range_HangulJamo()
        {
            RunRangeTestImpl(UnicodeFilters.HangulJamo, first: '\u1100', last: '\u11FF');
        }

        [Fact]
        public void Range_Ethiopic()
        {
            RunRangeTestImpl(UnicodeFilters.Ethiopic, first: '\u1200', last: '\u137F');
        }

        [Fact]
        public void Range_EthiopicSupplement()
        {
            RunRangeTestImpl(UnicodeFilters.EthiopicSupplement, first: '\u1380', last: '\u139F');
        }

        [Fact]
        public void Range_Cherokee()
        {
            RunRangeTestImpl(UnicodeFilters.Cherokee, first: '\u13A0', last: '\u13FF');
        }

        [Fact]
        public void Range_UnifiedCanadianAboriginalSyllabics()
        {
            RunRangeTestImpl(UnicodeFilters.UnifiedCanadianAboriginalSyllabics, first: '\u1400', last: '\u167F');
        }

        [Fact]
        public void Range_Ogham()
        {
            RunRangeTestImpl(UnicodeFilters.Ogham, first: '\u1680', last: '\u169F');
        }

        [Fact]
        public void Range_Runic()
        {
            RunRangeTestImpl(UnicodeFilters.Runic, first: '\u16A0', last: '\u16FF');
        }

        [Fact]
        public void Range_Tagalog()
        {
            RunRangeTestImpl(UnicodeFilters.Tagalog, first: '\u1700', last: '\u171F');
        }

        [Fact]
        public void Range_Hanunoo()
        {
            RunRangeTestImpl(UnicodeFilters.Hanunoo, first: '\u1720', last: '\u173F');
        }

        [Fact]
        public void Range_Buhid()
        {
            RunRangeTestImpl(UnicodeFilters.Buhid, first: '\u1740', last: '\u175F');
        }

        [Fact]
        public void Range_Tagbanwa()
        {
            RunRangeTestImpl(UnicodeFilters.Tagbanwa, first: '\u1760', last: '\u177F');
        }

        [Fact]
        public void Range_Khmer()
        {
            RunRangeTestImpl(UnicodeFilters.Khmer, first: '\u1780', last: '\u17FF');
        }

        [Fact]
        public void Range_Mongolian()
        {
            RunRangeTestImpl(UnicodeFilters.Mongolian, first: '\u1800', last: '\u18AF');
        }

        [Fact]
        public void Range_UnifiedCanadianAboriginalSyllabicsExtended()
        {
            RunRangeTestImpl(UnicodeFilters.UnifiedCanadianAboriginalSyllabicsExtended, first: '\u18B0', last: '\u18FF');
        }

        [Fact]
        public void Range_Limbu()
        {
            RunRangeTestImpl(UnicodeFilters.Limbu, first: '\u1900', last: '\u194F');
        }

        [Fact]
        public void Range_TaiLe()
        {
            RunRangeTestImpl(UnicodeFilters.TaiLe, first: '\u1950', last: '\u197F');
        }

        [Fact]
        public void Range_NewTaiLue()
        {
            RunRangeTestImpl(UnicodeFilters.NewTaiLue, first: '\u1980', last: '\u19DF');
        }

        [Fact]
        public void Range_KhmerSymbols()
        {
            RunRangeTestImpl(UnicodeFilters.KhmerSymbols, first: '\u19E0', last: '\u19FF');
        }

        [Fact]
        public void Range_Buginese()
        {
            RunRangeTestImpl(UnicodeFilters.Buginese, first: '\u1A00', last: '\u1A1F');
        }

        [Fact]
        public void Range_TaiTham()
        {
            RunRangeTestImpl(UnicodeFilters.TaiTham, first: '\u1A20', last: '\u1AAF');
        }

        [Fact]
        public void Range_CombiningDiacriticalMarksExtended()
        {
            RunRangeTestImpl(UnicodeFilters.CombiningDiacriticalMarksExtended, first: '\u1AB0', last: '\u1AFF');
        }

        [Fact]
        public void Range_Balinese()
        {
            RunRangeTestImpl(UnicodeFilters.Balinese, first: '\u1B00', last: '\u1B7F');
        }

        [Fact]
        public void Range_Sundanese()
        {
            RunRangeTestImpl(UnicodeFilters.Sundanese, first: '\u1B80', last: '\u1BBF');
        }

        [Fact]
        public void Range_Batak()
        {
            RunRangeTestImpl(UnicodeFilters.Batak, first: '\u1BC0', last: '\u1BFF');
        }

        [Fact]
        public void Range_Lepcha()
        {
            RunRangeTestImpl(UnicodeFilters.Lepcha, first: '\u1C00', last: '\u1C4F');
        }

        [Fact]
        public void Range_OlChiki()
        {
            RunRangeTestImpl(UnicodeFilters.OlChiki, first: '\u1C50', last: '\u1C7F');
        }

        [Fact]
        public void Range_SundaneseSupplement()
        {
            RunRangeTestImpl(UnicodeFilters.SundaneseSupplement, first: '\u1CC0', last: '\u1CCF');
        }

        [Fact]
        public void Range_VedicExtensions()
        {
            RunRangeTestImpl(UnicodeFilters.VedicExtensions, first: '\u1CD0', last: '\u1CFF');
        }

        [Fact]
        public void Range_PhoneticExtensions()
        {
            RunRangeTestImpl(UnicodeFilters.PhoneticExtensions, first: '\u1D00', last: '\u1D7F');
        }

        [Fact]
        public void Range_PhoneticExtensionsSupplement()
        {
            RunRangeTestImpl(UnicodeFilters.PhoneticExtensionsSupplement, first: '\u1D80', last: '\u1DBF');
        }

        [Fact]
        public void Range_CombiningDiacriticalMarksSupplement()
        {
            RunRangeTestImpl(UnicodeFilters.CombiningDiacriticalMarksSupplement, first: '\u1DC0', last: '\u1DFF');
        }

        [Fact]
        public void Range_LatinExtendedAdditional()
        {
            RunRangeTestImpl(UnicodeFilters.LatinExtendedAdditional, first: '\u1E00', last: '\u1EFF');
        }

        [Fact]
        public void Range_GreekExtended()
        {
            RunRangeTestImpl(UnicodeFilters.GreekExtended, first: '\u1F00', last: '\u1FFF');
        }

        [Fact]
        public void Range_GeneralPunctuation()
        {
            RunRangeTestImpl(UnicodeFilters.GeneralPunctuation, first: '\u2000', last: '\u206F');
        }

        [Fact]
        public void Range_SuperscriptsandSubscripts()
        {
            RunRangeTestImpl(UnicodeFilters.SuperscriptsandSubscripts, first: '\u2070', last: '\u209F');
        }

        [Fact]
        public void Range_CurrencySymbols()
        {
            RunRangeTestImpl(UnicodeFilters.CurrencySymbols, first: '\u20A0', last: '\u20CF');
        }

        [Fact]
        public void Range_CombiningDiacriticalMarksforSymbols()
        {
            RunRangeTestImpl(UnicodeFilters.CombiningDiacriticalMarksforSymbols, first: '\u20D0', last: '\u20FF');
        }

        [Fact]
        public void Range_LetterlikeSymbols()
        {
            RunRangeTestImpl(UnicodeFilters.LetterlikeSymbols, first: '\u2100', last: '\u214F');
        }

        [Fact]
        public void Range_NumberForms()
        {
            RunRangeTestImpl(UnicodeFilters.NumberForms, first: '\u2150', last: '\u218F');
        }

        [Fact]
        public void Range_Arrows()
        {
            RunRangeTestImpl(UnicodeFilters.Arrows, first: '\u2190', last: '\u21FF');
        }

        [Fact]
        public void Range_MathematicalOperators()
        {
            RunRangeTestImpl(UnicodeFilters.MathematicalOperators, first: '\u2200', last: '\u22FF');
        }

        [Fact]
        public void Range_MiscellaneousTechnical()
        {
            RunRangeTestImpl(UnicodeFilters.MiscellaneousTechnical, first: '\u2300', last: '\u23FF');
        }

        [Fact]
        public void Range_ControlPictures()
        {
            RunRangeTestImpl(UnicodeFilters.ControlPictures, first: '\u2400', last: '\u243F');
        }

        [Fact]
        public void Range_OpticalCharacterRecognition()
        {
            RunRangeTestImpl(UnicodeFilters.OpticalCharacterRecognition, first: '\u2440', last: '\u245F');
        }

        [Fact]
        public void Range_EnclosedAlphanumerics()
        {
            RunRangeTestImpl(UnicodeFilters.EnclosedAlphanumerics, first: '\u2460', last: '\u24FF');
        }

        [Fact]
        public void Range_BoxDrawing()
        {
            RunRangeTestImpl(UnicodeFilters.BoxDrawing, first: '\u2500', last: '\u257F');
        }

        [Fact]
        public void Range_BlockElements()
        {
            RunRangeTestImpl(UnicodeFilters.BlockElements, first: '\u2580', last: '\u259F');
        }

        [Fact]
        public void Range_GeometricShapes()
        {
            RunRangeTestImpl(UnicodeFilters.GeometricShapes, first: '\u25A0', last: '\u25FF');
        }

        [Fact]
        public void Range_MiscellaneousSymbols()
        {
            RunRangeTestImpl(UnicodeFilters.MiscellaneousSymbols, first: '\u2600', last: '\u26FF');
        }

        [Fact]
        public void Range_Dingbats()
        {
            RunRangeTestImpl(UnicodeFilters.Dingbats, first: '\u2700', last: '\u27BF');
        }

        [Fact]
        public void Range_MiscellaneousMathematicalSymbolsA()
        {
            RunRangeTestImpl(UnicodeFilters.MiscellaneousMathematicalSymbolsA, first: '\u27C0', last: '\u27EF');
        }

        [Fact]
        public void Range_SupplementalArrowsA()
        {
            RunRangeTestImpl(UnicodeFilters.SupplementalArrowsA, first: '\u27F0', last: '\u27FF');
        }

        [Fact]
        public void Range_BraillePatterns()
        {
            RunRangeTestImpl(UnicodeFilters.BraillePatterns, first: '\u2800', last: '\u28FF');
        }

        [Fact]
        public void Range_SupplementalArrowsB()
        {
            RunRangeTestImpl(UnicodeFilters.SupplementalArrowsB, first: '\u2900', last: '\u297F');
        }

        [Fact]
        public void Range_MiscellaneousMathematicalSymbolsB()
        {
            RunRangeTestImpl(UnicodeFilters.MiscellaneousMathematicalSymbolsB, first: '\u2980', last: '\u29FF');
        }

        [Fact]
        public void Range_SupplementalMathematicalOperators()
        {
            RunRangeTestImpl(UnicodeFilters.SupplementalMathematicalOperators, first: '\u2A00', last: '\u2AFF');
        }

        [Fact]
        public void Range_MiscellaneousSymbolsandArrows()
        {
            RunRangeTestImpl(UnicodeFilters.MiscellaneousSymbolsandArrows, first: '\u2B00', last: '\u2BFF');
        }

        [Fact]
        public void Range_Glagolitic()
        {
            RunRangeTestImpl(UnicodeFilters.Glagolitic, first: '\u2C00', last: '\u2C5F');
        }

        [Fact]
        public void Range_LatinExtendedC()
        {
            RunRangeTestImpl(UnicodeFilters.LatinExtendedC, first: '\u2C60', last: '\u2C7F');
        }

        [Fact]
        public void Range_Coptic()
        {
            RunRangeTestImpl(UnicodeFilters.Coptic, first: '\u2C80', last: '\u2CFF');
        }

        [Fact]
        public void Range_GeorgianSupplement()
        {
            RunRangeTestImpl(UnicodeFilters.GeorgianSupplement, first: '\u2D00', last: '\u2D2F');
        }

        [Fact]
        public void Range_Tifinagh()
        {
            RunRangeTestImpl(UnicodeFilters.Tifinagh, first: '\u2D30', last: '\u2D7F');
        }

        [Fact]
        public void Range_EthiopicExtended()
        {
            RunRangeTestImpl(UnicodeFilters.EthiopicExtended, first: '\u2D80', last: '\u2DDF');
        }

        [Fact]
        public void Range_CyrillicExtendedA()
        {
            RunRangeTestImpl(UnicodeFilters.CyrillicExtendedA, first: '\u2DE0', last: '\u2DFF');
        }

        [Fact]
        public void Range_SupplementalPunctuation()
        {
            RunRangeTestImpl(UnicodeFilters.SupplementalPunctuation, first: '\u2E00', last: '\u2E7F');
        }

        [Fact]
        public void Range_CJKRadicalsSupplement()
        {
            RunRangeTestImpl(UnicodeFilters.CJKRadicalsSupplement, first: '\u2E80', last: '\u2EFF');
        }

        [Fact]
        public void Range_KangxiRadicals()
        {
            RunRangeTestImpl(UnicodeFilters.KangxiRadicals, first: '\u2F00', last: '\u2FDF');
        }

        [Fact]
        public void Range_IdeographicDescriptionCharacters()
        {
            RunRangeTestImpl(UnicodeFilters.IdeographicDescriptionCharacters, first: '\u2FF0', last: '\u2FFF');
        }

        [Fact]
        public void Range_CJKSymbolsandPunctuation()
        {
            RunRangeTestImpl(UnicodeFilters.CJKSymbolsandPunctuation, first: '\u3000', last: '\u303F');
        }

        [Fact]
        public void Range_Hiragana()
        {
            RunRangeTestImpl(UnicodeFilters.Hiragana, first: '\u3040', last: '\u309F');
        }

        [Fact]
        public void Range_Katakana()
        {
            RunRangeTestImpl(UnicodeFilters.Katakana, first: '\u30A0', last: '\u30FF');
        }

        [Fact]
        public void Range_Bopomofo()
        {
            RunRangeTestImpl(UnicodeFilters.Bopomofo, first: '\u3100', last: '\u312F');
        }

        [Fact]
        public void Range_HangulCompatibilityJamo()
        {
            RunRangeTestImpl(UnicodeFilters.HangulCompatibilityJamo, first: '\u3130', last: '\u318F');
        }

        [Fact]
        public void Range_Kanbun()
        {
            RunRangeTestImpl(UnicodeFilters.Kanbun, first: '\u3190', last: '\u319F');
        }

        [Fact]
        public void Range_BopomofoExtended()
        {
            RunRangeTestImpl(UnicodeFilters.BopomofoExtended, first: '\u31A0', last: '\u31BF');
        }

        [Fact]
        public void Range_CJKStrokes()
        {
            RunRangeTestImpl(UnicodeFilters.CJKStrokes, first: '\u31C0', last: '\u31EF');
        }

        [Fact]
        public void Range_KatakanaPhoneticExtensions()
        {
            RunRangeTestImpl(UnicodeFilters.KatakanaPhoneticExtensions, first: '\u31F0', last: '\u31FF');
        }

        [Fact]
        public void Range_EnclosedCJKLettersandMonths()
        {
            RunRangeTestImpl(UnicodeFilters.EnclosedCJKLettersandMonths, first: '\u3200', last: '\u32FF');
        }

        [Fact]
        public void Range_CJKCompatibility()
        {
            RunRangeTestImpl(UnicodeFilters.CJKCompatibility, first: '\u3300', last: '\u33FF');
        }

        [Fact]
        public void Range_CJKUnifiedIdeographsExtensionA()
        {
            RunRangeTestImpl(UnicodeFilters.CJKUnifiedIdeographsExtensionA, first: '\u3400', last: '\u4DBF');
        }

        [Fact]
        public void Range_YijingHexagramSymbols()
        {
            RunRangeTestImpl(UnicodeFilters.YijingHexagramSymbols, first: '\u4DC0', last: '\u4DFF');
        }

        [Fact]
        public void Range_CJKUnifiedIdeographs()
        {
            RunRangeTestImpl(UnicodeFilters.CJKUnifiedIdeographs, first: '\u4E00', last: '\u9FFF');
        }

        [Fact]
        public void Range_YiSyllables()
        {
            RunRangeTestImpl(UnicodeFilters.YiSyllables, first: '\uA000', last: '\uA48F');
        }

        [Fact]
        public void Range_YiRadicals()
        {
            RunRangeTestImpl(UnicodeFilters.YiRadicals, first: '\uA490', last: '\uA4CF');
        }

        [Fact]
        public void Range_Lisu()
        {
            RunRangeTestImpl(UnicodeFilters.Lisu, first: '\uA4D0', last: '\uA4FF');
        }

        [Fact]
        public void Range_Vai()
        {
            RunRangeTestImpl(UnicodeFilters.Vai, first: '\uA500', last: '\uA63F');
        }

        [Fact]
        public void Range_CyrillicExtendedB()
        {
            RunRangeTestImpl(UnicodeFilters.CyrillicExtendedB, first: '\uA640', last: '\uA69F');
        }

        [Fact]
        public void Range_Bamum()
        {
            RunRangeTestImpl(UnicodeFilters.Bamum, first: '\uA6A0', last: '\uA6FF');
        }

        [Fact]
        public void Range_ModifierToneLetters()
        {
            RunRangeTestImpl(UnicodeFilters.ModifierToneLetters, first: '\uA700', last: '\uA71F');
        }

        [Fact]
        public void Range_LatinExtendedD()
        {
            RunRangeTestImpl(UnicodeFilters.LatinExtendedD, first: '\uA720', last: '\uA7FF');
        }

        [Fact]
        public void Range_SylotiNagri()
        {
            RunRangeTestImpl(UnicodeFilters.SylotiNagri, first: '\uA800', last: '\uA82F');
        }

        [Fact]
        public void Range_CommonIndicNumberForms()
        {
            RunRangeTestImpl(UnicodeFilters.CommonIndicNumberForms, first: '\uA830', last: '\uA83F');
        }

        [Fact]
        public void Range_Phagspa()
        {
            RunRangeTestImpl(UnicodeFilters.Phagspa, first: '\uA840', last: '\uA87F');
        }

        [Fact]
        public void Range_Saurashtra()
        {
            RunRangeTestImpl(UnicodeFilters.Saurashtra, first: '\uA880', last: '\uA8DF');
        }

        [Fact]
        public void Range_DevanagariExtended()
        {
            RunRangeTestImpl(UnicodeFilters.DevanagariExtended, first: '\uA8E0', last: '\uA8FF');
        }

        [Fact]
        public void Range_KayahLi()
        {
            RunRangeTestImpl(UnicodeFilters.KayahLi, first: '\uA900', last: '\uA92F');
        }

        [Fact]
        public void Range_Rejang()
        {
            RunRangeTestImpl(UnicodeFilters.Rejang, first: '\uA930', last: '\uA95F');
        }

        [Fact]
        public void Range_HangulJamoExtendedA()
        {
            RunRangeTestImpl(UnicodeFilters.HangulJamoExtendedA, first: '\uA960', last: '\uA97F');
        }

        [Fact]
        public void Range_Javanese()
        {
            RunRangeTestImpl(UnicodeFilters.Javanese, first: '\uA980', last: '\uA9DF');
        }

        [Fact]
        public void Range_MyanmarExtendedB()
        {
            RunRangeTestImpl(UnicodeFilters.MyanmarExtendedB, first: '\uA9E0', last: '\uA9FF');
        }

        [Fact]
        public void Range_Cham()
        {
            RunRangeTestImpl(UnicodeFilters.Cham, first: '\uAA00', last: '\uAA5F');
        }

        [Fact]
        public void Range_MyanmarExtendedA()
        {
            RunRangeTestImpl(UnicodeFilters.MyanmarExtendedA, first: '\uAA60', last: '\uAA7F');
        }

        [Fact]
        public void Range_TaiViet()
        {
            RunRangeTestImpl(UnicodeFilters.TaiViet, first: '\uAA80', last: '\uAADF');
        }

        [Fact]
        public void Range_MeeteiMayekExtensions()
        {
            RunRangeTestImpl(UnicodeFilters.MeeteiMayekExtensions, first: '\uAAE0', last: '\uAAFF');
        }

        [Fact]
        public void Range_EthiopicExtendedA()
        {
            RunRangeTestImpl(UnicodeFilters.EthiopicExtendedA, first: '\uAB00', last: '\uAB2F');
        }

        [Fact]
        public void Range_LatinExtendedE()
        {
            RunRangeTestImpl(UnicodeFilters.LatinExtendedE, first: '\uAB30', last: '\uAB6F');
        }

        [Fact]
        public void Range_MeeteiMayek()
        {
            RunRangeTestImpl(UnicodeFilters.MeeteiMayek, first: '\uABC0', last: '\uABFF');
        }

        [Fact]
        public void Range_HangulSyllables()
        {
            RunRangeTestImpl(UnicodeFilters.HangulSyllables, first: '\uAC00', last: '\uD7AF');
        }

        [Fact]
        public void Range_HangulJamoExtendedB()
        {
            RunRangeTestImpl(UnicodeFilters.HangulJamoExtendedB, first: '\uD7B0', last: '\uD7FF');
        }

        [Fact]
        public void Range_CJKCompatibilityIdeographs()
        {
            RunRangeTestImpl(UnicodeFilters.CJKCompatibilityIdeographs, first: '\uF900', last: '\uFAFF');
        }

        [Fact]
        public void Range_AlphabeticPresentationForms()
        {
            RunRangeTestImpl(UnicodeFilters.AlphabeticPresentationForms, first: '\uFB00', last: '\uFB4F');
        }

        [Fact]
        public void Range_ArabicPresentationFormsA()
        {
            RunRangeTestImpl(UnicodeFilters.ArabicPresentationFormsA, first: '\uFB50', last: '\uFDFF');
        }

        [Fact]
        public void Range_VariationSelectors()
        {
            RunRangeTestImpl(UnicodeFilters.VariationSelectors, first: '\uFE00', last: '\uFE0F');
        }

        [Fact]
        public void Range_VerticalForms()
        {
            RunRangeTestImpl(UnicodeFilters.VerticalForms, first: '\uFE10', last: '\uFE1F');
        }

        [Fact]
        public void Range_CombiningHalfMarks()
        {
            RunRangeTestImpl(UnicodeFilters.CombiningHalfMarks, first: '\uFE20', last: '\uFE2F');
        }

        [Fact]
        public void Range_CJKCompatibilityForms()
        {
            RunRangeTestImpl(UnicodeFilters.CJKCompatibilityForms, first: '\uFE30', last: '\uFE4F');
        }

        [Fact]
        public void Range_SmallFormVariants()
        {
            RunRangeTestImpl(UnicodeFilters.SmallFormVariants, first: '\uFE50', last: '\uFE6F');
        }

        [Fact]
        public void Range_ArabicPresentationFormsB()
        {
            RunRangeTestImpl(UnicodeFilters.ArabicPresentationFormsB, first: '\uFE70', last: '\uFEFF');
        }

        [Fact]
        public void Range_HalfwidthandFullwidthForms()
        {
            RunRangeTestImpl(UnicodeFilters.HalfwidthandFullwidthForms, first: '\uFF00', last: '\uFFEF');
        }

        [Fact]
        public void Range_Specials()
        {
            RunRangeTestImpl(UnicodeFilters.Specials, first: '\uFFF0', last: '\uFFFF');
        }

        private static void RunRangeTestImpl(ICodePointFilter filter, char first, char last)
        {
            // All defined chars in the range should be added to the set
            HashSet<char> charsStillToSee = new HashSet<char>();
            for (int i = first; i <= last; i++)
            {
                if (UnicodeHelpers.IsCharacterDefined((char)i))
                {
                    charsStillToSee.Add((char)i);
                }
            }

            // Each range should have *some* valid chars, otherwise the range is useless.
            Assert.NotEqual(0, charsStillToSee.Count);

            // Now iterate through the filter, checking that each code point is represented
            // in the hash set and occurs exactly once.
            foreach (int codePoint in filter.GetAllowedCodePoints())
            {
                char c = checked((char)codePoint);
                Assert.True(charsStillToSee.Remove(c), "Filter provided a code point that wasn't in the allow list.");
            }

            // Finally, the hash set should be emptied at this point.
            Assert.Equal(0, charsStillToSee.Count);
        }
    }
}

﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CK2Editor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using CK2Editor.Utility;

namespace Tests
{
    [TestClass]
    public class FormatUtilTests
    {
        [TestMethod]
        public void BasicUtilTest()
        {
            string text = "0123456789abcdefgho oijwao\t\n \n\tAxawjakl=\"g89\" nweabfgh98=\"\" xdk da={wefamkiwai{c}\t\nf3ds}\t\tawdwd=5\t\n\t\tseries={\n1 5 3 4 7 }\n";
            Assert.AreEqual(text, text.ToString());
            Assert.AreEqual(9, text.IndexOf("9"));
            Assert.AreEqual(-1, text.IndexOf("0", 10));
            Assert.AreEqual("wefamkiwai{c}\t\nf3ds", FormatUtil.ExtractDelimited(text, "da=").ToString());
            Assert.AreEqual(12, text.IndexOfAny(new string[] { "cdefgho o", "awjnwexdk da={wefa" }));
            Assert.AreEqual("5", FormatUtil.ExtractValue(text, "wdwd="));
            Assert.AreEqual("g89", FormatUtil.ExtractStringValue(text, "wjakl="));
            Assert.AreEqual("", FormatUtil.ExtractStringValue(text, "abfgh98="));
            Assert.AreEqual("\n1 5 3 4 7 ", FormatUtil.ExtractDelimited(text, "series=").ToString());
            Assert.AreEqual("1 5 3 4 7 ", FormatUtil.ReadValue(text, "series", "series").ToString());

            Dictionary<int, string> expected = new Dictionary<int, string>();
            expected.Add(0, "");
            expected.Add(20, "");
            expected.Add(31, "Axawjakl");
            expected.Add(46, "nweabfgh98");
            expected.Add(60, "");
            expected.Add(64, "da");
            expected.Add(90, "awdwd");
            expected.Add(101, "series");
            var result = FormatUtil.ListEntriesWithIndexes(text).ToDictionary();
            Assert.IsTrue(result.Count == expected.Count && result.SequenceEqual(expected));
        }

        [TestMethod]
        public void ListEntriesUtilTest()
        {
            string text;
            text = " dsa=b\n\t\t{\n\t}\n\ta=n\n\t\tabcdefgh={\n\t\t\tcba=\"3\"\n\t\t}";
            var entries = FormatUtil.ListEntriesWithIndexes(text).ToDictionary();
            Assert.AreEqual(entries[9], "");
            Assert.AreEqual(4, entries.Count);

            text = File.ReadAllText(TestsReference.MIN_TEST_PATH).Substring(6);
            if (text.StartsWith(FormattedReader.SAVE_HEADER)) //the header, specified in the constant above, is not part of the hierarchy
                text = text.Substring(FormattedReader.SAVE_HEADER.Length);
            text = text.TrimEnd(new char[] { ' ', '\n', '\t', '\r' });
            if (text.EndsWith(FormattedReader.SAVE_FOOTER)) //same for footer
                text = text.Substring(0, text.Length - FormattedReader.SAVE_FOOTER.Length);
            entries = FormatUtil.ListEntriesWithIndexes(text).ToDictionary();
            Dictionary<int, string> expected = new Dictionary<int, string>();
            expected.Add(3, "version");
            expected.Add(23, "date");
            expected.Add(40, "player");
            expected.Add(82, "player_realm");
            expected.Add(111, "dyn_title");
            expected.Add(221, "dyn_title");
            expected.Add(286, "dyn_title");
            expected.Add(353, "rebel");
            expected.Add(363, "unit");
            expected.Add(377, "sub_unit");
            expected.Add(395, "start_date");
            expected.Add(418, "flags");
            expected.Add(559, "dynasties");
            expected.Add(1217, "character");
            Assert.IsTrue(entries.Count == expected.Count && entries.SequenceEqual(expected));
        }
    }
}

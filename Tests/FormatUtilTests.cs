using System;
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
        public void UtilTestMethod()
        {

            //FormattedReader fr = new FormattedReader(fpath);
            //Editor ed = fr.ReadFile(path);               
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

            text = " dsa=b\n\t\t{\n\t}\n\ta=n\n\t\tabcdefgh={\n\t\t\tcba=\"3\"\n\t\t}";
            var entries = FormatUtil.ListEntriesWithIndexes(text).ToDictionary();
            Assert.AreEqual(entries[9], "");
            Assert.AreEqual(4, entries.Count);

            text=File.ReadAllText(
        }
    }
}

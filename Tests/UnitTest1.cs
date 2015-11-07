using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CK2Editor;
using CK2Editor.Editors;
using System.IO;
using CK2Editor.Utility;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UtilTestMethod()
        {

            //FormattedReader fr = new FormattedReader(fpath);
            //Editor ed = fr.ReadFile(path);

            string text = "0123456789abcdefgho oijwao\t\n \n\tAxawjakl=\"g89\"nwexdk da={wefamkiwai{c}\t\nf3ds}\t\tawdwd=5\t\n\t\tseries= {\n1 5 3 4 7 }\n";
            FileSection fs = new FileSection(text);
            Assert.AreEqual(text, fs.ToString());
            Assert.AreEqual(9, fs.IndexOf("9"));
            Assert.AreEqual(-1, fs.IndexOf("0", 10));
            Assert.AreEqual("wefamkiwai{c}\t\nf3ds", FormatUtil.ExtractDelimited(fs, "da=").ToString());
            Assert.AreEqual(12, fs.IndexOfAny(new string[] { "cdefgho o", "awjnwexdk da={wefa" }));
            Assert.AreEqual("5", FormatUtil.ExtractValue(fs, "wdwd="));
            Assert.AreEqual("g89", FormatUtil.ExtractStringValue(fs, "wjakl="));
            Assert.AreEqual("\n1 5 3 4 7 ", FormatUtil.ExtractDelimited(fs, "series=").ToString());
            Assert.AreEqual("1 5 3 4 7 ", FormatUtil.ReadValue(fs, "series", "series").ToString());
        }
    }
}

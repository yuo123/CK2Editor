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
            Assert.AreEqual(text, text.ToString());
            Assert.AreEqual(9, text.IndexOf("9"));
            Assert.AreEqual(-1, text.IndexOf("0", 10));
            Assert.AreEqual("wefamkiwai{c}\t\nf3ds", FormatUtil.ExtractDelimited(text, "da=").ToString());
            Assert.AreEqual(12, text.IndexOfAny(new string[] { "cdefgho o", "awjnwexdk da={wefa" }));
            Assert.AreEqual("5", FormatUtil.ExtractValue(text, "wdwd="));
            Assert.AreEqual("g89", FormatUtil.ExtractStringValue(text, "wjakl="));
            Assert.AreEqual("\n1 5 3 4 7 ", FormatUtil.ExtractDelimited(text, "series=").ToString());
            Assert.AreEqual("1 5 3 4 7 ", FormatUtil.ReadValue(text, "series", "series").ToString());
        }
    }
}

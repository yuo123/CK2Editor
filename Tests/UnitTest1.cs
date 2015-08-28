using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CK2Editor;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string path = @"D:\USERS\איל\Desktop\C# Projects\CK2Editor\Abyssinia999_05_08.ck2";
            FileEditor editor = new FileEditor(path);
            foreach (System.Collections.Generic.KeyValuePair<string, string> pair in editor.GetValues())
            {
                Console.WriteLine(pair.Key + ": " + pair.Value);
            }
        }
    }
}

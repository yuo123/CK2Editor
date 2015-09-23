using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CK2Editor;
using CK2Editor.Editors;

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
            for (int i = 0; i < 1; i++)
            {
                foreach (System.Collections.Generic.KeyValuePair<string, string> pair in editor.GetValues())
                {
                    Console.WriteLine(pair.Key + ": " + pair.Value);
                }
            }
        }
    }
}

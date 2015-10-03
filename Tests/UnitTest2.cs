using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Diagnostics;
using System.Text;
using CK2Editor.Utility;
using System.IO;

namespace Tests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void IndexOfTestMethod()
        {
            Stopwatch watch = new Stopwatch();
            const string path = @"D:\USERS\איל\Desktop\C# Projects\CK2Editor\Abyssinia999_05_08.ck2";
            string s = "8347w9xeyj7q389kxr78xk3y98xjkyr82y";//File.ReadAllText(path);
            StringBuilder sb = new StringBuilder(s);
            string needle = "b_tayma=";
            const int count = 20000000;

            watch.Start();
            for (int i = 0; i < count; i++)
            {
                sb.IndexOf(needle);
            }
            watch.Stop();
            Debug.WriteLine("IndexOf StringBuilder: " + watch.ElapsedMilliseconds + " miliseconds (" + watch.ElapsedMilliseconds / 1000 + " seconds)");

            watch.Restart();
            for (int i = 0; i < count; i++)
            {
                s.IndexOf(needle);
            }
            watch.Stop();
            Debug.WriteLine("IndexOf string: " + watch.ElapsedMilliseconds + " miliseconds (" + watch.ElapsedMilliseconds / 1000 + " seconds)");
        }
    }
}

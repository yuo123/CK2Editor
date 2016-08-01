using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CK2Editor;
using CK2Editor.Utility;

namespace Tests
{
    [TestClass]
    public class TraversalTest
    {
        [TestMethod]
        public void TestTraversal()
        {
            SectionEntry root = CreateTestTree();
            Entry cur = root.Entries[0];

            int n = 1;
            while (cur != null)
            {
                Assert.AreEqual(n.ToString(), cur.InternalName);
                cur = cur.Step();
                n++;
            }
        }

        private static SectionEntry CreateTestTree()
        {
            SectionEntry root = new SectionEntry("0");
            root.AddMany(new SectionEntry("1"), new SectionEntry("2"), new SectionEntry("9"), new SectionEntry("12"), new SectionEntry("13"));
            var n2 = (SectionEntry)root.Entries[1];
            n2.AddMany(new SectionEntry("3"), new ValueEntry("6"), new SectionEntry("7"));
            var n3 = (SectionEntry)n2.Entries[0];
            n3.AddMany(new ValueEntry("4"), new ValueEntry("5"));
            var n7 = (SectionEntry)n2.Entries[2];
            n7.AddMany(new ValueEntry("8"));
            var n9 = (SectionEntry)root.Entries[2];
            n9.AddMany(new ValueEntry("10"), new ValueEntry("11"));
            var n13 = (SectionEntry)root.Entries[4];
            n13.AddMany(new SectionEntry("14"), new ValueEntry("21"));
            var n14 = (SectionEntry)n13.Entries[0];
            n14.AddMany(new SectionEntry("15"), new SectionEntry("18"), new SectionEntry("19"));
            var n15 = (SectionEntry)n14.Entries[0];
            n15.AddMany(new ValueEntry("16"), new ValueEntry("17"));
            var n19 = (SectionEntry)n14.Entries[2];
            n19.AddMany(new ValueEntry("20"));
            return root;
        }
    }
}

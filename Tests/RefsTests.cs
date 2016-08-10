using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CK2Editor;
using CK2Editor.Utility;

namespace Tests
{
    [TestClass]
    public class RefsTests
    {
        [TestMethod]
        public void BasicReferenceTest()
        {
            SectionEntry root = GenerateTestTree();

            SectionEntry n1 = (SectionEntry)root.Entries[0];
            Entry n2 = root.Entries[1];
            Assert.AreEqual("First Node: A Value That Is A Name", FormattedReader.ParseValueRefs(n1, n1.FriendlyName)); //test the VNAME of n1 (relative value ref)
            Entry n1_1 = n1.Entries[0];
            Assert.AreEqual(n2, FormattedReader.ParseRef(n1_1, n1_1.Link)); //test the link of n1_1 (absolute ref)
            Entry n1_2 = n1.Entries[1];
            Assert.AreEqual(n1, FormattedReader.ParseRef(n1_2, n1_2.Link)); //test the link of n1_2 (relative ref that goes up the tree)
        }

        [TestMethod]
        public void NestedReferenceTest()
        {
            SectionEntry root = GenerateTestTree();
            Entry n2 = root.Entries[1];
            // test the VNAME of n2, which points to the VNAME of n1 which also contains a reference
            Assert.AreEqual("First Node: A Value That Is A Name", FormattedReader.ParseValueRefs(n2, n2.FriendlyName));
        }

        private static SectionEntry GenerateTestTree()
        {
            SectionEntry root = new EntryGrouper();

            SectionEntry n1 = new SectionEntry("n1", "First Node: [!/n1-1:[VALUE]!]", null);
            root.Entries.Add(n1);
            n1.Parent = root;
            n1.Root = root;
            n1.Entries.Add(new ValueEntry("n1-1", "First Value", "string", "A Value That Is A Name", "!/n2", n1, root));
            n1.Entries.Add(new ValueEntry("n1-2", "Second Value", "misc", "something", "/..", n1, root));
            root.Entries.Add(new ValueEntry("n2", "[!/../n1:[VNAME]!]", "number", "a normal value", null, root, root));

            return root;
        }
    }
}

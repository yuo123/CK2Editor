using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using CK2Editor;

namespace Tests
{
    [TestClass]
    public class FormattedReadingTests
    {
        [TestMethod]
        public void FormattedReaderTest()
        {
            FormattedReader reader = new FormattedReader(TestsReference.FORMAT_PATH);
            SectionEntry root = reader.ReadFile(TestsReference.MIN_TEST_PATH);
            SectionEntry player = new SectionEntry("player", "Player", null);
            player.Root = root;
            player.Parent = root;
            player.Entries.Add(new ValueEntry("id", "Id", "number", "665369", null, player, root));
            player.Entries.Add(new ValueEntry("type", "Type", "number", "66", null, player, root));
            Assert.IsTrue(player.Equals(root.Entries[2]));
            Assert.AreEqual(12, root.Entries.Count);

            Entry start = root.Entries[0];
            string refpath = "..";
            Assert.AreEqual(start.Parent, FormattedReader.ParseRef(start, refpath));
        }
    }
}

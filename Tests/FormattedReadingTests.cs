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
            FormattedReader reader = new FormattedReader("");
            SectionEntry root = reader.ReadFile("");
            SectionEntry player = new SectionEntry("player", "Player", null);
            player.Root = root;
            player.Parent = root;
            player.Values.Add(new ValueEntry("id", "Id", "number", "665369", null, player, root));
            player.Values.Add(new ValueEntry("type", "Type", "number", "66", null, player, root));
            Assert.IsTrue(player.Equals(root.Sections[0]));
            Assert.AreEqual(14, root.Entries.Count);
        }
    }
}

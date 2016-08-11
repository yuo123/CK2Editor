using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

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

        #region Samples
        public const string RECURSIVE_FORMAT =
            @"<File>
            <delayed_event name=""Delayed Event"" multiple=""same"" grouper-name=""(Delayed Events)"" >
                <event name=""Event"" type=""string"" />
                <days name=""Days"" type=""number"" />
                <scope name=""Scope"">
                  <char name=""Char"" type=""number"" />
                  <province name=""Province"" type=""number"" />
                  <seed name=""Seed"" type=""number"" />
                  <random name=""Random"" type=""number"" />
                  <from name=""From"" recursive=""0"">
                    <char name=""Char"" type=""number"" />
                    <province name=""Province"" type=""number"" />
                    <seed name=""Seed"" type=""number"" />
                    <random name=""Random"" type=""number"" />
                  </from>
                </scope>
              </delayed_event>
            </File>";
        public const string RECURSIVE_FILE =
            @"CK2txt
            delayed_event=
        	{
        		event=""WoL.5010""
        		days=46
        		scope=
        		{
        			char=625364
        			seed=500532338
        			random=500532338
        			from=
        			{
        				char=625364
        				seed=120643456
        				random=120643456
        				from=
        				{
        					province=638
        					seed=120643456
        					random=120643456
        					from=
        					{
        						char=625364
        						seed=120643456
        						random=120643456
        						from=
        						{
        							char=625364
        							seed=274551999
        							random=274551999
        							from=
        							{
        								char=625364
        								seed=274551999
        								random=274551999
        							}
        						}
        					}
        				}
        			}
        		}
        	}
        }";
        #endregion

        [TestMethod]
        public void RecursiveSectionTest()
        {
            var reader = new FormattedReader(new MemoryStream(Encoding.UTF8.GetBytes(RECURSIVE_FORMAT)));
            SectionEntry file = reader.ReadFileFromString(RECURSIVE_FILE);
            var second = (SectionEntry)((SectionEntry)((SectionEntry)((SectionEntry)((SectionEntry)file.Entries[0]).Entries[0]).Entries[2]).Entries[3]).Entries[3];

            Assert.AreEqual("Province", second.Entries[0].FriendlyName); //test that the second "from" was properly detected (by checking a value inside it)
            Assert.AreEqual("From", second.Entries[3].FriendlyName); //test that the third "from" was properly detected
        }
    }
}

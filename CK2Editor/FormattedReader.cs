using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

using CK2Editor.Editors;
using CK2Editor.Utility;

namespace CK2Editor
{
    public class FormattedReader
    {
        protected XmlDocument xmlDoc;

        public FormattedReader(string formatFileName)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(formatFileName);
        }
        public FormattedReader(Stream formatStream)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(formatStream);
        }

        public Editor ReadFile(string filename)
        {
            FileSection file = new FileSection(File.ReadAllText(filename));
            return ReadSection(file, xmlDoc.ChildNodes[1]);//nodes 0 and 1 are the root and File tags
        }

        public Editor ReadSection(FileSection file, XmlNode formatNode)
        {
            Editor re = new Editor();
            Dictionary<int, string> entries = null;
            foreach (XmlNode node in formatNode.ChildNodes)
            {
                if (!node.HasChildNodes)
                {//the node is a value, not a section
                    switch (node.Attributes["multiple"] != null ? node.Attributes["multiple"].Value : null)//check if node actually represnts a list of values
                    {
                        default://the node is a single value
                            {
                                ValueEntry ent = new ValueEntry();
                                ent.InternalName = node.LocalName;
                                ent.FriendlyName = node.Attributes["name"].Value;
                                ent.Type = node.Attributes["type"] != null ? node.Attributes["type"].Value : "misc";
                                ent.Value = ReadValue(file, ent.InternalName, ent.Type);
                                ent.Link = node.Attributes["link"] != null ? node.Attributes["link"].Value : null;
                                re.Values.Add(ent);
                            }
                            break;
                        case "same"://the node is multiple values, all with the same name
                            {
                                if (entries == null)
                                    entries = FormatUtil.ListEntriesWithIndexes(file);//all entries are cached in the entries variable
                                foreach (KeyValuePair<int, string> pair in entries)
                                {
                                    if (pair.Value == node.LocalName)
                                    {
                                        ValueEntry ent = new ValueEntry();
                                        ent.InternalName = pair.Value;
                                        ent.FriendlyName = node.Attributes["name"].Value;
                                        ent.Type = node.Attributes["type"] != null ? node.Attributes["type"].Value : "misc";
                                        ent.Value = ReadValue(file, ent.InternalName, ent.Type, pair.Key);
                                        ent.Link = node.Attributes["link"] != null ? node.Attributes["link"].Value : null;
                                        re.Values.Add(ent);
                                    }
                                }
                            }
                            break;
                    }
                }
                else
                {//the node is a section
                    switch (node.Attributes["multiple"] != null ? node.Attributes["multiple"].Value : null)//check if node actually represnts a list of sections
                    {
                        default://the node is a single section
                            {
                                SectionEntry ent = new SectionEntry();
                                ent.InternalName = node.LocalName;
                                ent.FriendlyName = node.Attributes["name"].Value;
                                ent.Section = ReadSection(FormatUtil.ExtractDelimited(file, ent.InternalName + '='), node);//note that this is an intentionally recursive call
                                ent.Link = node.Attributes["link"] != null ? node.Attributes["link"].Value : null;
                                re.Sections.Add(ent);
                                break;
                            }
                        case "same"://the node is a list of section, all named the same as the node
                            {
                                if (entries == null)
                                    entries = FormatUtil.ListEntriesWithIndexes(file);//all entries are cached in the entries variable
                                foreach (KeyValuePair<int, string> pair in entries)
                                {
                                    if (pair.Value == node.LocalName)
                                    {
                                        SectionEntry ent = new SectionEntry();
                                        ent.InternalName = pair.Value;
                                        ent.FriendlyName = node.Attributes["name"].Value;
                                        ent.Section = ReadSection(FormatUtil.ExtractDelimited(file, ent.InternalName + '=', pair.Key), node);//note that this is an intentionally recursive call
                                        ent.Link = node.Attributes["link"] != null ? node.Attributes["link"].Value : null;
                                        re.Sections.Add(ent);
                                    }
                                }

                                break;
                            }
                        case "number"://the node is a list of sections, each identified by a different integer number
                            {
                                if (entries == null)
                                    entries = FormatUtil.ListEntriesWithIndexes(file);//all entries are cached in the entries variable
                                foreach (KeyValuePair<int, string> pair in entries)
                                {
                                    int n;
                                    if (int.TryParse(pair.Value, out n))
                                    {
                                        SectionEntry ent = new SectionEntry();
                                        ent.InternalName = pair.Value;
                                        ent.FriendlyName = node.Attributes["name"].Value;
                                        ent.Section = ReadSection(FormatUtil.ExtractDelimited(file, ent.InternalName + '=', pair.Key), node);//note that this is an intentionally recursive call
                                        ent.Link = node.Attributes["link"] != null ? node.Attributes["link"].Value : null;
                                        re.Sections.Add(ent);
                                    }
                                }
                                break;
                            }
                    }
                }
            }

            return re;
        }

        public string ReadValue(FileSection scope, string name, string type, int startIndex = 0)
        {
            switch (type)
            {
                case "string":
                    return FormatUtil.ExtractStringValue(scope, name + '=', startIndex);
                case "series-compact":
                case "series":
                    return FormatUtil.ExtractDelimited(scope, name + '=', startIndex).ToString().Trim(new char[] { '\n', '\t' });
                default:
                    return FormatUtil.ExtractValue(scope, name + '=', startIndex);
            }
        }
    }
}

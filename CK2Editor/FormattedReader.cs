using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using CK2Editor.Editors;

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
            FileSection file = new FileSection(filename);
            return ReadSection(file);
        }

        public Editor ReadSection(FileSection section)
        {
            Editor re = new Editor();
            List<string> entries = null;
            foreach (XmlNode node in xmlDoc.ChildNodes)
            {
                if (!node.HasChildNodes)
                {//the node is a value, not a section
                    switch (node.Attributes["multiple"].Value)//check if node actually represnts a list of values
                    {
                        default://the node is a single value
                            {
                                string value;
                                switch (node.Attributes["type"].Value)
                                {
                                    case "string":
                                        value = Util.ExtractStringValue(section, node.LocalName);
                                        break;
                                    case "series":
                                        value = Util.ExtractDelimited(section, node.LocalName).ToString();
                                        break;
                                    default:
                                        value = Util.ExtractValue(section, node.LocalName);
                                        break;
                                }
                                re.Values.Add(new ValueEntry(node.LocalName, node.Attributes["name"].Value, node.Attributes["type"].Value, value, node.Attributes["link"].Value));
                            }
                            break;
                        case "number"://the node is multiple values, named as numbers
                            if (entries == null)
                            {
                                int n;
                                Util.ListEntries(section, s => int.TryParse(s, out n));
                            }
                            foreach (string entry in entries)
                            {
                                string value;
                                switch (node.Attributes["type"].Value)
                                {
                                    case "string":
                                        value = Util.ExtractStringValue(section, node.LocalName);
                                        break;
                                    case "series":
                                        value = Util.ExtractDelimited(section, node.LocalName).ToString();
                                        break;
                                    default:
                                        value = Util.ExtractValue(section, node.LocalName);
                                        break;
                                }
                                re.Values.Add(new ValueEntry(entry, node.Attributes["name"].Value, node.Attributes["type"].Value, value, node.Attributes["link"].Value));
                            }
                            break;
                    }
                }
                else
                {//the node is a section
                    switch (node.Attributes["multiple"].Value)//check if node actually represnts a list of sections
                    {
                        default:
                            re.Sections.Add(new SectionEntry(node.LocalName, node.Attributes["name"].Value, ReadSection(Util.ExtractDelimited(section, node.LocalName)), node.Attributes["link"].Value));
                            break;
                        case "number"://the node is multiple sections, named as numbers
                            if (entries == null)
                            {
                                int n;
                                Util.ListEntries(section, s => int.TryParse(s, out n));
                            }
                            foreach (string entry in entries)
                            {
                                re.Sections.Add(new SectionEntry(entry, node.Attributes["name"].Value, ReadSection(Util.ExtractDelimited(section, entry)), node.Attributes["link"].Value));
                            }
                            break;
                        case "same"://the node is multiple values, all named the same as the node

                            break;
                    }
                }
            }

            return re;
        }
    }
}

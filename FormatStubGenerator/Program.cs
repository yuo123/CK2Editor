using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

using CK2Editor;
using CK2Editor.Utility;

namespace FormatStubGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = null;
            if (args.Length > 0 && File.Exists(args[0]))
                path = args[0];
            else
                do
                {
                    if (path != null)
                        Console.WriteLine("Invalid path");
                    Console.Write("Enter the file's path: ");
                    path = Environment.ExpandEnvironmentVariables(Console.ReadLine());
                } while (!File.Exists(path));

            try
            {
                Console.WriteLine("Generating format file...");
                XmlDocument xmlDoc = new XmlDocument();

                string file = File.ReadAllText(path);
                xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));
                XmlNode fileNode = xmlDoc.CreateNode(XmlNodeType.Element, "File", null);
                GenerateSectionNode(xmlDoc, fileNode, file.Substring("CK2txt".Length));
                xmlDoc.AppendChild(fileNode);
                xmlDoc.Save(path + ".xml");
                Console.Write("Done! File saved to:\n{0}", path + ".xml");
            }
            catch (FileFormatException e)
            {
                Console.WriteLine("An error has occured: " + e.Message);
            }
            Console.ReadKey(false);
        }

        public static void GenerateSectionNode(XmlDocument doc, XmlNode parent, string scope)
        {
            bool foundNumber = false;
            bool foundBlank = false;
            bool foundDate = false;
            List<XmlNode> foundNames = new List<XmlNode>();
            foreach (var childPair in FormatUtil.ListEntriesWithIndexes(scope))
            {
                XmlAttribute multiple = null;
                double n;
                if (double.TryParse(childPair.Value, out n))
                {
                    if (foundNumber)
                    {
                        continue;
                    }
                    else
                    {
                        multiple = doc.CreateAttribute("multiple");
                        multiple.Value = "number";
                        foundNumber = true;
                    }
                }
                else
                {
                    if (childPair.Value == "")
                    {
                        if (foundBlank)
                            continue;
                        else
                        {
                            multiple = doc.CreateAttribute("multiple");
                            multiple.Value = "blank";
                            foundBlank = true;
                        }
                    }
                    else if (childPair.Value.Count(c => c == '.') == 2)
                    {
                        if (foundDate)
                            continue;
                        else
                        {
                            multiple = doc.CreateAttribute("multiple");
                            multiple.Value = "date";
                            foundDate = true;
                        }
                    }
                    else
                    {
                        XmlNode found = foundNames.Find(elem => elem.LocalName == childPair.Value);
                        if (found != null)
                        {
                            multiple = doc.CreateAttribute("multiple");
                            multiple.Value = "same";
                            found.Attributes.Append(multiple);
                            continue;
                        }
                    }
                }

                string type = FormattedReader.DetectType(scope, childPair);
                string name;
                XmlAttribute grouperName = null;
                if (multiple != null)
                {
                    grouperName = doc.CreateAttribute("grouper-name");
                    grouperName.Value = "";
                    switch (multiple.Value)
                    {
                        case "number":
                            name = "NUMBER";
                            break;
                        case "blank":
                            name = "BLANK";
                            break;
                        case "date":
                            name = "DATE";
                            break;
                        default:
                            name = childPair.Value;
                            grouperName.Value = Util.UppercaseWords(childPair.Value + "s");
                            break;
                    }
                }
                else
                    name = childPair.Value;

                XmlNode node = doc.CreateElement(name);
                XmlAttribute natt = doc.CreateAttribute("name");
                natt.Value = Util.UppercaseWords(name.Replace('_', ' '));
                node.Attributes.Append(natt);
                if (multiple != null)
                {
                    node.Attributes.Append(multiple);
                    node.Attributes.Append(grouperName);
                }
                parent.AppendChild(node);
                if (type == "section")
                {
                    GenerateSectionNode(doc, node, FormatUtil.ExtractDelimited(scope, childPair.Value, childPair.Key));
                }
                else
                {
                    XmlAttribute tatt = doc.CreateAttribute("type");
                    tatt.Value = type;
                    node.Attributes.Append(tatt);
                }
                foundNames.Add(node);
            }
        }
    }
}

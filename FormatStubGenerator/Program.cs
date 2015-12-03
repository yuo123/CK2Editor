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
                    path = Console.ReadLine();
                } while (!File.Exists(path));

            try
            {
                Console.WriteLine("Generating format file...");
                XmlDocument xmlDoc = new XmlDocument();

                string file = File.ReadAllText(path);
                xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));
                XmlNode fileNode = xmlDoc.CreateNode(XmlNodeType.Element, "File", null);
                GenerateSectionNode(xmlDoc, fileNode, file);
                xmlDoc.AppendChild(fileNode);
                xmlDoc.Save(path + ".xml");
                Console.Write("Done! File saved to:\n{0}", path + ".xml");
            }
            catch (FileFormatException e)
            {
                Console.WriteLine("An error has occured: " + e.Message);
            }
        }

        public static void GenerateSectionNode(XmlDocument doc, XmlNode parent, string scope)
        {
            XmlNode foundNumber = null;
            List<XmlNode> foundNames = new List<XmlNode>();
            foreach (var childPair in FormatUtil.ListEntriesWithIndexes(scope))
            {
                string type = FormattedReader.DetectType(scope, childPair);
                if (type == "section")
                {
                    bool assignFoundNumber = false;
                    int n;
                    if (int.TryParse(childPair.Value, out n))
                    {
                        if (foundNumber != null)
                        {
                            XmlAttribute multiple = doc.CreateAttribute("multiple");
                            multiple.Value = "number";
                            foundNumber.Attributes.Append(multiple);
                            continue;
                        }
                        else
                            assignFoundNumber = true;
                    }
                    else
                    {
                        XmlNode found = foundNames.Find(elem => elem.LocalName == childPair.Value);
                        if (found != null)
                        {
                            XmlAttribute multiple = doc.CreateAttribute("multiple");
                            multiple.Value = "same";
                            found.Attributes.Append(multiple);
                            continue;
                        }
                    }
                    XmlNode node = doc.CreateElement(childPair.Value);
                    parent.AppendChild(node);
                    XmlAttribute natt = doc.CreateAttribute("name");
                    natt.Value = Util.UppercaseWords(childPair.Value.Replace('_', ' '));
                    node.Attributes.Append(natt);
                    if (assignFoundNumber)
                        foundNumber = node;
                    else
                        foundNames.Add(node);
                    GenerateSectionNode(doc, node, FormatUtil.ExtractDelimited(scope, childPair.Value, childPair.Key));
                }
                else
                {
                    XmlNode node = doc.CreateElement(childPair.Value);
                    parent.AppendChild(node);
                    XmlAttribute natt = doc.CreateAttribute("name");
                    natt.Value = Util.UppercaseWords(childPair.Value.Replace('_', ' '));
                    node.Attributes.Append(natt);
                    XmlAttribute tatt = doc.CreateAttribute("type");
                    tatt.Value = type;
                    node.Attributes.Append(tatt);
                }
            }
        }
    }
}

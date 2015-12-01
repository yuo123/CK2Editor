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
            do
            {
                if (path != null)
                    Console.WriteLine("Invalid path");
                Console.Write("Enter the file's path: ");
                path = Console.ReadLine();
            } while (!File.Exists(path));

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                string file = File.ReadAllText(path);
                XmlNode fileNode = xmlDoc.CreateNode(XmlNodeType.Element, "File", null);
                GenerateSectionNode(xmlDoc, fileNode, file);
            }
            catch (FileFormatException e)
            {
                Console.WriteLine("An error has occured: " + e.Message);
            }
        }

        public static void GenerateSectionNode(XmlDocument doc, XmlNode parent, string scope)
        {
            foreach (var childPair in FormatUtil.ListEntriesWithIndexes(scope))
            {
                string type = FormattedReader.DetectType(scope, childPair);
                if (type == "section")
                {
                    XmlNode node = doc.CreateElement(childPair.Value);
                    parent.AppendChild(node);
                    XmlAttribute natt = doc.CreateAttribute("name");
                    node.AppendChild(natt);
                    GenerateSectionNode(doc, node, FormatUtil.ExtractDelimited(scope, childPair.Value, childPair.Key));
                }
                else
                {
                    XmlNode node = doc.CreateElement(childPair.Value);
                    parent.AppendChild(node);
                    XmlAttribute tatt = doc.CreateAttribute("type");
                    tatt.Value = type;
                    node.AppendChild(tatt);
                    XmlAttribute natt = doc.CreateAttribute("name");
                    node.AppendChild(natt);
                }
            }
        }
    }
}

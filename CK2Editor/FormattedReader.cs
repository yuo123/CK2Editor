using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

using CK2Editor.Utility;

namespace CK2Editor
{
    public class FormattedReader
    {
        public const string SAVE_HEADER = "CK2txt";
        public const string SAVE_FOOTER = "}";

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

        public SectionEntry ReadFile(string filename)
        {
            return ReadFileFromString(File.ReadAllText(Environment.ExpandEnvironmentVariables(filename), Encoding.UTF7));//Encoding is important!
        }

        public SectionEntry ReadFileFromString(string file)
        {
            if (file.StartsWith(SAVE_HEADER)) //the header, specified in the constant above, is not part of the hierarchy
                file = file.Substring(SAVE_HEADER.Length);
            file = file.TrimEnd(new char[] { ' ', '\n', '\t', '\r' });
            if (file.EndsWith(SAVE_FOOTER)) //same for footer
                file = file.Substring(0, file.Length - SAVE_FOOTER.Length);
            return ReadSection(file, xmlDoc.GetElementsByTagName("File")[0]);//get only the root "File" tag
        }

        public SectionEntry ReadSection(string file, XmlNode formatNode, SectionEntry root = null)
        {
            SectionEntry re;
            if (root == null)
            {
                re = new EntryGrouper();
                root = re; //if no root was provided, the current editor is the root
            }
            else
                re = new SectionEntry();
            re.Root = root;

            Dictionary<string, EntryGrouper> multiples = null;
            foreach (var pair in FormatUtil.ListEntriesWithIndexes(file))
            {
                SectionEntry parent = re;

                XmlNode childNode = FindNode(formatNode, pair.Value);//look for a format node that can describe this entry
                if (childNode != null)
                {
                    parent = HandleMultiples(childNode, ref multiples, re);

                    if (!childNode.HasChildNodes)
                    {//the node is a value
                        ValueEntry ent = new ValueEntry();
                        ent.InternalName = pair.Value;
                        ent.FriendlyName = childNode.Attributes["name"].Value;
                        ent.Type = childNode.Attributes["type"] != null ? childNode.Attributes["type"].Value : "misc";
                        ent.Value = FormatUtil.ReadValue(file, ent.InternalName, ent.Type, pair.Key);
                        ent.Link = childNode.Attributes["link"] != null ? childNode.Attributes["link"].Value : null;
                        ent.Parent = parent;
                        ent.Root = root;
                        parent.Entries.Add(ent);
                    }
                    else
                    {//the node is a section
                        SectionEntry ent = ReadSection(FormatUtil.ExtractDelimited(file, pair.Value, pair.Key), childNode, re.Root);
                        ent.InternalName = pair.Value;
                        ent.FriendlyName = childNode.Attributes["name"].Value;
                        ent.Link = childNode.Attributes["link"] != null ? childNode.Attributes["link"].Value : null;
                        ent.Parent = parent;
                        parent.Entries.Add(ent);
                    }
                }
                else //if no format node was found, try to supplement information
                {
                    string type = DetectType(file, pair);
                    if (type != "section")
                    {//the node is a value
                        ValueEntry ent = new ValueEntry();
                        ent.InternalName = pair.Value;
                        ent.Type = type;
                        ent.Value = FormatUtil.ReadValue(file, ent.InternalName, ent.Type, pair.Key);
                        ent.Parent = re;
                        ent.Root = root;
                        parent.Entries.Add(ent);
                    }
                    else
                    {//the node is a section
                        SectionEntry ent = new SectionEntry();
                        ent.InternalName = pair.Value;
                        ent = ReadSection(FormatUtil.ExtractDelimited(file, pair.Value, pair.Key), childNode, re.Root);
                        ent.Parent = parent;
                        parent.Entries.Add(ent);
                    }
                }
            }
            return re;
        }

        /// <summary>
        /// Checks if the node has the "multiple" attribute, and if so, initializes a dictionary entry for its grouper if one does not yet exist
        /// </summary>
        /// <param name="node">The xml node that describes the current entry</param>
        /// <param name="multiples">The dictionary holding the multiples groupers for the current section</param>
        /// <param name="parent">The parent of the current entry</param>
        /// <returns><paramref name="parent"/> if the "multiple" attribute was not found, and the grouper otherwise</returns>
        private SectionEntry HandleMultiples(XmlNode node, ref Dictionary<string, EntryGrouper> multiples, SectionEntry parent)
        {
            XmlAttribute multAtt = node.Attributes["multiple"];
            if (multAtt == null)
                return parent;
            else
            {
                EntryGrouper grouper = null;

                if (multiples == null)
                    multiples = new Dictionary<string, EntryGrouper>();
                if (!multiples.TryGetValue(multAtt.Value, out grouper))
                {//grouper initialization
                    grouper = new EntryGrouper();
                    grouper.InternalName = null;
                    grouper.FriendlyName = node.Attributes["grouper-name"].Value;

                    XmlAttribute series = node.ParentNode.Attributes["series"];
                    if (series != null)
                    {
                        grouper.SeriesFormatting = SectionEntry.ParseSeriesType(series.Value);
                        parent.SeriesFormatting = grouper.SeriesFormatting;
                    }

                    multiples[multAtt.Value] = grouper;
                    parent.Entries.Add(grouper);
                    grouper.Parent = parent;
                }
                return grouper;
            }
        }

        public static string DetectType(string file, KeyValuePair<int, string> pair)
        {
            int i;
            bool sawNewline = false;
            for (i = pair.Key + pair.Value.Length; i < file.Length; i++)//go through the file, starting after the name
            {
                if (file[i] == '\n')
                    sawNewline = true;
                else if (file[i] == '{')//if a curly bracket was found, move to the second loop
                    break;
                else if (!sawNewline)
                {
                    if (file[i] == '"')//if there is a '"' before a newline, this must be a string value
                        return "string";
                    else if (!char.IsWhiteSpace(file[i]))//if there is a visible character before a newline, this must be a non-string single value
                    {
#pragma warning disable 0168
                        int n;
#pragma warning restore 0168
                        if (char.IsDigit(file[i]))//if the first char is a digit, this must is either a number or a date
                        {
                            bool dot = false;
                            do
                            {
                                i++;
                                if (file[i] == '.')
                                {
                                    if (dot)//if two dots were found, this is a date (Y.M.D format)
                                        return "date";
                                    else dot = true;
                                }
                            } while (i < file.Length && !char.IsWhiteSpace(file[i]));
                            return "number";//if less than two dots were found before a newline, this is a number
                        }
                        else
                            return "misc";
                    }
                }
            }
            return "section";
            throw new FileFormatException("Invalid format for section or value " + pair.Value + " at position " + pair.Key);
        }

        private XmlNode FindNode(XmlNode parentNode, string name)
        {
            if (parentNode == null)
                return null;
            foreach (XmlNode child in parentNode.ChildNodes)
            {
                Func<string, bool> comparer = GetNameComparer(child);
                if (comparer.Invoke(name))
                    return child;
            }
            return null;
        }

        private Func<string, bool> GetNameComparer(XmlNode node)
        {
            XmlAttribute att = node.Attributes["multiple"];
            switch (att != null ? att.Value : null)
            {
                default:
                case "same":
                    return name => name == node.LocalName;
                case "number":
                    double n;
                    return name => double.TryParse(name, out n);
                case "date":
                    return name => name.Count(c => c == '.') == 2;
                case "blank":
                    return name => name == "";
                case "different":
                    return name => true;
            }
        }

        /// <summary>
        /// Fully parses a string containing value references by replacing the references with the referenced values
        /// </summary>
        /// <param name="start">The entry that contains the reference</param>
        /// <param name="s">The string to be parsed</param>
        /// <returns>The fully parsed string</returns>
        public static string ParseValueRefs(Entry start, string s)
        {
            if (s == null)
                return null;

            bool inref = false;
            int refstart = -1;
            for (int i = 0; i < s.Length; i++)
            {
                if (!inref)
                {
                    if (s[i] == '[' && s[i + 1] == '!') //detect the start of a reference
                    {
                        inref = true;
                        refstart = i;
                        i++; //increment i because the reference start is 2 characters
                    }
                }
                else
                {
                    if (s[i] == '!' && s[i + 1] == ']') //detect the end of a reference
                    {
                        inref = false;
                        int reflength = i - refstart + 2;
                        //compute the value of the reference
                        string parsed = ParseValueRefRecursive(start, s.Substring(refstart + 2, reflength - 4));
                        if (parsed == null)
                            parsed = "";
                        //replace the reference with its value
                        s = s.Remove(refstart, reflength);
                        i -= reflength;
                        s = s.Insert(refstart, parsed);
                        i += parsed.Length;
                        i++; //increment i because the reference end is 2 characters
                    }
                }
            }
            return s;
        }

        /// <summary>
        /// Parses a single reference into the string that it represents, and parses the result string if it contains references too
        /// </summary>
        /// <param name="start">The entry that contains the reference</param>
        /// <param name="sref">The reference, NOT enclosed by the reference markers ("[!" and "!]")</param>
        /// <returns>The recursively referenced string</returns>
        public static string ParseValueRefRecursive(Entry start, string sref)
        {
            Entry end;
            string ret = ParseValueRef(start, sref, out end);
            return ParseValueRefs(end, ret);
        }

        /// <summary>
        /// Parses a single reference into the string that it represents
        /// </summary>
        /// <param name="start">The entry that contains the reference</param>
        /// <param name="sref">The reference, NOT enclosed by the reference markers ("[!" and "!]")</param>
        /// <returns>The referenced string</returns>
        public static string ParseValueRef(Entry start, string sref)
        {
            Entry tmp;
            return ParseValueRef(start, sref, out tmp);
        }

        /// <summary>
        /// Parses a single reference into the string that it represents
        /// </summary>
        /// <param name="start">The entry that contains the reference</param>
        /// <param name="sref">The reference, NOT enclosed by the reference markers ("[!" and "!]")</param>
        /// <param name="end">The last entry in the reference</param>
        /// <returns>The referenced string</returns>
        public static string ParseValueRef(Entry start, string sref, out Entry end)
        {
            string[] comps = sref.Split(new char[] { ':' });
            if (comps.Length != 2)
                throw new ArgumentException("Value reference must contain exactly one colon", sref);
            end = ParseRef(start, comps[0]);
            if (end == null)//if the reference was not found
                return null;
            return ParseSymbol(start, end, comps[1]);
        }

        public static Entry ParseRef(Entry start, string sref)
        {
            return ParseRefPath(start, sref).LastOrDefault();
        }

        public static IEnumerable<Entry> ParseRefPath(Entry start, string sref)
        {
            if (sref == null)
                yield break;
            Entry current = start;
            if (sref.Length > 0 && sref[0] == '!')//reference path starting with another '!' means it starts at the root
            {
                current = current.Root;
                sref = sref.Remove(0, 1);
            }

            string[] comps = sref.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string compi in comps)
            {
                string comp = compi;

                foreach (Match match in Regex.Matches(comp, "\\[.*\\]"))
                {
                    comp = comp.Remove(match.Index, match.Length);
                    comp = comp.Insert(match.Index, ParseSymbol(start, current, match.Value));
                }
                if (compi == "..")
                {
                    current = current.Parent;
                    yield return current;
                }
                else
                {
                    SectionEntry section = current as SectionEntry;
                    if (section == null)
                        yield break;
                    current = section.Entries.FirstOrDefault(ent => ent.InternalName == comp);
                }
                yield return current;
            }
        }

        /// <summary>
        /// Parses a reference symbol into the string it represents for the given entry and start entry. More info in FormatSpec.md
        /// </summary>
        /// <param name="start">The entry that contains the reference</param>
        /// <param name="current">The entry for which the symbol should be evaluated</param>
        /// <returns>The string which the symbol was parsed into</returns>
        /// <exception cref="FileFormatException">Thrown when a value symbol was used on a section entry</exception>
        public static string ParseSymbol(Entry start, Entry current, string symbol)
        {
            switch (symbol)
            {
                case "[THISVALUE]":
                    {
                        var vent = start as ValueEntry;
                        if (vent == null)
                            throw new FileFormatException("Reference in format file could not be parsed: " + symbol + " (for entry " + start.InternalName + "): used [THISVALUE] for a section");
                        return vent.Value;
                    }
                case "[THISNAME]":
                    return start.InternalName;
                case "[THISVNAME]":
                    return start.FriendlyName;
                case "[VALUE]":
                    {
                        var vent = current as ValueEntry;
                        if (vent == null)
                            throw new FileFormatException("Reference in format file could not be parsed: " + symbol + " (for entry " + start.InternalName + "): used [VALUE] for a section");
                        return vent.Value;
                    }
                case "[NAME]":
                    return current.InternalName;
                case "[VNAME]":
                    return current.FriendlyName;
            }
            throw new FileFormatException("Reference in format file could not be parsed:  unknown symbol " + symbol);
        }
    }

    public class FileFormatException : Exception
    {
        public FileFormatException() { }

        public FileFormatException(string message) : base(message) { }

        public FileFormatException(string message, Exception innerException) : base(message, innerException) { }
    }
}

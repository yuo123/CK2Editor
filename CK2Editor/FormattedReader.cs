using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

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
            string file = File.ReadAllText(filename, Encoding.UTF7);//Encoding is important!
            return ReadSection(file, xmlDoc.ChildNodes[1]);//nodes 0 and 1 are the root and File tags
        }

        public Editor ReadSection(string file, XmlNode formatNode, IEditor root = null)
        {
            Editor re = new Editor();
            re.Root = root != null ? root : re;//if no root was provided, the current editor is the root

            foreach (var pair in FormatUtil.ListEntriesWithIndexes(file))
            {
                XmlNode childNode = FindNode(formatNode, pair.Value);//look for a format node that can describe this entry
                if (childNode != null)
                {
                    if (!childNode.HasChildNodes)
                    {//the node is a value
                        ValueEntry ent = new ValueEntry();
                        ent.InternalName = pair.Value;
                        ent.FriendlyName = childNode.Attributes["name"].Value;
                        ent.Type = childNode.Attributes["type"] != null ? childNode.Attributes["type"].Value : "misc";
                        ent.Value = FormatUtil.ReadValue(file, ent.InternalName, ent.Type, pair.Key);
                        ent.Link = childNode.Attributes["link"] != null ? childNode.Attributes["link"].Value : null;
                        ent.Editor = re;
                        re.Values.Add(ent);
                    }
                    else
                    {//the node is a section
                        SectionEntry ent = new SectionEntry();
                        ent.InternalName = pair.Value;
                        ent.FriendlyName = childNode.Attributes["name"].Value;
                        ent.Section = ReadSection(FormatUtil.ExtractDelimited(file, pair.Value, pair.Key), childNode, re.Root);
                        ent.Link = childNode.Attributes["link"] != null ? childNode.Attributes["link"].Value : null;
                        ent.Editor = re;
                        re.Sections.Add(ent);
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
                        ent.Editor = re;
                        re.Values.Add(ent);
                    }
                    else
                    {//the node is a section
                        SectionEntry ent = new SectionEntry();
                        ent.InternalName = pair.Value;
                        ent.Section = ReadSection(FormatUtil.ExtractDelimited(file, pair.Value, pair.Key), childNode, re.Root);
                        ent.Editor = re;
                        re.Sections.Add(ent);
                    }
                }
            }
            return re;
        }

        public static string DetectType(string file, KeyValuePair<int, string> pair)
        {
            int i;
            bool sawNewline = false;
            for (i = pair.Key + pair.Value.Length + 1; i < file.Length; i++)//go through the file, starting after the name
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
                            } while (!char.IsWhiteSpace(file[i]));
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
            switch (node.Attributes["multiple"] != null ? node.Attributes["multiple"].Value : null)
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

        public static string ParseValueRefs(Entry start, string s)
        {
            if (s == null)
                return null;
            if (start.InternalName == "501431")
                System.Diagnostics.Debugger.Break();
            bool inref = false;
            int refstart = -1;
            for (int i = 0; i < s.Length; i++)
            {
                if (!inref)
                {
                    if (s[i] == '[' && s[i + 1] == '!')
                    {
                        inref = true;
                        refstart = i;
                        i++;
                    }
                }
                else
                {
                    if (s[i] == '!' && s[i + 1] == ']')
                    {
                        inref = false;
                        int reflength = i - refstart + 2;
                        string parsed = ParseValueRef(start, s.Substring(refstart + 2, reflength - 4));
                        if (parsed == null)
                            parsed = "";
                        s = s.Remove(refstart, reflength);
                        i -= reflength;
                        s = s.Insert(refstart, parsed);
                        i += parsed.Length;
                        i++;
                    }
                }
            }
            return s;
        }

        public static string ParseValueRef(Entry start, string sref)
        {
            string[] comps = sref.Split(new char[] { ':' });
            Entry ent = ParseRef(start, comps[0]);
            if (ent == null)//if the reference wwas not found
                return null;
            switch (comps[1])
            {
                case "[VALUE]":
                    {
                        ValueEntry vent = ent as ValueEntry;
                        if (vent == null)
                            throw new FileFormatException("Reference in format file could not be parsed: Sections do not have a value! (" + "entry " + ent.InternalName + ")");
                        return vent.Value;
                    }
                default:
                    throw new FileFormatException("Reference in format file could not be parsed:  unknown symbol " + comps[1]);
            }
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
                if (start.Editor != null)
                {
                    current = new SectionEntry();//create a temprary wrapper SectionEntry, for convenience
                    ((SectionEntry)current).Section = start.Editor.Root;
                }
                sref = sref.Remove(0, 1);
            }

            string[] comps = sref.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string compi in comps)
            {
                SectionEntry section = current as SectionEntry;
                if (section == null)
                    yield break;
                string comp = compi;
                foreach (Match match in Regex.Matches(comp, "\\[.*\\]"))
                {
                    comp = comp.Remove(match.Index, match.Length);
                    comp = comp.Insert(match.Index, ParseSymbol(start, current, match.Value));
                }
                current = section.Section.Entries.FirstOrDefault(ent => ent.InternalName == comp);
                yield return current;
            }
        }

        public static string ParseSymbol(Entry start, Entry current, string value)
        {
            switch (value)
            {
                case "[VALUE]":
                    {
                        var vent = current as ValueEntry;
                        if (vent == null)
                            throw new FileFormatException("Reference in format file could not be parsed: " + value + " (for entry " + current.InternalName + ")");
                        return vent.Value;
                    }
                case "[THISVALUE]":
                    {
                        var vent = start as ValueEntry;
                        if (vent == null)
                            throw new FileFormatException("Reference in format file could not be parsed: " + value + " (for entry " + start.InternalName + ")");
                        return vent.Value;
                    }
                case "[NAME]":
                    {
                        var vent = current as ValueEntry;
                        if (vent == null)
                            throw new FileFormatException("Reference in format file could not be parsed: " + value + " (for entry " + current.InternalName + ")");
                        return vent.InternalName;
                    }
                case "[THISNAME]":
                    {
                        var vent = start as ValueEntry;
                        if (vent == null)
                            throw new FileFormatException("Reference in format file could not be parsed: " + value + " (for entry " + start.InternalName + ")");
                        return vent.InternalName;
                    }
                case "[VNAME]":
                    {
                        var vent = current as ValueEntry;
                        if (vent == null)
                            throw new FileFormatException("Reference in format file could not be parsed: " + value + " (for entry " + current.InternalName + ")");
                        return vent.FriendlyName;
                    }
                case "[THISVNAME]":
                    {
                        var vent = start as ValueEntry;
                        if (vent == null)
                            throw new FileFormatException("Reference in format file could not be parsed: " + value + " (for entry " + start.InternalName + ")");
                        return vent.FriendlyName;
                    }

            }
            throw new FileFormatException("Reference in format file could not be parsed:  unknown symbol " + value);
        }
    }

    public class FileFormatException : Exception
    {
        public FileFormatException() { }

        public FileFormatException(string message) : base(message) { }

        public FileFormatException(string message, Exception innerException) : base(message, innerException) { }
    }
}

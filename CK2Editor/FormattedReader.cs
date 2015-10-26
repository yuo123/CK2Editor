using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            FileSection file = new FileSection(File.ReadAllText(filename, Encoding.UTF7));//Encoding is important!
            return ReadSection(file, xmlDoc.ChildNodes[1]);//nodes 0 and 1 are the root and File tags
        }

        /*
                                ValueEntry ent = new ValueEntry();
                                ent.InternalName = node.LocalName;
                                ent.FriendlyName = node.Attributes["name"].Value;
                                ent.Type = node.Attributes["type"] != null ? node.Attributes["type"].Value : "misc";
                                ent.Value = ReadValue(file, ent.InternalName, ent.Type, location, out location);
                                location += ent.Value.Length;
                                ent.Link = node.Attributes["link"] != null ? node.Attributes["link"].Value : null;
                                re.Values.Add(ent);
         */
        public Editor ReadSection(FileSection file, XmlNode formatNode, IEditor root = null)
        {
            Editor re = new Editor();
            re.Root = root != null ? root : re;//if no root was provided, the current editor is the root
            Dictionary<int, string> entries = FormatUtil.ListEntriesWithIndexes(file);

            foreach (var pair in entries)
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
                        ent.Value = ReadValue(file, ent.InternalName, ent.Type, pair.Key);
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

            }

            return re;
        }

        private XmlNode FindNode(XmlNode parentNode, string name)
        {
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
                    int n;
                    return name => int.TryParse(name, out n);
            }
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

        public string ReadValue(FileSection scope, string name, string type, int startIndex, out int foundIndex)
        {
            switch (type)
            {
                case "string":
                    return FormatUtil.ExtractStringValue(scope, name + '=', startIndex, out foundIndex);
                case "series-compact":
                case "series":
                    return FormatUtil.ExtractDelimited(scope, name + '=', startIndex, out foundIndex).ToString().Trim(new char[] { '\n', '\t' });
                default:
                    return FormatUtil.ExtractValue(scope, name + '=', startIndex, out foundIndex);
            }
        }

        public static string ParseValueRefs(Entry start, string s)
        {
            bool inref = false;
            int refstart = -1;
            string ret = s;
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
                        string parsed = ParseValueRef(start, ret.Substring(refstart + 2, reflength - 4));
                        if (parsed == null)
                            return null;
                        ret = ret.Remove(refstart, reflength);
                        ret = ret.Insert(refstart, parsed);
                        i++;
                    }
                }
            }
            return ret;
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
            if (sref == null)
                return null;
            Entry current = start;
            if (sref.Length > 0 && sref[0] == '!')//reference path starting with another '!' means it starts at the root
            {
                current = new SectionEntry();//create a temprary wrapper SectionEntry, for convenience
                ((SectionEntry)current).Section = start.Editor.Root;
                sref = sref.Remove(0, 2);
            }

            string[] comps = sref.Split(new char[] { '/' });
            foreach (string compi in comps)
            {
                SectionEntry section = current as SectionEntry;
                if (section == null)
                    throw new FileFormatException("Reference in format file could not be parsed: tried to get parent of value (entry " + current.InternalName + ")");
                string comp = compi;
                foreach (Match match in Regex.Matches(comp, "\\[.*\\]"))
                {
                    comp = comp.Remove(match.Index, match.Length);
                    comp = comp.Insert(match.Index, ParseSymbol(start, current, match.Value));
                }
                current = section.Section.Entries.FirstOrDefault(ent => ent.InternalName == comp);
            }
            return current;
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

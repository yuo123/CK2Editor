using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CK2Editor.Utility;

namespace CK2Editor
{
    public enum SeriesType { None, Normal, Compact }

    public class SectionEntry : Entry
    {
        public SeriesType SeriesFormatting { get; set; }

        /// <summary>
        /// Gets the child entry of this section with the specified internal name
        /// </summary>
        public Entry this[string name]
        {
            get
            {
                return Entries.Find(ent => ent.InternalName == name);
            }
        }

        public SectionEntry()
        {
            Entries = new List<Entry>();
        }

        public SectionEntry(string internalName, string friendlyName, string link)
            : this()
        {
            InternalName = internalName;
            FriendlyName = friendlyName;
            Link = link;
        }

        /// <summary>
        /// Outputs the text representation of this section, as it would appear in a save file
        /// </summary>
        /// <param name="sb">The string builder to append to</param>
        /// <param name="indent">the base indent of this section inside the parent section</param>
        public override void Save(StringBuilder sb, int indent = 0)
        {
            //the identifier
            SaveIdentifier(sb, indent);
            //line break between the identifier and header
            if (SeriesFormatting != SeriesType.Compact)
                sb.Append('\n');
            //header
            SaveHeader(sb, SeriesFormatting != SeriesType.Compact ? indent : 0);
            //line break between the header and content
            if (SeriesFormatting != SeriesType.Compact)
                sb.Append('\n');
            //content
            if (SeriesFormatting == SeriesType.None)
                SaveContent(sb, indent + 1);
            else try
                {
                    ((EntryGrouper)this.Entries[0]).SaveContent(sb, indent + 1);
                }
                catch (Exception e) when (e is InvalidCastException || e is IndexOutOfRangeException)
                {
                    throw new InvalidOperationException("The 'series' attribute was used in an invalid case", e);
                }
            //footer
            SaveFooter(sb, SeriesFormatting != SeriesType.Compact ? indent : 0);
        }

        /// <summary>
        /// Saves the child entries of this section
        /// </summary>
        /// <param name="indent">The indent level of the children</param>
        protected virtual void SaveContent(StringBuilder sb, int indent)
        {
            foreach (Entry child in Entries)
            {
                if (SeriesFormatting == SeriesType.None)
                {
                    child.Save(sb, indent);
                    sb.Append('\n');
                }
                else
                {
                    child.Save(sb);
                    sb.Append(' ');
                }
            }

            if (SeriesFormatting == SeriesType.Normal)
                sb.IndentedAppend(indent);
        }

        /// <summary>
        /// Saves the string the appears before the contents of this section
        /// </summary>
        /// <param name="indent">The indent level of this section</param>
        protected virtual void SaveHeader(StringBuilder sb, int indent)
        {
            sb.IndentedAppend(indent, "{");
        }

        /// <summary>
        /// Saves the string the appears after the contents of this section
        /// </summary>
        /// <param name="indent">The indent level of this section</param>
        protected virtual void SaveFooter(StringBuilder sb, int indent)
        {
            sb.IndentedAppend(indent, "}");
        }

        /// <summary>
        /// Returns the string representation of this section as if it was the root section of a save file
        /// </summary>
        /// <returns></returns>
        public string SaveAsRoot()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(FormattedReader.SAVE_HEADER);
            sb.Append('\n');
            SaveContent(sb, 1);
            sb.Append(FormattedReader.SAVE_FOOTER);
            return sb.ToString();
        }

        public static SeriesType ParseSeriesType(string s)
        {
            switch (s)
            {
                case "none": return SeriesType.None;
                case "normal": return SeriesType.Normal;
                case "compact": return SeriesType.Compact;
                default: throw new ArgumentException("Unknown SeriesType value", "s");
            }
        }

        /// <summary>
        /// The collection of all the entries which are contained within this section
        /// </summary>
        public List<Entry> Entries { get; private set; }

        public override bool Equals(Entry other)
        {
            SectionEntry others = other as SectionEntry;
            if (others == null || !base.Equals(other))
                return false;
            var pairs = this.Entries.Zip(others.Entries, (a, b) => new { A = a, B = b });
            foreach (var pair in pairs)
            {
                if (!pair.A.Equals(pair.B))
                    return false;
            }
            return true;
        }

        protected override Entry CreateClone()
        {
            SectionEntry ret = new SectionEntry();
            ret.SeriesFormatting = this.SeriesFormatting;
            foreach (Entry child in this.Entries)
            {
                ret.Entries.Add(child.Clone());
            }
            return ret;
        }
    }
}

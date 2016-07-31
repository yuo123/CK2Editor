using System;
using System.Collections.Generic;
using System.Text;

using CK2Editor.Utility;


namespace CK2Editor
{
    public abstract class Entry : IEquatable<Entry>
    {
        /// <summary>
        /// A user friendly name, supplied by the format file
        /// </summary>
        public string FriendlyName { get; set; }
        /// <summary>
        /// The name used by the save file
        /// </summary>
        public string InternalName { get; set; }
        /// <summary>
        /// A link to a related section, supplied by the format file
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// The parent SectionEntry of this entry
        /// </summary>
        public SectionEntry Parent { get; set; }
        /// <summary>
        /// The first parent of this entry that is present in the file (i.e. not a grouper)
        /// </summary>
        public SectionEntry RealParent
        {
            get
            {
                SectionEntry cur = Parent;
                while (cur is EntryGrouper)
                {
                    cur = cur.Parent;
                }
                return cur;
            }
        }

        public SectionEntry Root { get; set; }


        public Entry() { }
        public Entry(Entry other)
        {
            this.FriendlyName = other.FriendlyName;
            this.InternalName = other.InternalName;
            this.Link = other.Link;
            this.Parent = other.Parent;
            this.Root = other.Root;
        }

        /// <summary>
        /// Outputs the text representation of this entry to a StringBuilder, as it would appear in a save file
        /// </summary>
        /// <param name="sb">The string builder to append to</param>
        /// <param name="indent">the base indent of this entry inside the parent section</param>
        public abstract void Save(StringBuilder sb, int indent = 0);

        /// <summary>
        /// Returns the text representation of this entry, as it would appear in a save file
        /// </summary>
        public string Save()
        {
            StringBuilder sb = new StringBuilder();
            Save(sb);
            return sb.ToString();
        }

        public virtual void SaveIdentifier(StringBuilder sb, int indent)
        {
            if (!string.IsNullOrEmpty(InternalName))
                sb.IndentedAppend(indent, InternalName + '=');
        }

        public virtual bool Equals(Entry other)
        {
            return other.InternalName == this.InternalName && other.FriendlyName == this.FriendlyName && other.Link == this.Link;
        }

        public override string ToString()
        {
            return base.ToString() + ":" + InternalName;
        }

        /// <summary>
        /// Creates a fully deep clone of this Entry, except for <see cref="Parent"/> and <see cref="Root"/>
        /// </summary>
        public virtual Entry Clone()
        {
            Entry ret = CreateClone();
            ret.InternalName = this.InternalName;
            ret.FriendlyName = this.FriendlyName;
            ret.Link = this.Link;
            return ret;
        }

        /// <summary>
        /// Creates a fully deep clone of this entry, excluding members defined in the base Entry class
        /// </summary>
        /// <returns></returns>
        protected abstract Entry CreateClone();
    }
}

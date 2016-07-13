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
                while (cur.IsGrouper)
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

        public virtual bool Equals(Entry other)
        {
            return other.InternalName == this.InternalName && other.FriendlyName == this.FriendlyName && other.Link == this.Link;
        }

        public override string ToString()
        {
            return base.ToString() + ":" + InternalName;
        }
    }
}

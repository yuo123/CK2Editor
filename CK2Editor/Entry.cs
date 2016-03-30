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
        /// The editor that edits this entry. Can be null if not part of an editors' tree
        /// </summary>
        //public SectionEntry SectionEntry { get; set; }
        /// <summary>
        /// The parent SectionEntry of this entry
        /// </summary>
        public SectionEntry Parent { get; set; }

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

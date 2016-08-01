using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CK2Editor
{
    public class ValueEntry : Entry
    {
        /// <summary>
        /// The value type, as defined by the spec. null assumes misc
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// The stored value
        /// </summary>
        public string Value { get; set; }

        public ValueEntry()
        {

        }

        public ValueEntry(string internalName) : this()
        {
            this.InternalName = internalName;
        }

        public ValueEntry(string internalName, string friendlyName, string type, string value, string link, SectionEntry parent, SectionEntry root) : this()
        {
            InternalName = internalName;
            FriendlyName = friendlyName;
            Type = type;
            Value = value;
            Link = link;
            Parent = parent;
            Root = root;
        }

        public override bool Equals(Entry other)
        {
            ValueEntry otherv = other as ValueEntry;
            return otherv != null && base.Equals(other) && otherv.Type == this.Type && otherv.Value == this.Value;
        }

        public override void Save(StringBuilder sb, int indent = 0)
        {
            if (RealParent != null && RealParent.InternalName == "data")
                System.Diagnostics.Debugger.Break();
            SaveCompleteValue(sb, indent);
        }

        public void SaveStringValue(StringBuilder sb)
        {
            sb.Append('"' + Value + '"');
        }

        public void SaveMiscValue(StringBuilder sb)
        {
            sb.Append(Value);
        }

        public void SaveCompleteValue(StringBuilder sb, int indent = 0)
        {
            SaveIdentifier(sb, indent);
            switch (Type)
            {
                case "string":
                    SaveStringValue(sb);
                    break;
                default:
                    SaveMiscValue(sb);
                    break;
            }
        }

        protected override Entry CreateClone()
        {
            ValueEntry ret = new ValueEntry();
            ret.Type = this.Type;
            ret.Value = this.Value;
            return ret;
        }
    }
}

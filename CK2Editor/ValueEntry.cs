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
        /// The value type, string, number, series or null (misc.)
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// The stores value
        /// </summary>
        public string Value { get; set; }

        public ValueEntry()
        {

        }

        public ValueEntry(string internalName, string friendlyName, string type, string value, string link)
        {
            InternalName = internalName;
            FriendlyName = friendlyName;
            Type = type;
            Value = value;
            Link = link;
        }
    }
}

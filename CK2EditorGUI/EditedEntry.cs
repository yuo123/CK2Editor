using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CK2Editor;

namespace CK2EditorGUI
{
    /// <summary>
    /// A wrapper for holding an edited entry, which can change while editing
    /// </summary>
    public class EditedEntry
    {
        /// <summary>
        /// The edited entry
        /// </summary>
        public Entry Entry { get; set; }

        public EditedEntry() { }
        /// <summary>
        /// Create a new Edited editing the specified Entry
        /// </summary>
        /// <param name="entry">The entry to edit</param>
        public EditedEntry(Entry entry)
        {
            this.Entry = entry;
        }

        public static implicit operator Entry(EditedEntry ee)
        {
            return ee.Entry;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CK2Editor;

namespace CK2Editor.Editors
{
    /// <summary>
    /// A CK2 save parsing conmponent. Each IParser handles a certian section or a reccuring structure in the save file.
    /// </summary>
    public interface IEditor
    {
        /// <summary>
        /// List of editors for all the sub-sections of the section that this editors edits
        /// </summary>
        IList<SectionEntry> Sections { get; }
        /// <summary>
        /// List of values that this editor can edit
        /// </summary>
        IList<ValueEntry> Values { get; }

        IList<Entry> Entries { get; }
        /// <summary>
        /// The Editor of the entire file this IEditor is part of
        /// </summary>
        IEditor Root { get; set; }
        /// <summary>
        /// Generates the text describing <c>Sections</c> and <c>Values</c> in the CK2 file format
        /// </summary>
        /// <param name="sb">the <c>StringBuilder</c> to append the output to</param>
        /// <param name="indent">The number of tab characters to insert before each line</param>
        void Save(StringBuilder sb, int indent = 0);
        /// <summary>
        /// Generates the text describing <c>Sections</c> and <c>Values</c> in the CK2 file format
        /// </summary>
        /// <returns>The output text</returns>
        string Save();
    }
}

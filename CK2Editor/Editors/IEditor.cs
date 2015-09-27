using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        IList<SectionEntry> Sections { get; set; }
        /// <summary>
        /// List of values that this editor can edit
        /// </summary>
        IList<ValueEntry> Values { get; set; }

        /// <summary>
        /// The raw text this editor is based upon
        /// </summary>
        string RawText { get; }

        /// <summary>
        /// Make the changes to <c>Sections</c> and <c>Values</c> reflect in <c>RawText</c>
        /// </summary>
        void Save();
    }
}

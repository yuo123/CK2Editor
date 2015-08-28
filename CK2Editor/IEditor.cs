using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Editor
{
    /// <summary>
    /// A CK2 save parsing conmponent. Each IParser handles a certian section or a reccuring structure in the save file.
    /// </summary>
    public interface IEditor
    {
        /// <summary>
        /// Gets the editors for all the sub-sections of the section that this editors edits
        /// </summary>
        IDictionary<string, IEditor> GetSections();
        /// <summary>
        /// Gets the values that this editor can edit
        /// </summary>
        IDictionary<string, string> GetValues();
        /// <summary>
        /// Sets a value this parser can edit
        /// </summary>
        /// <param name="name">The name of the value to set</param>
        /// <param name="value">The new value to set</param>
        void setValue(string name, string value);
    }
}

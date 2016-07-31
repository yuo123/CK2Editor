using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CK2Editor;

namespace CK2EditorGUI.EditorGUIs
{
    public interface IEditorGUIProvider
    {
        /// <summary>
        /// Creates a new instance of IEditorGUI of the type of this provider
        /// </summary>
        /// <returns></returns>
        IEditorGUI CreateEditor();
        /// <summary>
        /// Checks whether the specified entry can be edited by this editor type
        /// </summary>
        /// <param name="entry"></param>
        bool CanEdit(Entry entry);
        /// <summary>
        /// Generates a default entry that complies with this editor's structure
        /// </summary>
        Entry GenerateDefault();
        /// <summary>
        /// The user-friendly name of the structure this editor type can edit
        /// </summary>
        string StructureName { get; }
    }
}

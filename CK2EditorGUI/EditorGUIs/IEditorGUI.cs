using CK2Editor;
using System;

namespace CK2EditorGUI.EditorGUIs
{
    interface IEditorGUI
    {
        EditedEntry Edited { get; set; }
        void Save();
    }
}

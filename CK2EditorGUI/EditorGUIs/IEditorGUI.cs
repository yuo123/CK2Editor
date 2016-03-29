using System;
namespace CK2EditorGUI.EditorGUIs
{
    interface IEditorGUI
    {
        CK2Editor.Entry EditedEntry { get; set; }
        void Save();
    }
}

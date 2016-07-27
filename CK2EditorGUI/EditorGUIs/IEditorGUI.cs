using System.Windows.Forms;

namespace CK2EditorGUI.EditorGUIs
{
    public interface IEditorGUI
    {
        /// <summary>
        /// The currently edited entry
        /// </summary>
        EditedEntry Edited { get; set; }
        /// <summary>
        /// Modifies <c>Edited</c> to reflect the changes made through the GUI
        /// </summary>
        void Save();
        /// <summary>
        /// The control that can be put in a form to represent the edited entry
        /// </summary>
        Control Control { get; }
    }
}

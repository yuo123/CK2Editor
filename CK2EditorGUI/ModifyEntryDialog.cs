using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CK2Editor;
using CK2EditorGUI.EditorGUIs;

namespace CK2EditorGUI
{
    public partial class ModifyEntryDialog : Form
    {
        protected EditedEntry edited;
        public Entry Edited
        {
            get { return edited.Entry; }
            set
            {
                edited = new EditedEntry(value);
                foreach (IEditorGUI editor in editors)
                {
                    editor.Edited = edited;
                }
            }
        }
        List<IEditorGUI> editors = new List<IEditorGUI>();

        public ModifyEntryDialog()
        {
            InitializeComponent();
            editors.Add(genericEditor);
            editors.Add(rawEditor);

            genericEditor.StructureChanged += EditedStructureChanged;
        }

        private void EditedStructureChanged(object sender, EventArgs e)
        {
            rawEditor.OnAssignEdited();
        }

        public void Save()
        {
            foreach (IEditorGUI editor in editors)
            {
                editor.Save();
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Save();
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

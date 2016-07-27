﻿using System;
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
using CK2EditorGUI.Utility;

namespace CK2EditorGUI
{
    public partial class ModifyEntryDialog : Form
    {
        List<IEditorGUI> editors = new List<IEditorGUI>();

        protected EditedEntry edited;
        public Entry Edited
        {
            get { return edited.Entry; }
            set
            {
                edited = new EditedEntry(value);
                UpdateEditors();
            }
        }

        private void UpdateEditors()
        {
            List<IEditorGUI> specialEditors = EditorsInfo.FindEditors(Edited);
            foreach (IEditorGUI editor in specialEditors)
            {
                editor.Control.Dock = DockStyle.Top;
                this.Controls.Add(editor.Control);
            }
            editors.AddRange(specialEditors);

            foreach (IEditorGUI editor in editors)
            {
                editor.Edited = edited;
            }
        }

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

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
using CK2EditorGUI.Utility;

namespace CK2EditorGUI
{
    public partial class ModifyEntryDialog : Form
    {
        /// <summary>
        /// Gets whether the user has confirmed the modifications or canceled them. Do not apply any changes if this is false.
        /// </summary>
        public bool Confirmed { get; private set; }

        /// <summary>
        /// The height of the Save/Cancel buttons strip, including margins
        /// </summary>
        private static readonly Size DEFAULT_MINSIZE = new Size(483, 227);

        List<IEditorGUI> m_editors = new List<IEditorGUI>();

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
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) //Don't look for editors while being designed (see http://stackoverflow.com/a/1166547)
                return;

            SuspendLayout();
            ResetEditors();

            List<IEditorGUIProvider> specialEditors = EditorsInfo.FindEditors(Edited);
            foreach (IEditorGUIProvider provider in specialEditors)
            {
                IEditorGUI editor = provider.CreateEditor();
                editor.Control.Dock = DockStyle.Top;
                this.Controls.Add(editor.Control);
                this.MinimumSize = new Size(MinimumSize.Width, MinimumSize.Height + editor.Control.Height);

                m_editors.Add(editor);
            }
            
            foreach (IEditorGUI editor in m_editors)
            {
                editor.Edited = edited;
            }
            ResumeLayout();
        }

        private void ResetEditors()
        {
            while (m_editors.Count > 2) //remove all editors from index 2, and keep the generic editors
            {
                this.Controls.Remove(m_editors[2].Control);
                m_editors.RemoveAt(2);
            }
            this.MinimumSize = DEFAULT_MINSIZE;
            this.MaximumSize = DEFAULT_MINSIZE;
            UpdateBounds();
            Invalidate();
        }

        public ModifyEntryDialog()
        {
            InitializeComponent();
            m_editors.Add(genericEditor);
            m_editors.Add(rawEditor);
            Confirmed = false;
            this.Edited = null;

            genericEditor.StructureChanged += EditedStructureChanged;
        }

        private void EditedStructureChanged(object sender, EventArgs e)
        {
            UpdateEditors();
            rawEditor.OnAssignEdited();
        }

        public void Save()
        {
            foreach (IEditorGUI editor in m_editors)
            {
                editor.Save();
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Save();
            Confirmed = true;
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

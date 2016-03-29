using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using CK2Editor;

namespace CK2EditorGUI.EditorGUIs
{
    [DesignerCategory("Forms")]
    public partial class RawEditorGUI : EditorGUIBase, IEditorGUI
    {
        public RawEditorGUI()
        {
            InitializeComponent();
        }

        private ValueEntry edited;

        public Entry EditedEntry
        {
            get { return edited; }
            set
            {
                edited = value as ValueEntry;
                this.Enabled = edited != null;
                if (this.Enabled)
                {
                    valueBox.Text = edited.Value;
                    typeBox.Text = edited.Type;
                }
            }
        }

        public void Save()
        {
            if (edited != null)
            {
                edited.Value = valueBox.Text;
                edited.Type = typeBox.Text;
            }
        }
    }
}

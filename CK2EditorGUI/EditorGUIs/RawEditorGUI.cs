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
    public partial class RawEditorGUI : ValueEditorGUIBase, IEditorGUI
    {
        public RawEditorGUI()
        {
            InitializeComponent();
        }

        protected override void OnAssignEdited()
        {
            if (this.Value != null)
            {
                valueBox.Text = Value.Value;
                typeBox.Text = Value.Type;
            }
        }

        public void Save()
        {
            if (Value != null)
            {
                Value.Value = valueBox.Text;
                Value.Type = typeBox.Text;
            }
        }
    }
}

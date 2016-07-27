using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using CK2Editor;

namespace CK2EditorGUI.EditorGUIs
{
    [DesignerCategory("UserControl")]
    public partial class RawEditorGUI : ValueEditorGUIBase, IEditorGUI
    {
        public RawEditorGUI()
        {
            InitializeComponent();
        }

        public static bool CanEdit(Entry entry)
        {
            return true;
        }

        public override void OnAssignEdited()
        {
            base.OnAssignEdited();
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

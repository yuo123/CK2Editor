using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CK2EditorGUI.EditorGUIs
{
    [DesignerCategory("Form")]
    public class GenericEditorGUI : EditorGUIBase
    {
        private System.Windows.Forms.Label label1;
        private ComboBox typeBox;
        private System.Windows.Forms.ComboBox comboBox1;
    
        public GenericEditorGUI()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.Label label2;
            this.typeBox = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 6);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(38, 13);
            label2.TabIndex = 0;
            label2.Text = "Name:";
            // 
            // typeBox
            // 
            this.typeBox.FormattingEnabled = true;
            this.typeBox.Location = new System.Drawing.Point(47, 6);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(73, 21);
            this.typeBox.TabIndex = 1;
            // 
            // GenericEditorGUI
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.typeBox);
            this.Controls.Add(label2);
            this.Name = "GenericEditorGUI";
            this.Size = new System.Drawing.Size(256, 125);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

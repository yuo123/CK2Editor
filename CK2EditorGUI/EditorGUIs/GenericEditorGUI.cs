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
    public class GenericEditorGUI : EditorGUIBase
    {
        private ComboBox nameBox;
        private ComboBox structureBox;
    
        public GenericEditorGUI()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.nameBox = new System.Windows.Forms.ComboBox();
            this.structureBox = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 9);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(38, 13);
            label2.TabIndex = 0;
            label2.Text = "Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(3, 36);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(53, 13);
            label3.TabIndex = 3;
            label3.Text = "Structure:";
            // 
            // nameBox
            // 
            this.nameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameBox.FormattingEnabled = true;
            this.nameBox.Location = new System.Drawing.Point(47, 6);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(163, 21);
            this.nameBox.TabIndex = 1;
            // 
            // structureBox
            // 
            this.structureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.structureBox.Items.AddRange(new object[] {
            "raw"});
            this.structureBox.Location = new System.Drawing.Point(62, 33);
            this.structureBox.Name = "structureBox";
            this.structureBox.Size = new System.Drawing.Size(148, 21);
            this.structureBox.TabIndex = 2;
            // 
            // GenericEditorGUI
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(label3);
            this.Controls.Add(this.structureBox);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(label2);
            this.Name = "GenericEditorGUI";
            this.Size = new System.Drawing.Size(213, 61);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

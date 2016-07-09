﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using CK2Editor;

namespace CK2EditorGUI.EditorGUIs
{
    public class GenericEditorGUI : EditorGUIBase, IEditorGUI
    {
        private ComboBox nameBox;
        private ComboBox structureBox;

        public string SelectedStructure
        {
            get
            {
                return (string)structureBox.SelectedItem;
            }
        }

        private EditedEntry edited;

        public EditedEntry Edited
        {
            get { return edited; }
            set
            {
                edited = value;
                if (value.Entry != null)
                {
                    nameBox.Text = value.Entry.InternalName;
                }
            }
        }

        public GenericEditorGUI()
        {
            InitializeComponent();
            structureBox.SelectedValueChanged += structureChanged;
        }

        void structureChanged(object sender, EventArgs e)
        {
            if ((string)structureBox.SelectedItem == "raw value")
                edited.Entry = new ValueEntry();
            else if ((string)structureBox.SelectedItem == "raw section")
                edited.Entry = new SectionEntry();

            this.StructureChanged?.Invoke(this, new EventArgs());
        }

        public void Save()
        {
            Edited.Entry.InternalName = nameBox.Text;
        }

        public event EventHandler StructureChanged;

        private void InitializeComponent()
        {
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.nameBox = new System.Windows.Forms.ComboBox();
            this.structureBox = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.wrapper.SuspendLayout();
            this.SuspendLayout();
            // 
            // wrapper
            // 
            this.wrapper.Controls.Add(label3);
            this.wrapper.Controls.Add(this.structureBox);
            this.wrapper.Controls.Add(this.nameBox);
            this.wrapper.Controls.Add(label2);
            this.wrapper.Size = new System.Drawing.Size(281, 78);
            this.wrapper.Text = "text";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 16);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(38, 13);
            label2.TabIndex = 0;
            label2.Text = "Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 43);
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
            this.nameBox.Location = new System.Drawing.Point(50, 13);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(225, 21);
            this.nameBox.TabIndex = 1;
            // 
            // structureBox
            // 
            this.structureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.structureBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.structureBox.Items.AddRange(new object[] {
            "raw value",
            "raw section"});
            this.structureBox.Location = new System.Drawing.Point(65, 40);
            this.structureBox.Name = "structureBox";
            this.structureBox.Size = new System.Drawing.Size(210, 21);
            this.structureBox.TabIndex = 2;
            // 
            // GenericEditorGUI
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Name = "GenericEditorGUI";
            this.Size = new System.Drawing.Size(287, 84);
            this.Text = "text";
            this.wrapper.ResumeLayout(false);
            this.wrapper.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}

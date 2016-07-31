namespace CK2EditorGUI
{
    partial class ModifyEntryDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.rawEditor = new CK2EditorGUI.EditorGUIs.RawEditorGUI();
            this.genericEditor = new CK2EditorGUI.EditorGUIs.GenericEditorGUI();
            this.SuspendLayout();
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.Location = new System.Drawing.Point(375, 151);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(80, 25);
            this.saveBtn.TabIndex = 4;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelBtn.Location = new System.Drawing.Point(12, 151);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(80, 25);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // rawEditor
            // 
            this.rawEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.rawEditor.Location = new System.Drawing.Point(0, 73);
            this.rawEditor.Name = "rawEditor";
            this.rawEditor.Size = new System.Drawing.Size(467, 71);
            this.rawEditor.TabIndex = 3;
            this.rawEditor.TabStop = false;
            this.rawEditor.Text = "Raw Data";
            // 
            // genericEditor
            // 
            this.genericEditor.BackColor = System.Drawing.SystemColors.Control;
            this.genericEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.genericEditor.Location = new System.Drawing.Point(0, 0);
            this.genericEditor.Name = "genericEditor";
            this.genericEditor.Size = new System.Drawing.Size(467, 73);
            this.genericEditor.TabIndex = 0;
            this.genericEditor.TabStop = false;
            this.genericEditor.Text = "General Data";
            // 
            // ModifyEntryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(467, 188);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.rawEditor);
            this.Controls.Add(this.genericEditor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(483, 227);
            this.Name = "ModifyEntryDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modify/Add Entry";
            this.ResumeLayout(false);

        }

        #endregion

        private EditorGUIs.GenericEditorGUI genericEditor;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
        private EditorGUIs.RawEditorGUI rawEditor;
    }
}
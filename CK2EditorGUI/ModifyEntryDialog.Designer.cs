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
            this.rawEditorGUI1 = new CK2EditorGUI.EditorGUIs.RawEditorGUI();
            this.genericEditor = new CK2EditorGUI.EditorGUIs.GenericEditorGUI();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rawEditorGUI1
            // 
            this.rawEditorGUI1.Dock = System.Windows.Forms.DockStyle.Top;
            this.rawEditorGUI1.Location = new System.Drawing.Point(0, 73);
            this.rawEditorGUI1.Name = "rawEditorGUI1";
            this.rawEditorGUI1.Size = new System.Drawing.Size(467, 71);
            this.rawEditorGUI1.TabIndex = 3;
            this.rawEditorGUI1.TabStop = false;
            this.rawEditorGUI1.Text = "Raw Data";
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
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.Location = new System.Drawing.Point(375, 151);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(80, 25);
            this.saveBtn.TabIndex = 4;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
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
            this.Controls.Add(this.rawEditorGUI1);
            this.Controls.Add(this.genericEditor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ModifyEntryDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modify/Add Entry";
            this.ResumeLayout(false);

        }

        #endregion

        private EditorGUIs.GenericEditorGUI genericEditor;
        private EditorGUIs.RawEditorGUI rawEditorGUI1;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;


    }
}
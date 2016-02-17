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
            CK2EditorGUI.HorizontalDivider horizontalDivider1;
            this.genericEditorGUI1 = new CK2EditorGUI.EditorGUIs.GenericEditorGUI();
            horizontalDivider1 = new CK2EditorGUI.HorizontalDivider();
            this.SuspendLayout();
            // 
            // genericEditorGUI1
            // 
            this.genericEditorGUI1.BackColor = System.Drawing.SystemColors.Control;
            this.genericEditorGUI1.Dock = System.Windows.Forms.DockStyle.Top;
            this.genericEditorGUI1.Location = new System.Drawing.Point(0, 0);
            this.genericEditorGUI1.Name = "genericEditorGUI1";
            this.genericEditorGUI1.Size = new System.Drawing.Size(349, 60);
            this.genericEditorGUI1.TabIndex = 0;
            // 
            // horizontalDivider1
            // 
            horizontalDivider1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            horizontalDivider1.Dock = System.Windows.Forms.DockStyle.Top;
            horizontalDivider1.Location = new System.Drawing.Point(0, 60);
            horizontalDivider1.Name = "horizontalDivider1";
            horizontalDivider1.Size = new System.Drawing.Size(349, 2);
            horizontalDivider1.TabIndex = 1;
            // 
            // ModifyEntryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(349, 228);
            this.Controls.Add(horizontalDivider1);
            this.Controls.Add(this.genericEditorGUI1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ModifyEntryDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modify/Add Entry";
            this.ResumeLayout(false);

        }

        #endregion

        private EditorGUIs.GenericEditorGUI genericEditorGUI1;


    }
}
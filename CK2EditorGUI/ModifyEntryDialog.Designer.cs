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
            CK2EditorGUI.HorizontalDivider horizontalDivider2;
            this.rawEditorGUI1 = new CK2EditorGUI.EditorGUIs.RawEditorGUI();
            this.genericEditorGUI1 = new CK2EditorGUI.EditorGUIs.GenericEditorGUI();
            horizontalDivider1 = new CK2EditorGUI.HorizontalDivider();
            horizontalDivider2 = new CK2EditorGUI.HorizontalDivider();
            this.SuspendLayout();
            // 
            // rawEditorGUI1
            // 
            this.rawEditorGUI1.Dock = System.Windows.Forms.DockStyle.Top;
            this.rawEditorGUI1.Location = new System.Drawing.Point(0, 64);
            this.rawEditorGUI1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.rawEditorGUI1.Name = "rawEditorGUI1";
            this.rawEditorGUI1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.rawEditorGUI1.Size = new System.Drawing.Size(349, 74);
            this.rawEditorGUI1.TabIndex = 3;
            this.rawEditorGUI1.TabStop = false;
            this.rawEditorGUI1.Text = "rawEditorGUI1";
            // 
            // horizontalDivider1
            // 
            horizontalDivider1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            horizontalDivider1.Dock = System.Windows.Forms.DockStyle.Top;
            horizontalDivider1.Location = new System.Drawing.Point(0, 62);
            horizontalDivider1.Name = "horizontalDivider1";
            horizontalDivider1.Size = new System.Drawing.Size(349, 2);
            horizontalDivider1.TabIndex = 1;
            // 
            // horizontalDivider2
            // 
            horizontalDivider2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            horizontalDivider2.Dock = System.Windows.Forms.DockStyle.Top;
            horizontalDivider2.Location = new System.Drawing.Point(0, 60);
            horizontalDivider2.Name = "horizontalDivider2";
            horizontalDivider2.Size = new System.Drawing.Size(349, 2);
            horizontalDivider2.TabIndex = 2;
            // 
            // genericEditorGUI1
            // 
            this.genericEditorGUI1.BackColor = System.Drawing.SystemColors.Control;
            this.genericEditorGUI1.Dock = System.Windows.Forms.DockStyle.Top;
            this.genericEditorGUI1.Location = new System.Drawing.Point(0, 0);
            this.genericEditorGUI1.Name = "genericEditorGUI1";
            this.genericEditorGUI1.Size = new System.Drawing.Size(349, 60);
            this.genericEditorGUI1.TabIndex = 0;
            this.genericEditorGUI1.TabStop = false;
            // 
            // ModifyEntryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(349, 228);
            this.Controls.Add(this.rawEditorGUI1);
            this.Controls.Add(horizontalDivider1);
            this.Controls.Add(horizontalDivider2);
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
        private EditorGUIs.RawEditorGUI rawEditorGUI1;


    }
}
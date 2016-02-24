namespace CK2EditorGUI.EditorGUIs
{
    public partial class RawEditorGUI : EditorGUIBase
    {
        private System.Windows.Forms.ComboBox typeBox;
        private System.Windows.Forms.TextBox valueBox;

        private void InitializeComponent()
        {
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label1;
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.valueBox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(3, 55);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(59, 13);
            label3.TabIndex = 4;
            label3.Text = "Raw Type:";
            // 
            // typeBox
            // 
            this.typeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.typeBox.Items.AddRange(new object[] {
            "section",
            "string",
            "number",
            "date",
            "misc"});
            this.typeBox.Location = new System.Drawing.Point(68, 52);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(165, 21);
            this.typeBox.TabIndex = 3;
            // 
            // valueBox
            // 
            this.valueBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueBox.Location = new System.Drawing.Point(46, 26);
            this.valueBox.MinimumSize = new System.Drawing.Size(42, 20);
            this.valueBox.Name = "valueBox";
            this.valueBox.Size = new System.Drawing.Size(187, 20);
            this.valueBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 29);
            label1.MinimumSize = new System.Drawing.Size(37, 13);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(37, 13);
            label1.TabIndex = 0;
            label1.Text = "Value:";
            // 
            // RawEditorGUI
            // 
            this.Controls.Add(label3);
            this.Controls.Add(this.typeBox);
            this.Controls.Add(this.valueBox);
            this.Controls.Add(label1);
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.Name = "RawEditorGUI";
            this.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.Size = new System.Drawing.Size(236, 79);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

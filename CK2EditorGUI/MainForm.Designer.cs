namespace CK2EditorGUI
{
    partial class MainForm
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
            System.Windows.Forms.Label label3;
            this.saveSelector = new System.Windows.Forms.ComboBox();
            this.loadSaveButton = new System.Windows.Forms.Button();
            this.formatSelector = new System.Windows.Forms.ComboBox();
            this.refreshFormatsButton = new System.Windows.Forms.Button();
            this.saveFileChooser = new System.Windows.Forms.OpenFileDialog();
            this.saveButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            label2 = new System.Windows.Forms.Label();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            label3 = new System.Windows.Forms.Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(94, 27);
            label1.TabIndex = 4;
            label1.Text = "Currently Editing:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(this.saveSelector, 1, 0);
            tableLayoutPanel1.Controls.Add(this.loadSaveButton, 2, 0);
            tableLayoutPanel1.Controls.Add(this.saveButton, 3, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 27);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(1254, 27);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // saveSelector
            // 
            this.saveSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.saveSelector.FormattingEnabled = true;
            this.saveSelector.Items.AddRange(new object[] {
            "Choose a file or click load"});
            this.saveSelector.Location = new System.Drawing.Point(103, 3);
            this.saveSelector.Name = "saveSelector";
            this.saveSelector.Size = new System.Drawing.Size(1048, 21);
            this.saveSelector.TabIndex = 2;
            this.saveSelector.SelectedIndexChanged += new System.EventHandler(this.saveSelector_SelectedIndexChanged);
            // 
            // loadSaveButton
            // 
            this.loadSaveButton.Location = new System.Drawing.Point(1157, 3);
            this.loadSaveButton.Name = "loadSaveButton";
            this.loadSaveButton.Size = new System.Drawing.Size(46, 21);
            this.loadSaveButton.TabIndex = 5;
            this.loadSaveButton.Text = "Load...";
            this.loadSaveButton.UseVisualStyleBackColor = true;
            this.loadSaveButton.Click += new System.EventHandler(this.loadSaveButton_Click);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = System.Windows.Forms.DockStyle.Left;
            label2.Location = new System.Drawing.Point(41, 0);
            label2.Name = "label2";
            label2.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            label2.Size = new System.Drawing.Size(93, 13);
            label2.TabIndex = 0;
            label2.Text = "Current Section:";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            tableLayoutPanel2.Controls.Add(label3, 0, 0);
            tableLayoutPanel2.Controls.Add(this.formatSelector, 1, 0);
            tableLayoutPanel2.Controls.Add(this.refreshFormatsButton, 2, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new System.Drawing.Size(1254, 27);
            tableLayoutPanel2.TabIndex = 6;
            // 
            // label3
            // 
            label3.Dock = System.Windows.Forms.DockStyle.Fill;
            label3.Location = new System.Drawing.Point(3, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(94, 27);
            label3.TabIndex = 4;
            label3.Text = "Current Format:";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // formatSelector
            // 
            this.formatSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formatSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formatSelector.FormattingEnabled = true;
            this.formatSelector.Items.AddRange(new object[] {
            "Choose a format file"});
            this.formatSelector.Location = new System.Drawing.Point(103, 3);
            this.formatSelector.Name = "formatSelector";
            this.formatSelector.Size = new System.Drawing.Size(1068, 21);
            this.formatSelector.TabIndex = 2;
            // 
            // refreshFormatsButton
            // 
            this.refreshFormatsButton.Location = new System.Drawing.Point(1177, 3);
            this.refreshFormatsButton.Name = "refreshFormatsButton";
            this.refreshFormatsButton.Size = new System.Drawing.Size(65, 21);
            this.refreshFormatsButton.TabIndex = 5;
            this.refreshFormatsButton.Text = "Refresh";
            this.refreshFormatsButton.UseVisualStyleBackColor = true;
            this.refreshFormatsButton.Click += new System.EventHandler(this.refreshFormatsButton_Click);
            // 
            // saveFileChooser
            // 
            this.saveFileChooser.Multiselect = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(1209, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(42, 21);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 345);
            this.Controls.Add(tableLayoutPanel1);
            this.Controls.Add(tableLayoutPanel2);
            this.Name = "MainForm";
            this.Text = "Crusader Kings 2 Save SectionEntry";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox saveSelector;
        private System.Windows.Forms.OpenFileDialog saveFileChooser;
        private System.Windows.Forms.Button loadSaveButton;
        private System.Windows.Forms.ComboBox formatSelector;
        private System.Windows.Forms.Button refreshFormatsButton;
        private System.Windows.Forms.Button saveButton;
    }
}


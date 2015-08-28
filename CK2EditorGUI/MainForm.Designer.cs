namespace CK2Editor
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
            System.Windows.Forms.TabControl tabControl1;
            System.Windows.Forms.TabPage tabPage1;
            System.Windows.Forms.TabPage tabPage2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
            System.Windows.Forms.TabPage tabPage3;
            System.Windows.Forms.TabPage tabPage4;
            System.Windows.Forms.TabPage tabPage5;
            System.Windows.Forms.TabPage tabPage6;
            System.Windows.Forms.TabPage tabPage7;
            System.Windows.Forms.TabPage tabPage8;
            System.Windows.Forms.TabPage tabPage9;
            System.Windows.Forms.TabPage tabPage10;
            this.saveSelector = new System.Windows.Forms.ComboBox();
            this.loadSaveButton = new System.Windows.Forms.Button();
            this.fileChooser = new System.Windows.Forms.OpenFileDialog();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            tabPage2 = new System.Windows.Forms.TabPage();
            label1 = new System.Windows.Forms.Label();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tabPage3 = new System.Windows.Forms.TabPage();
            tabPage4 = new System.Windows.Forms.TabPage();
            tabPage5 = new System.Windows.Forms.TabPage();
            tabPage6 = new System.Windows.Forms.TabPage();
            tabPage7 = new System.Windows.Forms.TabPage();
            tabPage8 = new System.Windows.Forms.TabPage();
            tabPage9 = new System.Windows.Forms.TabPage();
            tabPage10 = new System.Windows.Forms.TabPage();
            tabControl1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Controls.Add(tabPage7);
            tabControl1.Controls.Add(tabPage8);
            tabControl1.Controls.Add(tabPage9);
            tabControl1.Controls.Add(tabPage10);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 27);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(1254, 318);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = System.Drawing.SystemColors.Control;
            tabPage1.Location = new System.Drawing.Point(4, 22);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(1246, 292);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = System.Drawing.SystemColors.Control;
            tabPage2.Location = new System.Drawing.Point(4, 22);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(1246, 292);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Dynasties";
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
            this.saveSelector.Size = new System.Drawing.Size(1068, 21);
            this.saveSelector.TabIndex = 2;
            // 
            // loadSaveButton
            // 
            this.loadSaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadSaveButton.Location = new System.Drawing.Point(1177, 3);
            this.loadSaveButton.Name = "loadSaveButton";
            this.loadSaveButton.Size = new System.Drawing.Size(74, 21);
            this.loadSaveButton.TabIndex = 3;
            this.loadSaveButton.Text = "Load...";
            this.loadSaveButton.UseVisualStyleBackColor = true;
            this.loadSaveButton.Click += new System.EventHandler(this.loadSaveButton_Click);
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
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(this.loadSaveButton, 2, 0);
            tableLayoutPanel1.Controls.Add(this.saveSelector, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(1254, 27);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // fileChooser
            // 
            this.fileChooser.Multiselect = true;
            // 
            // tabPage3
            // 
            tabPage3.BackColor = System.Drawing.SystemColors.Control;
            tabPage3.Location = new System.Drawing.Point(4, 22);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new System.Windows.Forms.Padding(3);
            tabPage3.Size = new System.Drawing.Size(1246, 292);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Characters";
            // 
            // tabPage4
            // 
            tabPage4.BackColor = System.Drawing.SystemColors.Control;
            tabPage4.Location = new System.Drawing.Point(4, 22);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new System.Windows.Forms.Padding(3);
            tabPage4.Size = new System.Drawing.Size(1246, 292);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Religions";
            // 
            // tabPage5
            // 
            tabPage5.BackColor = System.Drawing.SystemColors.Control;
            tabPage5.Location = new System.Drawing.Point(4, 22);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new System.Windows.Forms.Padding(3);
            tabPage5.Size = new System.Drawing.Size(1246, 292);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Provinces";
            // 
            // tabPage6
            // 
            tabPage6.BackColor = System.Drawing.SystemColors.Control;
            tabPage6.Location = new System.Drawing.Point(4, 22);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new System.Windows.Forms.Padding(3);
            tabPage6.Size = new System.Drawing.Size(1246, 292);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "Titles";
            // 
            // tabPage7
            // 
            tabPage7.BackColor = System.Drawing.SystemColors.Control;
            tabPage7.Location = new System.Drawing.Point(4, 22);
            tabPage7.Name = "tabPage7";
            tabPage7.Padding = new System.Windows.Forms.Padding(3);
            tabPage7.Size = new System.Drawing.Size(1246, 292);
            tabPage7.TabIndex = 6;
            tabPage7.Text = "Battles & Sieges";
            // 
            // tabPage8
            // 
            tabPage8.BackColor = System.Drawing.SystemColors.Control;
            tabPage8.Location = new System.Drawing.Point(4, 22);
            tabPage8.Name = "tabPage8";
            tabPage8.Padding = new System.Windows.Forms.Padding(3);
            tabPage8.Size = new System.Drawing.Size(1246, 292);
            tabPage8.TabIndex = 7;
            tabPage8.Text = "Wars";
            // 
            // tabPage9
            // 
            tabPage9.BackColor = System.Drawing.SystemColors.Control;
            tabPage9.Location = new System.Drawing.Point(4, 22);
            tabPage9.Name = "tabPage9";
            tabPage9.Padding = new System.Windows.Forms.Padding(3);
            tabPage9.Size = new System.Drawing.Size(1246, 292);
            tabPage9.TabIndex = 8;
            tabPage9.Text = "Active Diseases";
            // 
            // tabPage10
            // 
            tabPage10.BackColor = System.Drawing.SystemColors.Control;
            tabPage10.Location = new System.Drawing.Point(4, 22);
            tabPage10.Name = "tabPage10";
            tabPage10.Padding = new System.Windows.Forms.Padding(3);
            tabPage10.Size = new System.Drawing.Size(1246, 292);
            tabPage10.TabIndex = 9;
            tabPage10.Text = "Characters History";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 345);
            this.Controls.Add(tabControl1);
            this.Controls.Add(tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Crusader Kings 2 Save Editor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            tabControl1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox saveSelector;
        private System.Windows.Forms.Button loadSaveButton;
        private System.Windows.Forms.OpenFileDialog fileChooser;
    }
}


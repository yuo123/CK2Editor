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
            this.saveSelector = new System.Windows.Forms.ComboBox();
            this.loadSaveButton = new System.Windows.Forms.Button();
            this.fileChooser = new System.Windows.Forms.OpenFileDialog();
            this.editorList = new Aga.Controls.Tree.TreeViewAdv();
            this.nameColumn = new Aga.Controls.Tree.TreeColumn();
            this.valueColumn = new Aga.Controls.Tree.TreeColumn();
            label1 = new System.Windows.Forms.Label();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            label2 = new System.Windows.Forms.Label();
            tableLayoutPanel1.SuspendLayout();
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
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(this.saveSelector, 1, 0);
            tableLayoutPanel1.Controls.Add(this.loadSaveButton, 2, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
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
            this.saveSelector.Size = new System.Drawing.Size(1068, 21);
            this.saveSelector.TabIndex = 2;
            // 
            // loadSaveButton
            // 
            this.loadSaveButton.Location = new System.Drawing.Point(1177, 3);
            this.loadSaveButton.Name = "loadSaveButton";
            this.loadSaveButton.Size = new System.Drawing.Size(65, 21);
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
            // fileChooser
            // 
            this.fileChooser.Multiselect = true;
            // 
            // editorList
            // 
            this.editorList.BackColor = System.Drawing.SystemColors.Window;
            this.editorList.Columns.Add(this.nameColumn);
            this.editorList.Columns.Add(this.valueColumn);
            this.editorList.DefaultToolTipProvider = null;
            this.editorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorList.DragDropMarkColor = System.Drawing.Color.Black;
            this.editorList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editorList.GridLineStyle = ((Aga.Controls.Tree.GridLineStyle)((Aga.Controls.Tree.GridLineStyle.Horizontal | Aga.Controls.Tree.GridLineStyle.Vertical)));
            this.editorList.LineColor = System.Drawing.SystemColors.ControlDarkDark;
            this.editorList.LoadOnDemand = true;
            this.editorList.Location = new System.Drawing.Point(0, 27);
            this.editorList.Model = null;
            this.editorList.Name = "editorList";
            this.editorList.SelectedNode = null;
            this.editorList.ShowNodeToolTips = true;
            this.editorList.Size = new System.Drawing.Size(1254, 318);
            this.editorList.TabIndex = 0;
            this.editorList.UseColumns = true;
            // 
            // nameColumn
            // 
            this.nameColumn.Header = "Name";
            this.nameColumn.MinColumnWidth = 10;
            this.nameColumn.Sortable = true;
            this.nameColumn.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.nameColumn.TooltipText = null;
            this.nameColumn.Width = 300;
            // 
            // valueColumn
            // 
            this.valueColumn.Header = "Value";
            this.valueColumn.MinColumnWidth = 10;
            this.valueColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.valueColumn.TooltipText = null;
            this.valueColumn.Width = 300;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 345);
            this.Controls.Add(this.editorList);
            this.Controls.Add(tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Crusader Kings 2 Save Editor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox saveSelector;
        private System.Windows.Forms.OpenFileDialog fileChooser;
        private Aga.Controls.Tree.TreeViewAdv editorList;
        private Aga.Controls.Tree.TreeColumn nameColumn;
        private Aga.Controls.Tree.TreeColumn valueColumn;
        private System.Windows.Forms.Button loadSaveButton;
    }
}


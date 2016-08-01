namespace CK2EditorGUI
{
    partial class SearchDialog
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            this.identBox = new System.Windows.Forms.TextBox();
            this.sectionBtn = new System.Windows.Forms.RadioButton();
            this.valueBtn = new System.Windows.Forms.RadioButton();
            this.anyBtn = new System.Windows.Forms.RadioButton();
            this.valueBox = new System.Windows.Forms.TextBox();
            this.findBtn = new System.Windows.Forms.Button();
            this.dNameBox = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(32, 59);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(50, 13);
            label1.TabIndex = 0;
            label1.Text = "Identifier:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(45, 85);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(37, 13);
            label2.TabIndex = 1;
            label2.Text = "Value:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(32, 9);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(34, 13);
            label3.TabIndex = 2;
            label3.Text = "Type:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(7, 33);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(75, 13);
            label4.TabIndex = 11;
            label4.Text = "Display Name:";
            // 
            // identBox
            // 
            this.identBox.Location = new System.Drawing.Point(88, 56);
            this.identBox.Name = "identBox";
            this.identBox.Size = new System.Drawing.Size(145, 20);
            this.identBox.TabIndex = 1;
            // 
            // sectionBtn
            // 
            this.sectionBtn.AutoSize = true;
            this.sectionBtn.Location = new System.Drawing.Point(72, 7);
            this.sectionBtn.Name = "sectionBtn";
            this.sectionBtn.Size = new System.Drawing.Size(61, 17);
            this.sectionBtn.TabIndex = 5;
            this.sectionBtn.Text = "Section";
            this.sectionBtn.UseVisualStyleBackColor = true;
            this.sectionBtn.CheckedChanged += new System.EventHandler(this.sectionBtn_CheckedChanged);
            // 
            // valueBtn
            // 
            this.valueBtn.AutoSize = true;
            this.valueBtn.Location = new System.Drawing.Point(132, 7);
            this.valueBtn.Name = "valueBtn";
            this.valueBtn.Size = new System.Drawing.Size(52, 17);
            this.valueBtn.TabIndex = 6;
            this.valueBtn.Text = "Value";
            this.valueBtn.UseVisualStyleBackColor = true;
            // 
            // anyBtn
            // 
            this.anyBtn.AutoSize = true;
            this.anyBtn.Checked = true;
            this.anyBtn.Location = new System.Drawing.Point(190, 7);
            this.anyBtn.Name = "anyBtn";
            this.anyBtn.Size = new System.Drawing.Size(43, 17);
            this.anyBtn.TabIndex = 7;
            this.anyBtn.Text = "Any";
            this.anyBtn.UseVisualStyleBackColor = true;
            this.anyBtn.CheckedChanged += new System.EventHandler(this.anyBtn_CheckedChanged);
            // 
            // valueBox
            // 
            this.valueBox.Location = new System.Drawing.Point(88, 82);
            this.valueBox.Name = "valueBox";
            this.valueBox.Size = new System.Drawing.Size(145, 20);
            this.valueBox.TabIndex = 2;
            this.valueBox.TextChanged += new System.EventHandler(this.valueBox_TextChanged);
            // 
            // findBtn
            // 
            this.findBtn.Location = new System.Drawing.Point(88, 111);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(145, 22);
            this.findBtn.TabIndex = 9;
            this.findBtn.TabStop = false;
            this.findBtn.Text = "Find Next";
            this.findBtn.UseVisualStyleBackColor = true;
            this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
            // 
            // dNameBox
            // 
            this.dNameBox.Location = new System.Drawing.Point(88, 30);
            this.dNameBox.Name = "dNameBox";
            this.dNameBox.Size = new System.Drawing.Size(145, 20);
            this.dNameBox.TabIndex = 0;
            // 
            // SearchDialog
            // 
            this.AcceptButton = this.findBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 141);
            this.Controls.Add(label4);
            this.Controls.Add(this.dNameBox);
            this.Controls.Add(this.findBtn);
            this.Controls.Add(this.valueBox);
            this.Controls.Add(this.anyBtn);
            this.Controls.Add(this.valueBtn);
            this.Controls.Add(this.sectionBtn);
            this.Controls.Add(this.identBox);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SearchDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox identBox;
        private System.Windows.Forms.RadioButton sectionBtn;
        private System.Windows.Forms.RadioButton valueBtn;
        private System.Windows.Forms.RadioButton anyBtn;
        private System.Windows.Forms.TextBox valueBox;
        private System.Windows.Forms.Button findBtn;
        private System.Windows.Forms.TextBox dNameBox;
    }
}
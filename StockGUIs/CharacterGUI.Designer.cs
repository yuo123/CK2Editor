using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockGUIs
{
    public partial class CharacterGUI
    {
        private void InitializeComponent()
        {
            this.m_nameText = new System.Windows.Forms.Label();
            this.wrapper.SuspendLayout();
            this.SuspendLayout();
            // 
            // wrapper
            // 
            this.wrapper.Controls.Add(this.m_nameText);
            this.wrapper.Size = new System.Drawing.Size(340, 105);
            // 
            // m_nameText
            // 
            this.m_nameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_nameText.AutoSize = true;
            this.m_nameText.Location = new System.Drawing.Point(141, 16);
            this.m_nameText.Name = "m_nameText";
            this.m_nameText.Size = new System.Drawing.Size(84, 13);
            this.m_nameText.TabIndex = 0;
            this.m_nameText.Text = "Character Name";
            // 
            // CharacterGUI
            // 
            this.Name = "CharacterGUI";
            this.Size = new System.Drawing.Size(346, 111);
            this.wrapper.ResumeLayout(false);
            this.wrapper.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label m_nameText;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CK2EditorGUI
{
    public class PathDisplay : Panel
    {
        private string m_seperator;

        public string Seperator
        {
            get { return m_seperator; }
            set
            {
                m_seperator = value;
                rlab.Text = value;
            }
        }


        //the root label
        private Label rlab;

        public PathDisplay()
        {
            m_seperator = "/";
            rlab = new Label();
            rlab.Text = Seperator;
            rlab.Font = new System.Drawing.Font(rlab.Font, FontStyle.Underline);
            rlab.ForeColor = Color.Blue;
            FormatLabel(rlab);
            this.Controls.Add(rlab);
        }

        /// <summary>
        /// Expand the displayed path into <paramref name="into"/>
        /// </summary>
        /// <param name="into">The location to append to the end of the path</param>
        public void Expand(string into)
        {
            //label for the seperator
            Label sepLabel = new Label();
            sepLabel.Text = Seperator;
            FormatLabel(sepLabel);
            this.Controls.Add(sepLabel);
            sepLabel.BringToFront();

            //label for the location (given by the "into" parameter)
            Label locLabel = new Label();
            locLabel.Text = into;
            FormatLabel(locLabel);
            locLabel.Font = new Font(locLabel.Font, FontStyle.Underline);
            locLabel.ForeColor = Color.Blue;
            this.Controls.Add(locLabel);
            locLabel.BringToFront();
        }

        protected virtual void FormatLabel(Label label)
        {
            label.Dock = DockStyle.Left;
            label.AutoSize = true;
            label.Padding = new Padding(0);
            label.Margin = new Padding(0);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            return new Size(proposedSize.Width, rlab.Font.Height + this.Padding.Vertical + rlab.Margin.Vertical);
        }

        /// <summary>
        /// Go out of the last location in the path
        /// </summary>
        public void Retract()
        {
            if (this.Controls.Count <= 1)
                return;
            for (int i = 0; i < 2; i++)
            {
                this.Controls.RemoveAt(0);
            }
        }

        /// <summary>
        /// Sets the full path this PathDisplay should display
        /// </summary>
        /// <param name="path">The full path to expand into, entries seperated by <c>Seperator</c></param>
        public void SetPath(string path)
        {
            while (this.Controls.Count > 1)
            {
                this.Retract();
            }
            string[] comps = path.Split(new string[] { Seperator }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string comp in comps)
            {
                this.Expand(comp);
            }
        }

        /// <summary>
        /// The full path this PathDisplay is currently displaying, entries seperated by <c>Seperator</c>
        /// </summary>
        public override string Text
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (Control contr in this.Controls)
                {
                    sb.Append(contr.Text);
                }
                return sb.ToString();
            }
        }
    }
}

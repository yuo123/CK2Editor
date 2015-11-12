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
        public string Seperator { get; set; }
        public string FullPath { get; protected set; }

        public PathDisplay()
        {
            Seperator = "/";
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
            sepLabel.AutoSize = true;
            sepLabel.Dock = DockStyle.Left;
            this.Controls.Add(sepLabel);

            //label for the location (given by the "into" parameter)
            Label locLabel = new Label();
            locLabel.Text = into;
            locLabel.AutoSize = true;
            locLabel.Dock = DockStyle.Left;
            locLabel.Font = new Font(locLabel.Font, FontStyle.Underline);
            locLabel.ForeColor = Color.Blue;
            this.Controls.Add(locLabel);
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
                this.Controls.RemoveAt(this.Controls.Count - 1);
            }
        }
    }
}

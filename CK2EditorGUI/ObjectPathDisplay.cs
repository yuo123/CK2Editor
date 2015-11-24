using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CK2EditorGUI
{
    class ObjectPathDisplay : PathDisplay
    {
        protected override void LabelClicked(Control lab, EventArgs e)
        {
            string path = GetPathForLabel(lab);
            this.SetPath(path);
            ObjectPathClickEventArgs args = new ObjectPathClickEventArgs();
            args.Path = path;
            args.ObjectPath = GetObjectPathForLabel(lab);
            OnPathClicked(args);
        }

        protected virtual IEnumerable GetObjectPathForLabel(Control lab)
        {
            int clickedIndexed = this.Controls.GetChildIndex(lab);
            for (int i = this.Controls.Count - 1; i >= clickedIndexed; i -= 2)
            {
                yield return this.Controls[i].Tag;
            }
        }
    }

    public class ObjectPathClickEventArgs : PathClickEventArgs
    {
        public IEnumerable ObjectPath { get; set; }
    }
}

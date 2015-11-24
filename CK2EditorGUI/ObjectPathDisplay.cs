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
            ObjectPathClickEventArgs args = new ObjectPathClickEventArgs();
            args.Path = path;
            args.ObjectPath = GetObjectPathForLabel(lab);
            OnPathClicked(args);
            this.SetPath(path);
        }

        protected virtual IEnumerable GetObjectPathForLabel(Control lab)
        {
            int clickedIndexed = this.Controls.GetChildIndex(lab);
            for (int i = this.Controls.Count - 3; i >= clickedIndexed; i -= 2)
            {
                yield return this.Controls[i].Tag;
            }
        }

        public virtual void Expand(string into, object obj)
        {
            base.Expand(into);
            Control last = this.Controls[0];
            last.Tag = obj;
        }

        public virtual void SetPath(IDictionary<string, object> path)
        {
            while (this.Controls.Count > 1)
            {
                this.Retract();
            }
            foreach (var comp in path)
            {
                this.Expand(comp.Key, comp.Value);
            }
        }
    }

    public class ObjectPathClickEventArgs : PathClickEventArgs
    {
        public IEnumerable ObjectPath { get; set; }
    }
}

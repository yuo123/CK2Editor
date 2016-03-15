using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace CK2EditorGUI.EditorGUIs
{
    [Designer(typeof(ParentControlDesigner), typeof(IDesigner))]
    public class EditorGUIBase : UserControl
    {
        protected GroupBox wrapper;

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                wrapper.Text = value;
            }
        }

        public new ControlCollection Controls { get { return wrapper.Controls; } }

        public EditorGUIBase()
        {
            this.wrapper = new GroupBox();
            wrapper.Dock = DockStyle.Fill;
            base.Controls.Add(wrapper);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control != wrapper)
            {
                System.Diagnostics.Debugger.Log(1, "EditorGUIBase", "Control added directly to base collection. You must cast the EditorGUI to EditorGUIBase instead!");
                base.Controls.Remove(e.Control);
                this.Controls.Add(e.Control);
            }
        }
    }
}

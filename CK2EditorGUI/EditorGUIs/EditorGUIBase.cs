using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

using CK2Editor;

namespace CK2EditorGUI.EditorGUIs
{
    [Designer(typeof(ParentControlDesigner), typeof(IDesigner))]
    public class EditorGUIBase : UserControl //this should be abstract, but it creates problems with the designer of inheriting classes
    {
        protected GroupBox wrapper;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get
            {
                return wrapper.Text;
            }
            set
            {
                base.Text = value;
                wrapper.Text = value;
            }
        }

        protected override Padding DefaultPadding
        {
            get
            {
                return new Padding(3);
            }
        }

        public new Padding Padding
        {
            get
            {
                return wrapper.Padding;
            }
            set
            {
                wrapper.Padding = value;
            }
        }

        public Control Control { get { return this; } }

        public new ControlCollection Controls { get { return wrapper.Controls; } }

        public EditorGUIBase()
        {
            this.wrapper = new GroupBox();
            wrapper.Dock = DockStyle.Fill;
            this.Padding = DefaultPadding;

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

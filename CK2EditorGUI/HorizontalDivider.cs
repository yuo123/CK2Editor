using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CK2EditorGUI
{
    [System.ComponentModel.DesignerCategory("")]
    class HorizontalDivider : Label
    {
        public override string Text { get { return ""; } set { Debug.WriteLine("Setting 'Text' attribute on HorizontalDivider, which has no effect"); } }
        public override bool AutoSize { get { return false; } set { Debug.WriteLine("Setting 'AutoSize' attribute on HorizontalDivider, which has no effect"); } }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.Height = 2;
            base.OnSizeChanged(e);
        }

        public HorizontalDivider()
        {
            this.Text = "";
            this.BorderStyle = BorderStyle.Fixed3D;
            this.AutoSize = false;
            this.Height = 2;
            this.Anchor |= AnchorStyles.Left | AnchorStyles.Right;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Aga.Controls.Tree.NodeControls;

using CK2Editor;

namespace CK2EditorGUI.NodeControls
{
    class EntryValueNodeText : NodeTextBox
    {
        private TextBox linkBox;

        public override void SetValue(Aga.Controls.Tree.TreeNodeAdv node, object value)
        {
            ValueEntry ent = value as ValueEntry;
            if (ent == null)
                return;//if this is not a value entry, value is not relevant
            if (linkBox == null)
            {
                linkBox = new TextBox();
            }
            linkBox.Text = "(" + FormattedReader.ParseRef(ent, ent.Link).FriendlyName + ")";
            base.SetValue(node, value);
        }

        public override object GetValue(Aga.Controls.Tree.TreeNodeAdv node)
        {
            ValueEntry ent = node.Tag as ValueEntry;
            if (ent == null)
                return "";//if this is not a value entry, value is not relevant
            Entry linked = FormattedReader.ParseRef(ent, ent.Link);
            return ent.Value + (ent.Link != null ? "(" + FormattedReader.ParseValueRefs(linked, linked.FriendlyName) + ")" : "");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aga.Controls.Tree.NodeControls;
using Aga.Controls.Tree;

using CK2Editor;

namespace CK2EditorGUI.NodeControls
{
    class EntryNameNodeText : NodeTextBox
    {
        public override object GetValue(TreeNodeAdv node)
        {
            if (node.Tag == null)
            {//this is the button for adding new entries
                return "+++Add New+++";
            }
            Entry ent = node.Tag as Entry;
            if (ent == null)
                throw new ArgumentException("EntryNameNodeText can only be used with entries");
            string res = FormattedReader.ParseValueRefs(ent, ent.FriendlyName);
            return res != null ? res : ent.InternalName;
        }

        public override void MouseDoubleClick(TreeNodeAdvMouseEventArgs args)
        {
            base.MouseDoubleClick(args);
            Entry ent = args.Node.Tag as Entry;
            if (ent == null)
                return;

            ModifyEntryDialog diag = new ModifyEntryDialog();
            diag.EditEntry(ent);
            diag.ShowDialog();
        }

        public EntryNameNodeText()
        {
            this.EditEnabled = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aga.Controls.Tree.NodeControls;

using CK2Editor;

namespace CK2EditorGUI.NodeControls
{
    class EntryNameNodeText : NodeTextBox
    {
        public EntryNameNodeText()
            : base()
        {
            this.Font = new System.Drawing.Font("Arial", 14);
        }

        public override object GetValue(Aga.Controls.Tree.TreeNodeAdv node)
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
    }
}

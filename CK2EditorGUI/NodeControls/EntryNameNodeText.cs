using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aga.Controls.Tree.NodeControls;

using CK2Editor;
using CK2Editor.Editors;

namespace CK2EditorGUI.NodeControls
{
    class EntryNameNodeText : NodeTextBox
    {
        public override object GetValue(Aga.Controls.Tree.TreeNodeAdv node)
        {
            Entry ent = node.Tag as Entry;
            if (ent == null)
                throw new ArgumentException("EntryNameNodeText can only be used with entries");
            string res = FormattedReader.ParseValueRefs(ent, ent.FriendlyName);
            return res != null ? res : ent.InternalName;
        }
    }
}

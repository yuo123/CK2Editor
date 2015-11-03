using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aga.Controls.Tree.NodeControls;

using CK2Editor;

namespace CK2EditorGUI.NodeControls
{
    class EntryValueNodeText : NodeTextBox
    {
        public override object GetValue(Aga.Controls.Tree.TreeNodeAdv node)
        {
            ValueEntry ent = node.Tag as ValueEntry;
            if (ent == null)
                return "";

            string value = ent.Value;
            string link = "";
            if (ent.Link != null)
            {
                Entry linkEnt = FormattedReader.ParseRef(ent, ent.Link);
                if (linkEnt != null)
                    link = linkEnt.FriendlyName;
            }
            return value + " " + link;
        }
    }
}

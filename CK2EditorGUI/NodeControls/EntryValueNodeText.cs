using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Aga.Controls.Tree.NodeControls;

using CK2Editor;
using CK2EditorGUI.EditorGUIs;

namespace CK2EditorGUI.NodeControls
{
    class EntryValueNodeText : NodeTextBox
    {
        public override void MouseDoubleClick(Aga.Controls.Tree.TreeNodeAdvMouseEventArgs args)
        {
            base.MouseDoubleClick(args);
            ValueEntry ent = args.Node.Tag as ValueEntry;
            if (ent == null)
                return;//if this is not a value entry, value is not relevant
            EditorGUI egui = (EditorGUI)this.Parent.Model;
            egui.GotoLink(ent.Link, args.Node);
        }

        public override object GetValue(Aga.Controls.Tree.TreeNodeAdv node)
        {
            ValueEntry ent = node.Tag as ValueEntry;
            if (ent == null)
                return "";//if this is not a value entry, value is not relevant
            Entry linked = FormattedReader.ParseRef(ent, ent.Link);
            return ent.Value + (ent.Link != null && linked != null ? " (" + FormattedReader.ParseValueRefs(linked, linked.FriendlyName) + ")" : "");
        }

        protected override string FormatLabel(object obj)
        {
            //this is workaround to the fact that GDI+ can't handle very large strings. Normal Auto-ellipsis should adjust the length further
            string text = base.FormatLabel(obj);
            if (text.Length > 1000)
                return text.Substring(0, 1000) + "...";
            else
                return text;
        }
    }
}
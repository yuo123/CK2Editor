using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

using Aga.Controls.Tree;

using CK2Editor;
using CK2Editor.Editors;

namespace CK2EditorGUI.EditorGUIs
{
    public class EditorGUI : ITreeModel
    {
        public IEditor FileEditor { get; set; }

        public TreeViewAdv Tree { get; set; }

        public EditorGUI(IEditor editor)
        {
            FileEditor = editor;
        }

        public System.Collections.IEnumerable GetChildren(TreePath treePath)
        {
            IEditor ed = treePath.IsEmpty() ? this.FileEditor : ((SectionEntry)treePath.LastNode).Section;
            var ret = new List<Entry>(ed.Values.Count + ed.Sections.Count);
            ret.AddRange(ed.Values);
            ret.AddRange(ed.Sections);
            return ret;
        }

        public bool IsLeaf(TreePath treePath)
        {
            return treePath.LastNode is ValueEntry;
        }

        protected internal void GotoLink(string path, TreeNodeAdv start = null)
        {
            TreeNodeAdv node = start != null ? start : Tree.Root;
            Entry startEnt;
            if (node.Tag == null)
            {
                SectionEntry tempEnt = new SectionEntry();
                tempEnt.Section = this.FileEditor;
                startEnt = tempEnt;
            }
            else
                startEnt = (Entry)node.Tag;
            if (path[0] == '!')
                node = Tree.Root;
            foreach (Entry ent in FormattedReader.ParseRefPath(startEnt, path))
            {
                node.Expand();
                node = GetNodeForEntry(node, ent);
            }
            Tree.ScrollTo(node);
            //There seems to be a bug in treeViewAdv, where the selection will sometimes be wrong if you scroll at the same time, and this is a workaround.
            //I could fix it, but I would like the library to remain untouched, and this works (at least for now).
            Timer workAround = new Timer();
            workAround.Tick += WorkAround_Tick;
            workAround.Interval = 1;
            workAroundnode = node;
            workAround.Start();
        }
        private TreeNodeAdv workAroundnode;
        private void WorkAround_Tick(object sender, EventArgs e)
        {
            ((Timer)sender).Stop();
            workAroundnode.Tree.ClearSelection();
            workAroundnode.IsSelected = true;
        }

        protected internal static TreeNodeAdv GetNodeForEntry(TreeNodeAdv parent, Entry ent)
        {
            foreach (TreeNodeAdv node in parent.Children)
            {
                if (node.Tag == ent)
                    return node;
            }
            return null;
        }

        public event EventHandler<TreeModelEventArgs> NodesChanged;

        public event EventHandler<TreeModelEventArgs> NodesInserted;

        public event EventHandler<TreeModelEventArgs> NodesRemoved;

        public event EventHandler<TreePathEventArgs> StructureChanged;
    }
}

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
    class EditorGUI : ITreeModel
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
            foreach (Entry ent in FormattedReader.ParseRefPath(startEnt, path))
            {
                node.Expand();
                node = GetNodeForEntry(node, ent);
            }
            node.IsSelected = true;
            Tree.ScrollTo(node);
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

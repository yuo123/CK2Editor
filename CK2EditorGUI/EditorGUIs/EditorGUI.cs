using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel.Design;

using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;

using CK2Editor;
using CK2Editor.Editors;
using CK2EditorGUI.NodeControls;

namespace CK2EditorGUI.EditorGUIs
{
    [System.ComponentModel.Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class EditorGUI : UserControl, ITreeModel, IToolTipProvider
    {
        private IEditor m_fileEditor;
        public IEditor FileEditor
        {
            get
            {
                return m_fileEditor;
            }
            set
            {
                m_fileEditor = value;
                if (value != null)
                    ResumeLayout(true);
            }
        }

        public EditorGUI()
            : base()
        {
            SuspendLayout();
            InitializeComponent();

            Tree.Model = this;
            var nameControl = new EntryNameNodeText();
            nameControl.ToolTipProvider = this;
            nameControl.ParentColumn = nameColumn;
            nameControl.IncrementalSearchEnabled = true;
            editorList.NodeControls.Add(nameControl);

            var valueControl = new EntryValueNodeText();
            valueControl.ParentColumn = valueColumn;
            editorList.NodeControls.Add(valueControl);
        }

        public EditorGUI(IEditor editor)
            : base()
        {
            InitializeComponent();
            FileEditor = editor;
            Tree.Model = this;

        }


        public System.Collections.IEnumerable GetChildren(TreePath treePath)
        {
            if (this.FileEditor == null)
                return null;
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

        public TreeViewAdv Tree;
        private TreeColumn treeColumn1;
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

        public string GetToolTip(TreeNodeAdv node, NodeControl nodeControl)
        {
            var ent = node.Tag as Entry;
            if (ent != null)
            {
                switch (nodeControl.ParentColumn.Header)
                {
                    case "Name":
                        return "Internal Name: \"" + ent.InternalName + "\"";
                }
            }

            return null;
        }

        public event EventHandler<TreeModelEventArgs> NodesChanged;

        public event EventHandler<TreeModelEventArgs> NodesInserted;

        public event EventHandler<TreeModelEventArgs> NodesRemoved;

        public event EventHandler<TreePathEventArgs> StructureChanged;

        private void InitializeComponent()
        {
            this.Tree = new Aga.Controls.Tree.TreeViewAdv();
            this.editorList = new Aga.Controls.Tree.TreeViewAdv();
            this.nameColumn = new Aga.Controls.Tree.TreeColumn();
            this.valueColumn = new Aga.Controls.Tree.TreeColumn();
            this.SuspendLayout();
            // 
            // Tree
            // 
            this.Tree.BackColor = System.Drawing.SystemColors.Window;
            this.Tree.Columns.Add(this.nameColumn);
            this.Tree.Columns.Add(this.valueColumn);
            this.Tree.DefaultToolTipProvider = null;
            this.Tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tree.DragDropMarkColor = System.Drawing.Color.Black;
            this.Tree.GridLineStyle = ((Aga.Controls.Tree.GridLineStyle)((Aga.Controls.Tree.GridLineStyle.Horizontal | Aga.Controls.Tree.GridLineStyle.Vertical)));
            this.Tree.LineColor = System.Drawing.SystemColors.ControlDark;
            this.Tree.Location = new System.Drawing.Point(0, 0);
            this.Tree.Model = null;
            this.Tree.Name = "Tree";
            this.Tree.SelectedNode = null;
            this.Tree.Size = new System.Drawing.Size(1008, 398);
            this.Tree.TabIndex = 0;
            this.Tree.Text = "treeViewAdv1";
            // 
            // editorList
            // 
            this.editorList.BackColor = System.Drawing.SystemColors.Window;
            this.editorList.DefaultToolTipProvider = null;
            this.editorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorList.DragDropMarkColor = System.Drawing.Color.Black;
            this.editorList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editorList.GridLineStyle = ((Aga.Controls.Tree.GridLineStyle)((Aga.Controls.Tree.GridLineStyle.Horizontal | Aga.Controls.Tree.GridLineStyle.Vertical)));
            this.editorList.LineColor = System.Drawing.SystemColors.ControlDarkDark;
            this.editorList.LoadOnDemand = true;
            this.editorList.Location = new System.Drawing.Point(0, 27);
            this.editorList.Model = null;
            this.editorList.Name = "editorList";
            this.editorList.SelectedNode = null;
            this.editorList.ShowNodeToolTips = true;
            this.editorList.Size = new System.Drawing.Size(1254, 318);
            this.editorList.TabIndex = 0;
            this.editorList.UseColumns = true;
            // 
            // nameColumn
            // 
            this.nameColumn.Header = "Name";
            this.nameColumn.MinColumnWidth = 10;
            this.nameColumn.Sortable = true;
            this.nameColumn.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.nameColumn.TooltipText = null;
            this.nameColumn.Width = 300;
            // 
            // valueColumn
            // 
            this.valueColumn.Header = "Value";
            this.valueColumn.MinColumnWidth = 10;
            this.valueColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.valueColumn.TooltipText = null;
            this.valueColumn.Width = 300;
            // 
            // EditorGUI
            // 
            this.Controls.Add(this.Tree);
            this.Name = "EditorGUI";
            this.Size = new System.Drawing.Size(1008, 398);
            this.ResumeLayout(false);

        }

        private TreeViewAdv editorList;
        private Aga.Controls.Tree.TreeColumn nameColumn;
        private Aga.Controls.Tree.TreeColumn valueColumn;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing;
using System.ComponentModel;

using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;

using CK2Editor;
using CK2EditorGUI.NodeControls;

namespace CK2EditorGUI.EditorGUIs
{
    [System.ComponentModel.DesignerCategory("")]
    public class EditorGUI : Panel, ITreeModel, IToolTipProvider
    {
        private SectionEntry m_fileSectionEntry;
        public SectionEntry RootSection
        {
            get
            {
                return m_fileSectionEntry;
            }
            set
            {
                m_fileSectionEntry = value;

                this.StructureChanged(this, new TreePathEventArgs());
            }
        }

        public EditorGUI()
        {
            InitializeComponent();
        }

        //public SectionEntryGUI(SectionEntry editor)
        //    : base()
        //{
        //    InitializeComponent();
        //    RootSection = editor;
        //    Tree.Model = this;
        //}

        public System.Collections.IEnumerable GetChildren(TreePath treePath)
        {
            if (this.RootSection == null)
                return null;
            SectionEntry ed = treePath.IsEmpty() ? this.RootSection : ((SectionEntry)treePath.LastNode);
            var ret = new List<Entry>(ed.Values.Count + ed.Sections.Count);
            ret.AddRange(ed.Values);
            ret.AddRange(ed.Sections);
            ret.Add(null);//a null entry, which will be the button for adding new entries
            return ret;
        }

        public bool IsLeaf(TreePath treePath)
        {
            return !(treePath.LastNode is SectionEntry);
        }

        public void Goto(IEnumerable<Entry> path, TreeNodeAdv start = null)
        {
            if (path.Count() == 0)//special case to go to the root, which scrolls to the top
                Tree.ScrollTo(Tree.Root.Children[0]);
            TreeNodeAdv node = start != null ? start : Tree.Root;

            foreach (Entry ent in path)
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

        public void GotoLink(string path, TreeNodeAdv start = null)
        {
            TreeNodeAdv node = start != null ? start : Tree.Root;
            Entry startEnt;
            if (node.Tag == null)//the root node doesn't have a tag
            {//create a temporary wrapper SectionEntry
                startEnt = RootSection;
            }
            else
                startEnt = (Entry)node.Tag;
            node = Tree.Root;
            Goto(FormattedReader.ParseRefPath(startEnt, path));
        }

        private TreeViewAdv m_tree;

        public TreeViewAdv Tree
        {
            get { return m_tree; }
            set
            {
                if (m_tree != null)
                    m_tree.SelectionChanged -= Tree_SelectionChanged;
                m_tree = value;
                m_tree.Model = this;
                m_tree.SelectionChanged += Tree_SelectionChanged;
            }
        }

        void Tree_SelectionChanged(object sender, EventArgs e)
        {
            TreePath tpath = Tree.GetPath(Tree.SelectedNode);
            Dictionary<string, object> path = new Dictionary<string, object>(tpath.FullPath.Length);
            foreach (var obj in tpath.FullPath)
            {
                Entry ent = (Entry)obj;
                path.Add(ent.InternalName, ent);
            }
            pathDisplay.SetPath(path);
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

#pragma warning disable 67

        public event EventHandler<TreeModelEventArgs> NodesChanged;

        public event EventHandler<TreeModelEventArgs> NodesInserted;

        public event EventHandler<TreeModelEventArgs> NodesRemoved;

#pragma warning restore 67

        public event EventHandler<TreePathEventArgs> StructureChanged;

        private void InitializeComponent()
        {
            this.Tree = new Aga.Controls.Tree.TreeViewAdv();
            this.nameColumn = new TreeColumn();
            this.valueColumn = new TreeColumn();
            this.SuspendLayout();

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
            // Tree
            // 
            this.Tree.Columns.Add(this.nameColumn);
            this.Tree.Columns.Add(this.valueColumn);
            this.Tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tree.DragDropMarkColor = System.Drawing.Color.Black;
            this.Tree.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tree.GridLineStyle = ((Aga.Controls.Tree.GridLineStyle)((Aga.Controls.Tree.GridLineStyle.Horizontal | Aga.Controls.Tree.GridLineStyle.Vertical)));
            this.Tree.LineColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Tree.LoadOnDemand = true;
            this.Tree.Location = new System.Drawing.Point(0, 27);
            this.Tree.Name = "Tree";
            this.Tree.ShowNodeToolTips = true;
            this.Tree.Size = new System.Drawing.Size(1254, 318);
            this.Tree.UseColumns = true;
            //
            //nameControl
            //
            var nameControl = new EntryNameNodeText();
            nameControl.ToolTipProvider = this;
            nameControl.ParentColumn = nameColumn;
            nameControl.IncrementalSearchEnabled = true;
            nameControl.Trimming = StringTrimming.EllipsisCharacter;
            Tree.NodeControls.Add(nameControl);
            //
            //valueControl
            //
            var valueControl = new EntryValueNodeText();
            valueControl.ParentColumn = valueColumn;
            valueControl.Trimming = StringTrimming.EllipsisCharacter;
            Tree.NodeControls.Add(valueControl);
            //
            //pathDisplay
            //
            pathDisplay = new ObjectPathDisplay();
            pathDisplay.Dock = DockStyle.Top;
            pathDisplay.Height = 16;
            pathDisplay.PathClicked += pathDisplay_PathClicked;
            // 
            // SectionEntryGUI
            // 
            this.Controls.Add(this.Tree);
            this.Controls.Add(pathDisplay);
            this.Name = "SectionEntryGUI";
            this.Size = new System.Drawing.Size(1008, 398);
            this.BorderStyle = BorderStyle.None;
            this.ResumeLayout(true);
        }

        void pathDisplay_PathClicked(object sender, PathClickEventArgs e)
        {
            this.Goto(((ObjectPathClickEventArgs)e).ObjectPath.Cast<Entry>());
        }
        private TreeColumn nameColumn;
        private TreeColumn valueColumn;
        private ObjectPathDisplay pathDisplay;
    }
}

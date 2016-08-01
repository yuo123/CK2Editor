using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.ComponentModel;

using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;

using CK2Editor;
using CK2Editor.Utility;
using CK2EditorGUI.NodeControls;
using CK2EditorGUI.Utility;

namespace CK2EditorGUI.EditorGUIs
{
    [DesignerCategory("")]
    public class FileEditorGUI : EditorGUIBase, ITreeModel, IToolTipProvider
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

        private SearchDialog activeSearch;

        public FileEditorGUI()
        {
            InitializeComponent();

            this.ParentChanged += (sender, e) =>
            {
                this.FindForm().KeyPreview = true;
                this.FindForm().KeyDown += OnKeyDown;
            };
        }

        protected void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
                OpenSearch();
        }

        private void OpenSearch()
        {
            if (RootSection == null) //don't allow searches before a save is loaded
                return;

            if (activeSearch != null)
                activeSearch.Focus();
            else
            {
                this.activeSearch = new SearchDialog();
                activeSearch.FormClosed += (s, e) => activeSearch = null;
                activeSearch.Editor = this; //lets SearchDialog call FindNext()
                activeSearch.Owner = this.ParentForm; //prevents SearchDialog from getting behind the main form
                activeSearch.Location = this.PointToScreen(Point.Empty); //align the dialog with the top left corner of this control
                activeSearch.Left += this.Width - activeSearch.Width; //move it to the top right
                activeSearch.Show();
            }
        }

        public IEnumerable GetChildren(TreePath treePath)
        {
            if (this.RootSection == null)
                return null;
            SectionEntry ed = treePath.IsEmpty() ? this.RootSection : ((SectionEntry)treePath.LastNode);
            List<Entry> ret = new List<Entry>(ed.Entries);
            ret.Add(null);//a null entry, which will be the button for adding new entries
            return ret;
        }

        public bool IsLeaf(TreePath treePath)
        {
            return !(treePath.LastNode is SectionEntry);
        }

        public void Goto(Entry entry)
        {
            if (entry == null)
            {
                Goto((IEnumerable<Entry>)null);
                return;
            }

            var path = new Stack<Entry>();
            while (entry.Parent != null)
            {
                path.Push(entry);
                entry = entry.Parent;
            }
            Goto(path);
        }

        public void Goto(IEnumerable<Entry> path, TreeNodeAdv start = null)
        {
            if (path == null || path.Count() == 0)//special case to go to the root, which scrolls to the top
            {
                Tree.ScrollTo(Tree.Root.Children[0]);
                return;
            }
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

        public void FindNext(SearchOptions options)
        {
            Entry cur = GetSelectedEntry();
            if (cur != null)
                cur = cur.Step(); //start one after the selected
            else
                cur = RootSection.Entries[0];

            while (cur != null && !options.Matches(cur))
            {
                cur = cur.Step(); //most of the magic is in the Step() method
            }
            Goto(cur);
        }

        public Entry GetSelectedEntry()
        {
            if (m_tree == null || m_tree.SelectedNode == null)
                return null;

            return (Entry)m_tree.SelectedNode.Tag;
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
            NameObjectCollection path = new NameObjectCollection(tpath.FullPath.Length);
            foreach (var obj in tpath.FullPath)
            {
                Entry ent = (Entry)obj;
                path.Add(ent != null ? ent.InternalName : null, ent);
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

        public void OnModifyEntry(Entry ent, TreeNodeAdv node)
        {
            ModifyEntryDialog diag = new ModifyEntryDialog();
            if (ent != null)
                diag.Edited = ent.Clone();
            diag.ShowDialog();

            if (diag.Confirmed)
            {
                TreePath path = node.Tree.GetPath(node);
                SectionEntry parent = (SectionEntry)node.Parent.Tag;

                int nodeIndex = node.Index;
                if (ent != null)
                {//if the entry is different, we need to remove the old one
                    parent.Entries.Remove(ent);
                    this.NodesRemoved(this, new TreeModelEventArgs(path.Up(), new int[] { node.Index }, new object[] { ent }));
                }
                if (diag.Edited != null)
                {//if the new entry exists (this was not just a deletion), we need to add it to the tree
                    parent.Entries.Insert(nodeIndex, diag.Edited);
                    diag.Edited.Parent = parent;
                    this.NodesInserted(this, new TreeModelEventArgs(path.Up(), new int[] { nodeIndex }, new object[] { diag.Edited }));
                }
            }
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
            this.Tree.DrawControl += Tree_DrawControl;
            //
            //nameControl
            //
            var nameControl = new EntryNameNodeText();
            nameControl.VirtualMode = true;
            nameControl.ToolTipProvider = this;
            nameControl.ParentColumn = nameColumn;
            nameControl.IncrementalSearchEnabled = true;
            nameControl.Trimming = StringTrimming.EllipsisCharacter;
            Tree.NodeControls.Add(nameControl);
            //
            //valueControl
            //
            var valueControl = new EntryValueNodeText();
            valueControl.VirtualMode = true;
            valueControl.ParentColumn = valueColumn;
            valueControl.Trimming = StringTrimming.EllipsisCharacter;
            Tree.NodeControls.Add(valueControl);
            //
            //pathDisplay
            //
            pathDisplay = new ObjectPathDisplay();
            pathDisplay.Dock = DockStyle.Top;
            pathDisplay.Height = 16;
            pathDisplay.RootSymbol = "!";
            pathDisplay.PathClicked += pathDisplay_PathClicked;
            // 
            // SectionEntryGUI
            // 
            this.Controls.Add(this.Tree);
            this.Controls.Add(pathDisplay);
            this.Name = "SectionEntryGUI";
            this.Size = new System.Drawing.Size(1008, 398);
            this.ResumeLayout(true);
        }

        private void Tree_DrawControl(object sender, DrawEventArgs e)
        {
            if (e.Node.IsSelected)
            {
                e.BackgroundBrush = SystemBrushes.Highlight;
                e.TextColor = ((SolidBrush)SystemBrushes.HighlightText).Color;
            }
        }

        void pathDisplay_PathClicked(object sender, PathClickEventArgs e)
        {
            this.Goto(((ObjectPathClickEventArgs)e).ObjectPath.Cast<Entry>());
        }
        private TreeColumn nameColumn;
        private TreeColumn valueColumn;
        private ObjectPathDisplay pathDisplay;
    }

    public class SearchOptions
    {
        public SearchType Type { get; set; }
        public string Identifier { get; set; }
        public string FriendlyName { get; set; }
        public string Value { get; set; }

        public enum SearchType { Any, Section, Value };
        public static SearchType ParseType(string name)
        {
            switch (name)
            {
                case "Any": return SearchType.Any;
                case "Section": return SearchType.Section;
                case "Value": return SearchType.Value;

                default: throw new ArgumentException("Unknown SearchType name", "name");
            }
        }

        public bool Matches(Entry entry)
        {
            if (Type == SearchType.Section && entry is ValueEntry)
                return false;
            if (Type == SearchType.Value && entry is SectionEntry)
                return false;
            if (!string.IsNullOrEmpty(Identifier) && (entry.InternalName == null || !entry.InternalName.Contains(Identifier)))
                return false;
            if (entry is ValueEntry)
                if (!string.IsNullOrEmpty(Value) && !((ValueEntry)entry).Value.Contains(Value))
                    return false;
            if (!string.IsNullOrEmpty(FriendlyName) && (entry.FriendlyName == null || !(FormattedReader.ParseValueRefs(entry, entry.FriendlyName)).Contains(FriendlyName)))
                return false;

            return true;
        }
    }
}

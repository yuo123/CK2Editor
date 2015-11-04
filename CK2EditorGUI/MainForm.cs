using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Aga.Controls;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;

using CK2Editor.Editors;
using CK2Editor;
using CK2EditorGUI.EditorGUIs;
using CK2EditorGUI.NodeControls;

namespace CK2EditorGUI
{
    public partial class MainForm : Form, IToolTipProvider
    {
        public MainForm()
        {
            InitializeComponent();

            var nameControl = new EntryNameNodeText();
            nameControl.ToolTipProvider = this;
            nameControl.ParentColumn = nameColumn;
            //nameControl.Font=System.Drawing.Font.
            editorList.NodeControls.Add(nameControl);

            var valueControl = new EntryValueNodeText();
            valueControl.ParentColumn = valueColumn;
            editorList.NodeControls.Add(valueControl);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            saveSelector.SelectedIndex = 0;
            fileChooser.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Paradox Interactive\Crusader Kings II\save games";

            string path = @"C:\Users\User\Desktop\CK2Editor\Test_Save.ck2";
            FormattedReader reader = new FormattedReader(@"C:\Users\User\Desktop\CK2Editor\CK2Editor\CK2Save.xml");
            Editor ed = reader.ReadFile(path);
            EditorGUI egui = new EditorGUI(ed);
            egui.Tree = editorList;
            editorList.Model = egui;
        }

        private void loadSaveButton_Click(object sender, EventArgs e)
        {
            if (fileChooser.ShowDialog() == DialogResult.OK)
            {
                saveSelector.Items.AddRange(fileChooser.FileNames);
                saveSelector.SelectedIndex = saveSelector.Items.Count - 1;
            }
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


    }
}

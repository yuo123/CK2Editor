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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            saveSelector.SelectedIndex = 0;
            fileChooser.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Paradox Interactive\Crusader Kings II\save games";

            string path = Environment.ExpandEnvironmentVariables(@"%userprofile%\Desktop\CK2Editor\Test_Save.ck2");
            FormattedReader reader = new FormattedReader(@"Formats\CK2Save.xml");
            Editor ed = reader.ReadFile(path);
            editorList.FileEditor = ed;
        }

        private void loadSaveButton_Click(object sender, EventArgs e)
        {
            if (fileChooser.ShowDialog() == DialogResult.OK)
            {
                saveSelector.Items.AddRange(fileChooser.FileNames);
                saveSelector.SelectedIndex = saveSelector.Items.Count - 1;
            }
        }



    }
}

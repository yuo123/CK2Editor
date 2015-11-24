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
        private EditorGUI editorList;

        public MainForm()
        {
            InitializeComponent();

            saveSelector.SelectedIndex = 0;
            fileChooser.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Paradox Interactive\Crusader Kings II\save games";


            editorList = new EditorGUI();
            editorList.Dock = DockStyle.Fill;
            this.Controls.Add(editorList);
            editorList.BringToFront();

        }

        private void ReadFile(string formatPath, string filePath)
        {
            FileReadingDialog dialog = new FileReadingDialog();
            FormattedReader reader = new FormattedReader(formatPath);
            dialog.ReadingDone += Dialog_ReadingDone;
            dialog.ReadFile(reader, filePath);
            dialog.Show(this);
            dialog.Focus();
        }

        private void Dialog_ReadingDone(object sender, ReadingDoneEventArgs e)
        {
            if (e.Successful)
            {
                editorList.FileEditor = e.ResultEditor;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void loadSaveButton_Click(object sender, EventArgs e)
        {
            if (fileChooser.ShowDialog() == DialogResult.OK)
            {
                saveSelector.Items.AddRange(fileChooser.FileNames);
                saveSelector.SelectedIndex = saveSelector.Items.Count - 1;
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            string path = Environment.ExpandEnvironmentVariables(@"%userprofile%\Desktop\CK2Editor\Test_Save.ck2");
            ReadFile(@"Formats\CK2Save.xml", path);
        }
    }
}

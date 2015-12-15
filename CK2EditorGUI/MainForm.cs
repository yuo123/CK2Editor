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

using CK2Editor;
using CK2Editor.Utility;
using CK2EditorGUI;
using CK2EditorGUI.EditorGUIs;
using CK2EditorGUI.NodeControls;

namespace CK2EditorGUI
{
    public partial class MainForm : Form
    {
        private EditorGUI editorList;

        //The directory containing format xml files
        private static readonly string FormatDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "Formats";

        //The save file location first suggested to the user (<Documents>\Paradox Interactive\Crusader Kings II\save games")
        private static readonly string DefaultSaveDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.Combine("Paradox Interactive", "Crusader Kings II", "save games");


        public MainForm()
        {
            InitializeComponent();

            saveSelector.SelectedIndex = 0;
            saveFileChooser.InitialDirectory = DefaultSaveDir;

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
                editorList.FileSectionEntry = e.ResultSection;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshFormats();
        }

        private void loadSaveButton_Click(object sender, EventArgs e)
        {
            if (saveFileChooser.ShowDialog() == DialogResult.OK)
            {
                saveSelector.Items.AddRange(saveFileChooser.FileNames);
                saveSelector.SelectedIndex = saveSelector.Items.Count - 1;
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //string path = Environment.ExpandEnvironmentVariables(@"%userprofile%\Desktop\CK2Editor\Full_Test_Save.ck2");
            //ReadFile(@"Formats\CK2Save.xml", path);
        }

        private void RefreshFormats()
        {
            if (!Directory.Exists(FormatDir))
                MessageBox.Show("Could not find formats directory: " + FormatDir);
            else
            {
                formatSelector.Items.Clear();
                foreach (string file in Directory.GetFiles(FormatDir))
                {
                    if (file.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                        formatSelector.Items.Add(Path.GetFileNameWithoutExtension(file));
                }
                formatSelector.SelectedIndex = 0;
            }
        }

        private void refreshFormatsButton_Click(object sender, EventArgs e)
        {
            RefreshFormats();
        }

        private void saveSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (saveSelector.SelectedItem != null && formatSelector.SelectedItem != null && saveSelector.SelectedIndex != 0)
                ReadFile(Path.Combine(FormatDir, ((string)formatSelector.SelectedItem)) + ".xml", (string)saveSelector.SelectedItem);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveSelector.SelectedText != null && saveSelector.SelectedIndex != 0)
                File.WriteAllText((string)saveSelector.SelectedItem, editorList.FileSectionEntry.Save());
        }
    }
}

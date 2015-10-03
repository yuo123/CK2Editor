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

using CK2Editor.Editors;
using CK2Editor;

namespace CK2Editor
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

            string path = @"D:\USERS\איל\Desktop\C# Projects\CK2Editor\Abyssinia999_05_08.ck2";
            FormattedReader reader = new FormattedReader(@"D:\USERS\איל\Desktop\C# Projects\CK2Editor\CK2Editor\CK2Save.xml");
            Editor ed = reader.ReadFile(path);
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

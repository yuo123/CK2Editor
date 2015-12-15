using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CK2Editor;

namespace CK2EditorGUI
{
    internal sealed partial class FileReadingDialog : Form
    {
        private FormattedReader reader;
        private string path;
        private SectionEntry m_resultEditor;

        public SectionEntry ResultEditor
        {
            get { return m_resultEditor; }
            private set { m_resultEditor = value; }
        }


        internal FileReadingDialog()
        {
            InitializeComponent();
        }

        internal void ReadFile(FormattedReader reader, string path)
        {
            this.reader = reader;
            this.path = path;
            bworker.RunWorkerAsync();
        }

        internal event EventHandler<ReadingDoneEventArgs> ReadingDone;

        private void bworker_DoWork(object sender, DoWorkEventArgs e)
        {
            m_resultEditor = reader.ReadFile(path);
        }

        private void bworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Error loading file: " + path + ". Error: " + e.Error.Message);
                if (ReadingDone != null)
                {
                    ReadingDoneEventArgs args = new ReadingDoneEventArgs();
                    args.Successful = true;
                    ReadingDone(this, args);
                }
            }
            else if (ReadingDone != null)
            {
                ReadingDoneEventArgs args = new ReadingDoneEventArgs();
                args.ResultSection = m_resultEditor;
                args.Successful = true;
                ReadingDone(this, args);
            }
            this.Close();
        }

        private void FileReadingDialog_Load(object sender, EventArgs e)
        {
            CenterToParent();
        }

        private void FileReadingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {//Note! cancellation isn't yet implemented!
            bworker.CancelAsync();
        }
    }

    internal class ReadingDoneEventArgs : EventArgs
    {
        public SectionEntry ResultSection { get; set; }
        public bool Successful { get; set; }
    }
}

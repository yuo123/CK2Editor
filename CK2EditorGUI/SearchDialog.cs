using CK2EditorGUI.EditorGUIs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CK2EditorGUI
{
    public partial class SearchDialog : Form
    {
        public FileEditorGUI Editor { get; set; }

        public SearchDialog()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void sectionBtn_CheckedChanged(object sender, EventArgs e)
        {
            // if the "Section" type option is selected, disable the value field
            valueBox.Enabled = !sectionBtn.Checked;
        }

        private void valueBox_TextChanged(object sender, EventArgs e)
        {
            // when the user starts typing a value, automatically select the "Value" type option
            valueBtn.Checked = true;
        }

        private void anyBtn_CheckedChanged(object sender, EventArgs e)
        {
            // when selecting the "Any" type option, clear the value field
            if (anyBtn.Checked && valueBox.Text != "")
            {
                valueBox.ResetText();
                anyBtn.Checked = true;
            }
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            SearchOptions options = new SearchOptions();
            options.Type = GetSelectedType();
            options.FriendlyName = dNameBox.Text;
            options.Identifier = identBox.Text;
            options.Value = valueBox.Text;
            Editor.FindNext(options);
        }

        private SearchOptions.SearchType GetSelectedType()
        {
            if (anyBtn.Checked)
                return SearchOptions.SearchType.Any;
            if (sectionBtn.Checked)
                return SearchOptions.SearchType.Section;
            if (valueBtn.Checked)
                return SearchOptions.SearchType.Value;

            throw new InvalidOperationException("No search type was selected - this should not be possible");
        }
    }
}

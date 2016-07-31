using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CK2Editor;
using CK2Editor.Utility;
using CK2EditorGUI;
using CK2EditorGUI.EditorGUIs;

namespace StockGUIs
{
    public partial class CharacterGUI : SectionEditorGUIBase, IEditorGUI
    {
        public CharacterGUI()
        {
            InitializeComponent();
        }

        public override void OnAssignEdited()
        {
            base.OnAssignEdited();

            m_nameText.Text = Section.FriendlyName;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }

    public class CharacterGUIProvider : IEditorGUIProvider
    {
        public string StructureName { get { return "Character"; } }

        //default character structure cache
        private static SectionEntry m_default = null;

        public bool CanEdit(Entry entry)
        {
            SectionEntry ent = entry as SectionEntry;
            if (ent == null)
                return false;
            if (!ent.Entries.Any(e => e.InternalName == "birth_name" || e.InternalName == "b_n"))
                return false;

            return true;
        }

        public IEditorGUI CreateEditor()
        {
            return new CharacterGUI();
        }

        public Entry GenerateDefault()
        {
            if (m_default == null)
            {
                m_default = new SectionEntry();
                m_default.InternalName = "";
                m_default.FriendlyName = "[!/birth_name:[VALUE]!]";
                ValueEntry bname = new ValueEntry();
                bname.InternalName = "birth_name";
                bname.FriendlyName = "Birth Name";
                bname.Parent = m_default;
                m_default.Entries.Add(bname);
            }

            return m_default.Clone();
        }
    }
}

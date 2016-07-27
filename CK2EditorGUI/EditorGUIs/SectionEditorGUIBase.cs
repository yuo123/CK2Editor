using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CK2Editor;

namespace CK2EditorGUI.EditorGUIs
{
    public class SectionEditorGUIBase : EditorGUIBase
    {
        public SectionEntry Section { get { return Edited.Entry as SectionEntry; } }

        private EditedEntry edited;
        public virtual EditedEntry Edited
        {
            get { return edited; }
            set
            {
                edited = value;
                OnAssignEdited();
            }
        }

        public virtual void OnAssignEdited()
        {
            this.Enabled = edited.Entry is SectionEntry;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CK2Editor;

namespace CK2EditorGUI.EditorGUIs
{
    public class ValueEditorGUIBase : EditorGUIBase
    {
        public ValueEntry Value { get { return Edited.Entry as ValueEntry; } }

        private EditedEntry edited;
        public virtual EditedEntry Edited
        {
            get { return edited; }
            set
            {
                edited = value;
                this.Enabled = value.Entry is ValueEntry;
                OnAssignEdited();
            }
        }

        protected virtual void OnAssignEdited() { }
    }
}

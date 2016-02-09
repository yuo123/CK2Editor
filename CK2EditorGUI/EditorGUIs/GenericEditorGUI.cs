using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CK2EditorGUI.EditorGUIs
{
    [Designer("System.Windows.Forms.Design.ComponentDocumentDesigner"),
    DesignerCategory("Form")]
    public class GenericEditorGUI : EditorGUIBase
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
    
        public GenericEditorGUI()
        {
        }
    }
}

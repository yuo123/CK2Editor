using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CK2EditorGUI.EditorGUIs
{
    //[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class EditorGUIBase : GroupBox
    {
        public EditorGUIBase()
        {
            this.Margin = new Padding(3, 10, 3, 3);
            this.Padding = new Padding(0, 20, 0, 0);
        }
    }
}

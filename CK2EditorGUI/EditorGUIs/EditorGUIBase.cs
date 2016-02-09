using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CK2EditorGUI.EditorGUIs
{
    [System.ComponentModel.DesignerCategory("")]
#if DEBUG
    public class EditorGUIBase : Panel
#else
    public abstract class EditorGUIBase : Panel
#endif
    {
        public EditorGUIBase()
        {

        }
    }
}

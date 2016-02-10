using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CK2EditorGUI.EditorGUIs
{
#if DEBUG
    public class EditorGUIBase : UserControl
#else
    public abstract class EditorGUIBase : Panel
#endif
    {
        public EditorGUIBase()
        {

        }
    }
}

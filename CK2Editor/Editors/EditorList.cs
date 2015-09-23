using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Editor.Editors
{
    class EditorList<T> : List<T>, IEditorList<T> where T : IEditor
    {
    }
}

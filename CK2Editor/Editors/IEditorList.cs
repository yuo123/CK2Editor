using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Editor.Editors
{
    public interface IEditorList<T> : IList<T> where T : IEditor
    {
    }
}

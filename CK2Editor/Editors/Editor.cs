using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Editor.Editors
{
    public class Editor : IEditor
    {
        public IDictionary<string, IEditor> GetSections()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string> GetValues()
        {
            throw new NotImplementedException();
        }

        public void setValue(string name, string value)
        {
            throw new NotImplementedException();
        }
    }
}

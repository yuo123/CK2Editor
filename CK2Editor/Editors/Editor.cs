using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CK2Editor.Editors
{
    public abstract class Editor : IEditor
    {
        public virtual IDictionary<string, IEditor> GetSections()
        {
            throw new NotImplementedException();
        }

        public virtual IDictionary<string, string> GetValues()
        {
            PropertyInfo[] props = this.GetType().GetProperties();
            //filtered props
            IEnumerable<PropertyInfo> fprops = props.Where(p => p.PropertyType == typeof(string) && p.CanRead && p.CanWrite && p.IsDefined(typeof(EditorValueAttribute)));
            Dictionary<string, string> re = new Dictionary<string, string>(fprops.Count());
            foreach (PropertyInfo prop in fprops)
            {
                re.Add(prop.Name, (string)prop.GetValue(this));
            }
            return re;
        }

        public virtual void setValue(string name, string value)
        {
            PropertyInfo prop = this.GetType().GetProperty(name);
            if ((!(prop.PropertyType == typeof(string))) || (!prop.CanWrite) || (!prop.IsDefined(typeof(EditorValueAttribute))))
                throw new ArgumentException("Editor " + this.GetType().Name + "does not expose a valid value named " + name);
            prop.SetValue(this, value);
        }
    }
}

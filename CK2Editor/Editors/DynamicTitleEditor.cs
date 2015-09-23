using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CK2Editor.Editors
{
    public class DynamicTitleEditor : Editor
    {
        protected FileSection source;

        public DynamicTitleEditor(FileSection source)
        {
            this.source = source;
        }
        
        //public IDictionary<string, string> GetValues()
        //{
        //    Dictionary<string, string> re = new Dictionary<string, string>();

        //    re.Add("Internal Name", InternName);
        //    re.Add("Base Title", BaseTitle);
        //    re.Add("Is Custom", IsCustom);
        //    re.Add("Is Dynamic", IsDynamic);

        //    return re;
        //}

        public string InternalName
        {
            get { return Util.ExtractStringValue(source, "title="); }
            set { Util.ReplaceStringValue(source, "title=", value); }
        }

        public string BaseTitle
        {
            get { return Util.ExtractStringValue(source, "base_title="); }
            set { Util.ReplaceStringValue(source, "base_title=", value); }
        }

        public string IsCustom
        {
            get { return Util.ExtractValue(source, "is_custom="); }
            set { Util.ReplaceValue(source, "is_custom=", value); }
        }

        public string IsDynamic
        {
            get { return Util.ExtractValue(source, "is_dynamic="); }
            set { Util.ReplaceValue(source, "is_dynamic=", value); }
        }

    }
}

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

        [EditorValue]
        public string InternalName
        {
            get { return Util.ExtractStringValue(source, "title="); }
            set { Util.ReplaceStringValue(source, "title=", value); }
        }

        [EditorValue]
        public string BaseTitle
        {
            get { return Util.ExtractStringValue(source, "base_title="); }
            set { Util.ReplaceStringValue(source, "base_title=", value); }
        }

        [EditorValue]
        public string IsCustom
        {
            get { return Util.ExtractValue(source, "is_custom="); }
            set { Util.ReplaceValue(source, "is_custom=", value); }
        }

        [EditorValue]
        public string IsDynamic
        {
            get { return Util.ExtractValue(source, "is_dynamic="); }
            set { Util.ReplaceValue(source, "is_dynamic=", value); }
        }

    }
}

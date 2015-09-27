using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace CK2Editor.Editors
{
    public class FileEditor : Editor
    {
        protected FileSection source;

        public FileEditor(string path)
        {
            try
            {
                source = new FileSection(File.ReadAllText(path));
            }
            catch (FileNotFoundException e)
            {
                throw new ArgumentException("The file " + path + " could not be found", e);
            }
            catch (IOException e)
            {
                throw new IOException("Could not open the file " + path, e);
            }
        }

        [EditorValue]
        public string PlayerId
        {
            get
            {
                FileSection playerScope = Util.ExtractDelimited(source, "player=");
                return Util.ExtractValue(playerScope, "id="); 
            }
            set { Util.ReplaceStringValue(source, "version=", value); }
        }

        [EditorValue]
        public string Version
        {
            get { return Util.ExtractStringValue(source, "version="); }
            set { Util.ReplaceStringValue(source, "version=", value); }
        }

        [EditorValue]
        public string Date
        {
            get { return Util.ExtractStringValue(source, "date="); }
            set { Util.ReplaceStringValue(source, "date=", value); }
        }

        [EditorValue]
        public string PlayerRealm
        {
            get { return Util.ExtractStringValue(source, "player_realm="); }
            set { Util.ReplaceStringValue(source, "player_realm=", value); }
        }
    }
}
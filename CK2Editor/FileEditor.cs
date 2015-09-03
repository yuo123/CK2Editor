using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CK2Editor
{
    public class FileEditor : IEditor
    {
        private StringBuilder file;

        public FileEditor(string path)
        {
            try
            {
                file = new StringBuilder(File.ReadAllText(path));
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

        public string GetVersion()
        {
            return Util.ExtractStringValue(file, "version=");
        }

        public string GetPlayerId()
        {
            string playerScope = Util.ExtractDelimited(file, "player=");
            return Util.ExtractValue(playerScope, "id=");
        }

        public void SetVersion(string value)
        {
            Util.ReplaceStringValue(file, "version=", value);
        }

        public IDictionary<string, IEditor> GetSections()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string> GetValues()
        {
            Dictionary<string, string> re = new Dictionary<string, string>();

            re.Add("version", GetVersion());
            re.Add("player's id", GetPlayerId());

            return re;
        }

        public void setValue(string name, string value)
        {
            throw new NotImplementedException();
        }
    }
}
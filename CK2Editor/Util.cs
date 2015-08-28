using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Editor
{
    public static class Util
    {
        public static string ExtractDelimited(string scope, string identifier)
        {
            int iindex = scope.IndexOf(identifier);
            int i = scope.IndexOf('{') + 1;
            string re = "";
            int cbrackets = 0;
            char inp = scope[i];
            while (inp != '}' || cbrackets > 0)
            {
                re += inp;
                if (inp == '{')
                    cbrackets++;
                i++;
                inp = scope[i];
            }
            return re;
        }
        public static string ExtractStringValue(string scope, string name)
        {
            int index = scope.IndexOf(name) + name.Length + 1;
            int index2 = scope.IndexOf('"', index);
            return scope.Substring(index, index2 - index);
        }

        public static string ExtractValue(string scope, string name)
        {
            int index = scope.IndexOf(name) + name.Length;
            int index2 = scope.IndexOfAny(new char[] { ' ', '\n' }, index);
            return scope.Substring(index, index2 - index);
        }
    }
}

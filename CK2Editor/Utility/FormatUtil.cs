using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Editor.Utility
{
    public static class FormatUtil
    {
        /// <summary>
        /// Extracts a string of the file delimited by curly brackets and identified by <paramref name="identifier"/>
        /// </summary>
        /// <param name="scope">The string to search</param>
        /// <param name="identifier">The section's identifier, including the equals sign (<c>=</c>)</param>
        /// <returns>A new <c>string</c> which is a child of <paramref name="scope"/> and contains the delimited area, without the brackets</returns>
        public static string ExtractDelimited(string scope, string identifier, int startIndex = 0)
        {
            int iindex = scope.IndexOfAny(new string[] { "\n" + identifier, "\t" + identifier, identifier, " " + identifier }, startIndex);
            int i = scope.IndexOf("{", iindex) + 1;
            int cbrackets = 0;
            char inp = scope[i];
            int i2 = i;
            //advances through the file until it reaches the closing bracket (while ignoring brackets of lower scopes)
            while ((inp != '}' || cbrackets > 0) && i2 < scope.Length - 1)
            {
                i2++;
                if (inp == '{')
                    cbrackets++;
                else if (inp == '}')
                    cbrackets--;
                inp = scope[i2];
            }
            return scope.Substring(i, i2 - i);
        }

        /// <summary>
        /// Extracts a string of the file delimited by curly brackets and identified by <paramref name="identifier"/>
        /// </summary>
        /// <param name="scope">The string to search</param>
        /// <param name="identifier">The section's identifier, including the equals sign (<c>=</c>)</param>
        /// <returns>A new <c>string</c> which is a child of <paramref name="scope"/> and contains the delimited area, without the brackets</returns>
        /// <param name="startIndex">The index to start searching at</param>
        /// <param name="foundIndex">The index of the start of the delimited section</param>
        public static string ExtractDelimited(string scope, string identifier, int startIndex, out int foundIndex)
        {
            int iindex = scope.IndexOfAny(new string[] { "\n" + identifier, "\t" + identifier, identifier, " " + identifier }, startIndex);
            int i = scope.IndexOf("{", iindex) + 1;
            int cbrackets = 0;
            char inp = scope[i];
            int i2 = i;
            //advances through the file until it reaches the closing bracket (while ignoring brackets of lower scopes)
            while ((inp != '}' || cbrackets > 0) && i2 < scope.Length - 1)
            {
                i2++;
                if (inp == '{')
                    cbrackets++;
                else if (inp == '}')
                    cbrackets--;
                inp = scope[i2];
            }
            foundIndex = i;
            return scope.Substring(i, i2 - 1 - i);
        }

        /// <summary>
        /// Returns a string value delimited by two parentheses and preceded an identifier
        /// </summary>
        /// <param name="scope">The <c>string</c> to search</param>
        /// <param name="name">The identifier of the value, including the equals sign (<c>=</c>)</param>
        /// <returns>The value without the parentheses</returns>
        public static string ExtractStringValue(string scope, string name, int startIndex = 0)
        {
            int index = scope.IndexOfAny(new string[] { "\n" + name, "\t" + name, " " + name, name }, startIndex) + name.Length + 1;
            if (index == name.Length)//if the identifier was not found
                return "";
            int index2 = scope.IndexOf("\"", index);
            return scope.Substring(index, index2 - index);
        }

        /// <summary>
        /// Returns a string value delimited by two parentheses and preceded an identifier
        /// </summary>
        /// <param name="scope">The <c>string</c> to search</param>
        /// <param name="name">The identifier of the value, including the equals sign (<c>=</c>)</param>
        /// <returns>The value without the parentheses</returns>
        /// /// <param name="startIndex">The index to start searching at</param>
        /// <param name="foundIndex">The index of the start of the value</param>
        public static string ExtractStringValue(string scope, string name, int startIndex, out int foundIndex)
        {
            int index = scope.IndexOfAny(new string[] { "\n" + name, "\t" + name, " " + name, name }, startIndex) + name.Length + 1;
            if (index == name.Length)//if the identifier was not found
            {
                foundIndex = -1;
                return "";
            }
            int index2 = scope.IndexOf("\"", index + 1);
            foundIndex = index + 1;
            return scope.Substring(index, index2 - index);
        }

        /// <summary>
        /// Returns a non-string value preceded by an identifier
        /// </summary>
        /// <param name="scope">The <c>string</c> to search</param>
        /// <param name="name">The identifier of the value, including the equals sign (<c>=</c>)</param>
        /// <returns>The value</returns>
        public static string ExtractValue(string scope, string name, int startIndex = 0)
        {
            int index = scope.IndexOfAny(new string[] { "\n" + name, "\t" + name, " " + name, name }, startIndex) + name.Length;
            if (index == name.Length - 1)//if the identifier was not found
                return "";
            int index2 = scope.IndexOfAny(new char[] { ' ', '\n', '\t', '\r' }, index);
            return scope.Substring(index, index2 - index);
        }

        /// <summary>
        /// Returns a non-string value preceded by an identifier
        /// </summary>
        /// <param name="scope">The <c>string</c> to search</param>
        /// <param name="name">The identifier of the value, including the equals sign (<c>=</c>)</param>
        /// <returns>The value</returns>
        public static string ExtractValue(string scope, string name, int startIndex, out int foundIndex)
        {
            int index = scope.IndexOfAny(new string[] { "\n" + name, "\t" + name, " " + name, name }, startIndex) + name.Length;
            if (index == name.Length - 1)//if the identifier was not found
            {
                foundIndex = -1;
                return "";
            }
            int index2 = scope.IndexOfAny(new char[] { ' ', '\n' }, index);
            foundIndex = index + 1;
            return scope.Substring(index, index2 - index);
        }

        /// <summary>
        /// Reads a value of the specified type and name from the given scope, optionally starting at a specific index
        /// </summary>
        /// <param name="scope">The scope to search</param>
        /// <param name="name">The name of the value in the file, without the equal sign (<c>=</c>)</param>
        /// <param name="type">The value type</param>
        /// <param name="startIndex">The index to start the search at</param>
        /// <returns>A string representation of the value</returns>
        public static string ReadValue(string scope, string name, string type, int startIndex = 0)
        {
            switch (type)
            {
                case "string":
                    return FormatUtil.ExtractStringValue(scope, name + '=', startIndex);
                case "series-compact":
                case "series":
                    return ExtractDelimited(scope, name + '=', startIndex).Trim(new char[] { '\n', '\t', '\r' });
                default:
                    return FormatUtil.ExtractValue(scope, name + '=', startIndex);
            }
        }

        /// <summary>
        /// Reads a value of the specified type and name from the given scope, optionally starting at a specific index
        /// </summary>
        /// <param name="scope">The scope to search</param>
        /// <param name="name">The name of the value in the file</param>
        /// <param name="type">The value type</param>
        /// <param name="startIndex">The index to start the search at</param>
        /// <param name="foundIndex">The index the value was found at</param>
        /// <returns>A string representation of the value</returns>
        public static string ReadValue(string scope, string name, string type, int startIndex, out int foundIndex)
        {
            switch (type)
            {
                case "string":
                    return FormatUtil.ExtractStringValue(scope, name + '=', startIndex, out foundIndex);
                case "series-compact":
                case "series":
                    return ExtractDelimited(scope, name + '=', startIndex, out foundIndex).ToString().Trim(new char[] { '\n', '\t' });
                default:
                    return FormatUtil.ExtractValue(scope, name + '=', startIndex, out foundIndex);
            }
        }

        public static IEnumerable<string> ListEntries(string scope, Predicate<string> filter = null)
        {
            return ListEntriesWithIndexes(scope, 0, filter).Select(pair => pair.Value);
        }

        public static bool IsBrace(this char c)
        {
            return c == '{' || c == '}';
        }

        /// <summary>
        /// Go to the end of a section from its start
        /// </summary>
        /// <param name="str">The string to look in</param>
        /// <param name="start">The index to start at</param>
        /// <returns>The index one after the closing brace</returns>
        public static int GotoSectionEnd(string str, int start)
        {
            if (str[start] != '{')
                throw new ArgumentException("Must start at the opening brace of the section");
            int bcount = 1; //brace count, the initial 1 is the opening brace
            int i = start + 1;
            while (i < str.Length && bcount > 0)//bcount==0 means we hit the end
            {
                if (str[i] == '{')
                    bcount++;
                else if (str[i] == '}')
                    bcount--;
                i++;
            }
            return i;
        }

        /// <summary>
        /// Finds the start of the next "word" in a file, according to the CK2txt format
        /// </summary>
        /// <param name="str">The string to look in</param>
        /// <param name="start">The index to start at</param>
        /// <returns>The index of the word start, or the length of the string if a word was not found</returns>
        public static int GotoWordStart(string str, int start)
        {
            for (int i = start; i < str.Length; i++)
            {
                if (i > 83369480)
                    System.Diagnostics.Debug.WriteLine("location: " + i);
                if (!char.IsWhiteSpace(str[i]) && str[i] != '=')//word cannot start with an equal sign
                    return i;
            }
            return str.Length;
        }

        /// <summary>
        /// Finds the end of a "word" in a file, according to the CK2txt formats
        /// </summary>
        /// <param name="str">The string to look in</param>
        /// <param name="start">The index to start at</param>
        /// <returns>The index after the word ends, or the length of the string if the end was not found</returns>
        public static int GotoWordEnd(string str, int start)
        {
            if (str[start] == '"')//strings are easy, just find the second '"'
            {
                int index = str.IndexOf('"', start + 1);
                return index != -1 ? index + 1 : str.Length;
            }
            else if (str[start] == '{')//sections are whole, single "words"
                return GotoSectionEnd(str, start);
            else
            {
                for (int i = start; i < str.Length; i++)
                {
                    if (str[i] == '=' || char.IsWhiteSpace(str[i]) || str[i].IsBrace())//words end at whitespaces, equal signs or braces
                        return i;
                }
                return str.Length;
            }
        }

        public static IEnumerable<KeyValuePair<int, string>> ListEntriesWithIndexes(string scope, int location = 0, Predicate<string> filter = null)
        {
            int i = location;
            while (i < scope.Length)
            {
                int firsti = GotoWordStart(scope, i);
                if (firsti == scope.Length)
                    break;
                i = GotoWordEnd(scope, firsti);
                string name = null;
                if (scope[i] == '=')
                {
                    name = scope.Substring(firsti, i - firsti);
                    i = GotoWordStart(scope, i);
                    i = GotoWordEnd(scope, i);
                }
                else
                {
                    name = "";
                }
                yield return new KeyValuePair<int, string>(firsti, name);
            }
        }
    }
}

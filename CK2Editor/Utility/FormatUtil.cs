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
            int index2 = scope.IndexOf("\"", index + 1);
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

        public static List<string> ListEntries(string scope, Predicate<string> filter = null)
        {
            List<string> re = new List<string>();
            int brackets = 0;
            for (int i = 0; i < scope.Length; i++)
            {
                char c = scope[i];
                switch (c)
                {
                    case '{': brackets++;
                        break;
                    case '}': brackets--;
                        if (brackets < 0)
                            brackets = 0;
                        break;
                    case '=':
                        int index = scope.Last(ch => ch == ' ' || ch == '\t' || ch == '\n');
                        string name = scope.Substring(i + 1, index - 1 - i);
                        if (filter == null || filter.Invoke(name))
                            re.Add(name);
                        break;
                }
            }
            return re;
        }

        public static Dictionary<int, string> ListEntriesWithIndexes(string scope, int location = 0, Predicate<string> filter = null)
        {
            Dictionary<int, string> re = new Dictionary<int, string>();
            int brackets = 0;
            int length = scope.Length;
            for (int i = location; i < length; i++)
            {
                char c = scope[i];
                switch (c)
                {
                    case '{': brackets++;
                        break;
                    case '}': brackets--;
                        if (brackets < 0)
                            brackets = 0;
                        break;
                    case '=':
                        if (brackets > 0)
                            break;
                        int i2;
                        for (i2 = i - 1; i2 >= 0; i2--)
                        {
                            char c2 = scope[i2];
                            if (c2 == '\t' || c2 == '\n' || c2 == ' ')
                                break;
                        }
                        string name = scope.Substring(i2 + 1, i - 1 - i2);
                        if (filter == null || filter.Invoke(name))
                            re.Add(i2 + 1, name);
                        break;
                }
            }
            return re;
        }
        
        public static void OutputSectionStart(StringBuilder sb, string name, int indent)
        {
            sb.IndentedAppendLine(indent, name + '=');
            sb.IndentedAppendLine(indent, "{");
        }

        public static void OutputStringValue(StringBuilder sb, string value)
        {
            sb.AppendLine('"' + value + '"');
        }

        public static void OutputSeriesValue(StringBuilder sb, string value, int indent)
        {
            sb.Append("\n");//not using AppendLine, because that uses \n\t instead of just \n like the game does
            sb.IndentedAppendLine(indent, "{");
            sb.Append(value);
            sb.IndentedAppendLine(indent, "}");//in files genrated by the game, the indent is AFTER the series value. this is probably unintentional, but the goal is to replicate the game's exact behaviour
        }

        public static void OutputSeriesCompactValue(StringBuilder sb, string value)
        {
            sb.AppendLine('{' + value + '}');
        }

        public static void OutputSectionEnd(StringBuilder sb, int indent)
        {
            sb.IndentedAppendLine(indent, "}");
        }

        public static void OutputValueIdentifier(StringBuilder sb, string name, int indent)
        {
            sb.IndentedAppend(indent, name + '=');
        }

        public static void OutputValue(StringBuilder sb, string value)
        {
            sb.AppendLine(value);
        }

        public static void OutputValueFull(StringBuilder sb, ValueEntry entry, int indent)
        {
            OutputValueIdentifier(sb, entry.InternalName, indent);
            switch (entry.Type)
            {
                case "string":
                    OutputStringValue(sb, entry.Value);
                    break;
                case "series":
                    OutputSeriesValue(sb, entry.Value, indent);
                    break;
                case "series-compact":
                    OutputSeriesCompactValue(sb, entry.Value);
                    break;
                default:
                    OutputValue(sb, entry.Value);
                    break;
            }
        }
    }
}

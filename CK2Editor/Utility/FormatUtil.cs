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

        public static List<string> ListEntries(string scope, Predicate<string> filter = null)
        {
            return ListEntriesWithIndexes(scope, 0, filter).Values.ToList();
        }

        public static Dictionary<int, string> ListEntriesWithIndexes(string scope, int location = 0, Predicate<string> filter = null)
        {
            Dictionary<int, string> re = new Dictionary<int, string>();
            int brackets = 0;
            int length = scope.Length;
            for (int i = location; i < length; i++)//go through scope, looking for an equal sign that is not inside curly brackets
            {
                char c = scope[i];
                switch (c)
                {
                    case '{':
                        brackets++;
                        break;
                    case '}':
                        brackets--;
                        if (brackets < 0)
                            brackets = 0;
                        break;
                    default:
                        {
                            if (brackets > 0)//ignore lower scopes
                                break;
                            if (char.IsWhiteSpace(c))//ignore whitespaces
                                break;
                            int firsti = i++;
                            bool identifier = false;
                            for (; !identifier && !char.IsWhiteSpace(scope, i); i++)//go until the first whitespace, or until we discovered this is an identifier
                            {
                                if (scope[i] == '=')
                                {
                                    re.Add(firsti, scope.Substring(firsti, i - firsti));//if it is an equal sign, we found an identifier
                                    i--;//decrement i, so it isn't incremented from both loops and skips a character
                                    identifier = true;
                                }
                            }
                            if (!identifier)
                            {//if no equal sign was found, we found an entry
                                if (firsti == 0)//if we're at the start of the scope, this is an anonymous entry
                                {
                                    re.Add(0, "");
                                }
                                else
                                    for (int i2 = firsti - 1; i2 >= 0; i2--)//find the first non-whitespace backwards
                                    {
                                        if (scope[i2] == '=')//if an equal sign was found, we have already found the identifier
                                            break;
                                        if (!char.IsWhiteSpace(scope, i2) || i2 == 0)//if a non-whitespace was found, or the start of the scope was reached, this is an anonymous entry ("multiple='blank'" in format)
                                        {
                                            re.Add(firsti, "");
                                            break;
                                        }
                                    }
                            }
                        }
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
            sb.IndentedAppendLine(indent, "}");//in files generated by the game, the indent is AFTER the series value. this is probably unintentional, but the goal is to replicate the game's exact behaviour
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

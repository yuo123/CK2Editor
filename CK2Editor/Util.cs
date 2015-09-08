using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Editor
{
    public static class Util
    {
        /// <summary>
        /// Turns a negative index in a length context into an equivalent positive one
        /// </summary>
        /// <param name="index">The index to resolve, positive or negative</param>
        /// <param name="length">The length of the collection <paramref name="index"/> operates in</param>
        /// <returns>The equivalent positive index if <paramref name="index"/> is negative, or <c>index</c> itself if positive</returns>
        /// <exception cref="System.IndexOutOfRangeException">Thrown when <paramref name="index"/> is negative and its absolute value is greater than <paramref name="length"/></exception>
        public static int ResolveNegativeIndex(int index, int length)
        {
            if (index < 0)
            {
                //check valadity
                if (-index > length)
                    throw new IndexOutOfRangeException("The index was negative and it's absuloute value was greater than the length");
                //since index is negative, this is like length - Math.Abs(index)
                return length + index;
            }
            return index;
        }

        /// <summary>
        /// Returns the index of the start of the contents in a StringBuilder
        /// </summary>        
        /// <param name="value">The string to find</param>
        /// <param name="startIndex">The starting index.</param>
        /// <param name="ignoreCase">if set to <c>true</c> it will ignore case</param>
        /// <returns>The first index of the first occurance of <paramref name="value"/>, or -1 if not found</returns>
        public static int IndexOf(this StringBuilder sb, string value, int startIndex = 0, int maxIndex = -1, bool ignoreCase = false)
        {
            int index;
            int length = value.Length;

            //stop the search at this index
            int maxSearchLength = (ResolveNegativeIndex(maxIndex, sb.Length) - length) + 1;

            if (ignoreCase)
            {
                for (int i = startIndex; i < maxSearchLength; ++i)
                {
                    if (Char.ToLower(sb[i]) == Char.ToLower(value[0]))
                    {
                        index = 1;
                        while ((index < length) && (Char.ToLower(sb[i + index]) == Char.ToLower(value[index])))
                            ++index;

                        if (index == length)
                            return i;
                    }
                }

                return -1;
            }

            for (int i = startIndex; i < maxSearchLength; ++i)
            {
                if (sb[i] == value[0])
                {
                    //if theres a match of the first character, check if the rest matches
                    index = 1;
                    while ((index < length) && (sb[i + index] == value[index]))
                        ++index;

                    if (index == length)
                        return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Returns the index of the start of the any of the values in a StringBuilder
        /// </summary>        
        /// <param name="value">The characters to find</param>
        /// <param name="startIndex">The starting index.</param>
        /// <param name="ignoreCase">if set to <c>true</c> it will ignore case</param>
        /// <returns></returns>
        public static int IndexOfAny(this StringBuilder sb, char[] values, int startIndex = 0, int maxIndex = -1)
        {
            for (int i = startIndex; i <= ResolveNegativeIndex(maxIndex, sb.Length); ++i)
            {
                if (values.Contains(sb[i]))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Extracts a FileSection of the file delimited by curly brackets and identified by <paramref name="identifier"/>
        /// </summary>
        /// <param name="scope">The FileSection to search</param>
        /// <param name="identifier">The section's identifier, including the equals sign (<c>=</c>)</param>
        /// <returns>A new <c>FileSection</c> which is a child of <paramref name="scope"/> and contains the delimited area, without the brackets</returns>
        public static FileSection ExtractDelimited(FileSection scope, string identifier)
        {
            int iindex = scope.IndexOf(identifier);
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
                inp = scope[i2];
            }
            return new FileSection(scope, i, i2 - 1);
        }

        /// <summary>
        /// Returns a string value delimited by two parentheses and preceded an identifier
        /// </summary>
        /// <param name="scope">The <c>FileSection</c> to search</param>
        /// <param name="name">The identifier of the value, including the equals sign (<c>=</c>)</param>
        /// <returns>The value without the parentheses</returns>
        public static string ExtractStringValue(FileSection scope, string name)
        {
            int index = scope.IndexOf(name) + name.Length + 1;
            int index2 = scope.IndexOf("\"", index);
            return scope.ToString(index, index2);
        }

        /// <summary>
        /// Returns a non-string value preceded by an identifier
        /// </summary>
        /// <param name="scope">The <c>FileSection</c> to search</param>
        /// <param name="name">The identifier of the value, including the equals sign (<c>=</c>)</param>
        /// <returns>The value</returns>
        public static string ExtractValue(FileSection scope, string name)
        {
            int index = scope.IndexOf(name) + name.Length;
            if (index == name.Length)
                return "";
            int index2 = scope.IndexOfAny(new char[] { ' ', '\n' }, index);
            return scope.ToString(index, index2);
        }

        /// <summary>
        /// Replaces a string value delimited by two parentheses and preceded by an identifier
        /// </summary>
        /// <param name="scope">The <c>FileSection</c> to search</param>
        /// <param name="name">The identifier of the value, including the equals sign (<c>=</c>)</param>
        /// <param name="value">The string value to replace the existing value with</param>
        public static void ReplaceStringValue(FileSection scope, string name, string value)
        {
            int index = scope.IndexOf(name) + name.Length + 1;
            int index2 = scope.IndexOf("\"", index);
            scope.Remove(index, index2 - index);
            scope.Insert(value, index);
        }

        /// <summary>
        /// Replaces a non-string value preceded by an identifier
        /// </summary>
        /// <param name="scope">The <c>FileSection</c> to search</param>
        /// <param name="name">The identifier of the value, including the equals sign (<c>=</c>)</param>
        /// <param name="value">The non-string value to replace the existing value with</param>
        public static void ReplaceValue(FileSection scope, string name, string value)
        {
            int index = scope.IndexOf(name) + name.Length;
            int index2 = scope.IndexOfAny(new char[] { ' ', '\n' }, index);
            scope.Remove(index, index2 - index);
            scope.Insert(value, index);
        }
    }
}

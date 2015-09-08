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
                if (-index > length)
                    throw new IndexOutOfRangeException("The index was negative and it's absuloute value was greater than the length");
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
        /// <returns></returns>
        public static int IndexOf(this StringBuilder sb, string value, int startIndex = 0, int maxIndex = -1, bool ignoreCase = false)
        {
            int index;
            int length = value.Length;

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

        public static FileSection ExtractDelimited(FileSection scope, string identifier)
        {
            int iindex = scope.IndexOf(identifier);
            int i = scope.IndexOf("{", iindex) + 1;
            int cbrackets = 0;
            char inp = scope[i];
            int i2 = i;
            while ((inp != '}' || cbrackets > 0) && i2 < scope.Length - 1)
            {
                i2++;
                if (inp == '{')
                    cbrackets++;
                i++;
                inp = scope[i];
            }
            return new FileSection(scope, i, i2);
        }

        public static string ExtractStringValue(FileSection scope, string name)
        {
            int index = scope.IndexOf(name) + name.Length + 1;
            int index2 = scope.IndexOf("\"", index);
            return scope.ToString(index, index2);
        }

        public static string ExtractValue(FileSection scope, string name)
        {
            int index = scope.IndexOf(name) + name.Length;
            int index2 = scope.IndexOfAny(new char[] { ' ', '\n' }, index);
            return scope.ToString(index, index2);
        }

        public static void ReplaceStringValue(FileSection scope, string name, string value)
        {
            int index = scope.IndexOf(name) + name.Length + 1;
            int index2 = scope.IndexOf("\"", index);
            scope.Remove(index, index2 - index);
            scope.Insert(value, index);
        }

        public static void ReplaceValue(FileSection scope, string name, string value)
        {
            int index = scope.IndexOf(name) + name.Length;
            int index2 = scope.IndexOfAny(new char[] { ' ', '\n' }, index);
            scope.Remove(index, index2 - index);
            scope.Insert(value, index);
        }
    }
}

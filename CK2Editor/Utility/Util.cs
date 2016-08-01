using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Editor.Utility
{
    public static class Util
    {
        //taken from: http://www.dotnetperls.com/uppercase-first-letter
        public static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        public static readonly Encoding SAVE_ENCODING = Encoding.UTF7;

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
                //check validity
                if (-index > length)
                    throw new IndexOutOfRangeException("The index was negative and it's absolute value was greater than the length");
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
        /// <returns>The first index of the first occurrence of <paramref name="value"/>, or -1 if not found</returns>
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
        /// Returns the index of the start of the any of the values in a string
        /// </summary>        
        /// <param name="value">The characters to find</param>
        /// <param name="startIndex">The starting index.</param>
        /// <param name="ignoreCase">if set to <c>true</c> it will ignore case</param>
        /// <returns></returns>
        public static int IndexOfAny(this string s, string[] values, int startIndex = 0, int maxIndex = -1)
        {
            foreach (string val in values)
            {
                if (string.IsNullOrEmpty(val))
                {
                    return startIndex;
                }
            }
            int[] curs = new int[values.Length]; //curs holds the indexes of the character of each string in values we are currently looking for.
            maxIndex = Util.ResolveNegativeIndex(maxIndex, s.Length);
            for (int i = startIndex; i < maxIndex; i++)//main loop, goes through the entire string
            {
                for (int j = 0; j < curs.Length; j++)//secondary loop, goes through all of curs (and values)
                {

                    if (s[i] == values[j][curs[j]])//if there's a match
                    {
                        curs[j]++;
                        if (curs[j] == values[j].Length)//if we've reached the end of a value
                            return i - values[j].Length + 1;
                    }
                    else if (curs[j] != 0)//if there isn't a match, and the index wasn't already zero
                    {
                        curs[j] = 0;//reset the current index
                        j--;//rewind the loop, so it goes over this value again with the index of zero
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the start of the any of the values in a StringBuilder
        /// </summary>        
        /// <param name="value">The characters to find</param>
        /// <param name="startIndex">The starting index.</param>
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

        public static void IndentedAppend(this StringBuilder sb, int indent, string s = null)
        {
            sb.Append('\t', indent);
            if (s != null)
                sb.Append(s);
        }

        public static void IndentedAppendLine(this StringBuilder sb, int indent, string s = null)
        {
            IndentedAppend(sb, indent, s);
            sb.Append('\n');
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> enumurable)
        {
            return enumurable.ToDictionary(x => x.Key, x => x.Value);
        }

        public static IEnumerable<Entry> DepthFirstTraversal(this SectionEntry start)
        {
            // adapted from http://stackoverflow.com/a/5806795
            var visited = new HashSet<Entry>();
            var stack = new Stack<Entry>();

            stack.Push(start);

            while (stack.Count != 0)
            {
                var current = stack.Pop();

                if (!visited.Add(current))
                    continue;

                yield return current;

                var section = current as SectionEntry;
                if (section != null)
                {
                    var children = section.Entries.Where(n => !visited.Contains(n));

                    foreach (var neighbour in children.Reverse())
                        stack.Push(neighbour);
                }
            }
        }

        public static Entry Step(this Entry current)
        {
            //if this is a section with children, go into it
            var section = current as SectionEntry;
            if (section != null && section.Entries.Count > 0)
                return section.Entries[0];

            int index = current.Parent.Entries.FindIndex(ent => ReferenceEquals(ent, current)); //using FindIndex and ReferenceEquals because the Entry class overrides Equals
            while (index == current.Parent.Entries.Count - 1) //if we've hit the end of the parent section
            {
                if (current.Parent.Parent == null) //if this is a child of the root, we've found nothing
                    return null;
                current = current.Parent; //else, we go up one level and see if we can continue
                index = current.Parent.Entries.FindIndex(ent => ReferenceEquals(ent, current));
            }

            //if we can continue in the parent section, DO IT
            return current.Parent.Entries[index + 1];
        }

        public static void AddMany(this SectionEntry parent, params Entry[] items)
        {
            foreach (var item in items)
            {
                parent.Entries.Add(item);
                item.Parent = parent;
            }
        }
    }
}

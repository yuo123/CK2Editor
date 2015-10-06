using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CK2Editor.Utility;

namespace CK2Editor
{
    /// <summary>
    /// Represents a section of a mutable file, which can be independentley edited
    /// </summary>
    public class FileSection : IEnumerable<char>
    {
        protected class IndexChangedEventArgs : EventArgs
        {
            internal int Location;
            internal int Displacement;

            public IndexChangedEventArgs(int location, int displacement)
            {
                Location = location;
                Displacement = displacement;
            }
        }

        protected event EventHandler<FileSection.IndexChangedEventArgs> IndexChanged;

        /// <summary>
        /// The global StringBulder object shared by all FileSections of the same file
        /// </summary>
        protected StringBuilder gscope;
        /// <summary>
        /// The global index in <c>gscope</c> where this file section starts
        /// </summary>
        protected virtual int si { get; set; }
        /// <summary>
        /// The global index in <c>gscope</c> where this file section ends (inclusive)
        /// </summary>
        protected virtual int ei { get; set; }

        /// <summary>
        /// The parent section of this FileSection. <c>NullFileSection</c> if this is the top section (see <see cref="CK2Editor.NullFileSection"/>)
        /// </summary>
        public FileSection Parent { get; set; }

        public char this[int index]
        {
            get
            {
                index = ResolveNegativeIndex(index);
                ValidateIndex(index);
                return gscope[si + index];
            }
            set
            {
                index = ResolveNegativeIndex(index);
                ValidateIndex(index);
                gscope[si + index] = value;
            }
        }

        internal char GetCharAtUnsafe(int index)
        {
            return gscope[si + index];
        }

        /// <summary>
        /// The length of this FileSection
        /// </summary>
        public virtual int Length { get { return ei - si + 1; } }

        /// <summary>
        /// Creates a new root <c>FileSection</c> from the specified string, starting at <paramref name="startIndex"/> and ending at <paramref name="endIndex"/> of the string
        /// </summary>
        /// <param name="s">the string to base this FileSection on</param>
        /// <param name="startIndex">The index of <paramref name="s"/> this FileSection will start at</param>
        /// <param name="endIndex">The index of <paramref name="s"/> this FileSection will end at</param>
        public FileSection(string s, int startIndex = 0, int endIndex = -1) : this(new StringBuilder(s), startIndex, endIndex) { }

        /// <summary>
        /// Creates a new root <c>FileSection</c> from the specified <c>StringBuilder</c>, starting at <paramref name="startIndex"/> and ending at <paramref name="endIndex"/> of the string
        /// </summary>
        /// <param name="sb">the <c>StringBuilder</c> to base this FileSection on</param>
        /// <param name="startIndex">The index of <paramref name="sb"/> this FileSection will start at</param>
        /// <param name="endIndex">The index of <paramref name="sb"/> this FileSection will end at</param>
        public FileSection(StringBuilder sb, int startIndex = 0, int endIndex = -1)
        {
            Initialize(sb, startIndex, endIndex);
        }

        /// <summary>
        /// Creates a new child FileSection with the specified parent, starting at <paramref name="startIndex"/> and ending at <paramref name="endIndex"/> of the string
        /// </summary>
        /// <param name="parent">The parent FileSection of this FileSection</param>
        /// <param name="startIndex">The index of <paramref name="parent"/> this FileSection will start at</param>
        /// <param name="endIndex">The index of <paramref name="parent"/> this FileSection will end at</param>
        public FileSection(FileSection parent, int startIndex, int endIndex)
        {
            Initialize(parent.gscope, startIndex, endIndex, parent);
        }

        private void Initialize(StringBuilder sb, int startIndex, int endIndex, FileSection parent = null)
        {
            gscope = sb;
            if (parent != null)
            {
                Parent = parent;
                si = parent.si + parent.ResolveNegativeIndex(startIndex);
                ei = parent.si + parent.ResolveNegativeIndex(endIndex);
                parent.IndexChanged += Parent_IndexChanged;
            }
            else
            {
                Parent = new NullFileSection(gscope.Length);
                si = Util.ResolveNegativeIndex(startIndex, sb.Length);
                ei = Util.ResolveNegativeIndex(endIndex, sb.Length);
            }
            this.IndexChanged += FileSection_IndexChanged;
        }

        void FileSection_IndexChanged(object sender, FileSection.IndexChangedEventArgs e)
        {
            if (sender != this && Parent != null && Parent.IndexChanged != null)
            {
                Parent.IndexChanged(this, e);
            }
        }

        void Parent_IndexChanged(object sender, FileSection.IndexChangedEventArgs e)
        {
            int displacement = e.Displacement;
            if (displacement < -(ei + e.Location))
                displacement = -(ei + e.Location);

            if (e.Location > ei) //if the changed location is further down than this section extends, it does not affect it
                return;
            if (e.Location <= si)
                si += displacement;
            ei += displacement;
        }

        /// <summary>
        /// An empty costructor for use of special derived FileSections
        /// </summary>
        protected FileSection() { }

        /// <summary>
        /// Turns negtaive indexes (counted from the end of this FileSection) into the equivalent positive indexes. Returns <paramref name="index"/> if already positive
        /// </summary>
        /// <param name="index">The index to resolve</param>
        private int ResolveNegativeIndex(int index)
        {
            return Util.ResolveNegativeIndex(index, this.Length);
        }

        /// <summary>
        /// Checks the index is within bounds, and if not, throws an IndexOutOfRangeException
        /// <para>Note: this method does not resolve negative indexes. If these are accepted you should use <c>ResolveNegativeIndex</c> on <paramref name="index"/> beforehand</para>
        /// </summary>
        /// <param name="index">The index to validate. an exception will be thrown if it's not a valid index for this <c>FileSection</c></param>
        /// <exception cref="System.IndexOutOfRangeException">Thrown if the index is below zero or greater or equal to <see cref="this.Length"/></exception>
        protected void ValidateIndex(int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException("The index cannot be below zero (argument index was " + index + ")");
            if (index > this.Length)
                throw new IndexOutOfRangeException("The index cannot be equal to or greater than the length of the file section (argument index was " + index + ")");
        }

        /// <summary>
        /// Returns the index of the start of <paramref name="value"/> in this FileSection
        /// </summary>
        /// <param name="value">The value to look for</param>
        /// <param name="startIndex">The index to start the search in. Negative indexes possible</param>
        /// <param name="endIndex">The index to stop the search in. Negative indexes possible</param>
        /// <param name="ignoreCase">Whether the search will be case insensitive or not</param>
        /// <returns>The first index of the first occurance of <paramref name="value"/></returns>
        public int IndexOf(string value, int startIndex = 0, int endIndex = -1)
        {
            int re = gscope.IndexOf(value, startIndex + this.si, this.si + Math.Min(this.ResolveNegativeIndex(endIndex), this.Length - 1));
            if (re == -1)
                return -1;
            else
                return re - si;
        }

        /// <summary>
        /// Returns the index of any of <paramref name="values"/> in this FileSection
        /// </summary>
        /// <param name="values">The values to look for</param>
        /// <param name="startIndex">The index to start the search in. Negative indexes possible</param>
        /// <param name="endIndex">The index to stop the search in. Negative indexes possible</param>
        /// <param name="ignoreCase">Whether the search will be case insensitive or not</param>
        /// <returns>The index of the first occurance of any of <paramref name="values"/></returns>
        public int IndexOfAny(char[] values, int startIndex = 0, int endIndex = -1)
        {
            int result = gscope.IndexOfAny(values, startIndex + this.si, this.si + Math.Min(this.ResolveNegativeIndex(endIndex), this.Length - 1));
            return result != -1 ? result - si : -1;
        }

        /// <summary>
        /// Returns the first index of any of <paramref name="values"/> in this FileSection
        /// </summary>
        /// <param name="values">The value to look for</param>
        /// <param name="startIndex">The index to start the search in. Negative indexes possible</param>
        /// <param name="endIndex">The index to stop the search in. Negative indexes possible</param>
        /// <param name="ignoreCase">Whether the search will be case insensitive or not</param>
        /// <returns>The index of the first occurance of any of  <paramref name="values"/></returns>
        public int IndexOfAny(string[] values, int startIndex = 0, int endIndex = -1)
        {
            int result = gscope.IndexOfAny(values, startIndex + this.si, this.si + Math.Min(this.ResolveNegativeIndex(endIndex), this.Length - 1));
            return result != -1 ? result - si : -1;
        }

        /// <summary>
        /// Inserts a string value into a specified position of this FileSection
        /// </summary>
        /// <param name="index">The index to insert the <paramref name="value"/> in</param>
        /// <param name="value">The string value to insert</param>
        public void Insert(string value, int index = 0)
        {
            index = ResolveNegativeIndex(index);
            ValidateIndex(index);
            gscope.Insert(si + index, value);

            if (IndexChanged != null)
            {
                IndexChanged(this, new IndexChangedEventArgs(si + index, value.Length));
            }
        }

        /// <summary>
        /// Removes a specified number of characters from this FileSection in a specified position
        /// </summary>
        /// <param name="index"></param>
        /// <param name="length"></param>
        public void Remove(int index = 0, int index2 = -1)
        {
            index = ResolveNegativeIndex(index);
            ValidateIndex(index);
            index2 = ResolveNegativeIndex(index2);
            ValidateIndex(index2);
            gscope.Remove(si + index, index2 - index);

            if (IndexChanged != null)
            {
                IndexChanged(this, new IndexChangedEventArgs(si + index, -(index2 - index)));
            }
        }

        public IEnumerator<char> GetEnumerator()
        {
            for (int i = si; i <= ei; i++)
            {
                yield return gscope[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return ToString();
        }

        public string ToString(int index = 0, int index2 = -1)
        {
            index = ResolveNegativeIndex(index);
            index2 = ResolveNegativeIndex(index2);
            ValidateIndex(index);
            ValidateIndex(index2);
            return gscope.ToString(index + si, index2 - index + 1);
        }
    }

    /// <summary>
    /// A dummy FileSection, to serve as the parent of root FileSections.
    /// </summary>
    public class NullFileSection : FileSection
    {
        protected override int si
        {
            get
            {
                return 0;
            }
            set { }
        }
        protected override int ei
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new <c>NullFileSection</c> with a specified dummy length
        /// </summary>
        public NullFileSection(int length)
        {
            ei = length - 1;
        }
    }

}

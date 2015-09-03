using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Editor
{
    class FileSection : IEnumerable<char>
    {
        protected StringBuilder gscope;
        //start index
        protected virtual int si { get; protected set; }
        //end index
        protected virtual int ei { get; protected set; }

        public FileSection Parent { get; set; }

        public char this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Length)
                    throw new IndexOutOfRangeException("Tried to acces index " + index + ", which is out of this FileSection's bounds");
                return gscope[Parent.si + ResolveNegativeIndex(index)];
            }
            set { gscope[Parent.si + ResolveNegativeIndex(index)] = value; }
        }

        public virtual int Length { get { return ei - si + 1; } }

        public FileSection(string s, int startIndex, int endIndex) : this(new StringBuilder(s), startIndex, endIndex) { }

        public FileSection(StringBuilder sb, int startIndex = 0, int endIndex = -1)
        {
            Initialize(sb, startIndex, endIndex);
        }

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
            }
            else
            {
                Parent = new NullFileSection(gscope.Length);
                si = Util.ResolveNegativeIndex(startIndex, sb.Length);
                ei = Util.ResolveNegativeIndex(endIndex, sb.Length);
            }
        }

        protected FileSection();

        private int ResolveNegativeIndex(int index)
        {
            return Util.ResolveNegativeIndex(index, this.Length);
        }

        /// <summary>
        /// Checks the index is within bounds, and if not, throws an IndexOutOfRangeException
        /// <para>Note: this method does not resolve negative indexes. If these are accepted you should use <c>ResolveNegativeIndex</c> on <c>index</c> beforehand</para>
        /// </summary>
        /// <param name="index">The index to validate. an exception will be thrown if it's not a valid index for this <c>FileSection</c></param>
        /// <exception cref="System.IndexOutOfRangeException">Thrown if the index is below zero or greater or equal to <c>this.Length</c></exception>
        protected void ValidateIndex(int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException("The index cannot be below zero (argument index was " + index + ")");
            if (index > this.Length)
                throw new IndexOutOfRangeException("The index cannot be equal to or greater than the length of the file section (argument index was " + index + ")");
        }

        public int IndexOf(string value, int startIndex = 0, int endIndex = -1, bool ignoreCase = false)
        {
            return gscope.IndexOf(value, startIndex + this.si, this.si + Math.Min(this.ResolveNegativeIndex(endIndex), this.Length - 1), ignoreCase);
        }

        public int IndexOfAny(char[] values, int startIndex = 0, int endIndex = -1)
        {
            return gscope.IndexOfAny(values, startIndex + this.si, this.si + Math.Min(this.ResolveNegativeIndex(endIndex), this.Length - 1));
        }

        public void Insert(int index, string value)
        {
            index = ResolveNegativeIndex(index);
            ValidateIndex(index);
            gscope.Insert(si + index, value);
        }

        public void Remove(int index, int length)
        {
            index = ResolveNegativeIndex(index);
            ValidateIndex(index);
            gscope.Remove(si + index, length);
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

        public override virtual string ToString(int index, int index2)
        {
            index = ResolveNegativeIndex(index);
            index2 = ResolveNegativeIndex(index2);
            ValidateIndex(index);
            ValidateIndex(index2);
            return gscope.ToString(index, index2 - index);
        }
    }

    class NullFileSection : FileSection
    {
        protected override int si
        {
            get
            {
                return 0;
            }
            protected set { }
        }
        protected override int ei
        {
            get;
            protected set;
        }

        public NullFileSection(int length)
        {
            ei = length - 1;
        }
    }

}

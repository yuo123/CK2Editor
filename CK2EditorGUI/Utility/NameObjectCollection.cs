using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Specialized;

namespace CK2EditorGUI.Utility
{
    public class NameObjectCollection : NameObjectCollectionBase, IEnumerable<KeyValuePair<string, object>>
    {
        public NameObjectCollection() : base(StringComparer.Ordinal) { }
        public NameObjectCollection(int capacity) : base(capacity, StringComparer.Ordinal) { }
        public NameObjectCollection(IEqualityComparer comparer) : base(comparer) { }
        public NameObjectCollection(int capacity, IEqualityComparer comparer) : base(capacity, comparer) { }

        public new IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (string item in (IEnumerable)this)
            {
                yield return new KeyValuePair<string, object>(item, BaseGet(item));
            }
        }

        public object this[string key]
        {
            get
            {
                return BaseGet(key);
            }
            set
            {
                BaseSet(key, value);
            }
        }
        public object this[int key]
        {
            get
            {
                return BaseGet(key);
            }
            set
            {
                BaseSet(key, value);
            }
        }

        public void Add(string key, object value)
        {
            BaseAdd(key, value);
        }
        public void Add(KeyValuePair<string, object> pair)
        {
            BaseAdd(pair.Key, pair.Value);
        }

        public void Clear()
        {
            BaseClear();
        }

        public void Remove(string key)
        {
            BaseRemove(key);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }
    }
}

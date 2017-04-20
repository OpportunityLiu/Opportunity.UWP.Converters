using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.MathExpression
{
    class IdDictionary<TValue> : IDictionary<string, TValue>
    {
        private Dictionary<string, TValue> valueDic = new Dictionary<string, TValue>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, string> nameDic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public ICollection<string> Keys => this.valueDic.Keys;

        public ICollection<TValue> Values => this.valueDic.Values;

        public int Count => this.valueDic.Count;

        public bool IsReadOnly => false;

        public TValue this[string key]
        {
            get => this.valueDic[key];
            set
            {
                this.valueDic[key] = value;
                this.nameDic[key] = key;
            }
        }

        public string GetKey(string key)
        {
            return this.nameDic[key];
        }

        public void Add(string key, TValue value)
        {
            this.valueDic.Add(key, value);
            this.nameDic.Add(key, key);
        }

        public bool ContainsKey(string key) => this.valueDic.ContainsKey(key);

        public bool Remove(string key)
        {
            this.valueDic.Remove(key);
            return this.nameDic.Remove(key);
        }

        public bool TryGetValue(string key, out TValue value) => this.valueDic.TryGetValue(key, out value);

        void ICollection<KeyValuePair<string, TValue>>.Add(KeyValuePair<string, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            this.valueDic.Clear();
            this.nameDic.Clear();
        }

        bool ICollection<KeyValuePair<string, TValue>>.Contains(KeyValuePair<string, TValue> item)
            => ((ICollection<KeyValuePair<string, TValue>>)this.valueDic).Contains(item);

        void ICollection<KeyValuePair<string, TValue>>.CopyTo(KeyValuePair<string, TValue>[] array, int arrayIndex)
            => ((ICollection<KeyValuePair<string, TValue>>)this.valueDic).CopyTo(array, arrayIndex);

        bool ICollection<KeyValuePair<string, TValue>>.Remove(KeyValuePair<string, TValue> item)
        {
            var r = ((ICollection<KeyValuePair<string, TValue>>)this.valueDic).Remove(item);
            if(r)
                this.nameDic.Remove(item.Key);
            return r;
        }

        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator() => this.valueDic.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.valueDic.GetEnumerator();
    }
}

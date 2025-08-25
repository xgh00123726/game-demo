using System.Collections;
using System.Collections.Generic;

namespace GameBase.Tools
{
    public class IDContainer<T, T_Container> : IEnumerable<T>
        where T_Container : IDContainer<T, T_Container>, new()
    {
        private Dictionary<int, T> _items = new();
        protected static Dictionary<string, int> _nameIDDict = new();

        public static Dictionary<string, int> NameIDDict => _nameIDDict;

        static IDContainer()
        {
            var _instance = new T_Container();
            _instance.GetNameIDDict(out _nameIDDict);
        }

        protected virtual void GetNameIDDict(out Dictionary<string, int> dict)
        {
            dict = new Dictionary<string, int>();
        }

        public int Count => _items.Count;

        public bool ContainsKey(string key)
        {
            return _nameIDDict.ContainsKey(key);
        }

        public bool ContainsValueWith(int id)
        {
            return _items.ContainsKey(id);
        }

        public bool ContainsValueWith(string key)
        {
            if (!_nameIDDict.ContainsKey(key))
            {
                return false;
            }

            return _items.ContainsKey(_nameIDDict[key]);
        }

        public T this[int i]
        {
            get => _items[i];
            set => _items[i] = value;
        }

        public T this[string key]
        {
            get
            {
                if (!_nameIDDict.ContainsKey(key))
                {
                    XLogger.Instance.Log($"name id dict not contains key:{key}");
                    return default;
                }
                return _items[_nameIDDict[key]];
            }
            set => _items[_nameIDDict[key]] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_items.Values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_items.Values).GetEnumerator();
        }
    }
}

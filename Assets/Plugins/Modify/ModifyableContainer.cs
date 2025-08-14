using GameBase.Tools;
using NReco.Csv;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameBase.Modify
{
    public class ModifyableContainer<T> : IEnumerable<Modifyable<T>>
    {
        protected Dictionary<int, Modifyable<T>> _modifyables = new Dictionary<int, Modifyable<T>>();
        public int Count => _modifyables.Count;

        public static string ValidLabel => "-";

        public Modifyable<T> this[string s]
        {
            get => _modifyables[attrIDDict[s]];
            set => _modifyables[attrIDDict[s]] = value;
        }

        private static Dictionary<string, int> attrIDDict = new Dictionary<string, int>();

        public void AddModify(string key, Modifyer<T> modifyer)
        {
            if (!attrIDDict.ContainsKey(key))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log($"trying modify a unexist modifyable value which key is {key}");
                return;
            }

            _modifyables[attrIDDict[key]].AddModify(modifyer);
        }

        public void AddModify(int key, Modifyer<T> modifyer)
        {
            if (!_modifyables.ContainsKey(key))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log($"trying modify a unexist modifyable value which key is {key}");
                return;
            }

            _modifyables[key].AddModify(modifyer);
        }

        static ModifyableContainer()
        {
            StreamReader reader = File.OpenText($"{Application.streamingAssetsPath}/public/ModifyTable_{typeof(T).Name}.csv");
            CsvReader csvReader = new CsvReader(reader);

            while (csvReader.Read())
            {
                string label = csvReader[0];
                if (label != "-")
                {
                    continue;
                }

                attrIDDict[csvReader[2]] = int.Parse(csvReader[1]);
            }

            reader.Close();
        }

        public bool Contains(int id)
        {
            return _modifyables.ContainsKey(id);
        }

        public bool Contains(string id)
        {
            return _modifyables.ContainsKey(attrIDDict[id]);
        }

        public Modifyable<T> this[int i]
        {
            get => _modifyables[i];
            set => _modifyables[i] = value;
        }

        public IEnumerator<Modifyable<T>> GetEnumerator()
        {
            return ((IEnumerable<Modifyable<T>>)_modifyables).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_modifyables).GetEnumerator();
        }
    }
}

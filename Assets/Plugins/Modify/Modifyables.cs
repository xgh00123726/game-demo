using GameBase.Tools;
using NReco.Csv;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;

namespace GameBase.Modify
{
    public class ModifyTable
    {
        private static List<string> _modifyableNames;
        private static Dictionary<string, int> _modifyableIDs;

        static ModifyTable()
        {
            _modifyableNames = new();
            _modifyableIDs = new();

            StreamReader reader = File.OpenText($"{Application.streamingAssetsPath}/ConstructorData/ModifyContainer/ModifyTable_Single.csv");
            CsvReader csvReader = new CsvReader(reader);

            while (csvReader.Read())
            {
                string label = csvReader[0];
                if (label != "-")
                {
                    continue;
                }

                int id = int.Parse(csvReader[1]);
                string name = csvReader[2];

                _modifyableNames.Add(name);
                _modifyableIDs.Add(name, id);
            }

            reader.Close();
        }

        public static string GetName(int id) => _modifyableNames[id];
        public static int GetID(string name)
        {
            if (!_modifyableIDs.ContainsKey(name))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("invalid name");
            }

            return _modifyableIDs[name];
        }
    }

    public struct ModifyableGroup
    {
        public Modifyable set;
        public Modifyable setPer;
        public Modifyable sumPer;

        public Modifyable Set { get => set; set => set = value; }
        public Modifyable SetPer { get => setPer; set => setPer = value; }
        public Modifyable SumPer { get => sumPer; set => sumPer = value; }
    }

    public class Modifyables : IEnumerable<ModifyableGroup>
    {
        private Dictionary<int, ModifyableGroup> _modifyableGroups = new();

        public void Set(string key, float value)
        {
            int id = ModifyTable.GetID(key);
            Set(id, value);
        }

        public void Set(int id, float value)
        {
            if (_modifyableGroups.ContainsKey(id))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("duplicate key");
            }

            var set = ModifyableSys.Instance.NewEntity(value);
            var setPer = ModifyableSys.Instance.NewEntity();
            var sumPer = ModifyableSys.Instance.NewEntity();
            _modifyableGroups[id] = new ModifyableGroup()
            {
                set = set,
                setPer = setPer,
                sumPer = sumPer
            };
        }

        public void Clear()
        {
            foreach (var val in _modifyableGroups.Values)
            {
                ModifyableSys.Instance.InternalRemoveEntity(val.set);
                ModifyableSys.Instance.InternalRemoveEntity(val.setPer);
                ModifyableSys.Instance.InternalRemoveEntity(val.sumPer);
            }
            _modifyableGroups.Clear();
        }

        public int Count => _modifyableGroups.Count;

        public bool ContainsKey(string key)
        {
            int id = ModifyTable.GetID(key);
            return _modifyableGroups.ContainsKey(id);
        }

        public bool ContainsKey(int key)
        {
            return _modifyableGroups.ContainsKey(key);
        }

        public float this[int i]
        {
            get
            {
                var valueSet = _modifyableGroups[i].set.valueSet;
                var value = _modifyableGroups[i].set.value;
                var setPer = _modifyableGroups[i].setPer.value;
                var sumPer = _modifyableGroups[i].sumPer.value;

                return (value + valueSet * setPer) * (1 + sumPer);
            }
        }

        public float this[string key]
        {
            get => this[ModifyTable.GetID(key)];
        }

        public IEnumerator<ModifyableGroup> GetEnumerator()
        {
            return ((IEnumerable<ModifyableGroup>)_modifyableGroups.Values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_modifyableGroups.Values).GetEnumerator();
        }

        public void ModifySet(int key, Modifyer modifyer)
        {
            if (!ContainsKey(key))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log($"trying modify a unexist modifyable value which key is {key}");
            }

            _modifyableGroups[key].set.AddModify(modifyer);
        }

        public void ModifySet(string key, Modifyer modifyer)
        {
            ModifySet(ModifyTable.GetID(key), modifyer);
        }

        public void ModifySetPer(int key, Modifyer modifyer)
        {
            if (!ContainsKey(key))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log($"trying modify a unexist modifyable value which key is {key}");
            }

            _modifyableGroups[key].setPer.AddModify(modifyer);
        }

        public void ModifySetPer(string key, Modifyer modifyer)
        {
            ModifySetPer(ModifyTable.GetID(key), modifyer);
        }

        public void ModifySumPer(int key, Modifyer modifyer)
        {
            if (!ContainsKey(key))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log($"trying modify a unexist modifyable value which key is {key}");
            }

            _modifyableGroups[key].sumPer.AddModify(modifyer);
        }

        public void ModifySumPer(string key, Modifyer modifyer)
        {
            ModifySumPer(ModifyTable.GetID(key), modifyer);
        }
    }
}

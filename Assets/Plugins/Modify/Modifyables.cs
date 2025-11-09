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
        private static List<string> _modifyableNames = new();
        private static List<string> _modifyableTextureNames = new();
        private static Dictionary<string, int> _modifyableIDs = new();

        static ModifyTable()
        {
            Init();
        }

        private static void Init()
        {
            for (int i = 0; i < ModifyableDataBase.Instance.Size; i++)
            {
                var data = ModifyableDataBase.Instance[i];
                _modifyableNames.Add(data.name);
                _modifyableIDs.Add(data.name, i);
                _modifyableTextureNames.Add(data.textureName);
            }
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

        public static string GetTextureName(int id) => _modifyableTextureNames[id];
    }

    public class Modifyables : IEnumerable<Modifyable>
    {
        private Dictionary<int, Modifyable> _modifyables = new();

        public void Set(string key, float value)
        {
            int id = ModifyTable.GetID(key);
            Set(id, value);
        }

        public void Set(int id, float value)
        {
            if (_modifyables.ContainsKey(id))
            {
                _modifyables[id].value = value;
                _modifyables[id].valueSet = value;
            }
            else
            {
                var set = ModifyableSys.Instance.NewEntity(value);
                _modifyables.Add(id, set);
            }
        }

        public void Modify(int id, float value)
        {
            if (ContainsKey(id))
            {
                var m = ModifyerSys.Instance.NewEntity();
                m.value = value;
                m.AddTo(_modifyables[id]);
            }
        }

        public void ModifyKey(string key, float value)
        {
            Modify(ModifyTable.GetID(key), value);
        }

        public void Modify(string key, float value)
        {
            Modify(ModifyTable.GetID(key), value);
        }

        public void ForceModify(int id, float value)
        {
            if (ContainsKey(id))
            {
                _modifyables[id].valueSet += value;
            }
        }


        public bool ContainsKey(string key)
        {
            int id = ModifyTable.GetID(key);
            return _modifyables.ContainsKey(id);
        }

        public bool ContainsKey(int key)
        {
            return _modifyables.ContainsKey(key);
        }

        public Modifyable this[int i]
        {
            get => _modifyables[i];
            set => _modifyables[i] = value;
        }

        public Modifyable this[string key]
        {
            get => this[ModifyTable.GetID(key)];
            set => this[ModifyTable.GetID(key)] = value;
        }

        public float GetKeyValue(string key)
        {
            return this[key].value;
        }

        public IEnumerator<Modifyable> GetEnumerator()
        {
            return ((IEnumerable<Modifyable>)_modifyables.Values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_modifyables.Values).GetEnumerator();
        }
    }
}

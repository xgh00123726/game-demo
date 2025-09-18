using GameBase.Modify;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameBase.Buffs
{
    public class BuffModifyers
    {
        private Dictionary<int, Modifyer> _fixedModifyers = new();
        private Dictionary<int, Modifyer> _currModifyers = new();
        private Dictionary<int, Modifyer> _setModifyers = new();

        public Dictionary<int, Modifyer> FixedModifyers => _fixedModifyers;
        public Dictionary<int, Modifyer> CurrModifyers => _currModifyers;
        public Dictionary<int, Modifyer> SetModifyers => _setModifyers;

        public void AddFixed(int key, Modifyer modifyer)
        {
            _fixedModifyers.Add(key, modifyer);
        }

        public void AddFixed(string key, Modifyer modifyer)
        {
            AddFixed(ModifyTable.GetID(key), modifyer);
        }

        public void AddCurr(int key, Modifyer modifyer)
        {
            _currModifyers.Add(key, modifyer);
        }

        public void AddCurr(string key, Modifyer modifyer)
        {
            _currModifyers.Add(ModifyTable.GetID(key), modifyer);
        }

        public void AddSet(int key, Modifyer modifyer)
        {
            _setModifyers.Add(key, modifyer);
        }
        public void AddSet(string key, Modifyer modifyer)
        {
            _setModifyers.Add(ModifyTable.GetID(key), modifyer);
        }

        public void Clear()
        {
            _fixedModifyers.Clear();
            _currModifyers.Clear();
            _setModifyers.Clear();
        }
    }
}

using GameBase.Modify;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameBase.Buffs
{
    public class BuffModifyers
    {
        private Dictionary<int, Modifyer<float>> _fixedModifyers = new();
        private Dictionary<int, Modifyer<float>> _currModifyers = new();
        private Dictionary<int, Modifyer<float>> _setModifyers = new();

        public Dictionary<int, Modifyer<float>> FixedModifyers => _fixedModifyers;
        public Dictionary<int, Modifyer<float>> CurrModifyers => _currModifyers;
        public Dictionary<int, Modifyer<float>> SetModifyers => _setModifyers;

        public void AddFixed(int key, Modifyer<float> modifyer)
        {
            _fixedModifyers.Add(key, modifyer);
        }

        public void AddFixed(string key, Modifyer<float> modifyer)
        {
            AddFixed(ModifyableContainer<float>.GetIDOfKey(key), modifyer);
        }

        public void AddCurr(int key, Modifyer<float> modifyer)
        {
            _currModifyers.Add(key, modifyer);
        }

        public void AddCurr(string key, Modifyer<float> modifyer)
        {
            _currModifyers.Add(ModifyableContainer<float>.GetIDOfKey(key), modifyer);
        }

        public void AddSet(int key, Modifyer<float> modifyer)
        {
            _setModifyers.Add(key, modifyer);
        }
        public void AddSet(string key, Modifyer<float> modifyer)
        {
            _setModifyers.Add(ModifyableContainer<float>.GetIDOfKey(key), modifyer);
        }

        public void Clear()
        {
            _fixedModifyers.Clear();
            _currModifyers.Clear();
            _setModifyers.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using GameBase.Modify;

namespace GameBase.Buff
{
    internal class BuffContainer
    {
        internal BuffContainer() { }
        private Dictionary<IModifyable, Dictionary<Type, GBuff>> _objectBuffs = new Dictionary<IModifyable, Dictionary<Type, GBuff>> { };
        internal Dictionary<IModifyable, Dictionary<Type, GBuff>> ObjectBuffs => _objectBuffs;

        internal bool HasBuff(IModifyable o, GBuff buff)
        {
            return _objectBuffs.ContainsKey(o) && _objectBuffs[o].ContainsKey(buff.GetType());
        }

        internal void Add(IModifyable o, GBuff buff)
        {
            if (!_objectBuffs.ContainsKey(o))
            {
                _objectBuffs[o] = new Dictionary<Type, GBuff> { { buff.GetType(), buff } };
                return;
            }
            _objectBuffs[o][buff.GetType()] = buff;
        }

        internal void Remove(GBuff buff)
        {
            _objectBuffs[buff.Target].Remove(buff.GetType());
        }
    }
}

using System.Collections.Generic;
using GameBase.Modify;
using UnityEngine;

namespace GameBase.Buff
{
    public class BuffMgr : MonoBehaviour
    {
        internal static BuffContainer _container = new BuffContainer();
        internal static List<GBuff> _removeList = new List<GBuff>();
        #region debug
        static BuffMgr _instance;
        public int buffCount = 0;
        public int buffAdded = 0;
        public int buffRemoved = 0;
        #endregion

        public static bool HasBuff(IModifyable o, GBuff buff)
        {
            return _container.HasBuff(o, buff);
        }

        internal static void Add(IModifyable o, GBuff buff)
        {
            _container.Add(o, buff);
            _instance.buffCount++;
            _instance.buffAdded++;
        }

        internal static void Remove(GBuff buff)
        {
            _removeList.Add(buff);
            _instance.buffRemoved++;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }

        private void Update()
        {
            foreach (var objectBuff in _container.ObjectBuffs.Values)
            {
                foreach (var buff in objectBuff.Values)
                {
                    buff.durationRemain = Mathf.Max(0f, buff.durationRemain - Time.deltaTime);
                    if (buff.durationRemain <= 0)
                    {
                        buff.Clear();
                    }
                }
            }
            foreach (var buff in _removeList)
            {
                _container.Remove(buff);
            }
            _instance.buffCount -= _removeList.Count;
            _removeList.Clear();
        }
    }
}

using GameBase.EntitySystem;
using GameBase.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.AI
{
    public class AISys : Singleton<AISys>, IBaseSys
    {
        internal LinkedList<BaseAI> AIList = new();
        internal LinkedList<BaseAI> AINeedAdd = new();
        internal LinkedList<BaseAI> AINeedRemove = new();
        internal bool _inUpdate = false;

        internal Dictionary<System.Type, BaseObjectPool<BaseAI>> pools = new();

        public AISys()
        {
            SingletonEntitySysInstance.CreateShadowMono(this);
        }

        public T NewEntity<T>(Action<T> Init = null) where T : BaseAI, new()
        {
            var type = typeof(T);
            if (!pools.ContainsKey(type))
            {
                var pool = new BaseObjectPool<BaseAI>();
                pool.InstantiateFunc = static () => new T();
                pools.Add(type, pool);
            }

            var ret = pools[type].Get() as T;

            Init?.Invoke(ret);
            AINeedAdd.AddLast(ret);

            return ret;
        }

        public void RemoveEntity(BaseAI ai)
        {
            ai.enable = false;
            if (_inUpdate)
            {
                AINeedRemove.AddLast(ai);
            }
            else
            {
                AIList.Remove(ai);
                pools[ai.GetType()].Release(ai);
            }
        }

        void IBaseSys.FixedUpdate()
        {
            foreach (var ai in AINeedAdd)
            {
                AIList.AddLast(ai);
            }
            AINeedAdd.Clear();
            foreach (var ai in AINeedRemove)
            {
                AIList.Remove(ai);
                pools[ai.GetType()].Release(ai);
            }
            AINeedRemove.Clear();

            _inUpdate = true;
            foreach (var ai in AIList)
            {
                if (Time.time > ai.enableRecoverTime)
                {
                    ai.enable = true;
                }
                if (ai.enable)
                {
                    ai.Update();
                }
                if (ai.owner == null)
                {
                    XLogger.Instance.Level(XLogger.LogLevel.Warning)
                        .Log("ai has null owner");
                    RemoveEntity(ai);
                    continue;
                }
            }
            _inUpdate = false;
        }

        int IBaseSys.GetActiveCount()
        {
            return AIList.Count;
        }

        int IBaseSys.GetEntityCount()
        {
            return AIList.Count;
        }

        int IBaseSys.GetReleasedCount()
        {
            return 0;
        }

        void IBaseSys.Update()
        {

        }
    }
}

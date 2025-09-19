using GameBase.EntitySystem;
using GameBase.Tools;
using System;
using System.Collections.Generic;

namespace GameBase.AI
{
    public class AISys : Singleton<AISys>, IBaseSys
    {
        internal LinkedList<Action> actions = new();

        internal Dictionary<Type, BaseObjectPool<BaseAI>> pools = new();

        public AISys()
        {
            ShadowMono.CreateShadowMono(this);
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

            return ret;
        }


        int IBaseSys.GetActiveCount()
        {
            return actions.Count;
        }

        int IBaseSys.GetEntityCount()
        {
            return actions.Count;
        }

        int IBaseSys.GetReleasedCount()
        {
            return 0;
        }

        void IBaseSys.Update()
        {
            foreach (var action in actions)
            {
                action?.Invoke();
            }
        }
    }
}

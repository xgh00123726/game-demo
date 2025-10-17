using GameBase.Tools;
using System.Collections.Generic;

namespace GameBase.EntitySystem
{
    public abstract class KeyEntitySys<T_Key, T_Entity, T_Sys> : Singleton<T_Sys>, IBaseSys
        where T_Entity : class, IKeyEntity<T_Key>
        where T_Sys : KeyEntitySys<T_Key, T_Entity, T_Sys>, new()
    {
        private bool _useObjectPool = true;
        private bool _useDefaultContainer = true;
        protected bool _fixedUpdate = false;

        protected EntitySys<T_Entity> _sys;
        protected Dictionary<T_Key, BaseObjectPool<T_Entity>> _pools;

        protected KeyEntitySys()
        {
            SingletonEntitySysInstance.CreateShadowMono(this);

            if (_useObjectPool)
            {
                _pools = new();
            }
            if (_useDefaultContainer)
            {
                _sys = new EntitySys<T_Entity>(new LinkListContainer<T_Entity>())
                {
                    _UpdateAction = UpdateEntity,
                    _StartAction = EntityStart,
                };
            }
        }

        protected virtual T_Entity CtorT(T_Key k) { return default; }
        protected virtual void OnGet(T_Entity e) { }
        protected virtual void OnRelease(T_Entity e) { }

        private T_Entity GetEntityFromPool(T_Key k)
        {
            if (!_pools.ContainsKey(k))
            {
                var pool = new BaseObjectPool<T_Entity>();
                _pools[k] = pool;
                pool.InstantiateFunc = () =>
                {
                    var e = CtorT(k);
                    e.Key = k;
                    return e;
                };
                pool.InstantiateAction = OnGet;
                pool.ReleaseAction = OnRelease;
            }

            return _pools[k].Get();
        }
        protected virtual T_Entity GetEntity(T_Key k) { return default; }

        private void ReleaseEntityToPool(T_Entity e)
        {
            if (_pools.ContainsKey(e.Key))
            {
                var pool = _pools[e.Key];
                pool.Release(e);
            }
        }

        protected virtual void ReleaseEntity(T_Entity e) { }

        public T_Entity NewEntity(T_Key k)
        {
            T_Entity ret;
            if (_useObjectPool)
            {
                ret = GetEntityFromPool(k);
            }
            else
            {
                ret = GetEntity(k);
            }

            _sys.AddEntity(ret);
            return ret;
        }

        /// <summary>
        /// 将实体标记为删除
        /// </summary>
        public void RemoveEntity(T_Entity e)
        {
            if (e == null) return;

            if (_useObjectPool)
            {
                ReleaseEntityToPool(e);
            }
            else
            {
                ReleaseEntity(e);
            }

            _sys.RemoveEntity(e);
        }


        /// <summary>
        /// 当实体活跃时调用
        /// <list type="bullet">
        /// <item><param name="e"><paramref name="e"/>:实体引用</param></item>
        /// </list></summary>
        protected virtual void UpdateEntity(T_Entity e) { }
        protected virtual void EntityStart(T_Entity e) { }
        protected int CurrentIterateIndex => _sys.CurrentIterIndex;

        public virtual IEContainer<T_Entity> Entities => _sys.Entities;

        protected virtual void Update() => _sys.Iterate();

        void IBaseSys.Update()
        {
            if (_fixedUpdate)
            {
                return;
            }

            Update();
        }

        void IBaseSys.FixedUpdate()
        {
            if (!_fixedUpdate)
            {
                return;
            }

            Update();
        }

        int IBaseSys.GetEntityCount()
        {
            return Entities.Count;
        }

        int IBaseSys.GetReleasedCount()
        {
            var sum = 0;
            foreach (var c in _pools.Values)
            {
                sum += c.Count;
            }
            return sum;
        }

        int IBaseSys.GetActiveCount()
        {
            return 0;
        }
    }
}


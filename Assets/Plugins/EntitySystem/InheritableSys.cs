using GameBase.Tools;
using System;
using System.Collections.Generic;

namespace GameBase.EntitySystem
{
    public abstract class InheritableSys<T_Entity, T_Sys> : Singleton<T_Sys>, IBaseSys
        where T_Entity : class
        where T_Sys : InheritableSys<T_Entity, T_Sys>, new()
    {
        private bool _useObjectPool = true;
        private bool _useDefaultContainer = true;
        protected bool _fixedUpdate = false;

        protected EntitySys<T_Entity> _sys;
        protected Dictionary<Type, BaseObjectPool<T_Entity>> _pools;

        protected InheritableSys()
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

        protected virtual T CtorT<T>() where T : T_Entity, new() { return new T(); }
        protected virtual void OnGet(T_Entity e) { }
        protected virtual void OnRelease(T_Entity e) { }

        private T GetEntityFromPool<T>() where T : class, T_Entity, new()
        {
            if (!_pools.ContainsKey(typeof(T)))
            {
                var pool = new BaseObjectPool<T_Entity>();
                _pools[typeof(T)] = pool;
                pool.InstantiateFunc = CtorT<T>;
                pool.InstantiateAction = OnGet;
                pool.ReleaseAction = OnRelease;
            }

            return _pools[typeof(T)].Get() as T;
        }
        protected virtual T GetEntity<T>() where T : class, T_Entity { return default; }

        private void ReleaseEntityToPool<T>(T e) where T : class, T_Entity
        {
            if (_pools.ContainsKey(typeof(T)))
            {
                var pool = _pools[typeof(T)];
                pool.Release(e);
            }
        }

        protected virtual void ReleaseEntity<T>(T e) where T : class, T_Entity { }

        public T NewEntity<T>() where T : class, T_Entity, new()
        {
            T ret;
            if (_useObjectPool)
            {
                ret = GetEntityFromPool<T>();
            }
            else
            {
                ret = GetEntity<T>();
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


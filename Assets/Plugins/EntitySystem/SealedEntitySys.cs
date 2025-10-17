using GameBase.Tools;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameBase.EntitySystem
{
    /// <summary>
    /// 基本的实体系统
    /// <list type="bullet">
    /// <item><typeparam name="T_Entity"><typeparamref name="T_Entity"/>:实体类型</typeparam></item>
    /// </list></summary>
    public abstract class SealedEntitySys<T_Entity, T_Sys> : Singleton<T_Sys>, IBaseSys
        where T_Entity : class, new()
        where T_Sys : SealedEntitySys<T_Entity, T_Sys>, new()
    {
        private bool _useObjectPool = true;
        private bool _useDefaultContainer = true;
        protected bool _fixedUpdate = false;

        protected EntitySys<T_Entity> _sys;
        protected BaseObjectPool<T_Entity> _pool;

        protected SealedEntitySys()
        {
            SingletonEntitySysInstance.CreateShadowMono(this);

            if (_useObjectPool)
            {
                _pool = new()
                {
                    InstantiateFunc = static () => new T_Entity(),
                    InstantiateAction = OnGet,
                    ReleaseAction = OnRelease,
                };
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

        protected virtual T_Entity CtorT() { return new(); }
        protected virtual void OnGet(T_Entity e) { }
        protected virtual void OnRelease(T_Entity e) { }

        private T_Entity GetEntityFromPool()
        {
            return _pool.Get();
        }
        protected virtual T_Entity GetEntity() { return default; }

        private void ReleaseEntityToPool(T_Entity e)
        {
            _pool.Release(e);
        }

        protected virtual void ReleaseEntity(T_Entity e) { }

        public T_Entity NewEntity()
        {
            T_Entity ret;
            if (_useObjectPool)
            {
                ret = GetEntityFromPool();
            }
            else
            {
                ret = GetEntity();
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
            return _pool.Count;
        }

        int IBaseSys.GetActiveCount()
        {
            return 0;
        }
    }
}

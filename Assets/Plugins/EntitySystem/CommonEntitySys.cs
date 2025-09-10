using System;
using System.Collections.Generic;
using UnityEngine;
using GameBase.Tools;

namespace GameBase.EntitySystem
{
    /// <summary>
    /// 基本的实体系统
    /// <list type="bullet">
    /// <item><typeparam name="T_Entity"><typeparamref name="T_Entity"/>:实体类型</typeparam></item>
    /// </list></summary>
    public abstract class CommonEntitySys<T_Entity, T_Instance> : Signleton<T_Instance>, IBaseSys
        where T_Entity : class, IEntity, new()
        where T_Instance : CommonEntitySys<T_Entity, T_Instance>, new()
    {
        public int sysID = 0;
        public int entityCount = 0;
        public IEnumerable<T_Entity> Entities => _entities;
        public IEConstructor<T_Entity> Constructor
        {
            set => _entityConstructor = value;
            get => _entityConstructor;
        }

        protected internal LinkedList<T_Entity> _entityNeedRegister = new LinkedList<T_Entity>();
        protected internal LinkedList<T_Entity> _entitiesNeedRemove = new LinkedList<T_Entity>();
        protected internal IEConstructor<T_Entity> _entityConstructor = new CommonConstructor<T_Entity>();
        protected internal LinkedList<T_Entity> _entities = new LinkedList<T_Entity>();

        protected internal int tick = 0;
        protected internal int fixedTick = 0;
        protected internal virtual float FixedFreq => 60f;
        private float _updateTimeAccumulate = 0;

        protected CommonEntitySys()
        {
            ShadowMono.CreateShadowMono(this);
        }

        /// <summary>
        /// 将实体标记为删除
        /// </summary>
        protected void RemoveEntity(T_Entity e)
        {
            if (e == null) return;

            if (_entitiesNeedRemove.Contains(e))
            {
                return;
            }

            _entitiesNeedRemove.AddLast(e);
        }

        protected void AddToNeedRegisterImmediately(T_Entity e)
        {
            if (e == null) return;

            _entityNeedRegister.AddLast(e);
        }

        /// <summary>
        /// 当实体活跃时调用
        /// <list type="bullet">
        /// <item><param name="e"><paramref name="e"/>:实体引用</param></item>
        /// </list></summary>
        protected abstract void UpdateEntity(T_Entity e);

        protected virtual void FixedUpdateEntity(T_Entity e) { }

        /// <summary>
        /// new实体时调用
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRegisterEntityToActives(T_Entity e) { }

        /// <summary>
        /// release实体时调用
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRemoveEntityFromActives(T_Entity e) { }

        public int Tick => tick;
        public int FixedTick => fixedTick;


        public T_Entity NewFromPool()
        {
            return _entityConstructor.GetEntity();
        }

        public void RegisterEntity(T_Entity e)
        {
            e.InstanceID = PoolInfo.allocatedID++;
            AddToNeedRegisterImmediately(e);
            OnRegisterEntityToActives(e);
        }


        /// <summary>
        /// 立刻创建一个对象，可以在对象被遍历时使用，会在立刻将对象的unity对象创建出来
        /// </summary>
        /// <typeparam name="T_EntityType"></typeparam>
        /// <returns></returns>
        public T_Entity NewEntity(Action<T_Entity> Init = null)
        {
            var e = NewFromPool();
            Init?.Invoke(e);
            RegisterEntity(e);
            return e;
        }

        internal protected virtual void Awake()
        {
            PoolInfo.entitySysNum++;
            sysID = PoolInfo.entitySysNum;
            Tools.XLogger.Instance.Color(Color.green).IF(false).
                Log($"entity sys: {this.GetType().Name} has awaken, entitySys id: {sysID}, instance hash:{GetHashCode()}");
        }

        private void SysUpdate()
        {
            foreach (T_Entity e in _entityNeedRegister)
            {
                _entities.AddLast(e);
            }
            _entityNeedRegister.Clear();

            foreach (var e in _entities)
            {
                UpdateEntity(e);
            }

            foreach (var e in _entitiesNeedRemove)
            {
                OnRemoveEntityFromActives(e);
                _entities.Remove(e);
                _entityConstructor.ReleaseEntity(e);
            }
            _entitiesNeedRemove.Clear();
        }

        /// <summary>
        /// 不推荐重写Update
        /// </summary>
        internal protected virtual void Update()
        {
            SysUpdate();
            tick++;
            float fixedPeriod = 1 / FixedFreq;
            _updateTimeAccumulate += Time.deltaTime;
            while (_updateTimeAccumulate > fixedPeriod)
            {
                foreach (var e in _entities)
                {
                    FixedUpdateEntity(e);
                }
                _updateTimeAccumulate -= fixedPeriod;
                fixedTick++;
            }
        }

        void IBaseSys.Awake()
        {
            Awake();
        }

        void IBaseSys.Update()
        {
            Update();
        }

        int IBaseSys.GetEntityCount()
        {
            return _entities.Count;
        }

        int IBaseSys.GetReleasedCount()
        {
            return _entityConstructor.Count;
        }

        int IBaseSys.GetActiveCount()
        {
            return 0;
        }
    }
}

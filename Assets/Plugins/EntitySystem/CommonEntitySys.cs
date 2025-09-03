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
    public abstract class CommonEntitySys<T_Entity, T_Instance> : IBaseSys
        where T_Entity : class, IEntity, new()
        where T_Instance : CommonEntitySys<T_Entity, T_Instance>, new()
    {
        public int sysID = 0;
        public int entityCount = 0;
        public int entityNewTimes = 0;
        public IEnumerable<T_Entity> Entities => _entities;
        public IEContainer<T_Entity> Container
        {
            set => _entityContainer = value;
            get => _entityContainer;
        }

        protected internal LinkedList<T_Entity> _entityNeedRegister = new LinkedList<T_Entity>();
        protected internal LinkedList<T_Entity> _entitiesNeedRemove = new LinkedList<T_Entity>();
        protected internal IEContainer<T_Entity> _entityContainer = new CommonEntityContainer<T_Entity>();
        protected internal LinkedList<T_Entity> _entities = new LinkedList<T_Entity>();
        protected internal Dictionary<int, Delegate> entityGenerateDelegates = new();

        protected internal int tick = 0;
        protected internal int fixedTick = 0;
        protected internal virtual float FixedFreq => 60f;
        private float _updateTimeAccumulate = 0;

        internal static T_Instance instance;

        public static T_Instance Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T_Instance();
                    ShadowMono.CreateShadowMono(instance);
                }

                return instance;
            }
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

        protected void RegisterImmediately(T_Entity e)
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

        protected virtual void BeforeFirstUpdate(T_Entity e) { }

        public int Tick => tick;
        public int FixedTick => fixedTick;

        /// <summary>
        /// 立刻创建一个对象，可以在对象被遍历时使用，会在立刻将对象的unity对象创建出来
        /// </summary>
        /// <typeparam name="T_EntityType"></typeparam>
        /// <returns></returns>
        public T_Entity NewEntity(Action<T_Entity> Init = null)
        {
            ++entityNewTimes;

            var e = _entityContainer.GetEntity();
            e.InstanceID = PoolInfo.allocatedID++;
            Init?.Invoke(e);
            RegisterImmediately(e);
            OnRegisterEntityToActives(e);
            return e;
        }

        internal protected virtual void Awake()
        {
            PoolInfo.entitySysNum++;
            sysID = PoolInfo.entitySysNum;
            Tools.XLogger.Instance.Color(Color.green).IF(false).
                Log($"entity sys: {this.GetType().Name} has awaken, entitySys id: {sysID}, instance hash:{instance.GetHashCode()}");
        }

        private void SysUpdate()
        {
            foreach (T_Entity e in _entityNeedRegister)
            {
                _entities.AddLast(e);
                BeforeFirstUpdate(e);
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
                _entityContainer.ReleaseEntity(e);
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
            return _entityContainer.Count;
        }

        int IBaseSys.GetActiveCount()
        {
            return 0;
        }
    }
}

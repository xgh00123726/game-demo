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
    public abstract class SimplestEntitySys<T_Entity, T_Container, T_Instance> : IBaseSys
        where T_Entity : class, IEntity, new()
        where T_Container : IEContainer, new()
        where T_Instance : SimplestEntitySys<T_Entity, T_Container, T_Instance>, new()
    {
        public int sysID = 0;
        public int entityCount = 0;
        public int entityNewTimes = 0;
        public IEnumerable<T_Entity> Entities => _entities;

        protected int tick = 0;
        protected int fixedTick = 0;
        protected virtual float FixedFreq => 60f;
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

        protected LinkedList<T_Entity> _entityNeedRegister = new LinkedList<T_Entity>();
        protected LinkedList<T_Entity> _entitiesNeedRemove = new LinkedList<T_Entity>();
        protected T_Container _entityContainer = new T_Container();
        protected LinkedList<T_Entity> _entities = new LinkedList<T_Entity>();
        protected Dictionary<int, Delegate> entityGenerateDelegates = new();
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

        protected virtual void OnRegisterEntityToActives(T_Entity e) { }

        protected virtual void OnRemoveEntityFromActives(T_Entity e) { }

        protected virtual void BeforeFirstUpdate(T_Entity e) { }

        public int Tick => tick;
        public int FixedTick => fixedTick;

        /// <summary>
        /// 立刻创建一个对象，可以在对象被遍历时使用，会在立刻将对象的unity对象创建出来
        /// </summary>
        /// <typeparam name="T_EntityType"></typeparam>
        /// <returns></returns>
        public T_EntityType NewEntity<T_EntityType>(Action<T_EntityType> Init = null) where T_EntityType : class, T_Entity, new()
        {
            ++entityNewTimes;

            _entityContainer.RegisterType<T_EntityType>();

            var e = _entityContainer.GetEntity<T_EntityType>();
            e.InstanceID = PoolInfo.allocatedID++;
            Init?.Invoke(e);
            RegisterImmediately(e);
            OnRegisterEntityToActives(e);
            return e;
        }


        /// <summary>
        /// 使用生成器id创建实体
        /// </summary>
        /// <typeparam name="T_EntityType"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_EntityType NewEntity<T_EntityType>(int id) where T_EntityType : class, T_Entity, new()
        {
            if (!entityGenerateDelegates.ContainsKey(id))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid entity generator id:{id}");
                return null;
            }
            if (entityGenerateDelegates[id] == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("null entity generator");
                return null;
            }

            if (entityGenerateDelegates[id] is Func<T_EntityType> func)
            {
                _entityContainer.RegisterType<T_EntityType>();

                var e = func();
                return e;
            }
            else
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("entity generator get error type");
                return default;
            }
        }

        /// <summary>
        /// 将快速实体生成器注册进系统
        /// </summary>
        /// <param name="eGen"></param>
        /// <returns>快速实体生成器的id</returns>
        public void RegisterEntityGenerateDeletate<T_entityType>(int id, Func<T_entityType> eGen) where T_entityType : T_Entity
        {
            if (entityGenerateDelegates.ContainsKey(id))
            {
                XLogger.Instance.Log($"register duplulicate generator id:{id}");
            }
            entityGenerateDelegates.Add(id, eGen);
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

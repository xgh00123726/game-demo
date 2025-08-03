using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tools
{
    /// <summary>
    /// 基本的实体系统
    /// <list type="bullet">
    /// <item><typeparam name="T_entity"><typeparamref name="T_entity"/>:实体类型</typeparam></item>
    /// </list></summary>
    public abstract class SimplestEntitySys<T_entity, T_container> : MonoBehaviour
        where T_entity : IEntity, new()
        where T_container : IEntityContainer<T_entity>, IEnumerable<T_entity>, new()
    {
        public int sysID = 0;
        public int entityCount = 0;
        public int allocatedID = 0;

        protected int tick = 0;
        protected int fixedTick = 0;
        protected virtual float FixedFreq => 60f;
        private float _updateTimeAccumulate = 0;
        private float _lastUpdateTime;
        private static SimplestEntitySys<T_entity, T_container> _instance;
        public static SimplestEntitySys<T_entity, T_container> Instance => _instance;

        protected LinkedList<T_entity> _entityNeedRegister = new LinkedList<T_entity>();
        protected LinkedList<T_entity> _entitiesNeedRemove = new LinkedList<T_entity>();
        protected T_container _activeEntities = new T_container();
        protected List<Delegate> entityGenerateDelegates = new List<Delegate>();
        /// <summary>
        /// 将实体标记为删除
        /// </summary>
        protected void RemoveEntity(T_entity e)
        {
            if (e == null) return;

            if (_entitiesNeedRemove.Contains(e))
            {
                return;
            }

            _entitiesNeedRemove.AddLast(e);
        }

        /// <summary>
        /// 注册实体，在下个周期前实例化实体
        /// </summary>
        /// <param name="e"></param>
        protected void Register(T_entity e)
        {
            if (e == null) return;

            _entityNeedRegister.AddLast(e);
        }

        /// <summary>
        /// 当实体活跃时调用
        /// <list type="bullet">
        /// <item><param name="e"><paramref name="e"/>:实体引用</param></item>
        /// </list></summary>
        protected abstract void UpdateEntity(T_entity e);

        protected virtual void FixedUpdateEntity(T_entity e) { }

        protected abstract void OnRegisterEntity(T_entity e);

        protected abstract void OnRemoveEntity(T_entity e);

        public int Tick => tick;
        public int FixedTick => fixedTick;

        public T_entityType NewEntity<T_entityType>() where T_entityType : T_entity, new()
        {
            var e = new T_entityType();
            e.ID = allocatedID++;
            Register(e);
            return e;
        }

        public T_entityType NewEntity<T_entityType>(T_entityType e) where T_entityType : T_entity
        {
            e.ID = allocatedID++;
            Register(e);
            return e;
        }

        /// <summary>
        /// 使用生成器id创建实体
        /// </summary>
        /// <typeparam name="T_entityType"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_entityType NewEntity<T_entityType>(int id) where T_entityType : T_entity
        {
            if (id >= entityGenerateDelegates.Count || id < 0)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("invalid entity generator id");
            }
            if (entityGenerateDelegates[id] == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("null entity generator generator");
            }
            var func = entityGenerateDelegates[id] as Func<T_entityType>;
            var e = func();
            e.ID = allocatedID++;
            Register(e);
            return e;
        }

        /// <summary>
        /// 将快速实体生成器注册进系统
        /// </summary>
        /// <param name="eGen"></param>
        /// <returns>快速实体生成器的id</returns>
        public int RegisterEntityGenerateDeletate<T_entityType>(Func<T_entityType> eGen) where T_entityType : T_entity
        {
            entityGenerateDelegates.Add(eGen);
            return entityGenerateDelegates.Count - 1;
        }

        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;

            StaticInfo.entitySysNum++;
            sysID = StaticInfo.entitySysNum;
            Tools.XLogger.Instance.Color(Color.green).
                Log($"entity sys: {this.GetType().Name} has awaken, entitySys id: {sysID}, instance hash:{_instance.GetHashCode()}");
        }

        private void SysUpdate()
        {
            foreach (T_entity e in _entityNeedRegister)
            {
                _activeEntities.Add(e);
                OnRegisterEntity(e);
            }
            _entityNeedRegister.Clear();

            foreach (var e in _activeEntities)
            {
                UpdateEntity(e);
            }

            foreach (var e in _entitiesNeedRemove)
            {
                OnRemoveEntity(e);
                _activeEntities.Release(e);
            }
            _entitiesNeedRemove.Clear();

            entityCount = _activeEntities.Count;
        }

        protected void Update()
        {
            SysUpdate();
            tick++;
            float fixedPeriod = 1 / FixedFreq;
            _updateTimeAccumulate += Time.deltaTime;
            while (_updateTimeAccumulate > fixedPeriod)
            {
                foreach (var e in _activeEntities)
                {
                    FixedUpdateEntity(e);
                }
                _updateTimeAccumulate -= fixedPeriod;
                fixedTick++;
            }
        }
    }
}

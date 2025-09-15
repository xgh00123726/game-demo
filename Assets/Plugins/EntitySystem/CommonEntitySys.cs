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
    public abstract class CommonEntitySys<T_Entity, T_Instance> : Singleton<T_Instance>, IBaseSys
        where T_Entity : class, IEntity, new()
        where T_Instance : CommonEntitySys<T_Entity, T_Instance>, new()
    {
        private float _updateTimeAccumulate = 0;
        private int _currentIterateIndex = 0;
        private bool _inUpdating = false;
        private IEConstructor<T_Entity> _entityConstructor = new PoolConstructor<T_Entity>();
        private IEContainer<T_Entity> _entities = new LinkListContainer<T_Entity>();

        protected internal LinkedList<T_Entity> _entityNeedRegister = new LinkedList<T_Entity>();
        protected internal LinkedList<T_Entity> _entitiesNeedRemove = new LinkedList<T_Entity>();

        protected internal int tick = 0;
        protected internal int fixedTick = 0;

        public int sysID = 0;
        public int entityCount = 0;

        protected internal virtual float FixedFreq => 60f;

        protected CommonEntitySys()
        {
            ShadowMono.CreateShadowMono(this);
            PoolInfo.entitySysNum++;
            sysID = PoolInfo.entitySysNum;
            Tools.XLogger.Instance.Color(Color.green).IF(false).
                Log($"entity sys: {this.GetType().Name} has awaken, entitySys id: {sysID}, instance hash:{GetHashCode()}");
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

            OnRemoveEntityFromActives(e);

            if (!_inUpdating)
            {
                Entities.Remove(e);
            }
            else
            {
                _entitiesNeedRemove.AddLast(e);
            }
        }

        protected void AddToNeedRegister(T_Entity e)
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

        protected int CurrentIterateIndex => _currentIterateIndex;

        public virtual IEContainer<T_Entity> Entities => _entities;
        public virtual IEConstructor<T_Entity> Constructor => _entityConstructor;

        public int Tick => tick;
        public int FixedTick => fixedTick;


        public T_Entity NewFromPool()
        {
            return Constructor.GetEntity();
        }

        public void RegisterEntity(T_Entity e)
        {
            e.InstanceID = PoolInfo.allocatedID++;


            OnRegisterEntityToActives(e);
            if (_inUpdating)
            {
                AddToNeedRegister(e);
            }
            else
            {
                Entities.Add(e);
            }
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

        private void SysUpdate()
        {
            foreach (T_Entity e in _entityNeedRegister)
            {
                Entities.Add(e);
            }
            _entityNeedRegister.Clear();

            _currentIterateIndex = 0;
            _inUpdating = true;
            foreach (var e in Entities)
            {
                UpdateEntity(e);
                _currentIterateIndex++;
            }
            _inUpdating = false;

            foreach (var e in _entitiesNeedRemove)
            {
                Entities.Remove(e);
                Constructor.ReleaseEntity(e);
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
                _inUpdating = true;
                foreach (var e in _entities)
                {
                    FixedUpdateEntity(e);
                }
                _inUpdating = false;
                _updateTimeAccumulate -= fixedPeriod;
                fixedTick++;
            }
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
            return Constructor.Count;
        }

        int IBaseSys.GetActiveCount()
        {
            return 0;
        }
    }
}

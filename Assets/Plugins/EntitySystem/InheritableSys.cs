using GameBase.Tools;
using System;
using System.Collections.Generic;

namespace GameBase.EntitySystem
{
    public abstract class InheritableSys<T_Entity, T_Sys> : Singleton<T_Sys>, IBaseSys
        where T_Entity : class
        where T_Sys : InheritableSys<T_Entity, T_Sys>, new()
    {
        private int _currentIterateIndex = 0;
        private bool _inUpdating = false;
        private Dictionary<Type, BaseObjectPool<T_Entity>> _pools = new();
        private IEContainer<T_Entity> _entities = new LinkListContainer<T_Entity>();
        protected bool _fixedUpdate = false;

        protected internal LinkedList<T_Entity> _entityNeedRegister = new LinkedList<T_Entity>();
        protected internal LinkedList<T_Entity> _entitiesNeedRemove = new LinkedList<T_Entity>();

        protected InheritableSys()
        {
            SingletonEntitySysInstance.CreateShadowMono(this);
        }

        /// <summary>
        /// 将实体标记为删除
        /// </summary>
        public void RemoveEntity(T_Entity e)
        {
            if (e == null) return;

            if (_entitiesNeedRemove.Contains(e))
            {
                return;
            }

            if (!_inUpdating)
            {
                Entities.Remove(e);
                _pools[e.GetType()].Release(e);
            }
            else
            {
                _entitiesNeedRemove.AddLast(e);
            }
        }


        /// <summary>
        /// 当实体活跃时调用
        /// <list type="bullet">
        /// <item><param name="e"><paramref name="e"/>:实体引用</param></item>
        /// </list></summary>
        protected abstract void UpdateEntity(T_Entity e);

        protected int CurrentIterateIndex => _currentIterateIndex;

        public virtual IEContainer<T_Entity> Entities => _entities;


        public void RegisterEntity(T_Entity e)
        {
            if (_inUpdating)
            {
                _entityNeedRegister.AddLast(e);
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
        public T NewEntity<T>() where T : class, T_Entity, new()
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type))
            {
                _pools[type] = new BaseObjectPool<T_Entity>();
                _pools[type].InstantiateFunc = static () => new T();
            }
            var e = _pools[type].Get();
            RegisterEntity(e);
            return e as T;
        }

        /// <summary>
        /// 不推荐重写Update
        /// </summary>
        internal protected virtual void Update()
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
                _pools[e.GetType()].Release(e);
            }
            _entitiesNeedRemove.Clear();
        }

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


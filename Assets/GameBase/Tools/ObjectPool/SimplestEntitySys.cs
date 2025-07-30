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
        where T_entity : new()
        where T_container : IEntityContainer<T_entity>, IEnumerable<T_entity>, new()
    {
        public int sysID = 0;
        public int entityCount = 0;

        private static SimplestEntitySys<T_entity, T_container> _instance;
        public static SimplestEntitySys<T_entity, T_container> Instance => _instance;

        protected LinkedList<T_entity> _entityNeedRegister = new LinkedList<T_entity>();
        protected LinkedList<T_entity> _entitiesNeedRemove = new LinkedList<T_entity>();
        protected T_container _activeEntities = new T_container();

        /// <summary>
        /// 将实体标记为删除
        /// </summary>
        protected void RemoveEntity(T_entity e)
        {
            if (e == null) return;

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

        protected abstract void OnRegisterEntity(T_entity e);

        protected abstract void OnRemoveEntity(T_entity e);

        public T_entity NewEntity()
        {
            var e = new T_entity();
            Register(e);
            return e;
        }


        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;

            StaticInfo.entitySysNum++;
            sysID = StaticInfo.entitySysNum;
            Tools.Logger.Instance.
                Log($"entity sys: {this.GetType().Name} has awaken, entitySys id: {sysID}, instance hash:{_instance.GetHashCode()}");
        }

        protected void Update()
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
    }
}

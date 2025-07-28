using System.Collections.Generic;
using UnityEngine;
namespace GameBase.Tools
{
    public abstract class UnPoolableObjectEntitySys<T_entity, T_obj> : MonoBehaviour 
        where T_entity : IPoolableObject, IEntity<T_obj>, new()
    {
        public int sysID = 0;
        private static UnPoolableObjectEntitySys<T_entity, T_obj> _instance;
        private LinkedList<T_entity> _entityNeedGenerate = new LinkedList<T_entity>();
        private LinkedList<T_entity> _entitiesNeedRemove = new LinkedList<T_entity>();
        private PoolableObjectPool<T_entity> _entityPool = new PoolableObjectPool<T_entity>()
        {
            InstantiateObject = () => new T_entity()
        };

        /// <summary>
        /// 当实体对应的物体被获取时调用
        /// </summary>
        protected abstract void OnObjectGet(T_entity e);

        /// <summary>
        /// 当实体对应的物体被释放时调用
        /// </summary>
        protected abstract void OnObjectRelease(T_entity e);

        /// <summary>
        /// 将实体标记为删除
        /// </summary>
        protected void RemoveEntity(T_entity e)
        {
            _entitiesNeedRemove.AddLast(e);
        }

        /// <summary>
        /// 根据id生成实体对象
        /// </summary>
        protected abstract T_obj GetObj(int id);

        /// <summary>
        /// 当实体活跃时调用
        /// <list type="bullet">
        /// <item><param name="p"><paramref name="p"/>:实体引用</param></item>
        /// </list></summary>
        protected abstract void UpdateEntity(T_entity e);

        public T_entity Get()
        {
            var e = _entityPool.Get();
            _entityNeedGenerate.AddLast(e);

            return e;
        }

        public static UnPoolableObjectEntitySys<T_entity, T_obj> Instance => _instance;

        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;

            StaticInfo.entitySysNum++;
            sysID = StaticInfo.entitySysNum;
            Tools.Logger.Instance.
                Log($"entity sys: {this.GetType().Name} has awaken, entitySys id: {sysID}, instance hash:{_instance.GetHashCode()}");
        }

        protected virtual void Update()
        {
            foreach (T_entity e in _entityNeedGenerate)
            {
                e.Obj = GetObj(e.ID);
                OnObjectGet(e);
            }
            _entityNeedGenerate.Clear();

            foreach (var e in _entityPool.ActiveList)
            {
                UpdateEntity(e);
            }

            foreach (var e in _entitiesNeedRemove)
            {
                OnObjectRelease(e);
                _entityPool.Release(e);
            }
            _entitiesNeedRemove.Clear();
        }
    }
}

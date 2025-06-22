using GameBase.Tools;
using UnityEngine;

namespace GameBase.Resources
{
    public class PoolableMonoMgr<T> where T : MonoBehaviour, IPoolableObject
    {
        private static PoolableMonoMgr<T> _instance;
        public static PoolableMonoMgr<T> Instance(PrefabType type) => _instance ??= new PoolableMonoMgr<T>(type);

        ObjectPool<T> _pool;
        public ObjectPool<T> Pool => _pool;

        private PoolableMonoMgr(PrefabType type)
        {
            _pool = new ObjectPool<T>();
            _pool.InstantiateObject = () =>
            {
                var obj = ResourceMgr.InstaniatePrefab(type, typeof(T).Name);
                var prefab = obj.GetComponent<T>();
                if (prefab != null)
                {
                    return prefab;
                }
                else
                {
                    return obj.AddComponent<T>();
                }
            };
        }

        

        public T Get()
        {
            return _pool.Get();
        }
        public void Release(T obj)
        {
            _pool.Release(obj);
        }
        public void TryRelease(T obj)
        {
            _pool.TryRelease(obj);
        }
    }
}

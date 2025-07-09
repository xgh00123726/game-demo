using GameBase.Tools;
using UnityEngine;

namespace GameBase.Resources
{
    /// <summary>
    /// 泛型对象管理器, 会使用对象池管理每个创建的对象
    /// <list type="bullet">
    /// <item><typeparam name="T"><typeparamref name="T"/>:需要被管理的类型</typeparam></item>
    /// </list></summary>
    public class PoolableMonoMgr<T> where T : MonoBehaviour, IPoolableObject
    {
        private static PoolableMonoMgr<T> _instance;
        public static PoolableMonoMgr<T> Instance(PrefabType type, string subFolder = null) => _instance ??= new PoolableMonoMgr<T>(type, subFolder);

        ObjectPool<T> _pool;
        /// <summary>
        /// 管理prefab的对象池
        /// </summary>
        public ObjectPool<T> Pool => _pool;

        private PoolableMonoMgr(PrefabType type, string subFolder)
        {
            _pool = new ObjectPool<T>();
            _pool.InstantiateObject = () =>
            {
                var obj = ResourceMgr.InstaniatePrefab(type, typeof(T).Name, subFolder);
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

        /// <summary>
        /// 从对象池中申请对象,如果没有则新创建一个
        /// </summary>
        /// <returns>申请的对象</returns>
        public T Get()
        {
            return _pool.Get();
        }
        /// <summary>
        /// 将对象强制释放回对象池
        /// <list type="bullet">
        /// <item><param name="obj"><paramref name="obj"/>:被释放的对象</param></item>
        /// </list></summary>
        public void Release(T obj)
        {
            _pool.Release(obj);
        }

        /// <summary>
        /// 尝试将对象释放回对象池
        /// <list type="bullet">
        /// <item><param name="obj"><paramref name="obj"/>:被释放的对象</param></item>
        /// </list></summary>
        /// <returns>是否放回成功</returns>
        public bool TryRelease(T obj)
        {
            return _pool.TryRelease(obj);
        }
    }
}

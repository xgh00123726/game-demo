using System.Collections;
using System.Collections.Generic;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Resources
{
    /// <summary>
    /// 预制件管理器，针对具有PoolablePrefab脚本的预制件
    /// </summary>
    public class PoolablePrefabMgr : MonoBehaviour
    {
        private static Dictionary<string, ObjectPool<PoolablePrefab>> _prefabPools = new Dictionary<string, ObjectPool<PoolablePrefab>>();
        
        private static ObjectPool<PoolablePrefab> PoolOf<T>(PrefabType type, string subFolder = null) where T : PoolablePrefab
        {
            string name = typeof(T).Name;
            if (_prefabPools.ContainsKey(name))
            {
                return _prefabPools[name];
            }
            var pool = new ObjectPool<PoolablePrefab>();
            pool.InstantiateObject = () =>
            {
                var obj = ResourceMgr.InstaniatePrefab(type, name, subFolder);
                var prefab = obj.GetComponent<PoolablePrefab>();
                if (prefab != null)
                {
                    return prefab;
                }
                else
                {
                    return obj.AddComponent<T>();
                }
            };
            _prefabPools[name] = pool;
            return pool;
        }

        /// <summary>
        /// 从PrefabMgr的对象池中获取一个对象,如果没有,则新实例化一个
        /// <list type="bullet">
        /// <item><typeparam name="T"><typeparamref name="T"/>:需要实例化的prefab, 必须继承自monobehavior且实现IPoolableObject</typeparam></item>
        /// <item><param name="type"><paramref name="type"/>:prefab类型, 即prefab所在的一级文件夹</param></item>
        /// <item><param name="subFolder"><paramref name="subFolder"/>:prefab所在的二三级文件夹</param></item>
        /// </list></summary>
        /// <returns>实例化后的对象</returns>
        public static T GetFromPool<T>(PrefabType type, string subFolder = null) where T : PoolablePrefab
        {
            return PoolOf<T>(type, subFolder).Get() as T;
        }

        /// <summary>
        /// 实例化一个对象,不保存在PrefabMgr的对象池里
        /// <list type="bullet">
        /// <item><typeparam name="T"><typeparamref name="T"/>:需要实例化的prefab, 必须继承自monobehavior且实现IPoolableObject</typeparam></item>
        /// <item><param name="type"><paramref name="type"/>:prefab类型, 即prefab所在的一级文件夹</param></item>
        /// <item><param name="subFolder"><paramref name="subFolder"/>:prefab所在的二三级文件夹</param></item>
        /// </list></summary>
        /// <returns>实例化后的对象</returns>
        public static T GetNotfromPool<T>(PrefabType type, string subFolder = null) where T : MonoBehaviour
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
        }

        /// <summary>
        /// 将对象释放回对象池
        /// <list type="bullet">
        /// <item><param name="prefab"><paramref name="prefab"/>:需要被释放的对象</param></item>
        /// </list></summary>
        public static void ReleaseToPool(PoolablePrefab prefab)
        {
            string name = prefab.GetType().Name;
            _prefabPools[name].Release(prefab);
        }

        public static void TryReleaseToPool(PoolablePrefab prefab)
        {
            string name = prefab.GetType().Name;
            _prefabPools[name].TryRelease(prefab);
        }

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            foreach (var pool in _prefabPools.Values)
            {
                foreach (var prefab in pool.ActiveList)
                {
                    if (prefab.ReleaseTrigger)
                    {
                        pool.ReleaseToBuffer(prefab);
                    }
                }
                pool.FlushReleaseBuffer();
            }
        }
    }
}

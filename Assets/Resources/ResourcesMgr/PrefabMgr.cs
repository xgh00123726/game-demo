using System.Collections;
using System.Collections.Generic;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Resources
{
    public class PrefabMgr : MonoBehaviour
    {
        private static PrefabMgr _instance;
        public static PrefabMgr Instance => _instance;

        private Dictionary<string, ObjectPool<PoolablePrefab>> _prefabPools;
        
        private ObjectPool<PoolablePrefab> PoolOf<T>(PrefabType type) where T : PoolablePrefab
        {
            string name = typeof(T).Name;
            if (_prefabPools.ContainsKey(name))
            {
                return _prefabPools[name];
            }
            var pool = new ObjectPool<PoolablePrefab>();
            pool.InstantiateObject = () =>
            {
                var obj = ResourceMgr.InstaniatePrefab(type, name);
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

        public T GetFromPool<T>(PrefabType type) where T : PoolablePrefab
        {
            return PoolOf<T>(type).Get() as T;
        }

        public T GetNotfromPool<T>(PrefabType type) where T : MonoBehaviour
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
        }

        public void ReleaseToPool(PoolablePrefab prefab)
        {
            string name = prefab.GetType().Name;
            _prefabPools[name].Release(prefab);
        }

        public void TryReleaseToPool(PoolablePrefab prefab)
        {
            string name = prefab.GetType().Name;
            _prefabPools[name].TryRelease(prefab);
        }

        void Awake()
        {
            _prefabPools = new Dictionary<string, ObjectPool<PoolablePrefab>>();
            _instance = this;
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
                pool.PushReleaseBuffer();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
using Logger = GameBase.Tools.Logger;

namespace GameBase.Effects
{
    /// <summary>
    /// 爆炸特效对象池集合, 包含了所有爆炸特效的对象池
    /// </summary>
    public class ExplorePool
    {
        static ExplorePool()
        {
            ResourceMgr.LoadAllPrefab(PrefabType.Effect, "Hits and explosions");
        }

        private static Dictionary<string, ObjectPool<Explore>> _pools = new Dictionary<string, ObjectPool<Explore>>();
        /// <summary>
        /// 从对象池中获取一个爆炸特效的gameobject
        /// <list type="bullet">
        /// <item><param name="name"><paramref name="name"/>:效果名称</param></item>
        /// </list></summary>
        /// <returns>爆炸特效的gameobject</returns>
        public static Explore Get(string name)
        {
            if (!_pools.ContainsKey(name))
            {
                _pools[name] = new ObjectPool<Explore>();
                _pools[name].InstantiateObject = () =>
                {
                    var obj = ResourceMgr.InstaniatePrefab(PrefabType.Effect, name);
                    var explore = obj.GetComponent<Explore>();
                    if (explore == null)
                    {
                        explore = obj.AddComponent<Explore>();
                    }
                    explore.prefabName = name;
                    return explore;
                };
            }
            return _pools[name].Get();
        }

        /// <summary>
        /// 回收一个爆炸特效
        /// <list type="bullet">
        /// <item><param name="explore"><paramref name="explore"/>:爆炸效果的gameobject</param></item>
        /// </list></summary>
        public static void Release(Explore explore, string name = null)
        {
            string exploreName = name;
            if (name == null || name.Length == 0 || name == "")
            {
                exploreName = explore.prefabName;
            }
            if (!_pools.ContainsKey(exploreName))
            {
                Logger.Level(Logger.LogLevel.Error)
                    .Log($"unknwon explore object, name:{exploreName}");
            }
            _pools[exploreName].Release(explore);
        }
    }
}

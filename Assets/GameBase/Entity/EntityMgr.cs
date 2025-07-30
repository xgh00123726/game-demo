using System.Collections.Generic;
using UnityEngine;
using GameBase.LifeTime;
using GameBase.Resources;
using GameBase.Tools;
using Logger = GameBase.Tools.Logger;
using GameBase.Math;
using UnityEngine.UIElements;

namespace GameBase.Entity
{
    public class EntityMgr : IManager
    {
        public delegate bool EntityFilter(GameEntity entity);
        static List<GameEntity> _entities = new List<GameEntity>();
        static Dictionary<string, CSObjectPool<GameEntity>> _entitiyPools = new Dictionary<string, CSObjectPool<GameEntity>>();
        static EntityMgr()
        {
            LifeTimeMgr.RegisterMgr(new EntityMgr());
        }

        internal static void RegisterEntity(GameEntity entity)
        {
            _entities.Add(entity);
        }

        internal static void UnRegisterEntity(GameEntity entity)
        {
            _entities.Remove(entity);
        }

        public static bool OnlyEnemy(GameEntity entity)
        {
            return entity.camp == GameEntity.Camp.Rival;
        }

        /// <summary>
        /// 观测量
        /// <list type="bullet">
        /// <item><param name="prefabName"><paramref name="prefabName"/>:预制件名</param></item>
        /// </list>
        /// </summary>
        /// <returns>该预制件在对象池中的储备，1：活跃对象，2：非活跃对象</returns>
        public static int[] ObjectNumOf(string prefabName)
        {
            if (_entitiyPools.ContainsKey(prefabName))
            {
                return new int[2]{_entitiyPools[prefabName].ActiveList.Count, _entitiyPools[prefabName].ReleasedList.Count};
            }
            return new int[2] { 0, 0 };
        }

        /// <summary>
        /// 对象池字典增加一个对象池
        /// <list type="bullet">
        /// <item><param name="prefabName"><paramref name="prefabName"/>:需要增加的key名</param></item>
        /// </list></summary>
        private static void AddToPools(string prefabName)
        {
            _entitiyPools[prefabName] = new CSObjectPool<GameEntity>()
            {
            };
        }

        /// <summary>
        /// 从对象池中获取一个GameEntity对象
        /// <list type="bullet">
        /// <item><param name="prefabName"><paramref name="prefabName"/>:GameEntity对象的预制件名字</param></item>
        /// </list></summary>
        /// <returns>获取的GameEntity对象</returns>
        public static GameEntity GetFromPool(string prefabName)
        {
            if(!_entitiyPools.ContainsKey(prefabName))
            {
                AddToPools(prefabName);
            }
            var entity = _entitiyPools[prefabName].Get();
            entity.PrefabName = prefabName;
            return entity;
        }

        /// <summary>
        /// 释放一个对象池对象
        /// <list type="bullet">
        /// <item><param name="entity"><paramref name="entity"/>:需要被释放的对象</param></item>
        /// </list></summary>
        public static void Release(GameEntity entity)
        {
            var prefabName = entity.PrefabName;
            if (!_entitiyPools.ContainsKey(prefabName))
            {
                AddToPools(prefabName);
            }
            _entitiyPools[prefabName].Release(entity);
        }

        /// <summary>
        /// 返回一个Mono 管理器
        /// <list type="bullet">
        /// <item><typeparam name="T"><typeparamref name="T"/>:被管理的类型</typeparam></item>
        /// </list></summary>
        /// <returns></returns>
        public static PoolableMonoMgr<T> GetMonoMgr<T>() where T : GameEntity
        {
            return PoolableMonoMgr<T>.Instance(PrefabType.Entity);
        }

        /// <summary>
        /// 返回指定位置最近的游戏实体
        /// <list type="bullet">
        /// <item><param name="position"><paramref name="position"/>:指定的位置</param></item>
        /// <item><param name="rangeLimit"><paramref name="rangeLimit"/>:只会寻找到rangeLimit距离内的实体，负数表示无穷</param></item>
        /// </list></summary>
        /// <returns>符合条件最近的实体，没有实体满足条件则返回null</returns>
        public static GameEntity NearestEntity(Vector3 position, float rangeLimit = -1)
        {
            GameEntity ret = null;
            float minDistance = float.PositiveInfinity;
            foreach (var entity in _entities)
            {
                if (rangeLimit > 0)
                {
                    float dis = (entity.transform.position - position).magnitude;
                    if (dis > rangeLimit) continue;
                    minDistance = dis;
                    ret = entity;
                }
            }

            return ret;
        }

        /// <summary>
        /// 返回指定位置最近的游戏实体
        /// <list type="bullet">
        /// <item><param name="position"><paramref name="position"/>:指定的位置</param></item>
        /// <item><param name="filter"><paramref name="filter"/>:寻找过滤器</param></item>
        /// <item><param name="rangeLimit"><paramref name="rangeLimit"/>:只会寻找到rangeLimit距离内的实体，负数表示无穷</param></item>
        /// </list></summary>
        /// <returns>符合条件最近的实体，没有实体满足条件则返回null</returns>
        public static GameEntity NearestEntity(Vector3 position, EntityFilter filter, float rangeLimit = -1)
        {
            if (filter == null)
            {
                return NearestEntity(position, rangeLimit);
            }
            GameEntity ret = null;
            float minDistance = float.PositiveInfinity;
            foreach (var entity in _entities)
            {
                if (!filter(entity)) continue; // 不满足过滤需求

                if (rangeLimit > 0)
                {
                    float dis = (entity.transform.position - position).magnitude;
                    if (dis > rangeLimit) continue;
                    minDistance = dis;
                    ret = entity;
                }
            }

            return ret;
        }

        /// <summary>
        /// 返回范围内所有的的游戏实体
        /// <list type="bullet">
        /// <item><param name="center"><paramref name="center"/>:指定的位置</param></item>
        /// <item><param name="radius"><paramref name="radius"/>:半径</param></item>
        /// <item><param name="filter"><paramref name="filter"/>:寻找过滤器</param></item>
        /// </list></summary>
        /// <returns>符合条件最近的实体，没有实体满足条件则返回null</returns>
        public static LinkedList<GameEntity> EntityWithin(Vector3 center, float radius, EntityFilter filter)
        {
            if (filter == null)
            {
                return EntityWithin(center, radius);
            }

            LinkedList<GameEntity> ret = new LinkedList<GameEntity>();

            foreach (var entity in _entities)
            {
                if (!filter(entity)) continue;

                if (GMath.IsIntersect(center, radius, entity.transform.position, entity.sphereCollider.radius))
                {
                    ret.AddLast(entity);
                }
            }

            return ret;
        }

        /// <summary>
        /// 返回范围内所有的的游戏实体
        /// <list type="bullet">
        /// <item><param name="center"><paramref name="center"/>:指定的位置</param></item>
        /// <item><param name="radius"><paramref name="radius"/>:半径</param></item>
        /// </list></summary>
        /// <returns>符合条件最近的实体，没有实体满足条件则返回null</returns>
        public static LinkedList<GameEntity> EntityWithin(Vector3 center, float radius)
        {
            LinkedList<GameEntity> ret = new LinkedList<GameEntity>();

            foreach (var entity in _entities)
            {
                if (GMath.IsIntersect(center, radius, entity.transform.position, entity.sphereCollider.radius))
                {
                    ret.AddLast(entity);
                }
            }

            return ret;
        }

        void IManager.Update()
        {
            
        }
    }
}

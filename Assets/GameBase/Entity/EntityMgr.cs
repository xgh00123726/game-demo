using System.Collections.Generic;
using UnityEngine;
using GameBase.Object;

namespace GameBase.Entity
{
    public class EntityMgr : IManager
    {
        static List<GameEntity> _entities = new List<GameEntity>();
        internal static void RegisterEntity(GameEntity entity)
        {
            _entities.Add(entity);
        }

        internal static void UnRegisterEntity(GameEntity entity)
        {
            _entities.Remove(entity);
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

        void IManager.Update()
        {
            
        }
    }
}

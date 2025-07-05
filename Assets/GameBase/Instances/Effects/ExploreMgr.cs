using UnityEngine;
using GameBase.Tools;
using GameBase.Object;
using Logger = GameBase.Tools.Logger;

namespace GameBase.Effects
{
    public class ExploreMgr : IManager
    {
        /// <summary>
        /// 在指定地点出召唤爆炸特效, 持续一定时间, 并可以指定爆炸效果
        /// <list type="bullet">
        /// <item><param name="position"><paramref name="position"/>:被指定的召唤地点</param></item>
        /// <item><param name="duration"><paramref name="duration"/>爆炸特效持续时间</param></item>
        /// <item><param name="name"><paramref name="name"/>:特效风格</param></item>
        /// </list></summary>
        public static void InvokeExplore(Vector3 position, float duration = 1f, string name = "Default")
        {
            var explore = ExplorePool.Get(name);
            if (explore == null)
            {
                Logger.Level(Logger.LogLevel.Warning)
                    .Log("invalid explore name");
                return;
            }
            else
            {
                explore.transform.position = position;
            }
            Timer.AddTask(duration, () =>
            {
                ExplorePool.Release(explore);
            });
            Logger.Log($"explore:{explore}, here:{position}");
        }

        void IManager.Update()
        {
            
        }
    }
}

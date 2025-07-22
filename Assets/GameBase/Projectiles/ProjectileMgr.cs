using GameBase.Resources;
using GameBase.Object;

namespace GameBase.Projectile
{
    public class ProjectileMgr<T> : IManager where T : ProjectileObject
    {
        /// <summary>
        /// 这里有个很有意思的问题, 暂时记录一下
        /// ProjectileMgr是针对每一种射弹都实例化一个管理器
        /// 且ProjectileMgr必须要有Update方法, 无用质疑的是必须使用单例模式, 否则只能用静态对象存储对象池, 显然不可能
        ///     1. 如果继承自monobehavior, 则必须在单例构造中去实例化(在游戏中)一个物体, 调用awake方法, 才可以真正完成实例化
        ///     2. 而使用自定义的update, 则可以有效避免这个问题, 在构造函数中注册一个update事件即可
        /// </summary>
        private static ProjectileMgr<T> _instance = new ProjectileMgr<T>();
        public static ProjectileMgr<T> Instance => _instance;

        public PoolableMonoMgr<T> MonoMgr { get; private set; }

        ProjectileMgr(string subFolder = null)
        {
            LifeTimeMgr.RegisterMgr(this);
            MonoMgr = PoolableMonoMgr<T>.Instance(PrefabType.Projectile, subFolder);
        }

        /// <summary>
        /// 从对象池中创建一个射弹
        /// <list type="bullet">
        /// <item><param name="owner"><paramref name="owner"/>:射弹拥有者</param></item>
        /// </list></summary>
        /// <returns>创建的射弹</returns>
        public T CreateProjectile()
        {
            var projectile = MonoMgr.Get();
            return projectile;
        }

        void IManager.Update()
        {
            foreach (var proj in MonoMgr.Pool.ActiveList)
            {
                proj._Update();
                if (proj.canRelease)
                {
                    MonoMgr.Pool.ReleaseToBuffer(proj);
                }
            }
            MonoMgr.Pool.FlushReleaseBuffer();
        }
    }
}

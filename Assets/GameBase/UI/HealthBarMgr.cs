using GameBase.Object;
using GameBase.Resources;

namespace GameBase.UI
{
    /// <summary>
    /// π‹¿Ì—™Ãı
    /// </summary>
    public class HealthBarMgr : IManager
    {
        static PoolableMonoMgr<HealthBar> _healthBarPoolMgr;

        static HealthBarMgr()
        {
            LifeTimeMgr.RegisterMgr(new HealthBarMgr());
            _healthBarPoolMgr = PoolableMonoMgr<HealthBar>.Instance(PrefabType.UI);
        }

        public static HealthBar Get(IHealthBarOwner owner)
        {
            var healthBar = _healthBarPoolMgr.Get();
            healthBar._owner = owner;
            return healthBar;
        }

        public static void Release(HealthBar healthBar)
        {
            _healthBarPoolMgr.Release(healthBar);
        }

        void IManager.Update()
        {
            foreach (var healthBar in _healthBarPoolMgr.Pool.ActiveList)
            {
                healthBar._Update();
            }
        }
    }
}

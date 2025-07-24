using System.Collections.Generic;

namespace GameBase.Projectile
{
    public class AOEProjectile : ProjectileObject
    {
        public float radius = 1f;            // 影响范围
        private LinkedList<IProjectileTarget> _targets = new LinkedList<IProjectileTarget>(); // 多目标

        public void AddTarget(IProjectileTarget target)
        {
            if (_whites.Contains(target)) return;

            _targets.AddLast(target);
        }

        public LinkedList<IProjectileTarget> Targets
        {
            get => _targets;
            set => _targets = value;
        }

        protected override void OnHit()
        {
            base.OnHit();

            foreach (var target in _targets)
            {
                HitTarget(target);
            }
            _targets.Clear();
        }
    }
}

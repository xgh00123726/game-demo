using UnityEngine;

namespace GameBase.Projectile
{
    public class Tracer : CurveBase
    {
        public float turnSpeed = 1f;
        public Tracer(ICurvableProjectile projectile) : base(projectile)
        {
        }

        public override void DirUpdate()
        {
            Vector3 currDir = _projectile.Dir;
            Vector3 expectDir = _projectile.Target - _projectile.Transform.position;
            _projectile.Dir = Vector3.Lerp(currDir, expectDir, Mathf.Clamp01(_projectile.LifeTime));
        }
    }
}

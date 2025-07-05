using UnityEngine;

namespace GameBase.Projectile
{
    // 分时段抛物线，前期抛物，后期追踪
    // @angle: 抛出角度
    // @time: 持续时间 
    public class ParabolicTimeLimit : CurveBase
    {
        public float angle = 85f;
        public float time = 0.5f;

        public ParabolicTimeLimit(ICurvableProjectile projectile) : base(projectile)
        {
        }

        public override void DirUpdate()
        {
            if (_projectile.LifeTime < time)
            {
                _projectile.Dir = (_projectile.Target - _projectile.Start);
                _projectile.Transform.Rotate(-angle, 0f, 0f);
            }
            else
            {
                Vector3 currDir = _projectile.Dir;
                Vector3 expectDir = _projectile.Target - _projectile.Transform.position;
                _projectile.Dir = Vector3.Lerp(currDir, expectDir, Mathf.Clamp01(_projectile.LifeTime));
            }
        }
    }
}

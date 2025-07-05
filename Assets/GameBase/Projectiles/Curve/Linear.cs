using UnityEngine;

namespace GameBase.Projectile
{
    // 直线轨迹
    // @speed: 速度
    public class Linear : CurveBase
    {
        public Linear(ICurvableProjectile projectile) : base(projectile)
        {
        }

        public override void DirUpdate()
        {
            _projectile.Dir = _projectile.Target - _projectile.Start;
        }
    }
}

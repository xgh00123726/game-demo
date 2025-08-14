using UnityEngine;

namespace GameBase.Flyings
{
    // 直线轨迹
    // @speed: 速度
    public class Linear : CurveBase
    {
        public Linear(ICurveable projectile) : base(projectile)
        {
        }

        protected override Vector3 GetDirDelta()
        {
            return _projectile.Dest - _projectile.Src;
        }
    }
}

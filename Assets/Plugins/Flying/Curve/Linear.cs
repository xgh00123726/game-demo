using UnityEngine;

namespace GameBase.Flyings
{
    // 直线轨迹
    // @Speed: 速度
    public class Linear : Curve
    {
        protected override Vector3 GetDirDelta()
        {
            return _projectile.Dir;
        }
    }
}

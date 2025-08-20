using GameBase.Tools;
using UnityEngine;

namespace GameBase.Flyings
{
    public class Fall : CurveBase
    {
        public Fall(ICurveable projectile) : base(projectile)
        {
            freezeY = false;
        }
        protected override Vector3 GetDirDelta()
        {
            return _projectile.Dest - _projectile.Src;
        }
    }
}

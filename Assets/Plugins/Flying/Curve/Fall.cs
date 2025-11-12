using GameBase.Tools;
using UnityEngine;

namespace GameBase.Flyings
{
    public class Fall : Curve
    {
        public Fall()
        {
            _freezeY = false;
        }
        protected override Vector3 GetDirDelta()
        {
            return _projectile.Dest - _projectile.Src;
        }
    }
}

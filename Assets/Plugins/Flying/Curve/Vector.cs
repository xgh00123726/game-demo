using UnityEngine;

namespace GameBase.Flyings
{
    public class Vector : Curve
    {
        protected bool _isTravelEnd = false;

        public float length;
        protected override Vector3 GetDirDelta()
        {
            return _projectile.Dir;
        }

        protected override void PosUpdate()
        {
            if ((_projectile.Position - _projectile.Src).magnitude >= length)
            {
                _isTravelEnd = true;
                return;
            }
            Vector3 delta = GetPosDelta();

            if (_freezeY)
            {
                delta.y = 0f;
            }

            _projectile.Position = _projectile.Position + GetPosDelta();
        }

        protected override bool IsTravelEnd => _isTravelEnd;
    }
}

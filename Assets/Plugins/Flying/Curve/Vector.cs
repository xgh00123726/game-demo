using UnityEngine;

namespace GameBase.Flyings
{
    public class Vector : CurveBase
    {
        private bool isCurveEnd = false;
        public Vector(ICurveable projectile) : base(projectile)
        {
        }

        protected override Vector3 GetDirDelta()
        {
            return _projectile.Dir;
        }

        protected override Vector3 GetPosDelta()
        {
            if ((_projectile.Position - _projectile.Src).magnitude >= _projectile.MaxTravel)
            {
                isCurveEnd = true;
                return Vector3.zero;
            }
            return base.GetPosDelta();
        }

        public override bool CurveEnd()
        {
            return isCurveEnd;
        }
    }
}

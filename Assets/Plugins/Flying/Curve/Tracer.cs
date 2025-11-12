using UnityEngine;

namespace GameBase.Flyings
{
    public class Tracer : Curve
    {
        public float turnSpeed = 1f;
        public float turnAcc = 3f;
        protected override Vector3 GetDirDelta()
        {
            Vector3 currDir = _projectile.Dir;
            Vector3 expectDir = _projectile.Dest - _projectile.Position;
            var tarDir = Vector3.Lerp(currDir, expectDir, Mathf.Clamp01(_projectile.LifeTime) * turnSpeed);
            turnSpeed += turnAcc * Time.deltaTime;

            return tarDir;
        }
    }
}

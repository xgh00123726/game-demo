using UnityEngine;

namespace GameBase.Flyings
{
    public class Slower : Linear
    {
        private float speedActual;
        public float factor = 0f;
        public float slowDis = 2f;
        public Slower(ICurveable projectile) : base(projectile)
        {
        }

        protected override Vector3 GetPosDelta()
        {
            float disToDest = (_projectile.Position - _projectile.Dest).magnitude;

            if (disToDest < slowDis)
            {
                speedActual = (factor + disToDest) / (factor + slowDis) * speed;
            }
            else
            {
                speedActual = speed;
            }

            return _projectile.Dir * speedActual * Time.deltaTime;
        }
    }
}

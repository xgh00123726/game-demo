using UnityEngine;

namespace GameBase.Flyings
{
    public class Slower : Linear
    {
        private float speedActual;
        public float Factor { get; set; } = 0f;
        public float SlowDis { get; set; } = 2f;
        protected override Vector3 GetPosDelta()
        {
            float disToDest = (_projectile.Position - _projectile.Dest).magnitude;

            if (disToDest < SlowDis)
            {
                speedActual = (Factor + disToDest) / (Factor + SlowDis) * Speed;
            }
            else
            {
                speedActual = Speed;
            }

            return _projectile.Dir * speedActual * Time.deltaTime;
        }
    }
}

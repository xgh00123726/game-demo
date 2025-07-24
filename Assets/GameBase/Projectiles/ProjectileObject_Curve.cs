using UnityEngine;
namespace GameBase.Projectile
{
    public partial class ProjectileObject
    {
        public CurveBase curve;
        public float LifeTime => Time.time - _instantiateTime;
        public float Speed
        {
            get
            {
                if (curve == null) return 0;
                return curve.speed;
            }
            set
            {
                if (curve == null) return;
                curve.speed = value;
            }
        }

        public Vector3 Dir
        {
            get => transform.forward;
            set => transform.forward = value;
        }

        void CurveUpdate()
        {
            if (curve == null) return;
            curve.DirUpdate();
            curve.PosUpdate();
        }
    }
}

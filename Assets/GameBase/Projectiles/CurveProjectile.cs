using UnityEngine;

namespace GameBase.Projectile
{
    public class CurveProjectile : GProjectile,
        ICurvableProjectile
    {
        public CurveBase _curve;
        public float speed = 1f;
        public CurveBase Curve
        {
            get => _curve;
            set => _curve = value;
        }

        private void Awake()
        {
            _curve = new Linear(this);
        }

        Transform ICurvableProjectile.Transform => transform;

        Vector3 ICurvableProjectile.Target => Dest;

        Vector3 ICurvableProjectile.Start => _src;

        float ICurvableProjectile.LifeTime => Time.time - _instantiateTime;

        Vector3 ICurvableProjectile.Dir
        {
            get => transform.forward;
            set => transform.forward = value;
        }
        internal override void _Update()
        {
            base._Update();
            _curve.speed = speed;
            _curve.DirUpdate();
            _curve.PosUpdate();
        }
    }
}

using UnityEngine;

namespace GameBase.Projectile
{
    public class CurveProjectile : Projectile,
        ICurvableProjectile
    {
        public CurveBase _curve;
        public CurveBase Curve
        {
            get => _curve;
            set => _curve = value;
        }

        protected virtual void Awake()
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
            _curve.DirUpdate();
            _curve.PosUpdate();
        }
    }
}

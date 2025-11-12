using GameBase.Tools;
using UnityEngine;

namespace GameBase.Flyings
{
    public abstract class Curve
    {
        protected ICurveable _projectile;
        protected bool _freezeY = true;

        public float speed = 1f;
        public float duration = 10f;

        internal void SetOwner(ICurveable curveable)
        {
            _projectile = curveable;
        }

        protected abstract Vector3 GetDirDelta();

        protected virtual void DirUpdate()
        {
            var dir = GetDirDelta();
            if (_freezeY)
            {
                dir.y = 0;
            }

            if (dir.magnitude < 0.01f)
            {
                return;
            }
            _projectile.Dir = dir;
        }

        protected virtual Vector3 GetPosDelta()
        {
            return _projectile.Dir * speed * Time.deltaTime;
        }
        protected virtual void PosUpdate()
        {
            Vector3 delta = GetPosDelta();
            var distanceVec = _projectile.Position - _projectile.Dest;

            if (_freezeY)
            {
                delta.y = 0f;
                distanceVec.y = 0f;
            }

            float disToDest = distanceVec.magnitude;
            float deltaMag = delta.magnitude;

            if (disToDest < deltaMag)
            {
                _projectile.Position = _projectile.Dest;
            }
            else
            {
                _projectile.Position = _projectile.Position + delta;
            }
        }

        protected virtual bool IsTravelEnd => false;
        private bool IsTimeout => _projectile.LifeTime > duration;
        public bool IsEnd => IsTravelEnd || IsTimeout;

        public void Update()
        {
            if (_projectile.LifeTime > duration)
            {
                return;
            }

            DirUpdate();
            PosUpdate();
        }
    }
}

using GameBase.Tools;
using UnityEngine;

namespace GameBase.Flyings
{
    public abstract class CurveBase
    {
        public ICurveable _projectile;
        public float speed = 1f;
        public bool freezeY = true;
        
        public CurveBase(ICurveable projectile)
        {
            _projectile = projectile;
        }

        protected abstract Vector3 GetDirDelta();

        public virtual void DirUpdate()
        {
            var dir = GetDirDelta();
            if (freezeY)
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
        public virtual void PosUpdate()
        {
            Vector3 delta = GetPosDelta();
            var distanceVec = _projectile.Position - _projectile.Dest;

            if (freezeY)
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

        public void DirInit(Vector3 dir)
        {
            _projectile.Dir = dir;
        }

        public void DirInit()
        {
            _projectile.Dir = _projectile.Dest - _projectile.Src;
        }
    }
}

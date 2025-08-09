using UnityEngine;

namespace GameBase.Projectile
{
    public abstract class CurveBase
    {
        public ICurveProjectile _projectile;
        public float speed = 1f;
        
        public CurveBase(ICurveProjectile projectile)
        {
            _projectile = projectile;
        }
        /// <summary>
        /// 方向更新
        /// </summary>
        public abstract void DirUpdate();

        protected virtual Vector3 GetPosDelta()
        {
            return _projectile.Dir * speed * Time.deltaTime;
        }
        public virtual void PosUpdate()
        {
            Vector3 delta = GetPosDelta();
            delta.y = 0f;
            float disToDest = (_projectile.Position - _projectile.Dest).magnitude;
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
    }
}

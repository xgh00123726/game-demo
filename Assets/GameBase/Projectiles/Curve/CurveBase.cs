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
        public virtual void PosUpdate()
        {
            float disToDest = (_projectile.Position - _projectile.Dest).magnitude;
            Vector3 delta = _projectile.Dir * speed * Time.deltaTime;
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

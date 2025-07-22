using UnityEngine;
namespace GameBase.Projectile
{
    public class Projectile<T> where T : ProjectileObject
    {
        T pobj;
        public IProjectileOwner owenr;
        public IProjectileTarget target;
        public float damage;                    // 伤害
        public float radius = 1f;            // 影响范围
        public bool isImmediatly = false;   // 是否瞬间型技能
        public Vector3 src;                 // 源位置
        public Vector3 dest;                // 目标位置

        public Projectile()
        {
            pobj = ProjectileMgr<T>.Instance.CreateProjectile();
        }

        public T SetAttr()
        {
            pobj.Owner = owenr;
            pobj.Target = target;
            pobj.damage = damage;
            pobj.radius = radius;
            pobj.isImmediatly = isImmediatly;
            pobj._src = src;
            pobj._dest = dest;

            return pobj;
        }
    }
}

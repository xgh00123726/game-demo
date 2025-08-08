using UnityEngine;
namespace GameBase.Projectile
{
    public interface IProjectileTarget
    {
        int InstanceID { get; }
        Vector3 Center { get; }
        float Radius { get; }

        void GetDamage(float damage);
    }
}

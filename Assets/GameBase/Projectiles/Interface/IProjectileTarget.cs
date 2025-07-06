using UnityEngine;
namespace GameBase.Projectile
{
    public interface IProjectileTarget
    {
        Vector3 Center { get; }
        float Radius { get; }
    }
}

using UnityEngine;

namespace GameBase.Projectiles
{
    public interface IProjectileTarget
    {
        Vector3 Center { get; }
        float Radius { get; }
    }
}

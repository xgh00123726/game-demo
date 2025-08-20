using UnityEngine;

namespace GameBase.Projectiles
{
    public interface IProjectileTarget
    {
        Vector3 Center { get; }
        int InstanceID { get; }
        float Radius { get; }
        
    }
}

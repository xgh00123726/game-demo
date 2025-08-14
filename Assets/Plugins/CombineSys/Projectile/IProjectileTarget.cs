using GameBase.Flyings;
using UnityEngine;

namespace Combines.Projectiles
{
    public interface IProjectileTarget
    {
        Vector3 Center { get; }
        int InstanceID { get; }
        float Radius { get; }
        
    }
}

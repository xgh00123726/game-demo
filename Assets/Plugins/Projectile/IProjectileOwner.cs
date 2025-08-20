using UnityEngine;

namespace GameBase.Projectiles
{
    public interface IProjectileOwner
    {
        Vector3 HandPosition { get; }
    }
}

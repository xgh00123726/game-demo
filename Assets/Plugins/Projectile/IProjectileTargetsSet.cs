using GameBase.Math;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Projectiles
{
    public interface IProjectileTargetsSet
    {
        IEnumerable<IProjectileTarget> TargetsInShape(IShape2D shape);
        IProjectileTarget NearestTarget(Vector3 center, float radius);
    }
}

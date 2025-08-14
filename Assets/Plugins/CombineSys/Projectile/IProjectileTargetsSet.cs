using GameBase.Math;
using System.Collections.Generic;

namespace Combines.Projectiles
{
    public interface IProjectileTargetsSet
    {
        IEnumerable<IProjectileTarget> TargetsInShape(IShape2D shape);
    }
}

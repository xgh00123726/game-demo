using GameBase.Math;
using System.Collections.Generic;

namespace GameBase.Projectile
{
    public interface IProjectileTargetSys
    {
        public delegate bool TargetFilter(IProjectileTarget e);
        LinkedList<IProjectileTarget> TargetsInShape(IShape2D shape, TargetFilter filter);
    }
}

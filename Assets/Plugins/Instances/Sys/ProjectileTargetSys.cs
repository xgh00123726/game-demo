using GameBase.Math;
using GameBase.Projectile;
using System.Collections.Generic;

namespace GameBase.Instance
{
    public class ProjectileTargetSys : IProjectileTargetSys
    {
        LinkedList<IProjectileTarget> IProjectileTargetSys.TargetsInShape(IShape2D shape)
        {
            LinkedList<Creature.Creature> cs;
            cs = CreatureSys.Instance.CreaturesInShape(shape, null);

            var ret = new LinkedList<IProjectileTarget>();
            foreach (var e in cs)
            {
                if (e is IProjectileTarget target)
                {
                    ret.AddLast(target);
                }
            }
            return ret;
        }
    }
}

using GameBase.Math;
using GameBase.Projectile;
using GameBase.Tools;
using System.Collections.Generic;

namespace GameBase.Instance
{
    public class ProjectileTargetSys : IProjectileTargetSys
    {
        LinkedList<IProjectileTarget> IProjectileTargetSys.TargetsInShape(IShape2D shape, IProjectileTargetSys.TargetFilter filter)
        {
            LinkedList<Creature.Creature> cs;
            if (filter != null)
            {
                cs = CreatureSys.CreaturesInShape(shape, (Creature.Creature e) =>
                {
                    return filter?.Invoke(e) == true;
                });
            }
            else
            {
                cs = CreatureSys.CreaturesInShape(shape, null);
            }

            var ret = new LinkedList<IProjectileTarget>();
            foreach (var e in cs)
            {
                ret.AddLast(e);
            }
            return ret;
        }
    }
}

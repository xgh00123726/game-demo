using GameBase.Creature;
using GameBase.Math;
using GameBase.Flyings;
using System.Collections.Generic;
using Combines.Projectiles;


namespace Instance.GameSys
{
    public class SimplestProjectileTargetSys : IProjectileTargetsSet
    {
        private static SimplestProjectileTargetSys _instance;
        public static SimplestProjectileTargetSys Instance
        {
            get
            {
                _instance ??= new SimplestProjectileTargetSys();
                return _instance;
            }
        }
        IEnumerable<IProjectileTarget> IProjectileTargetsSet.TargetsInShape(IShape2D shape)
        {
            LinkedList<IProjectileTarget> ret = new();
            foreach(var c in CreatureSys.Instance.Entities)
            {
                if (c is IProjectileTarget tar)
                {
                    if (shape.Contains(tar.Center.x, tar.Center.z))
                    {
                        ret.AddLast(tar);
                    }
                }
            }

            return ret;
        }
    }
}

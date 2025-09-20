using GameBase.Creatures;
using GameBase.Math;
using System.Collections.Generic;
using GameBase.Projectiles;
using UnityEngine;


namespace Constructor.Projectiles
{
    public class CommonTargetSet : IProjectileTargetsSet
    {
        private static CommonTargetSet _instance;
        public static CommonTargetSet Instance
        {
            get
            {
                _instance ??= new CommonTargetSet();
                return _instance;
            }
        }

        IEnumerable<IProjectileTarget> IProjectileTargetsSet.TargetsInShape(IShape2D shape)
        {
            LinkedList<IProjectileTarget> ret = new();
            foreach(var c in CreatureSys.Instance.Entities)
            {
                if (c.tag != Tag.CommonCreature) continue;

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

        IProjectileTarget IProjectileTargetsSet.NearestTarget(Vector3 center, float radius)
        {
            float minDis = radius;
            IProjectileTarget ret = null;
            foreach (var c in CreatureSys.Instance.Entities)
            {
                if (c.tag != Tag.CommonCreature) continue;

                if (c is IProjectileTarget tar)
                {
                    float dis = GMath.GameDistance(center, tar.Center);
                    if (dis <= minDis)
                    {
                        minDis = dis;
                        ret = tar;
                    }
                }
            }

            return ret;
        }
    }
}

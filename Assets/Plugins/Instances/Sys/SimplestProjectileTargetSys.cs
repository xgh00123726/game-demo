using GameBase.Creatures;
using GameBase.Math;
using GameBase.Flyings;
using System.Collections.Generic;
using Combines.Projectiles;
using UnityEngine;


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
        public IEnumerable<IProjectileTarget> TargetsInShape(IShape2D shape)
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

        public IProjectileTarget NearestTarget(Vector3 center, float radius)
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

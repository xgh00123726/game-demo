using GameBase.Creatures;
using GameBase.Math;
using System.Collections.Generic;
using GameBase.Triggers;
using UnityEngine;
using GameBase.Tools;


namespace Constructor.Triggers
{
    public class CommonTargetSet : Singleton<CommonTargetSet>, ITriggerTargetsSet
    {
        List<ITriggerTarget> ITriggerTargetsSet.TargetsInShape(IShape2D shape, GameBase.Triggers.CampType camp)
        {
            List<ITriggerTarget> ret = new();
            foreach(var c in CreatureSys.Instance.Entities)
            {
                if (SuperEnum.NoOverlap((uint)c.camp, (uint)camp)) continue;

                if (shape.Contains(c.Position.x, c.Position.z))
                {
                    ret.Add(c);
                }
            }

            return ret;
        }

        public Creature NearestTarget(Vector3 center, float radius, GameBase.Creatures.CampType camp)
        {
            float minDis = radius;
            Creature ret = null;
            foreach (var c in CreatureSys.Instance.Entities)
            {
                if (SuperEnum.NoOverlap((uint)c.camp, (uint)camp)) continue;

                float dis = GMath.GameDistance(center, c.Position);
                if (dis <= minDis)
                {
                    minDis = dis;
                    ret = c;
                }
            }

            return ret;
        }

        public Creature NearestTarget(Vector3 center, float radius, GameBase.Triggers.CampType camp)
        {
            return NearestTarget(center, radius, (GameBase.Creatures.CampType)camp);
        }

        public Creature NearestTarget(Vector3 center, float radius, GameBase.Spells.CampType camp)
        {
            return NearestTarget(center, radius, (GameBase.Creatures.CampType)camp);
        }
    }
}

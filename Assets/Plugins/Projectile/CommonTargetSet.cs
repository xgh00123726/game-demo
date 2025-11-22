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
        List<ITriggerTarget> ITriggerTargetsSet.TargetsInShape(IShape2D shape, ITriggerCamp camp)
        {
            List<ITriggerTarget> ret = new();
            foreach(var c in CreatureSys.Instance.Entities)
            {
                if (c.Camp.And(camp.ToUint()).IsNone())
                {
                    continue;
                }

                if (shape.Contains(c.Position))
                {
                    ret.Add(c);
                }
            }

            return ret;
        }

        public Creature NearestTarget(Vector3 center, float radius, Camp camp)
        {
            float minDis = radius;
            Creature ret = null;
            foreach (var c in CreatureSys.Instance.Entities)
            {
                if (c.Camp.And(camp).IsNone())
                {
                    continue;
                }

                float dis = GMath.GameDistance(center, c.Position);
                if (dis <= minDis)
                {
                    minDis = dis;
                    ret = c;
                }
            }

            return ret;
        }
    }
}

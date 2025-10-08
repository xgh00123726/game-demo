using GameBase.Creatures;
using GameBase.Math;
using System.Collections.Generic;
using GameBase.Triggers;
using UnityEngine;


namespace Constructor.Triggers
{
    public class CommonTargetSet : ITriggerTargetsSet
    {
        private static CommonTargetSet _instance = new();
        public static CommonTargetSet Instance => _instance;

        IEnumerable<ITriggerTarget> ITriggerTargetsSet.TargetsInShape(IShape2D shape)
        {
            LinkedList<ITriggerTarget> ret = new();
            foreach(var c in CreatureSys.Instance.Entities)
            {
                if (c.tag != CreatureTag.CommonCreature) continue;

                if (c is ITriggerTarget tar)
                {
                    if (shape.Contains(tar.Center.x, tar.Center.z))
                    {
                        ret.AddLast(tar);
                    }
                }
            }

            return ret;
        }

        ITriggerTarget ITriggerTargetsSet.NearestTarget(Vector3 center, float radius)
        {
            float minDis = radius;
            ITriggerTarget ret = null;
            foreach (var c in CreatureSys.Instance.Entities)
            {
                if (c.tag != CreatureTag.CommonCreature) continue;

                if (c is ITriggerTarget tar)
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

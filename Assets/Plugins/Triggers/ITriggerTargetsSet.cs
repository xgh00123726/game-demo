using GameBase.Math;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Triggers
{
    public interface ITriggerTargetsSet
    {
        IEnumerable<ITriggerTarget> TargetsInShape(IShape2D shape);
        ITriggerTarget NearestTarget(Vector3 center, float radius);
    }
}

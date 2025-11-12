using GameBase.Math;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Triggers
{
    public interface ITriggerTargetsSet
    {
        List<ITriggerTarget> TargetsInShape(IShape2D shape, ITriggerCamp camp);
    }
}

using GameBase.Flyings;
using GameBase.Math;
using GameBase.Triggers;
using GameBase.Tools;
using NReco.Csv;
using System;
using UnityEngine;
using GameBase.EntitySystem;

namespace Constructor.Triggers
{
    public struct AreaCircleTriggerData
    {
        public int maxEffectTimes;
        public bool hasWhite;
        public float radius;
        public int damage;
        public TargetSetType targetSetType;
    }
    public class AreaCircleTrigger : SealedConstructor<AreaCircleTriggerData, Trigger, AreaCircleTrigger>
    {
        protected override string RelativePath => "Trigger/AreaCircleTrigger.csv";

        protected override Trigger GetFromData(in AreaCircleTriggerData data)
        {
            var e = TriggerSys.Instance.NewEntity();
            e.maxeffectTimes = data.maxEffectTimes;
            e.hasWhite = data.hasWhite;
            e.shape = new GMath.Circle(Vector2.zero, data.radius);
            e.targetsSet = TargetSetFactorary.Get(data.targetSetType);
            e.action = Constructor.Triggers.Action.TriggerActionFactory.Instance.Get(Triggers.Action.Type.Damage, data.damage);
            return e;
        }
    }
}

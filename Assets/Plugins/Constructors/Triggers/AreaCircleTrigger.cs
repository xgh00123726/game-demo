using GameBase.Math;
using GameBase.Triggers;
using UnityEngine;
using GameBase.EntitySystem;

namespace Constructor.Triggers
{
    public struct AreaCircleTriggerData
    {
        public float radius;
        public string effectName;
        public string audioName;
    }
    public class AreaCircleTrigger : SealedConstructor<AreaCircleTriggerData, Trigger, AreaCircleTrigger>
    {
        protected override string RelativePath => "Trigger/AreaCircleTrigger.csv";

        protected override Trigger GetFromData(in AreaCircleTriggerData data)
        {
            var e = TriggerSys.Instance.NewEntity();
            e.shape = new GMath.Circle(Vector2.zero, data.radius);

            e.trigEffect = data.effectName;
            e.trigAudio = data.audioName;

            return e;
        }
    }
}

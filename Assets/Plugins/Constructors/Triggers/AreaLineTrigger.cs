using GameBase.Math;
using GameBase.Triggers;
using UnityEngine;
using GameBase.EntitySystem;

namespace Constructor.Triggers
{
    public struct AreaLineTriggerData
    {
        public float width;
        public float length;
        public float pivot;
        public string effectName;
        public string audioName;
    }
    public class AreaLineTrigger : SealedConstructor<AreaLineTriggerData, Trigger, AreaLineTrigger>
    {
        protected override string RelativePath => "Trigger/AreaLineTrigger.csv";

        protected override Trigger GetFromData(in AreaLineTriggerData data)
        {
            var e = TriggerSys.Instance.NewEntity();
            e.shape = new GMath.Line(Vector2.zero, Vector2.one, data.length)
            {
                width = data.width,
                pivot = data.pivot
            };

            e.trigEffect = data.effectName;
            e.trigAudio = data.audioName;

            return e;
        }
    }
}

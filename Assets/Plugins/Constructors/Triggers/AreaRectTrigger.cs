using GameBase.EntitySystem;
using GameBase.Math;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Triggers
{
    public struct AreaRectTriggerData
    {
        public int width;
        public string effectName;
        public string audioName;
    }
    public class AreaRectTrigger : SealedConstructor<AreaRectTriggerData, Trigger, AreaRectTrigger>
    {
        protected override string RelativePath => "Trigger/AreaRectTrigger.csv";

        protected override Trigger GetFromData(in AreaRectTriggerData data)
        {
            var e = TriggerSys.Instance.NewEntity();
            e.shape = new GMath.Rect2D(data.width, 1f);

            e.trigEffect = data.effectName;
            e.trigAudio = data.audioName;

            return e;
        }
    }
}

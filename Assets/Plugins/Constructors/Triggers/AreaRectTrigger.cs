using GameBase.Math;
using GameBase.Triggers;
using GameBase.EntitySystem;

namespace Constructor.Triggers
{
    public struct AreaRectTriggerData
    {
        public int width;
        public int damage;
    }
    public class AreaRectTrigger : SealedConstructor<AreaRectTriggerData, Trigger, AreaRectTrigger>
    {
        protected override string RelativePath => "Trigger/AreaRectTrigger.csv";

        protected override Trigger GetFromData(in AreaRectTriggerData data)
        {
            var e = TriggerSys.Instance.NewEntity();
            e.maxeffectTimes = 100;
            e.hasWhite = true;
            e.shape = new GMath.Rect2D(data.width, 1f);
            e.targetsSet = TargetSetFactorary.Get(TargetSetType.Common);
            var damage = data.damage;
            e.action = Constructor.Triggers.Action.TriggerActionFactory.Instance.Get(Triggers.Action.Type.Damage, data.damage);

            return e;
        }
    }
}

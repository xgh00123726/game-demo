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
    public class AreaRectTrigger : EntityConstructor<AreaRectTriggerData, Trigger, TriggerSys, AreaRectTrigger>
    {
        protected override string RelativePath => "Trigger/AreaRectTrigger.csv";

        protected override TriggerSys SysInstance => TriggerSys.Instance;

        protected override void ESet(Trigger e, in AreaRectTriggerData data)
        {
            e.maxeffectTimes = 100;
            e.hasWhite = true;
            e.shape = new GMath.Rect2D(data.width, 1f);
            e.targetsSet = TargetSetFactorary.Get(TargetSetType.Common);
            var damage = data.damage;
            e.action = Constructor.Triggers.Action.Factory.Instance.Get(Triggers.Action.Type.Damage, data.damage);
        }
    }
}

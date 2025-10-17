using GameBase.Triggers;
using NReco.Csv;
using GameBase.EntitySystem;

namespace Constructor.Triggers
{
    public struct SingleTriggerData
    {
        public int damage;
    }
    public class SingleTrigger : SealedConstructor<SingleTriggerData, Trigger, SingleTrigger>
    {
        protected override string RelativePath => "Trigger/SingleTrigger.csv";

        protected override Trigger GetFromData(in SingleTriggerData data)
        {
            var e = TriggerSys.Instance.NewEntity();
            e.maxeffectTimes = 1;
            e.hasWhite = false;
            var damage = data.damage;
            e.action = Constructor.Triggers.Action.TriggerActionFactory.Instance.Get(Triggers.Action.Type.Damage, data.damage);
            return e;
        }
    }
}

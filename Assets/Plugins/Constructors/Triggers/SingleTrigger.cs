using GameBase.Triggers;
using NReco.Csv;
using GameBase.EntitySystem;

namespace Constructor.Triggers
{
    public struct SingleTriggerData
    {
        public int damage;
    }
    public class SingleTrigger : EntityConstructor<SingleTriggerData, Trigger, TriggerSys, SingleTrigger>
    {
        protected override string RelativePath => "Projectile/SingleTrigger.csv";

        protected override TriggerSys SysInstance => TriggerSys.Instance;

        protected override void ESet(Trigger e, in SingleTriggerData data)
        {
            e.maxeffectTimes = 1;
            e.hasWhite = false;
            var damage = data.damage;
            e.action = Constructor.Triggers.Action.Factory.Instance.Get(Triggers.Action.Type.Damage, data.damage);
        }
    }
}

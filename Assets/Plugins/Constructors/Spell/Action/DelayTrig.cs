using Constructor.Triggers;
using GameBase.Effects;
using GameBase.EntitySystem;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.Triggers;

namespace Constructor.Spells.Action
{
    public struct DelayTrigData
    {
        public float delay;
        public Triggers.Type triggerType;
        public int triggerID;
        public Effects.Type effectType;
        public int effectID;
        public TargetSetType targetSetType;
    }

    public class DelayTrig : ISpellAction
    {
        public DelayTrigData data;
        bool ISpellAction.CastAction(Spell spell)
        {
            Timer.AddTask(data.delay, () =>
            {
                var t = Triggers.Factory.Instance.Get(data.triggerType, data.triggerID);
                t.targetsSet = TargetSetFactorary.Get(data.targetSetType);
                var e = Effects.Factory.Instance.Get(data.effectType, data.effectID);
            });


            return true;
        }
    }

    public class DelayTrigCon : BaseConstructor<DelayTrigData, DelayTrig, DelayTrigCon>
    {

        protected override string RelativePath => "Spell/Action/DelayTrigCon.csv";

        protected override DelayTrig Get()
        {
            return new DelayTrig();
        }

        protected override void Set(DelayTrig e, in DelayTrigData data)
        {
            e.data = data;
        }
    }
}

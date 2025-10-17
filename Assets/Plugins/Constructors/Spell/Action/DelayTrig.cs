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
                var t = Triggers.TriggerFactory.Instance.Get(data.triggerType, data.triggerID);
                t.targetsSet = TargetSetFactorary.Get(data.targetSetType);
                var e = Effects.EffectFactory.Instance.Get(data.effectType, data.effectID);
            });


            return true;
        }
    }

    public class DelayTrigCon : SealedConstructor<DelayTrigData, DelayTrig, DelayTrigCon>
    {
        protected override string RelativePath => "Spell/Action/DelayTrigCon.csv";

        protected override DelayTrig GetFromData(in DelayTrigData data)
        {
            var e = new DelayTrig();
            e.data = data;
            return e;
        }
    }
}

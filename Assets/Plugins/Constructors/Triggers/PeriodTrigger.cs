using Constructor.Effects;
using GameBase.EntitySystem;
using GameBase.Spells;
using GameBase.Triggers;

namespace Constructor.Triggers
{
    public struct PeriodTriggerData
    {
        public float period;
        public TrigStyle trigStyle;
        public EffectType effectType;
        public int effectID;
        public TargetSetType targetSetType;
    }

    public class PeriodTrigger : SealedConstructor<PeriodTriggerData, Trigger, PeriodTrigger>
    {
        protected override string RelativePath => "Trigger/PeriodTrigger.csv";

        protected override Trigger GetFromData(in PeriodTriggerData data)
        {
            var e = TriggerSys.Instance.NewEntity();
            e.targetsSet = TargetSetFactorary.Get(data.targetSetType);
            e.trigPeriod = data.period;
            e.trigStyle = data.trigStyle;
            var effectType = data.effectType;
            var effectID = data.effectID;
            e.OnTrig = () =>
            {
                Effects.EffectFactory.Instance.Get(effectType, effectID);
            };
            return e;
        }
    }
}

using GameBase.EntitySystem;
using GameBase.Spells;
using GameBase.Triggers;

namespace Constructor.Triggers
{
    public struct PeriodTriggerData
    {
        public float period;
        public TrigStyle trigStyle;
        public Effects.Type effectType;
        public int effectID;
        public TargetSetType targetSetType;
    }

    public class PeriodTrigger : EntityConstructor<PeriodTriggerData, Trigger, TriggerSys, PeriodTrigger>
    {
        protected override string RelativePath => "Trigger/PeriodTrigger.csv";

        protected override TriggerSys SysInstance => TriggerSys.Instance;

        protected override void ESet(Trigger e, in PeriodTriggerData data)
        {
            e.targetsSet = TargetSetFactorary.Get(data.targetSetType);
            e.trigPeriod = data.period;
            e.trigStyle = data.trigStyle;
            var effectType = data.effectType;
            var effectID = data.effectID;
            e.OnTrig = () =>
            {
                Effects.Factory.Instance.Get(effectType, effectID);
            };
        }
    }
}

using GameBase.Triggers;
using GameBase.EntitySystem;
using System.Collections.Generic;

namespace Constructor.Triggers
{
    public enum TriggerType
    {
        AreaCircleTrigger,
        AreaRectTrigger,
        SingleTrigger
    }
    public class TriggerFactory : ConstructorFactory<TriggerType, Trigger, TriggerFactory>
    {
        protected override Dictionary<TriggerType, System.Func<int, Trigger>> GetConstructorGetDict()
        {
            return new()
            {
                {TriggerType.AreaCircleTrigger, AreaCircleTrigger.Instance.Get},
                {TriggerType.AreaRectTrigger, AreaRectTrigger.Instance.Get},
                {TriggerType.SingleTrigger, SingleTrigger.Instance.Get},
            };
        }
    }
}

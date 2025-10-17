using GameBase.Triggers;
using GameBase.EntitySystem;
using System.Collections.Generic;

namespace Constructor.Triggers
{
    public enum Type
    {
        AreaCircleTrigger,
        AreaRectTrigger,
        SingleTrigger
    }
    public class TriggerFactory : ConstructorFactory<Type, Trigger, TriggerFactory>
    {
        protected override Dictionary<Type, System.Func<int, Trigger>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.AreaCircleTrigger, AreaCircleTrigger.Instance.Get},
                {Type.AreaRectTrigger, AreaRectTrigger.Instance.Get},
                {Type.SingleTrigger, SingleTrigger.Instance.Get},
            };
        }
    }
}

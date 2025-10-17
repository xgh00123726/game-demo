using GameBase.Triggers;
using GameBase.EntitySystem;
using System.Collections.Generic;

namespace Constructor.Triggers.Action
{
    public enum Type
    {
        Damage,
    }
    public class TriggerActionFactory : ConstructorFactory<Type, ITriggerAction, TriggerActionFactory>
    {
        protected override Dictionary<Type, System.Func<int, ITriggerAction>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.Damage, DamageCon.Get },
            };
        }
    }
}

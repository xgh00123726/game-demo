using GameBase.Effects;
using GameBase.EntitySystem;
using System.Collections.Generic;

namespace Constructor.Effects
{
    public enum Type
    {
        Common,
    }
    public class EffectFactory : ConstructorFactory<Type, Effect, EffectFactory>
    {
        protected override Dictionary<Type, System.Func<int, Effect>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.Common, Common.Instance.Get }
            };
        }
    }
}

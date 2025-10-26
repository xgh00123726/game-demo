using GameBase.Effects;
using GameBase.EntitySystem;
using System.Collections.Generic;

namespace Constructor.Effects
{
    public enum EffectType
    {
        None,
        Common,
    }
    public class EffectFactory : ConstructorFactory<EffectType, Effect, EffectFactory>
    {
        protected override Dictionary<EffectType, System.Func<int, Effect>> GetConstructorGetDict()
        {
            return new()
            {
                { EffectType.None, static (int index) => null},
                { EffectType.Common, Common.Instance.Get }
            };
        }
    }
}

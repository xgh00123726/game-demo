using System.Collections.Generic;

namespace Constructor.Spells.Action.Modifyables.Modifier
{
    public enum Type
    {
        FireDisfuseModifier,
        FlyingDistanceModifier,
        FlyingNumModifier,
        MultipleModifier
    }
    public class SpellActionModifierFactory : ConstructorFactory<Type, BaseModifier, SpellActionModifierFactory>
    {
        protected override Dictionary<Type, System.Func<int, BaseModifier>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.FireDisfuseModifier, FireDisfuseModifierCon.Instance.Get },
                {Type.FlyingDistanceModifier, FlyingDistanceModifierCon.Instance.Get },
                {Type.FlyingNumModifier, (val) => new FlyingNumModifier(val) },
                {Type.MultipleModifier, (val) => new MultipleModifier(val) },
            };
        }
    }
}



using System.Collections.Generic;
using GameBase.EntitySystem;

namespace Constructor.Spells.Action
{
    public enum ModifierType
    {
        FireDisfuseModifier,
        FlyingDistanceModifier,
        FlyingNumModifier,
        MultipleModifier
    }
    public class SpellActionModifierFactory : ConstructorFactory<ModifierType, BaseModifier, SpellActionModifierFactory>
    {
        protected override Dictionary<ModifierType, System.Func<int, BaseModifier>> GetConstructorGetDict()
        {
            return new()
            {
                {ModifierType.FireDisfuseModifier, FireDisfuseModifierCon.Instance.Get },
                {ModifierType.FlyingDistanceModifier, FlyingDistanceModifierCon.Instance.Get },
                {ModifierType.FlyingNumModifier, (val) => new FlyingNumModifier(val) },
                {ModifierType.MultipleModifier, (val) => new MultipleModifier(val) },
            };
        }
    }
}



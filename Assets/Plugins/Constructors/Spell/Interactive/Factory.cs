using GameBase.Spells;
using GameBase.EntitySystem;
using System;
using System.Collections.Generic;

namespace Constructor.Spells.Interactive
{
    public enum Type
    {
        KeyCommon,
        KeyFast,
        Always,
        Invokable
    }
    public class Factory : ConstructorFactory<Type, ISpellInteractive, Factory>
    {
        protected override Dictionary<Type, Func<int, ISpellInteractive>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.Invokable, HasIndicator.Get },
            };
        }
    }
}

using GameBase.Spells;
using GameBase.EntitySystem;
using System;
using System.Collections.Generic;

namespace Constructor.Spells.Interactive
{
    public enum Type
    {
        DotExternalSet
    }
    public class InteractiveFactory : ConstructorFactory<Type, ISpellInteractive, InteractiveFactory>
    {
        protected override Dictionary<Type, Func<int, ISpellInteractive>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.DotExternalSet, DotExternalSetCon.Instance.Get },
            };
        }
    }
}

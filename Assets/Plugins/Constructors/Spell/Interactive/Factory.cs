using GameBase.Spells;
using GameBase.EntitySystem;
using System;
using System.Collections.Generic;

namespace Constructor.Spells.Interactive
{
    public enum Type
    {
        DotInternalSet,
        MouseInput,
        DotExternalSet
    }
    public class Factory : ConstructorFactory<Type, ISpellInteractive, Factory>
    {
        protected override Dictionary<Type, Func<int, ISpellInteractive>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.MouseInput, MouseInput.Get },
                {Type.DotInternalSet, DotInternalSet.Get },
                {Type.DotExternalSet, DotExternalSetCon.Instance.Get },
            };
        }
    }
}

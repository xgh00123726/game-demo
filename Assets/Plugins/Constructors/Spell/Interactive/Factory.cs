using Constructor.Spells.Action;
using GameBase.Spells;
using GameBase.Tools;
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
                {Type.Always, AlwaysCon.Instance.Get },
                {Type.Invokable, Invokable.Get },
            };
        }
    }
}

using Constructor.Spells.Action;
using GameBase.Spells;
using System;
using System.Collections.Generic;

namespace Constructor.Spells.Interactive
{
    public enum Type
    {
        KeyCommon,
    }
    public class Factory : ConstructorFactory<Type, ISpellInteractive, Factory>
    {
        protected override Dictionary<Type, Func<int, ISpellInteractive>> ConstructorGetDict =>
            new()
            {
                {Type.KeyCommon, KeyCommon.Instance.Get },
            };
    }
}

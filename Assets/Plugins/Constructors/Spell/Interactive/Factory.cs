using Constructor.Spells.Action;
using GameBase.Spells;
using System;
using System.Collections.Generic;

namespace Constructor.Spells.Interactive
{
    public enum Type
    {
        KeyCommon,
        KeyFast,
        Always,
    }
    public class Factory : ConstructorFactory<Type, ISpellInteractive, Factory>
    {
        protected override Dictionary<Type, Func<int, ISpellInteractive>> ConstructorGetDict =>
            new()
            {
                {Type.KeyCommon, KeyCommonCon.Instance.Get },
                {Type.KeyFast, KeyFastCon.Instance.Get },
                {Type.Always, AlwaysCon.Instance.Get },
            };
    }
}

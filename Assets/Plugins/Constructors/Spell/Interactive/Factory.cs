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
    }
    public class Factory : ConstructorFactory<Type, ISpellInteractive, Factory>
    {
        protected override Dictionary<Type, Func<int, ISpellInteractive>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.KeyCommon, KeyCommonCon.Instance.Get },
                {Type.KeyFast, KeyFastCon.Instance.Get },
                {Type.Always, AlwaysCon.Instance.Get },
            };
        }

        public void SetHotKey(ISpellInteractive interactive, KeyFunction function)
        {
            if (interactive is KeyCommon keyCommon)
            {
                keyCommon.readyKey = function;
            }
            else if (interactive is KeyFast keyFast)
            {
                keyFast.castKey = function;
            }
        }
    }
}

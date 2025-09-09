using NReco.Csv;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Constructor.Spells.Action.Modifyables.Modifier
{
    public enum Type
    {
        FireDisfuseModifier,
        FlyingDistanceModifier,
        FlyingNumModifier,
        MultipleModifier
    }
    public class Factory : ConstructorFactory<Type, BaseModifier, Factory>
    {
        protected override Dictionary<Type, System.Func<int, BaseModifier>> ConstructorGetDict =>
            new()
            {
                {Type.FireDisfuseModifier, FireDisfuseModifierCon.Instance.Get },
                {Type.FlyingDistanceModifier, FlyingDistanceModifierCon.Instance.Get },
                {Type.FlyingNumModifier, (val) => new FlyingNumModifier(val) },
                {Type.MultipleModifier, (val) => new MultipleModifier(val) },
            };
    }
}



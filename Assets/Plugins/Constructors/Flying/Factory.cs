using GameBase.Flyings;
using System.Collections.Generic;

namespace Constructor.Flyings
{
    public enum Type
    {
        Common,
    }
    public class Factory : ConstructorFactory<Type, Flying, Factory>
    {
        protected override Dictionary<Type, System.Func<int, Flying>> ConstructorGetDict =>
            new()
            {
                { Type.Common, Common.Instance.Get},
            };
    }
}

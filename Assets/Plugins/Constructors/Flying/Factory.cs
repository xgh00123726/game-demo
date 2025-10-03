using GameBase.Flyings;
using GameBase.EntitySystem;
using System.Collections.Generic;

namespace Constructor.Flyings
{
    public enum Type
    {
        Common,
    }
    public class Factory : ConstructorFactory<Type, Flying, Factory>
    {
        protected override Dictionary<Type, System.Func<int, Flying>> GetConstructorGetDict()
        {
            return new()
            {
                { Type.Common, Common.Instance.Get},
            };
        }
    }
}

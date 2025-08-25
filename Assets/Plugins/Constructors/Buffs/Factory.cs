using GameBase.Buffs;
using System.Collections.Generic;

namespace Constructor.Buffs
{
    public enum Type
    {
        Common,
    }
    public class Factory : ConstructorFactory<Type, Buff, Factory>
    {
        protected override Dictionary<Type, System.Func<int, Buff>> ConstructorGetDict =>
            new()
            {
                {Type.Common, Common.Instance.Get }
            };
    }
}

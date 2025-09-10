using GameBase.Creatures;
using System.Collections.Generic;

namespace Constructor.Creatures
{
    public enum Type
    {
        Common,
    }
    public class Factory : ConstructorFactory<Type, Creature, Factory>
    {
        protected override Dictionary<Type, System.Func<int, Creature>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.Common, Common.Instance.Get }
            };
        }
    }
}

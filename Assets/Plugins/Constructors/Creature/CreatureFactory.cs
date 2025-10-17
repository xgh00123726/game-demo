using GameBase.Creatures;
using GameBase.EntitySystem;
using System.Collections.Generic;

namespace Constructor.Creatures
{
    public enum Type
    {
        Common,
    }
    public class CreatureFactory : ConstructorFactory<Type, Creature, CreatureFactory>
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

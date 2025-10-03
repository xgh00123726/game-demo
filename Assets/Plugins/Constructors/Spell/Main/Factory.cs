using GameBase.Spells;
using System.Collections.Generic;
using GameBase.EntitySystem;

namespace Constructor.Spells.Main
{
    public enum Type
    {
        Common,
    }
    public class Factory : ConstructorFactory<Type, Spell, Factory>
    {
        protected override Dictionary<Type, System.Func<int, Spell>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.Common, Common.Instance.Get },
            };
        }
    }
}

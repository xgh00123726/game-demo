using GameBase.Effects;
using System.Collections.Generic;

namespace Constructor.Effects
{
    public enum Type
    {
        Common,
    }
    public class Factory : ConstructorFactory<Type, Effect, Factory>
    {
        protected override Dictionary<Type, System.Func<int, Effect>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.Common, Common.Instance.Get }
            };
        }
    }
}

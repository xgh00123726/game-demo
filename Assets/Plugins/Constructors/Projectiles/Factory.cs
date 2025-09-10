using GameBase.Projectiles;
using System.Collections.Generic;

namespace Constructor.Projectiles
{
    public enum Type
    {
        AreaCircleTrigger,
        AreaRectTrigger,
        SingleTrigger
    }
    public class Factory : ConstructorFactory<Type, Projectile, Factory>
    {
        protected override Dictionary<Type, System.Func<int, Projectile>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.AreaCircleTrigger, AreaCircleTrigger.Instance.Get},
                {Type.AreaRectTrigger, AreaRectTrigger.Instance.Get},
                {Type.SingleTrigger, SingleTrigger.Instance.Get},
            };
        }
    }
}

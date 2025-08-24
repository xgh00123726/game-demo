using GameBase.GEffects;
using GameBase.Projectiles;
using System.Collections.Generic;

namespace Constructor.GEffects
{
    public enum Type
    {
        Damage
    }
    public class Factory : ConstructorFactory<Type, GEffect<IProjectileOwner, IProjectileTarget>, Factory>
    {
        protected override Dictionary<Type, System.Func<int, GEffect<IProjectileOwner, IProjectileTarget>>> ConstructorGetDict =>
            new()
            {
                {Type.Damage, Damage.Instance.Get }
            };
    }
}

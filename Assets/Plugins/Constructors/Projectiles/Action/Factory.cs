using GameBase.Projectiles;
using System.Collections.Generic;

namespace Constructor.Projectiles.Action
{
    public enum Type
    {
        Damage,
    }
    public class Factory : ConstructorFactory<Type, IProjectileAction, Factory>
    {
        protected override Dictionary<Type, System.Func<int, IProjectileAction>> ConstructorGetDict =>
            new()
            {
                {Type.Damage, DamageCon.Get },
            };
    }
}

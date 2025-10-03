using GameBase.Projectiles;
using GameBase.EntitySystem;
using System.Collections.Generic;

namespace Constructor.Projectiles.Action
{
    public enum Type
    {
        Damage,
    }
    public class Factory : ConstructorFactory<Type, IProjectileAction, Factory>
    {
        protected override Dictionary<Type, System.Func<int, IProjectileAction>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.Damage, DamageCon.Get },
            };
        }
    }
}

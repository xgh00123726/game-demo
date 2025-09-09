using System;

namespace GameBase.Projectiles
{
    public interface IProjectileAction
    {
        void Effect(Projectile e, IProjectileTarget target);
    }
}

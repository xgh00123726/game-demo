using UnityEngine;
using GameBase.Tools;
using GameBase.Creature;
using GameBase.GCamera;
using GameBase.Flyings;
using GameBase.Math;
using GameBase.Instance;
using Instance.GameSys;
using Combines.Projectiles;
using Instance.GEffects;

namespace Constructor.Projectiles
{
    public class ProjectileRegister
    {
        private static Projectile ProjectileGen_0()
        {
            var proj = ProjectileSys.Instance.NewEntity<Projectile>();

            proj.maxeffectTimes = 1;

            return proj;
        }

        private static Projectile ProjectileGen_1()
        {
            var proj = ProjectileSys.Instance.NewEntity<Projectile>();

            proj.maxeffectTimes = 999;
            proj.hasWhite = true;
            proj.shape = new GMath.Circle(Vector2.zero, 1f);
            proj.targetsSet = SimplestProjectileTargetSys.Instance;

            return proj;
        }

        private static Projectile ProjectileGen_2()
        {
            var proj = ProjectileSys.Instance.NewEntity<Projectile>();

            proj.maxeffectTimes = 1;

            return proj;
        }

        private static Projectile ProjectileGen_3()
        {
            var proj = ProjectileSys.Instance.NewEntity<Projectile>();

            proj.maxeffectTimes = 999;
            proj.shape = new GMath.Circle(Vector2.zero, 1f);
            proj.targetsSet = SimplestProjectileTargetSys.Instance;

            return proj;
        }

        private static Projectile ProjectileGen_4()
        {
            var proj = ProjectileSys.Instance.NewEntity<Projectile>();

            proj.maxeffectTimes = 1;
            proj.hasWhite = true;
            proj.shape = new GMath.Circle(Vector2.zero, 1f);
            proj.targetsSet = SimplestProjectileTargetSys.Instance;
            proj.effectConstructor = () => GEDamage.New(-10);

            return proj;
        }

        public static void RegisterProjectileGenerator()
        {
            ProjectileSys.Instance.RegisterEntityGenerateDeletate(ProjectileGen_0);
            ProjectileSys.Instance.RegisterEntityGenerateDeletate(ProjectileGen_1);
            ProjectileSys.Instance.RegisterEntityGenerateDeletate(ProjectileGen_2);
            ProjectileSys.Instance.RegisterEntityGenerateDeletate(ProjectileGen_3);
            ProjectileSys.Instance.RegisterEntityGenerateDeletate(ProjectileGen_4);
        }
    }
}

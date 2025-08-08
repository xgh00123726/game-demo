using UnityEngine;
using GameBase.Tools;
using GameBase.Creature;
using GameBase.GCamera;
using GameBase.Projectile;
using GameBase.Math;
using GameBase.Instance;

namespace Constructor.Projectiles
{
    public class ProjectileRegister
    {
        private static Projectile ProjectileGen_0()
        {
            var proj = ProjectileSys.Instance.NewEntity<Projectile>();
            proj.ObjID = 2;
            proj.curveType = CurveFactory.CurveType.Tracer;
            proj.speed = 30f;
            proj.damage = 10f;
            proj.maxExistTime = 10f;
            proj.penetrate = 1;

            return proj;
        }

        private static Projectile ProjectileGen_1()
        {
            var proj = ProjectileSys.Instance.NewEntity<Projectile>();
            proj.ObjID = 3;
            proj.dest = CameraSys.MouseHitPosition;
            var curve = CurveFactory.CreateInstance(CurveFactory.CurveType.Slower, proj) as Slower;
            curve.factor = 0f;
            curve.slowDis = 1.5f;
            proj.curve = curve;
            proj.speed = 3f;
            proj.damage = 10f;
            proj.penetrate = 999;
            proj.whiteEnable = true;
            proj.maxExistTime = 10f;
            proj.shape = new GMath.Circle(Vector2.zero, 1f);
            XLogger.Instance.Log("proj 1 gen");

            return proj;
        }

        private static Projectile ProjectileGen_2()
        {
            var proj = ProjectileSys.Instance.NewEntity<Projectile>();
            proj.ObjID = 2;
            proj.curveType = CurveFactory.CurveType.Tracer;
            proj.speed = 30f;
            proj.damage = 5f;
            proj.maxExistTime = 10f;
            proj.penetrate = 1;
            proj.OnAliveFixed = (Projectile e) =>
            {
                if (ProjectileSys.Instance.FixedTick % 5 == 0 && !e.target.Exist)
                {
                    e.target = PossibleObj<IProjectileTarget>.New(
                        CreatureSys.Instance.NearestEntity<Enemy1>(e.Obj.transform.position, null, 0));
                }
            };

            return proj;
        }

        private static Projectile ProjectileGen_3()
        {
            var proj = ProjectileSys.Instance.NewEntity<Projectile>();
            proj.ObjID = 3;
            proj.dest = CameraSys.MouseHitPosition;
            proj.curveType = CurveFactory.CurveType.Liner;
            proj.speed = 3f;
            proj.damage = 10f;
            proj.penetrate = 999;
            proj.maxExistTime = 10f;
            proj.shape = new GMath.Circle(Vector2.zero, 1f);

            return proj;
        }

        public static void RegisterProjectileGenerator()
        {
            ProjectileSys.Instance.RegisterEntityGenerateDeletate(ProjectileGen_0);
            ProjectileSys.Instance.RegisterEntityGenerateDeletate(ProjectileGen_1);
            ProjectileSys.Instance.RegisterEntityGenerateDeletate(ProjectileGen_2);
            ProjectileSys.Instance.RegisterEntityGenerateDeletate(ProjectileGen_3);
        }
    }
}

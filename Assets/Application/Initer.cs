using GameBase.GCamera;
using GameBase.Instance;
using GameBase.Math;
using GameBase.Projectile;
using GameBase.Spell;
using GameBase.Tools;
using System;
using UnityEngine;
using XLua;
public class Initer : MonoBehaviour
{
    private Projectile ProjectileGen_0()
    {
        var proj = new Projectile();
        proj.bodyID = 2;
        proj.curveType = CurveFactory.CurveType.Tracer;
        proj.speed = 30f;
        proj.damage = 10f;
        proj.maxExistTime = 10f;
        proj.penetrate = 1;

        return proj;
    }

    private Projectile ProjectileGen_1()
    {
        var proj = new Projectile();
        proj.bodyID = 3;
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

        return proj;
    }

    private Projectile ProjectileGen_2()
    {
        var proj = new Projectile();
        proj.bodyID = 2;
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
                    CreatureSys.NearestEntity<Enemy1>(e.Obj.transform.position, null, 0));
            }
        };

        return proj;
    }

    private Projectile ProjectileGen_3()
    {
        var proj = new Projectile();
        proj.bodyID = 3;
        proj.dest = CameraSys.MouseHitPosition;
        proj.curveType = CurveFactory.CurveType.Liner;
        proj.speed = 3f;
        proj.damage = 10f;
        proj.penetrate = 999;
        proj.maxExistTime = 10f;
        proj.shape = new GMath.Circle(Vector2.zero, 1f);

        return proj;
    }

    private void RegisterProjectileGenerator()
    {
        ProjectileSys.Instance.RegisterEntityGenerateDeletate(ProjectileGen_0);
        ProjectileSys.Instance.RegisterEntityGenerateDeletate(ProjectileGen_1);
        ProjectileSys.Instance.RegisterEntityGenerateDeletate(ProjectileGen_2);
        ProjectileSys.Instance.RegisterEntityGenerateDeletate(ProjectileGen_3);
    }

    void Start()
    {

        var builder = new BehaviorTreeBuilder();

        builder.Repeat(3)
                    .Sequence()
                        .Log("this is test for behavior tree builder")
                    .Back()
                .End();
        builder.Tree.Tick();

        Physics.gravity = new Vector3(0, -100, 0);


        ProjectileSys.ProjectileTargetSys = new ProjectileTargetSys();

        XLua.LuaEnv luaEnv = new XLua.LuaEnv();
        luaEnv.DoString("print('hello world')");

        RegisterProjectileGenerator();
    }

    private void Update()
    {
    }
}

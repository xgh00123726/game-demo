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
    }

    private void Update()
    {
    }
}

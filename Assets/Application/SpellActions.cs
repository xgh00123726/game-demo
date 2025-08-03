using GameBase.GCamera;
using GameBase.Instance;
using GameBase.Math;
using GameBase.Projectile;
using GameBase.Spell;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

public class SpellActions
{
    public static void Frisbee(Spell spell)
    {
        var projOwner = spell.speller.Get() as IProjectileOwner;
        var proj = ProjectileSys.Instance.NewEntity<Projectile>(1);
        proj.owner = PossibleObj<IProjectileOwner>.New(projOwner);
    }

    public static void FrisbeeEmiter(Spell spell)
    {
        var projOwner = spell.speller.Get() as IProjectileOwner;
        var proj = ProjectileSys.Instance.NewEntity<Projectile>(1);
        proj.owner = PossibleObj<IProjectileOwner>.New(projOwner);
        proj.OnAliveFixed += (Projectile e) =>
        {
            if (ProjectileSys.Instance.FixedTick % 20 == 0)
            {
                var proj = ProjectileSys.Instance.NewEntity<Projectile>(2);
                proj.src = e.Obj.transform.position;
            }
        };
    }

    public static void Frisbeeing(Spell spell)
    {
        var projOwner = spell.speller.Get() as IProjectileOwner;
        var proj = ProjectileSys.Instance.NewEntity<Projectile>(3);
        proj.owner = PossibleObj<IProjectileOwner>.New(projOwner);
        proj.hitInterval = 10;
        proj.damage = 3;
    }

    public static void Ezreal_Q(Spell spell)
    {

    }

    public static void Ezreal_E(Spell spell)
    {
        var projOwner = spell.speller.Get() as IProjectileOwner;

        var target = CreatureSys.NearestEntity<Enemy1>(projOwner.HandPostion, null, 0);
        if (target == null)
        {
            var text = TextSys.Instance.NewEntity<FloatText>();
            text.bodyID = 4;
            text.value = "No Target";
            text.showPosition = projOwner.HandPostion;
        }

        var proj = ProjectileSys.Instance.NewEntity<Projectile>(0);
        proj.target = PossibleObj<IProjectileTarget>.New(target);
        proj.owner = PossibleObj<IProjectileOwner>.New(projOwner);
    }
}

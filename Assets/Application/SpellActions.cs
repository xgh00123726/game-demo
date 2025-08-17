using GameBase.Buffs;
using GameBase.GCamera;
using GameBase.Instance;
using GameBase.Math;
using GameBase.Flyings;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.UI;
using Instance.Buffs;
using UnityEngine;
using Combines.Projectiles;

public class SpellActions
{
    public static void FallingStone(Spell spell)
    {
        ProjectileSys.Instance.NewEntity<Projectile>(4).owner = spell.speller as IProjectileOwner;
    }

    public static void Frisbee(Spell spell)
    {
        var projOwner = spell.speller as IProjectileOwner;
        var proj = ProjectileSys.Instance.NewEntity<Projectile>(1);
        proj.owner = projOwner;
    }

    public static void FrisbeeEmiter(Spell spell)
    {
        var projOwner = spell.speller as IProjectileOwner;
        var proj = ProjectileSys.Instance.NewEntity<Projectile>(1);
        proj.owner = projOwner;
    }

    public static void Frisbeeing(Spell spell)
    {
        var projOwner = spell.speller as IProjectileOwner;
        var proj = ProjectileSys.Instance.NewEntity<Projectile>(3);
        proj.owner = projOwner;
    }

    public static void Ezreal_Q(Spell spell)
    {

    }

    public static void Ezreal_E(Spell spell)
    {
        var projOwner = spell.speller as IProjectileOwner;

        var target = CreatureSys.Instance.NearestEntity<Enemy1>(projOwner.HandPosition, null, 0);
        if (target == null)
        {
            var text = TextSys.Instance.NewEntity<FloatText>();
            text.value = "No Target";
            text.showPosition = projOwner.HandPosition;
        }

        var proj = ProjectileSys.Instance.NewEntity<Projectile>(0);
        proj.target = target;
        proj.owner = projOwner;
    }

    public static void Faster(Spell spell)
    {
        if (spell.speller is IBuffOwner owner)
        {
            var buff = BuffSys.Instance.NewEntity<Buff>(0);
            buff.owner = owner;
            owner.Buffs.AddBuff(buff);
        }
    }
}

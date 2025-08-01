using System.Collections;
using System.Collections.Generic;
using GameBase.Projectile;
using GameBase.Spell;
using GameBase.Tools;
using UnityEngine;

public class SpellActions
{
    public static void Ezreal_Q(Spell spell)
    {

    }

    public static void Ezreal_E(Spell spell)
    {
        var proj = ProjectileSys.Instance.NewEntity<Projectile>();
        var projOwner = spell.speller.Get() as IProjectileOwner;

        var target = CreatureSys.NearestEntity(projOwner.HandPostion);
        XLogger.Instance.Log(target);
        proj.target = PossibleObj<IProjectileTarget>.New(target);
        proj.bodyID = 0;
        proj.owner = PossibleObj<IProjectileOwner>.New(projOwner);
        proj.curveType = CurveFactory.CurveType.Tracer;
        proj.speed = 50;
    }
}

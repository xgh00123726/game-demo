using Combines.Projectiles;
using GameBase.Math;
using GameBase.Modify;
using GameBase.Tools;
using Instance.GameSys;
using System;
using UnityEngine;

namespace GameBase.Spells
{
    public class SpellActionRegister
    {
        private static void SpellAction_0(Spell spell)
        {
            ProjectileSys.Instance.NewEntity<Projectile>(4).owner = spell.speller as IProjectileOwner;
        }

        private static void SpellAction_1(Spell spell)
        {
            var mOwner = spell.speller as IModifyOwner<float>;
            if (mOwner == null)
            {
                XLogger.Instance.Log("owner null");
                return;
            }
            if (!mOwner.Modifyables.Contains("attackRange"))
            {
                XLogger.Instance.Log("no attack range");
                return;
            }
            float attackRange = mOwner.Modifyables["attackRange"].Value;
            Vector2 center = new Vector2(spell.speller.Position.x, spell.speller.Position.z);

            var target = SimplestProjectileTargetSys.Instance.NearestTarget(center, attackRange);
            if (target == null)
            {
                XLogger.Instance.Log("no target");
                return;
            }

            var proj = ProjectileSys.Instance.NewEntity<Projectile>(0);

            proj.owner = spell.speller as IProjectileOwner;
            proj.target = target;
        }
        public static void RegisterGenerator()
        {
            SpellSys.Instance.RegisterSpellAction(0, SpellAction_0);
            SpellSys.Instance.RegisterSpellAction(1, SpellAction_1);
        }
    }
}

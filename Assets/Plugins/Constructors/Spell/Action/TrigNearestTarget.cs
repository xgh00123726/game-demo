using GameBase.EntitySystem;
using GameBase.GCamera;
using GameBase.Modify;
using GameBase.Projectiles;
using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;
using System;
using UnityEngine;

namespace Constructor.Spells.Action
{
    public struct TrigNearestTargetData
    {
        public Projectiles.Type projectileType;
        public int projectileID;
        public Flyings.Type flyingType;
        public int flyingID;
    }

    public class TrigNearestTarget : ISpellAction
    {
        public TrigNearestTargetData data;
        bool ISpellAction.CastAction(Spell spell)
        {
            var mOwner = spell.speller as IModifieder;
            if (mOwner == null)
            {
                XLogger.Instance.Log("owner null");
                return false;
            }
            if (!mOwner.Modifyables.ContainsKey("attackRange"))
            {
                XLogger.Instance.Log("no attack range");
                return false;
            }
            float attackRange = mOwner.Modifyables["attackRange"];
            Vector3 center = new Vector3(spell.speller.Position.x, 0, spell.speller.Position.z);

            var target = TargetSetFactorary.GetTargetSet("Common").NearestTarget(center, attackRange);
            if (target == null)
            {
                return false;
            }

            var pOwner = spell.speller as IProjectileOwner;

            var e = Projectiles.Factory.Instance.Get(data.projectileType, data.projectileID);
            e.Flying = Flyings.Factory.Instance.Get(data.flyingType, data.flyingID);
            e.Flying.Src = pOwner.HandPosition + new Vector3(0, 1, 0);
            e.Flying.dest = target.Center;
            e.Flying.curve.DirInit();
            e.owner = pOwner;
            e.target = target;

            return true;
        }
    }

    public class TrigNearestTargetCon : BaseConstructor<TrigNearestTargetData, TrigNearestTarget, TrigNearestTargetCon>
    {
        protected override string RelativePath => "Spell/Action/TrigNearestTarget.csv";

        protected override TrigNearestTarget Get()
        {
            return new TrigNearestTarget();
        }

        protected override void Set(TrigNearestTarget e, in TrigNearestTargetData data)
        {
            e.data = data;
        }
    }
}

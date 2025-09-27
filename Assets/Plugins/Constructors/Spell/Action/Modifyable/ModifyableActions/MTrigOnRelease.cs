using GameBase.EntitySystem;
using GameBase.GCamera;
using GameBase.Projectiles;
using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;
using System;
using UnityEngine;

namespace Constructor.Spells.Action.Modifyables
{
    public class MTrigOnRelease : ISpellAction
    {
        public TrigOnReleaseData data;

        public bool CastAction(Spell spell)
        {
            if (spell.speller is IProjectileOwner pOwner)
            {
                var ef = Flyings.Factory.Instance.Get(data.flying1Type, data.flying1ID);
                ef.Src = pOwner.HandPosition + new Vector3(data.xOffset, data.yOffset, data.zOffset);
                ef.dest = CameraSys.MouseHitPosition;
                ef.OnHit += () =>
                {
                    var e = Projectiles.Factory.Instance.Get(data.projType, data.projectileID);
                    e.Flying = Flyings.Factory.Instance.Get(data.flying2Type, data.flying2ID);
                    e.shape.Center = new Vector2(ef.dest.x, ef.dest.z);
                    e.Flying.Src = ef.dest;
                    e.Flying.dest = ef.dest;
                    e.owner = pOwner;
                };

                return true;
            }

            return false;
        }
    }
    public class MTrigOnReleaseCon : BaseConstructor<TrigOnReleaseData, MTrigOnRelease, MTrigOnReleaseCon>
    {
        protected override string RelativePath => "Spell/Action/TrigOnRelease.csv";

        protected override MTrigOnRelease Get()
        {
            return new MTrigOnRelease();
        }

        protected override void Set(MTrigOnRelease e, in TrigOnReleaseData data)
        {
            e.data = data;
        }
    }
}

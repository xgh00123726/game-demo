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
    public class MTrigOnRelease : IAction
    {
        public TrigOnReleaseData data;

        public void CastAction(Spell spell)
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
            }
        }
    }
    public class MTrigOnReleaseCon : BaseConstructor<TrigOnReleaseData, MTrigOnRelease, MTrigOnReleaseCon>
    {
        protected override string RelativePath => "Spell/Action/TrigOnRelease.csv";

        protected override MTrigOnRelease Get()
        {
            return new MTrigOnRelease();
        }

        protected override void Parse(CsvReader line, ref TrigOnReleaseData data)
        {
            Enum.TryParse(line[1], out data.flying1Type);
            data.flying1ID = int.Parse(line[2]);
            Enum.TryParse(line[3], out data.flying2Type);
            data.flying2ID = int.Parse(line[4]);
            Enum.TryParse(line[5], out data.projType);
            data.projectileID = int.Parse(line[6]);
            data.xOffset = float.Parse(line[7]);
            data.yOffset = float.Parse(line[8]);
            data.zOffset = float.Parse(line[9]);
        }

        protected override void Set(MTrigOnRelease e, in TrigOnReleaseData data)
        {
            e.data = data;
        }
    }
}

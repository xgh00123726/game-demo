using GameBase.EntitySystem;
using GameBase.GCamera;
using GameBase.Projectiles;
using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;
using System;
using UnityEngine;

namespace Constructor.Spells.Action
{
    public struct TrigOnReleaseData
    {
        public Flyings.Type flying1Type;
        public int flying1ID;
        public Flyings.Type flying2Type;
        public int flying2ID;
        public Projectiles.Type projType;
        public int projectileID;
        public float xOffset;
        public float yOffset;
        public float zOffset;
    }

    public class TrigOnRelease : ISpellAction
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
    public class TrigOnReleaseCon : BaseConstructor<TrigOnReleaseData, TrigOnRelease, TrigOnReleaseCon>
    {
        protected override string RelativePath => "Spell/Action/TrigOnRelease.csv";

        protected override TrigOnRelease Get()
        {
            return new TrigOnRelease();
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

        protected override void Set(TrigOnRelease e, in TrigOnReleaseData data)
        {
            e.data = data;
        }
    }
}

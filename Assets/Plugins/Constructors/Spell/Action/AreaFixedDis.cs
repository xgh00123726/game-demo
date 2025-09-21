using GameBase.GCamera;
using GameBase.Projectiles;
using GameBase.Spells;
using NReco.Csv;
using System;
using UnityEngine;

namespace Constructor.Spells.Action
{
    public struct AreaFixedDisData
    {
        public Flyings.Type flyingType;
        public int flyingID;
        public Projectiles.Type projectileType;
        public int projectileID;
        public int distance;
    }

    public class AreaFixedDis : ISpellAction
    {
        public AreaFixedDisData data;
        bool ISpellAction.CastAction(Spell spell)
        {
            var pOwner = spell.speller as IProjectileOwner;
            if (pOwner == null)
            {
                return false;
            }

            var ef = Flyings.Factory.Instance.Get(data.flyingType, data.flyingID);
            ef.Src = spell.speller.Position;
            Vector3 dir = (CameraSys.MouseHitPosition - spell.speller.Position).normalized;
            ef.dest = ef.Src + dir * data.distance;
            var ep = Projectiles.Factory.Instance.Get(data.projectileType, data.projectileID);
            ep.owner = pOwner;
            ep.Flying = ef;

            return true;
        }
    }

    public class AreaFixedDisCon : BaseConstructor<AreaFixedDisData, AreaFixedDis, AreaFixedDisCon>
    {
        protected override string RelativePath => "Spell/Action/AreaFixedDis.csv";

        protected override AreaFixedDis Get()
        {
            return new AreaFixedDis();
        }

        protected override void Parse(CsvReader line, ref AreaFixedDisData data)
        {
            Enum.TryParse(line[1], out data.flyingType);
            data.flyingID = int.Parse(line[2]);
            Enum.TryParse(line[3], out data.projectileType);
            data.projectileID = int.Parse(line[4]);
            data.distance = int.Parse(line[5]);
        }

        protected override void Set(AreaFixedDis e, in AreaFixedDisData data)
        {
            e.data = data;
        }
    }
}

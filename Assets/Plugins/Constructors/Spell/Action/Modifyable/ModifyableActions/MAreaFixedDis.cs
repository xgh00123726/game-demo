using GameBase.GCamera;
using GameBase.Projectiles;
using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;
using System;
using UnityEngine;

namespace Constructor.Spells.Action.Modifyables
{
    //public struct MAreaFixedDisData
    //{
    //    public Flyings.Type flyingType;
    //    public int flyingID;
    //    public Projectiles.Type projectileType;
    //    public int projectileID;
    //    public int distance;
    //}
    public class MAreaFixedDis : ModifyableAction
    {
        public AreaFixedDisData data;

        protected override void CastAction(Spell spell, in ModifyableModifyData modifyData)
        {
            var pOwner = spell.speller as IProjectileOwner;
            if (pOwner == null)
            {
                return;
            }

            int flyingNumsModify = modifyData.flyingNums > 0 ? modifyData.flyingNums : 0;
            float angleDelta = modifyData.fireDisfuse / (1 + flyingNumsModify);
            float flyingDistance = data.distance + modifyData.flyingDistance;
            for (int i = 0; i < 1 + flyingNumsModify; ++i)
            {
                var ef = Flyings.Factory.Instance.Get(data.flyingType, data.flyingID);
                ef.Src = spell.speller.Position;
                Vector3 dir = (CameraSys.MouseHitPosition - spell.speller.Position).normalized;
                Quaternion rotate = Quaternion.Euler(0, angleDelta * i, 0);
                dir = rotate * dir;
                ef.dest = ef.Src + dir * flyingDistance;
                var ep = Projectiles.Factory.Instance.Get(data.projectileType, data.projectileID);
                ep.owner = pOwner;
                ep.Flying = ef;
            }
        }
    }
    public class MAreaFixedDisCon : BaseConstructor<AreaFixedDisData, MAreaFixedDis, MAreaFixedDisCon>
    {
        protected override string RelativePath => "Spell/Action/AreaFixedDis.csv";

        protected override MAreaFixedDis Get()
        {
            return new MAreaFixedDis();
        }

        protected override void Parse(CsvReader line, ref AreaFixedDisData data)
        {
            Enum.TryParse(line[1], out data.flyingType);
            data.flyingID = int.Parse(line[2]);
            Enum.TryParse(line[3], out data.projectileType);
            data.projectileID = int.Parse(line[4]);
            data.distance = int.Parse(line[5]);
        }

        protected override void Set(MAreaFixedDis e, in AreaFixedDisData data)
        {
            e.data = data;
        }
    }
}

using Constructor.GEffects;
using GameBase.EntitySystem;
using GameBase.Math;
using GameBase.Projectiles;
using NReco.Csv;
using System;
using UnityEngine;

namespace Constructor.Projectiles
{
    public struct AreaRectTriggerData
    {
        public int width;
        public int damage;
    }
    public class AreaRectTrigger : EntityConstructor<AreaRectTriggerData, Projectile, SimpleEntityContainer, ProjectileSys, AreaRectTrigger>
    {
        protected override string RelativePath => "Projectile/AreaRectTrigger.csv";

        protected override ProjectileSys SysInstance => ProjectileSys.Instance;

        protected override void Parse(CsvReader line, ref AreaRectTriggerData data)
        {
            data.width = int.Parse(line[1]);
            data.damage = int.Parse(line[2]);
        }

        protected override void Set(Projectile e, in AreaRectTriggerData data)
        {
            e.maxeffectTimes = 100;
            e.hasWhite = true;
            e.shape = new GMath.Rect2D(data.width, 1f);
            e.targetsSet = TargetSetFactorary.GetTargetSet("Common");
            var damage = data.damage;
            e.effectConstructor = () => GEffects.Factory.Instance.Get(GEffects.Type.Damage, damage);
        }
    }
}

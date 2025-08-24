using Constructor.GEffects;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Math;
using GameBase.Projectiles;
using GameBase.Tools;
using NReco.Csv;
using System;
using UnityEngine;

namespace Constructor.Projectiles
{
    public struct AreaCircleTriggerData
    {
        public int maxEffectTimes;
        public bool hasWhite;
        public float radius;
        public int damage;
    }
    public class AreaCircleTrigger : EntityConstructor<AreaCircleTriggerData, Projectile, SimpleEntityContainer, ProjectileSys, AreaCircleTrigger>
    {
        protected override string RelativePath => "Projectile/AreaCircleTrigger.csv";

        protected override ProjectileSys SysInstance => ProjectileSys.Instance;

        protected override void Parse(CsvReader line, ref AreaCircleTriggerData data)
        {
            data.maxEffectTimes = int.Parse(line[1]);
            data.hasWhite = bool.Parse(line[2]);
            data.radius = float.Parse(line[3]);
            data.damage = int.Parse(line[4]);
        }

        protected override void Set(Projectile e, in AreaCircleTriggerData data)
        {
            e.maxeffectTimes = data.maxEffectTimes;
            e.hasWhite = data.hasWhite;
            e.shape = new GMath.Circle(Vector2.zero, data.radius);
            e.targetsSet = TargetSetFactorary.GetTargetSet("Common");
            var damage = data.damage;
            e.effectConstructor = () => GEffects.Factory.Instance.Get(GEffects.Type.Damage, damage);
        }
    }
}

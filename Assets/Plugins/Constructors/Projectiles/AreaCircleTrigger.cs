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
    public class AreaCircleTrigger : EntityConstructor<AreaCircleTriggerData, Projectile, ProjectileSys, AreaCircleTrigger>
    {
        protected override string RelativePath => "Projectile/AreaCircleTrigger.csv";

        protected override ProjectileSys SysInstance => ProjectileSys.Instance;

        protected override void ESet(Projectile e, in AreaCircleTriggerData data)
        {
            e.maxeffectTimes = data.maxEffectTimes;
            e.hasWhite = data.hasWhite;
            e.shape = new GMath.Circle(Vector2.zero, data.radius);
            e.targetsSet = TargetSetFactorary.GetTargetSet("Common");
            e.action = Constructor.Projectiles.Action.Factory.Instance.Get(Projectiles.Action.Type.Damage, data.damage);
        }
    }
}

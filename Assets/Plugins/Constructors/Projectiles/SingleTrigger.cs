using Constructor.GEffects;
using GameBase.EntitySystem;
using GameBase.Projectiles;
using GameBase.Tools;
using NReco.Csv;
using System;

namespace Constructor.Projectiles
{
    public struct SingleTriggerData
    {
        public int damage;
    }
    public class SingleTrigger : EntityConstructor<SingleTriggerData, Projectile, SimpleEntityContainer, ProjectileSys, SingleTrigger>
    {
        protected override string RelativePath => "Projectile/SingleTrigger.csv";

        protected override ProjectileSys SysInstance => ProjectileSys.Instance;

        protected override void Parse(CsvReader line, ref SingleTriggerData data)
        {
            data.damage = int.Parse(line[1]);
        }

        protected override void Set(Projectile e, in SingleTriggerData data)
        {
            e.maxeffectTimes = 1;
            e.hasWhite = false;
            var damage = data.damage;
            e.effectConstructor = () => GEffects.Factory.Instance.Get(GEffects.Type.Damage, damage);
        }
    }
}

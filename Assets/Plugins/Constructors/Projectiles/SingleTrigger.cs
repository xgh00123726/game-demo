using GameBase.Projectiles;
using NReco.Csv;

namespace Constructor.Projectiles
{
    public struct SingleTriggerData
    {
        public int damage;
    }
    public class SingleTrigger : EntityConstructor<SingleTriggerData, Projectile, ProjectileSys, SingleTrigger>
    {
        protected override string RelativePath => "Projectile/SingleTrigger.csv";

        protected override ProjectileSys SysInstance => ProjectileSys.Instance;

        protected override void ESet(Projectile e, in SingleTriggerData data)
        {
            e.maxeffectTimes = 1;
            e.hasWhite = false;
            var damage = data.damage;
            e.action = Constructor.Projectiles.Action.Factory.Instance.Get(Projectiles.Action.Type.Damage, data.damage);
        }
    }
}

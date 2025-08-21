using Constructor.GEffects;
using GameBase.EntitySystem;
using GameBase.Math;
using GameBase.Projectiles;
using NReco.Csv;

namespace Constructor.Projectiles
{
    public struct Area2Data
    {
        public int distance;
        public int width;
        public int damage;
        public int flyingID;
    }
    public class Area2 : EntityConstructor<Area2Data, Projectile, SimpleEntityContainer, ProjectileSys, Area2>
    {
        protected override string RelativePath => "Projectile/Area2.csv";

        protected override ProjectileSys SysInstance => ProjectileSys.Instance;

        protected override void Parse(CsvReader line, ref Area2Data data)
        {
            data.distance = int.Parse(line[1]);
            data.width = int.Parse(line[2]);
            data.damage = int.Parse(line[3]);
            data.flyingID = int.Parse(line[4]);
        }

        protected override void Set(Projectile e, in Area2Data data)
        {
            e.maxeffectTimes = 100;
            e.hasWhite = true;
            e.shape = new GMath.Rect2D(data.width, data.distance / 10);
            e.targetsSet = TargetSetFactorary.GetTargetSet("Common");
            var damage = data.damage;
            e.effectConstructor = () => GEDamage.New(-damage);
            e.flying = Flyings.Common.Instance.Get(data.flyingID);
        }
    }
}

using Constructor.GEffects;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Math;
using GameBase.Projectiles;
using GameBase.Tools;
using NReco.Csv;
using UnityEngine;

namespace Constructor.Projectiles
{
    public struct Area1Data
    {
        public int maxEffectTimes;
        public bool hasWhite;
        public float radius;
        public int damage;
        public int flyingID;
    }
    public class Area1 : BaseConstructor<Area1Data, Projectile, SimpleEntityContainer, ProjectileSys, Area1>
    {
        protected override string RelativePath => "Projectile/Area1.csv";

        protected override ProjectileSys SysInstance => ProjectileSys.Instance;

        protected override void Parse(CsvReader line, ref Area1Data data)
        {
            data.maxEffectTimes = int.Parse(line[1]);
            data.hasWhite = bool.Parse(line[2]);
            data.radius = float.Parse(line[3]);
            data.damage = int.Parse(line[4]);
            data.flyingID = int.Parse(line[5]);
        }

        protected override void Set(Projectile e, in Area1Data data)
        {
            e.maxeffectTimes = data.maxEffectTimes;
            e.hasWhite = data.hasWhite;
            e.shape = new GMath.Circle(Vector2.zero, data.radius);
            e.targetsSet = TargetSetFactorary.GetTargetSet("Common");
            var damage = data.damage;
            e.effectConstructor = () => GEDamage.New(-damage);
            e.flying = Flyings.Common.Instance.Get(data.flyingID);
        }
    }
}

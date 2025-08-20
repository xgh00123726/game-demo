using GameBase.EntitySystem;
using GameBase.Projectiles;
using GameBase.Tools;
using NReco.Csv;

namespace Constructor.Projectiles
{
    public struct Tracer1Data
    {
        public int flyingID;
    }
    public class Tracer1 : BaseConstructor<Tracer1Data, Projectile, SimpleEntityContainer, ProjectileSys, Tracer1>
    {
        protected override string RelativePath => "Projectile/Tracer1.csv";

        protected override ProjectileSys SysInstance => ProjectileSys.Instance;

        protected override void Parse(CsvReader line, ref Tracer1Data data)
        {
            data.flyingID = int.Parse(line[1]);
        }

        protected override void Set(Projectile e, in Tracer1Data data)
        {
            e.maxeffectTimes = 1;
            e.hasWhite = false;
            e.flying = Flyings.Common.Instance.Get(data.flyingID);
        }
    }
}

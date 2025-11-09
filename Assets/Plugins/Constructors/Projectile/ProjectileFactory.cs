using Constructor.Flyings;
using Constructor.Triggers;
using GameBase.EntitySystem;
using GameBase.Math;
using GameBase.Projectiles;
using GameBase.Triggers;
using GameBase.Tools;

namespace Constructor.Projectiles
{
    public class ProjectileFactory : YamlFactory<ProjectileData, Projectile, ProjectileFactory>
    {
        protected override string YamlFolder => null;

        public IShape2D GetShape(ProjectileShapeData data)
        {
            if (data == null) return null;

            if (data.type == ProjectileShapeType.Circle)
            {
                var c = new GMath.Circle(data.radius);
                return c;
            }
            if (data.type == ProjectileShapeType.Linear)
            {
                var l = new GMath.Line();
                l.Length = data.length;
                return l;
            }
            if (data.type == ProjectileShapeType.Rect)
            {
                return null;
            }

            return null;
        }

        protected override Projectile GetEntity(ProjectileData data)
        {
            var p = ProjectileSys.Instance.NewEntity();
            var f = FlyingFactory.Instance.GetFromData(data.flying);
            var t = TriggerSys.Instance.NewEntity();
            
            t.hasWhite = data.hasWhite;
            t.shape = GetShape(data.shape);
            t.maxEffectTimes = data.maxEffectTimes;
            t.hitEffect = data.hitEffectName;
            t.trigEffect = data.trigEffectName;
            t.hitAudio = data.hitAudioName;
            t.trigAudio = data.trigAudioName;
            t.targetsSet = CommonTargetSet.Instance;

            f.maxTravel = data.maxTravel;

            p.tag = data.tagEnum;
            p.trigger = t;
            p.flying = f;

            return p;
        }
    }
}

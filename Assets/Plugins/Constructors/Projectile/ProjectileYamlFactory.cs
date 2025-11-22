using Constructor.Flyings;
using Constructor.Triggers;
using GameBase.EntitySystem;
using GameBase.Math;
using GameBase.Projectiles;
using GameBase.Triggers;
using GameBase.Tools;

namespace Constructor.Projectiles
{
    public class ProjectileYamlFactory : YamlFactory<ProjectileData, Projectile, ProjectileYamlFactory>
    {
        protected override string Folder => null;

        public IShape2D GetShape(ProjectileShapeData data)
        {
            if (data == null) return null;

            if (data.Type == ProjectileShapeType.Circle)
            {
                var c = new GMath.Circle(data.Radius);
                return c;
            }
            if (data.Type == ProjectileShapeType.Linear)
            {
                var l = new GMath.Line();
                l.Length = data.Length;
                return l;
            }
            if (data.Type == ProjectileShapeType.Rect)
            {
                return null;
            }

            return null;
        }

        protected override Projectile GetEntity(ProjectileData data)
        {
            var p = ProjectileSys.Instance.NewEntity();
            var f = FlyingYamlFactory.Instance.GetFromData(data.Flying);
            var t = TriggerSys.Instance.NewEntity();
            
            t.HasWhite = data.HasWhite;
            t.Shape = GetShape(data.Shape);
            t.MaxEffectTimes = data.MaxEffectTimes;
            t.HitEffect = data.HitEffectName;
            t.TrigEffect = data.TrigEffectName;
            t.HitAudio = data.HitAudioName;
            t.TrigAudio = data.TrigAudioName;
            t.TargetsSet = CommonTargetSet.Instance;

            p.Tag = data.TagEnum;
            p.Trigger = t;
            p.Flying = f;
            p.Damage = data.Damage;
            p.DamageTextPrefabName = data.DamageTextPrefabName;
            p.DamageTextColor = data.DamageTextColor;
            p.AmpFactor = data.AmpFactor;
            p.FindTargetRange = data.FindTargetRange;

            return p;
        }
    }
}

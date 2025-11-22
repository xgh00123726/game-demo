using GameBase.EntitySystem;
using GameBase.Math;
using GameBase.Tools;
using GameBase.Triggers;
namespace Constructor.Triggers
{
    public class TriggerYamlFactory : YamlFactory<TriggerData, Trigger, TriggerYamlFactory>
    {
        protected override string Folder => null;

        public IShape2D GetShape(TriggerShapeData data)
        {
            if (data == null) return null;

            if (data.Type == TriggerShapeType.Circle)
            {
                var c = new GMath.Circle(data.Radius);
                return c;
            }
            if (data.Type == TriggerShapeType.Linear)
            {
                var l = new GMath.Line(data.Length)
                {
                    width = data.Width,
                    pivot = data.Pivot
                };
                return l;
            }
            if (data.Type == TriggerShapeType.Rect)
            {
                return null;
            }

            return null;
        }

        protected override Trigger GetEntity(TriggerData data)
        {
            var t = TriggerSys.Instance.NewEntity();

            t.Action = new TriggerAction()
            {
                damage = data.Damage,
                color = data.DamageTextColor,
                tag = data.TagEnum,
                prefabName = data.DamageTextPrefabName,
                buffName = data.BuffName,
                buffDuration = data.BuffDuration,
            };

            t.MaxEffectTimes = data.MaxEffectTimes;
            t.Shape = GetShape(data.Shape);
            t.Delay = data.Delay;
            t.TrigStyle = data.TrigStyle;
            t.TrigPeriod = data.TrigPeriod;
            t.ExistTime = data.ExistTime;
            t.CreateAudio = data.CreateAudioName;
            t.CreateEffect = data.CreateEffectName;
            t.TrigAudio = data.TrigAudioName;
            t.TrigEffect = data.TrigEffectName;
            t.HitAudio = data.HitAudioName;
            t.HitEffect = data.HitEffectName;

            return t;
        }
    }
}

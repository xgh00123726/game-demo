using GameBase.EntitySystem;
using GameBase.Math;
using GameBase.Tools;
using GameBase.Triggers;
namespace Constructor.Triggers
{
    public class TriggerYamlFactory : YamlFactory<TriggerData, Trigger, TriggerYamlFactory>
    {
        protected override string YamlFolder => null;

        public IShape2D GetShape(TriggerShapeData data)
        {
            if (data == null) return null;

            if (data.type == TriggerShapeType.Circle)
            {
                var c = new GMath.Circle(data.radius);
                return c;
            }
            if (data.type == TriggerShapeType.Linear)
            {
                var l = new GMath.Line(data.length)
                {
                    width = data.width,
                    pivot = data.pivot
                };
                return l;
            }
            if (data.type == TriggerShapeType.Rect)
            {
                return null;
            }

            return null;
        }

        protected override Trigger GetEntity(TriggerData data)
        {
            var t = TriggerSys.Instance.NewEntity();

            t.action = new TriggerAction()
            {
                damage = data.damage,
                color = data.damageTextColor,
                tag = data.tagEnum,
                prefabName = data.damageTextPrefabName,
            };

            t.maxEffectTimes = data.maxEffectTimes;
            t.shape = GetShape(data.shape);
            t.delay = data.delay;
            t.trigStyle = data.trigStyle;
            t.trigPeriod = data.trigPeriod;
            t.existTime = data.existTime;
            t.createAudio = data.createAudioName;
            t.createEffect = data.createEffectName;
            t.trigAudio = data.trigAudioName;
            t.trigEffect = data.trigEffectName;
            t.hitAudio = data.hitAudioName;
            t.hitEffect = data.hitEffectName;

            return t;
        }
    }
}

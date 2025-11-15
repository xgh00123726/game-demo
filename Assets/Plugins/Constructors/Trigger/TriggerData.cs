using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Triggers
{
    public enum TriggerShapeType
    {
        Circle,
        Rect,
        Linear,
    }

    public class TriggerShapeData
    {
        public TriggerShapeType type;
        public float radius;
        public float length;
        public float width;
        public float pivot;
    }
    public class TriggerData
    {
        public int maxEffectTimes;
        public TriggerShapeData shape;
        public TriggerActionTag tagEnum;
        public TrigStyle trigStyle;
        public float delay;
        public float trigPeriod;
        public float existTime;

        public float damage;
        public float ampFactor;
        public Color damageTextColor;

        public string damageTextPrefabName;
        public string trigEffectName;
        public string trigAudioName;
        public string hitEffectName;
        public string hitAudioName;
        public string createEffectName;
        public string createAudioName;
        public string tag;
    }
}

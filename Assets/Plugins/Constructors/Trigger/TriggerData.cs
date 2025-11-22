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
        public TriggerShapeType Type {  get; set; }
        public float Radius { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Pivot { get; set; }
    }
    public class TriggerData
    {
        public int MaxEffectTimes { get; set; }
        public TriggerShapeData Shape {  get; set; }
        public TriggerActionTag TagEnum { get; set; }
        public TrigStyle TrigStyle { get; set; }
        public float Delay { get; set; }
        public float TrigPeriod { get; set; }
        public float ExistTime { get; set; }
        public float BuffDuration { get; set; }

        public float Damage {  get; set; }
        public float AmpFactor { get; set; }
        public Color DamageTextColor { get; set; }

        public string DamageTextPrefabName { get; set; }
        public string TrigEffectName { get; set; }
        public string TrigAudioName { get; set; }
        public string HitEffectName { get; set; }
        public string HitAudioName { get; set; }
        public string CreateEffectName { get; set; }
        public string CreateAudioName {  get; set; }
        public string BuffName { get; set; }
        public string Tag {  get; set; }
    }
}

using Constructor.Flyings;
using GameBase.Creatures;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Projectiles
{
    public enum ProjectileShapeType
    {
        Circle,
        Rect,
        Linear,
    }

    public class ProjectileShapeData
    {
        public ProjectileShapeType Type {  get; set; }
        public float Radius { get; set; }
        public float Length { get; set; }
    }

    public class ProjectileData
    {
        public FlyingData Flying { get; set; }
        public ProjectileShapeData Shape { get; set; }
        public int MaxEffectTimes { get; set; }
        public bool HasWhite {  get; set; }
        public GameBase.Projectiles.Tag TagEnum { get; set; }
        public float FindTargetRange { get; set; }
        public float Damage { get; set; }
        public float AmpFactor { get; set; }
        public Color DamageTextColor { get; set; }

        public string DamageTextPrefabName { get; set; }
        public string Tag {  get; set; }
        public string HitEffectName {  get; set; }
        public string TrigEffectName { get; set; }
        public string HitAudioName { get; set; }
        public string TrigAudioName { get; set; }
    }
}

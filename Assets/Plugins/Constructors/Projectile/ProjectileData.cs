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
        public ProjectileShapeType type;
        public float radius;
        public float length;
    }

    public class ProjectileData
    {
        public FlyingData flying;
        public ProjectileShapeData shape;
        public int maxEffectTimes;
        public bool hasWhite;
        public GameBase.Projectiles.Tag tagEnum;
        public float findTargetRange;
        public float damage;
        public float ampFactor;
        public Color damageTextColor;

        public string damageTextPrefabName;
        public string tag;
        public string hitEffectName;
        public string trigEffectName;
        public string hitAudioName;
        public string trigAudioName;
    }
}

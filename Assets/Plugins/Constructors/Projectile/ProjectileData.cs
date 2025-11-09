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
        public float maxTravel;
        public GameBase.Projectiles.Tag tagEnum;

        public string tag;
        public string hitEffectName;
        public string trigEffectName;
        public string hitAudioName;
        public string trigAudioName;
    }
}

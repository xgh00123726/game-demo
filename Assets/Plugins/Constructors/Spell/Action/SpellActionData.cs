using Constructor.Flyings;
using Constructor.Projectiles;
using UnityEngine;

namespace Constructor.Spells
{
    public class SpellActionData
    {
        public SpellActionType type;
        public ProjectileData projectile;
        public Vector3 flyingSrcOffset;
        public int slotNum;
        public int buffID;
        public float damage;
        public float ampFactor;
        public float duration;
    }
}
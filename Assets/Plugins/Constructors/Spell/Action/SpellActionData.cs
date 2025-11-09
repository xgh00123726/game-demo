using Constructor.Flyings;
using UnityEngine;

namespace Constructor.Spells
{
    public class SpellActionData
    {
        public SpellActionType type;
        public FlyingData flying;
        public Vector3 flyingSrcOffset;
        public int slotNum;
        public int buffID;
        public int maxEffectTimes;
        public float distance;
        public float radius;
        public float damage;
        public float ampFactor;
        public float delay;
        public float duration;
        public string effectName;
        public string hitAudio;
        public string castAudio;
    }
}
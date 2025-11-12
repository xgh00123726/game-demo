using UnityEngine;

namespace GameBase.Spells
{
    public class Spell
    {
        internal bool isTrig = false;
        internal bool isWaitCast = false;
        internal float cooldownRemain = 0;
        internal bool isCoolOver = false;

        public float point;            // 前摇
        public float backswing;        // 后摇
        public float duration;         // 持续时间
        public float minCastAngle;     // 最小施法角度
        public float cooldown;

        public string textureName;
        public ISpellCampSet targetCampSet;
        public ISpellCamp targetCamp;
        public Vector3 castPosition;
        public Tag tag;
        public CastIndicatorType indicatorType;
        public float length;
        public float radius;

        public ISpeller speller;
        public ISpellAction action;

        public bool IsTrig => isTrig;
        public float CooldownRemain => cooldownRemain;
        public bool IsCoolOver => isCoolOver;

        public void TryCast()
        {
            isTrig = true;
        }

        public void Interrupt()
        {
            isTrig = false;
            isWaitCast = false;
        }
    }
}

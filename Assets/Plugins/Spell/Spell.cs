using UnityEngine;

namespace GameBase.Spells
{
    public class Spell
    {
        internal bool isTrig = false;
        internal bool isWaitCast = false;
        internal float cooldownRemain = 0;
        internal bool isCoolOver = false;

        public float Point { get; set; }            // 前摇
        public float Backswing { get; set; }        // 后摇
        public float Duration { get; set; }         // 持续时间
        public float MinCastAngle { get; set; }     // 最小施法角度
        public float Cooldown { get; set; }
        public string TextureName { get; set; }
        public ISpellCampSet TargetCampSet {  get; set; }
        public ISpellCamp TargetCamp { get; set; }
        public Vector3 CastPosition { get; set; }
        public Tag Tag { get; set; }
        public CastIndicatorType IndicatorType { get; set; }
        public float Length { get; set; }
        public float Radius { get; set; }
        public ISpeller Speller { get; set; }
        public ISpellAction Action { get; set; }
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

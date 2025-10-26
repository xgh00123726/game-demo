using UnityEngine;

namespace GameBase.Spells
{
    public class Spell
    {
        internal bool isTrig = false;
        internal bool isWaitCast = false;

        public float point;            // 前摇
        public float backswing;        // 后摇
        public float duration;         // 持续时间
        public float minCastAngle;     // 最小施法角度

        public int iconTextureID;

        public ISpeller speller;
        public ISpellAction action;
        public ISpellCoolingdown spellCoolingdown = new CommonSpellCoolingdown();
        public ISpellInteractive interactive;

        public bool IsTrig => isTrig;

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

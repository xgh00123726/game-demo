using System;

namespace GameBase.Spells
{
    public class Spell
    {
        public float point;            // 前摇
        public float backswing;        // 后摇
        public float duration;         // 持续时间

        public int iconTextureID;

        public ISpeller speller;
        public ISpellAction action;
        public ISpellCoolingdown spellCoolingdown = new CommonSpellCoolingdown();
        public ISpellInteractive interactive;
    }
}

using System;
using GameBase.EntitySystem;

namespace GameBase.Spells
{
    public class Spell : IEntity
    {
        public float coolingTimeSet;   // 冷却时间
        public float point;            // 前摇
        public float backswing;        // 后摇
        public float duration;         // 持续时间

        public ISpeller speller;
        public IAction actionInterface;
        public ISpellInteractive interactive;

        public Action<Spell> RegistertoActivesDelegate;
        public Action<Spell> RemoveFromActiveDelegate;

        internal bool coolReady; // 技能冷却完成，可以进行交互
        internal float spellMoment;             // 施法时刻
        internal float coolingTimeRemain;       // 剩余冷却时间

        public float CoolingTimeRemain => coolingTimeRemain;
        public int InstanceID { get; set; }
    }
}

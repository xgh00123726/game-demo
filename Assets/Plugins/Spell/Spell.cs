using System;
using UnityEngine;
using GameBase.Tools;
using GameBase.Math;

namespace GameBase.Spells
{
    public class Spell : IEntity
    {
        public float coolingTimeSet;           // 冷却时间
        public bool targetable;
        public PossibleObj<ISpellTarget> target;
        public PossibleObj<Vector3> dest;
        public PossibleObj<ISpeller> speller;
        public PossibleObj<IShape2D> shape;
        public Action<Spell> CastAction;         // 技能动作
        public float point;      // 前摇
        public float backswing;  // 后摇
        public float duration;   // 持续时间

        public Func<Spell, bool> CastJugDelegate;
        public Func<Spell, bool> CancelJugDelegate;
        public Func<Spell, bool> ReadyJugDelegate;

        public Action SpellReadyingDelegate;
        public Action SpellToReadyDelegate;
        public Action SpellExitReadyDelegate;

        public Action<Spell> RegistertoActivesDelegate;
        public Action<Spell> RemoveFromActiveDelegate;

        internal bool userReady;
        internal bool coolReady;
        internal float spellMoment;             // 施法时刻
        internal float coolingTimeRemain;       // 剩余冷却时间
        internal bool hasTarget = false;       // 是否具有目标

        public float CoolingTimeRemain => coolingTimeRemain;

        public int InstanceID { get; set; }
    }
}

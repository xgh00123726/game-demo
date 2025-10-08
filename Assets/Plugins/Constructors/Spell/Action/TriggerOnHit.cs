using GameBase.EntitySystem;
using GameBase.GCamera;
using GameBase.Triggers;
using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;
using System;
using UnityEngine;
using Constructor.Spells.Interactive;
using GameBase.Flyings;

namespace Constructor.Spells.Action
{
    public struct TriggerOnHitData
    {
        public Flyings.Type flying1Type;
        public int flying1ID;
        public Flyings.Type flying2Type;
        public int flying2ID;
        public Triggers.Type triggerType;
        public int triggerID;
        public float xOffset;
        public float yOffset;
        public float zOffset;
    }

    public class TriggerOnHit : ISpellAction
    {
        public TriggerOnHitData data;

        public bool CastAction(Spell spell)
        {
            var speller = spell.speller as ITriggerOwner;
            var interactive = spell.interactive as IDotInput;
            if (speller != null && interactive != null)
            {
                var f = Flyings.Factory.Instance.Get(data.flying1Type, data.flying1ID);
                f.Src = speller.HandPosition + new Vector3(data.xOffset, data.yOffset, data.zOffset);
                f.target = new FixedFlyingTarget()
                {
                    Position = interactive.Position,
                };
                f.OnHit += () =>
                {
                    var t = Triggers.Factory.Instance.Get(data.triggerType, data.triggerID);
                    t.shape.Center = new Vector2(f.target.Position.x, f.target.Position.z);
                    t.owner = speller;
                    t.attach = f;
                    t.Trig();
                };

                return true;
            }

            return false;
        }
    }
    public class TriggerOnHitCon : BaseConstructor<TriggerOnHitData, TriggerOnHit, TriggerOnHitCon>
    {
        protected override string RelativePath => "Spell/Action/TriggerOnHit.csv";

        protected override TriggerOnHit Get()
        {
            return new TriggerOnHit();
        }

        protected override void Set(TriggerOnHit e, in TriggerOnHitData data)
        {
            e.data = data;
        }
    }
}

using Constructor.Spells.Interactive;
using Constructor.Triggers;
using Constructor.Triggers.Action;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Math;
using GameBase.Spells;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Spells.Action
{
    public struct MAreaFixedDisData
    {
        public Flyings.Type flyingType;
        public int flyingID;
        public TargetSetType targetSetType;
        public int slotNum;
        public float distance;
        public float radius;
        public float damage;
        public float ampFactor;
    }

    /// <summary>
    /// ’ŸªΩ∑…––πÃ∂®æ‡¿Îµƒ…‰µØ£¨…‰µØŒ™AOE
    /// </summary>
    public class MAreaFixedDis : ModifyableAction
    {
        public MAreaFixedDisData data;

        protected override Flying GenFlying(Spell spell, in SpellActionModifierData modifyData)
        {
            if (spell.speller is Creature c &&
                spell.interactive is DotExternalSet interactive)
            {

                var f = Flyings.FlyingFactory.Instance.Get(data.flyingType, data.flyingID);
                f.Src = c.Position;
                var dir = (interactive.position - c.Position).normalized;
                f.target = new FixedFlyingTarget()
                {
                    Position = f.Src + dir * data.distance,
                };

                var t = TriggerSys.Instance.NewEntity();
                t.maxeffectTimes = 99;
                t.targetsSet = TargetSetFactorary.Get(data.targetSetType);
                t.shape = new GMath.Circle() { r = 1 };
                t.owner = c;
                t.attach = f;
                t.trigStyle = TrigStyle.Always;

                var damage = data.damage + c.modifyables["damage"].Value * data.ampFactor;
                t.action = new Damage(damage);

                return f;
            }

            return null;
        }
    }
    public class MAreaFixedDisCon : SealedConstructor<MAreaFixedDisData, MAreaFixedDis, MAreaFixedDisCon>
    {
        protected override string RelativePath => "Spell/Action/MAreaFixedDis.csv";

        protected override MAreaFixedDis GetFromData(in MAreaFixedDisData data)
        {
            var e = new MAreaFixedDis()
            {
                Size = data.slotNum
            };
            e.data = data;
            return e;
        }
    }
}

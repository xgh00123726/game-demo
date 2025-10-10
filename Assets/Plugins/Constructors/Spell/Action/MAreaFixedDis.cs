using Constructor.Spells.Interactive;
using Constructor.Triggers;
using Constructor.Triggers.Action;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.GCamera;
using GameBase.Math;
using GameBase.Spells;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Spells.Action.Modifyables
{
    public struct MAreaFixedDisData
    {
        public Flyings.Type flyingType;
        public int flyingID;
        public TargetSetType targetSetType;
        public float distance;
        public float radius;
        public float damage;
        public float ampFactor;
    }
    public class MAreaFixedDis : ModifyableAction
    {
        public MAreaFixedDisData data;

        protected override bool CastAction(Spell spell, in ModifyableModifyData modifyData)
        {
            if (spell.speller is Creature c &&
                spell.interactive is DotExternalSet interactive)
            {

                var f = Flyings.Factory.Instance.Get(data.flyingType, data.flyingID);
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

                var damage = data.damage + c.modifyables["damage"] * data.ampFactor;
                t.action = new Damage(damage);

                t.SetWhites();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class MAreaFixedDisCon : BaseConstructor<MAreaFixedDisData, MAreaFixedDis, MAreaFixedDisCon>
    {
        protected override string RelativePath => "Spell/Action/MAreaFixedDis.csv";

        protected override MAreaFixedDis Get()
        {
            return new MAreaFixedDis();
        }

        protected override void Set(MAreaFixedDis e, in MAreaFixedDisData data)
        {
            e.data = data;
        }
    }
}

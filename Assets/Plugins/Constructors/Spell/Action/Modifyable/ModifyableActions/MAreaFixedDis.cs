using GameBase.Flyings;
using GameBase.GCamera;
using GameBase.Triggers;
using GameBase.Spells;
using GameBase.EntitySystem;
using UnityEngine;

namespace Constructor.Spells.Action.Modifyables
{
    //public struct MAreaFixedDisData
    //{
    //    public Flyings.AIType flyingType;
    //    public int flyingID;
    //    public Triggers.AIType triggerType;
    //    public int triggerID;
    //    public int distance;
    //}
    public class MAreaFixedDis : ModifyableAction
    {
        public AreaFixedDisData data;

        protected override Flying GenFlying(Spell spell, float angleOffset, float flyingDistanceModify)
        {
            float flyingDistance = data.distance + flyingDistanceModify;

            var f = Flyings.Factory.Instance.Get(data.flyingType, data.flyingID);
            f.Src = spell.speller.Position;
            Vector3 dir = (CameraSys.MouseHitPosition - spell.speller.Position).normalized;
            Quaternion rotate = Quaternion.Euler(0, angleOffset, 0);
            f.target = new FixedFlyingTarget()
            {
                Position = f.Src + rotate * dir * flyingDistance,
            };

            return f;
        }

        protected override Trigger GenProjectile(Flying flying)
        {
            var t = Triggers.Factory.Instance.Get(data.projectileType, data.projectileID);
            t.attach = flying;
            flying.OnHit = t.Trig;

            return t;
        }
    }
    public class MAreaFixedDisCon : BaseConstructor<AreaFixedDisData, MAreaFixedDis, MAreaFixedDisCon>
    {
        protected override string RelativePath => "Spell/Action/AreaFixedDis.csv";

        protected override MAreaFixedDis Get()
        {
            return new MAreaFixedDis();
        }

        protected override void Set(MAreaFixedDis e, in AreaFixedDisData data)
        {
            e.data = data;
        }
    }
}

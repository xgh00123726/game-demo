using GameBase.GCamera;
using GameBase.Triggers;
using GameBase.Spells;
using GameBase.EntitySystem;
using UnityEngine;
using Constructor.Spells.Interactive;
using GameBase.Flyings;

namespace Constructor.Spells.Action
{
    public struct AreaFixedDisData
    {
        public Flyings.Type flyingType;
        public int flyingID;
        public Triggers.Type projectileType;
        public int projectileID;
        public int distance;
    }

    public class AreaFixedDis : ISpellAction
    {
        public AreaFixedDisData data;
        bool ISpellAction.CastAction(Spell spell)
        {
            var speller = spell.speller as ITriggerOwner;
            var interactive = spell.interactive as IDotInput;
            if (speller == null || interactive == null)
            {
                return false;
            }

            var f = Flyings.Factory.Instance.Get(data.flyingType, data.flyingID);
            f.Src = spell.speller.Position;
            Vector3 dir = (interactive.Position - spell.speller.Position).normalized;
            f.target = new FixedFlyingTarget()
            {
                Position = f.Src + dir * data.distance,
            };

            var t = Triggers.Factory.Instance.Get(data.projectileType, data.projectileID);
            t.owner = speller;
            t.attach = f;
            f.OnHit = t.Trig;

            return true;
        }
    }

    public class AreaFixedDisCon : BaseConstructor<AreaFixedDisData, AreaFixedDis, AreaFixedDisCon>
    {
        protected override string RelativePath => "Spell/Action/AreaFixedDis.csv";

        protected override AreaFixedDis Get()
        {
            return new AreaFixedDis();
        }

        protected override void Set(AreaFixedDis e, in AreaFixedDisData data)
        {
            e.data = data;
        }
    }
}

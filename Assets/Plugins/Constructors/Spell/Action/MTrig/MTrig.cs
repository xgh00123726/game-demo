using Constructor.Projectiles;
using Constructor.Triggers;
using GameBase.Creatures;
using GameBase.Projectiles;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Spells
{
    public class MTrig : ModifyableAction
    {
        public MTrigData Data { get; set; }
        protected override bool CastAction(Spell spell, in SpellActionModifierData modifyData)
        {
            if (spell.Speller is Creature c)
            {
                var t = TriggerYamlFactory.Instance.GetFromData(Data.Trigger);
                var trigPosition = Vector3.zero;
                if (Data.TrigPositionStyle == TrigPositionStyle.Mouse)
                {
                    trigPosition = spell.CastPosition;
                }
                else if (Data.TrigPositionStyle == TrigPositionStyle.Self)
                {
                    trigPosition = c.Position;
                }

                if (t.Shape != null)
                {
                    t.Shape.Dir = spell.CastPosition - spell.Speller.Position;
                }

                t.Attach = new FixedAttacher()
                {
                    Position = trigPosition
                };
                t.Owner = c;
                t.TargetsSet = CommonTargetSet.Instance;
                t.TargetCamp = (Camp)spell.TargetCamp.ToUint();

                return true;
            }

            return false;
        }
    }
}

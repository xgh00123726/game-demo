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
        public MTrigData data;
        protected override bool CastAction(Spell spell, in SpellActionModifierData modifyData)
        {
            if (spell.speller is Creature c)
            {
                var t = TriggerYamlFactory.Instance.GetFromData(data.trigger);
                var trigPosition = Vector3.zero;
                if (data.trigPositionStyle == TrigPositionStyle.Mouse)
                {
                    trigPosition = spell.castPosition;
                }
                else if (data.trigPositionStyle == TrigPositionStyle.Self)
                {
                    trigPosition = c.Position;
                }

                if (t.shape != null)
                {
                    t.shape.Dir = spell.castPosition - spell.speller.Position;
                }

                t.attach = new FixedAttacher()
                {
                    Position = trigPosition
                };
                t.owner = c;
                t.targetsSet = CommonTargetSet.Instance;
                t.targetCamp = (Camp)spell.targetCamp.ToUint();

                return true;
            }

            return false;
        }
    }
}

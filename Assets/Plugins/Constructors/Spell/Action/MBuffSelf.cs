using GameBase.Buffs;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.EntitySystem;
using GameBase.Creatures;

namespace Constructor.Spells.Action
{
    /// <summary>
    /// 为自己施加buff
    /// </summary>
    public class MBuffSelf : ModifyableAction
    {
        protected override bool CastAction(Spell spell, in SpellActionModifierData modifyData)
        {
            if (spell.speller is Creature c)
            {
                c.AddBuff(data.buffID, data.duration);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

using GameBase.Spells;
using GameBase.UI;

namespace Instance.Spells
{
    public class ViewableSpell :
        IViewableSpell
    {
        public ViewableSpell(Spell spell)
        {
            this.spell = spell;
        }

        private Spell spell;

        float IViewableSpell.CoolingRemain => spell.CoolingTimeRemain;

        float IViewableSpell.CoolingSet => spell.coolingTimeSet;

        int IViewableSpell.Charge => 0;
    }
}

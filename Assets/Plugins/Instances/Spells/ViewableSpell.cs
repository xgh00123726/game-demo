using GameBase.Spells;
using GameBase.UI;

namespace Instance.Spells
{
    public class ViewableSpell :
        IViewableSpell
    {
        private Spell spell;
        private int _iconTextureID;

        public ViewableSpell(Spell spell, int iconTextureID)
        {
            this.spell = spell;
            _iconTextureID = iconTextureID;
        }

        float IViewableSpell.CoolingRemain => spell.CoolingTimeRemain;

        float IViewableSpell.CoolingSet => spell.coolingTimeSet;

        int IViewableSpell.Charge => 0;

        int IViewableSpell.IconTextureID => _iconTextureID;
    }
}

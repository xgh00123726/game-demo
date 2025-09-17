using GameBase.Spells;
using GameBase.UI;

namespace Instance.UI.MVC
{
    public class SpellViewInfo : ISpellViewInfo
    {
        private Spell _spell;
        public SpellViewInfo(Spell spell)
        {
            _spell = spell;
        }

        float ISpellViewInfo.CoolingRemain => _spell.CoolingTimeRemain;

        float ISpellViewInfo.CoolingSet => _spell.coolingTimeSet;
    }
}

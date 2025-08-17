using GameBase.Tools;
using GameBase.Spells;
using GameBase.UI;
using GameBase.Indicators;
using UnityEngine;

namespace Instance.Spells
{
    public class ViewableSpell :
        IViewableSpell
    {
        public ViewableSpell(Spell spell, int texureID)
        {
            this.spell = spell;
            this.textureID = texureID;
        }

        private int textureID;
        private Spell spell;

        float IViewableSpell.CoolingRemain => spell.CoolingTimeRemain;

        float IViewableSpell.CoolingSet => spell.coolingTimeSet;

        int IViewableSpell.Charge => 0;

        int IViewableSpell.TextureID => textureID;
    }
}

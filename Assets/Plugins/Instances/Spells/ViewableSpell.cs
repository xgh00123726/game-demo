using GameBase.Tools;
using GameBase.Spells;
using GameBase.UI;
using GameBase.Indicators;
using UnityEngine;

namespace Instance.Spells
{
    public class ViewableSpell : IndicatorSpell,
        IViewableSpell
    {
        public int textureID = 0;
        private static DetailUI detailUI;

        static ViewableSpell()
        {
            detailUI = DetailUISys.Instance.NewEntity<DetailUI>();
        }

        public ViewableSpell() : base()
        {
            RegistertoActivesDelegate += InitSpellConfig;
        }

        float IViewableSpell.CoolingRemain => CoolingTimeRemain;

        float IViewableSpell.CoolingSet => coolingTimeSet;

        int IViewableSpell.Charge => 0;

        int IViewableSpell.TextureID => textureID;

        private void InitSpellConfig(Spell e)
        {
            var spellItem = SpellPanel.Instance.NewEntity<SpellItem>();
            spellItem.bindSpell = this;

            spellItem.AfterInstantiateUObjectDelegate = (BasePanelItem item) =>
            {
                detailUI.detailables.Add(spellItem);
            };
        }
    }
}

using GameBase.Tools;
using GameBase.Spells;
using GameBase.UI;
using GameBase.Indicators;
using UnityEngine;

namespace Instance.Spells
{
    public class ViewableSpell : Spell,
        IViewableSpell
    {
        public int textureID = 0;
        private static DetailUI detailUI;

        static ViewableSpell()
        {
            detailUI = DetailUISys.Instance.NewEntity<DetailUI>();
        }

        public ViewableSpell()
        {
            RegistertoActivesDelegate += InitSpellConfig;
        }

        public KeyFunction hotKey;

        float IViewableSpell.CoolingRemain => CoolingTimeRemain;

        float IViewableSpell.CoolingSet => coolingTimeSet;

        int IViewableSpell.Charge => 0;

        int IViewableSpell.TextureID => textureID;

        private void InitSpellConfig(Spell e)
        {
            if (targetable)
            {
                ReadyJugDelegate = SpellReady;
                CastJugDelegate = SpellCast;
            }
            else
            {
                CastJugDelegate = SpellReady;
            }

            CancelJugDelegate = SpellCancel;

            var spellItem = SpellPanel.Instance.NewEntity<SpellItem>();
            spellItem.bindSpell = this;

            spellItem.AfterInstantiateUObjectDelegate = (BasePanelItem item) =>
            {
                detailUI.detailables.Add(spellItem);
            };
        }

        private bool SpellReady(Spell spell)
        {
            return Inputs.GetKeyDown(hotKey);
        }

        private bool SpellCast(Spell spell)
        {
            return Inputs.GetKeyDown(KeyFunction.MouseConfirm);
        }

        private bool SpellCancel(Spell spell)
        {
            return Inputs.GetKeyDown(KeyFunction.Cancel);
        }
    }
}

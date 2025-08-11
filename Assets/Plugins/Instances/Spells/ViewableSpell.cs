using GameBase.Tools;
using GameBase.Spell;
using GameBase.UI;

namespace GameBase.Instance
{
    public class ViewableSpell : Spell.Spell,
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

        private void InitSpellConfig(Spell.Spell e)
        {
            if (targetable)
            {
                ReadyDelegate = SpellReady;
                CastDelegate = SpellCast;
            }
            else
            {
                CastDelegate = SpellReady;
            }

            CancelDelegate = SpellCancel;

            var spellItem = SpellPanel.Instance.NewEntity<SpellItem>();
            spellItem.bindSpell = this;

            spellItem.AfterInstantiateUObjectDelegate = (BasePanelItem item) =>
            {
                detailUI.detailables.Add(spellItem);
            };
        }

        private bool SpellReady(Spell.Spell spell)
        {
            return Inputs.GetKeyDown(hotKey);
        }

        private bool SpellCast(Spell.Spell spell)
        {
            return Inputs.GetKeyDown(KeyFunction.MouseConfirm);
        }

        private bool SpellCancel(Spell.Spell spell)
        {
            return Inputs.GetKeyDown(KeyFunction.Cancel);
        }
    }
}

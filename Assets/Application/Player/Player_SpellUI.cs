using GameBase.Modify;
using GameBase.Spells;
using GameBase.UI;
using Instance.Spells;
public partial class Player
{
    private DetailUI spellDetailUI;

    private void ShowSpellUI(Spell spell, int texureID)
    {
        var sitem = SpellPanel.Instance.NewEntity<SpellItem>();
        var viewable = new ViewableSpell(spell, texureID);
        sitem.bindSpell = viewable;

        sitem.AfterInstantiateUObjectDelegate = (BasePanelItem item) =>
        {
            spellDetailUI.detailables.Add(sitem);
        };
    }

    public void SpellUIInit()
    {
        spellDetailUI = DetailUISys.Instance.NewEntity<DetailUI>();

        foreach (var s in charater.SpellContainer)
        {
            ShowSpellUI(s, 1);
        }
    }
}

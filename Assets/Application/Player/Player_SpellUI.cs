using GameBase.Modify;
using GameBase.Spells;
using GameBase.UI;
using Instance.Spells;
public partial class Player
{
    private DetailUI spellDetailUI;

    private void ShowSpellUI(Spell spell, int texureID)
    {
        var item = SpellPanel.Instance.NewEntity((SpellItem e) =>
        {
            e.bindSpell = new ViewableSpell(spell);
        });

        spellDetailUI.detailables.Add(item);
    }

    public void SpellUIInit()
    {
        var e = SpellPanel.Instance;
        spellDetailUI = DetailUISys.Instance.NewEntity();

        foreach (var s in charater.SpellContainer)
        {
            ShowSpellUI(s, 1);
        }
    }
}

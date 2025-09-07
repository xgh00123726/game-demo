using GameBase.Modify;
using GameBase.Spells;
using GameBase.UI;
using Instance.Spells;
public partial class Player
{
    private void ShowSpellUI(Spell spell, int texureID)
    {
        var item = SpellPanel.Instance.NewEntity((SpellItem e) =>
        {
            e.bindSpell = new ViewableSpell(spell, texureID);
        });
    }

    public void SpellUIInit()
    {
        var e = SpellPanel.Instance;

        ShowSpellUI(charater.SpellContainer[0], 34);
        ShowSpellUI(charater.SpellContainer[1], 35);
        ShowSpellUI(charater.SpellContainer[2], 33);
        ShowSpellUI(charater.SpellContainer[3], 33);
    }
}

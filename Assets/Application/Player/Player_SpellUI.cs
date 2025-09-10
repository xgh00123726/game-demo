using GameBase.Modify;
using GameBase.Spells;
using GameBase.UI;
using Instance.Spells;
public partial class Player
{
    private void ShowSpellUI(Spell spell, int texureID)
    {
        var item = SpellViewPanel.Instance.NewEntity((SpellViewItem e) =>
        {
            e.bindSpell = new ViewableSpell(spell, texureID);
        });
    }

    public void SpellUIInit()
    {
        var e = SpellViewPanel.Instance;

        ShowSpellUI(character.spells[0], 34);
        ShowSpellUI(character.spells[1], 35);
        ShowSpellUI(character.spells[2], 33);
        ShowSpellUI(character.spells[3], 33);
    }
}

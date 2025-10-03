using GameBase.Inventorys;
using GameBase.Spells;
using GameBase.UI;
using Instance;
using Constructor.Spells.Main;
using Constructor.Spells.Action.Modifyables;
using GameBase.Tools;

public partial class Player
{
    private SpellViewPanel _spellViewPanel;
    private SpellActionModifierViewPanel _spellActionModifierViewPanel;

    private void AddSpell(Type type, int id)
    {
        var spell = Factory.Instance.Get(type, id);

        if (spell.action is ModifyableAction mAct)
        {
            mAct.Size = 5;
        }

        spell.speller = character;
        character.spells.Add(spell);
    }

    private void OnClickSpell(int index)
    {
        _spellActionModifierViewPanel.UpdateSpell(character.spells[index]);
    }

    private void SpellInit()
    {
        _spellViewPanel = new SpellViewPanel();
        _spellActionModifierViewPanel = new SpellActionModifierViewPanel();

        _spellViewPanel.layout = new DefaultLayout()
        {
            xInterval = 235,
        };

        _spellActionModifierViewPanel.layout = new DefaultLayout()
        {
            xInterval = 180
        };

        AddSpell(Type.Common, 0);
        AddSpell(Type.Common, 1);
        AddSpell(Type.Common, 5);
        AddSpell(Type.Common, 4);

        _spellViewPanel.OnClickedItem = OnClickSpell;

        //var spella = Constructor.Spells.Main.Common.Instance.Get(2);
        //spella.speller = character;
        _spellViewPanel.UpdateOwner(character);
        _spellViewPanel.Show();
        SpellCaster.Instance.SetActiverSpeller(character);
    }

    private void SpellUpdate()
    {
        _spellViewPanel.UpdateCooling(character);
    }
}

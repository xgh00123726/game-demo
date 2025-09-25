using GameBase.Inventorys;
using GameBase.Spells;
using GameBase.UI;
using Instance;
using Constructor.Spells.Main;

public partial class Player
{
    private SpellViewPanel _spellViewPanel;
    private SpellController _spellController;

    private void AddSpell(Type type, int id)
    {
        var spell = Factory.Instance.Get(type, id);
        spell.speller = character;
        character.spells.AddItem(spell);
    }

    private void SpellInit()
    {
        var _spellModel = new DynInventory<SpellItemData>();
        _spellController = new SpellController(_spellModel);
        _spellViewPanel = SpellViewPanel.Instance;
        _spellViewPanel.layout = new DefaultLayout()
        {
            xInterval = 235,
        };

        AddSpell(Type.Common, 0);
        AddSpell(Type.Common, 1);
        AddSpell(Type.Common, 5);
        AddSpell(Type.Common, 4);

        //var spella = Constructor.Spells.Main.Common.Instance.Get(2);
        //spella.speller = character;
        _spellViewPanel.SetOwner(character);
        _spellViewPanel.Show();
        SpellCaster.Instance.SetActiverSpeller(character);
    }

    private void SpellUpdate()
    {
        _spellViewPanel.SetCooling(character);
    }
}

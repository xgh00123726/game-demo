using GameBase.Inventorys;
using GameBase.Spells;
using GameBase.UI;

namespace Instance.MVC
{
    public class SpellController : MVController<SpellItemData, SpellViewItem, SpellViewPanel, SpellController>
    {
        public SpellModel spellModel;

        protected override IInventoryModel<SpellItemData> Model => spellModel;

        protected override SpellViewPanel View => SpellViewPanel.Instance;

        protected override IDataBase<SpellItemData> DataBase => throw new System.NotImplementedException();

        protected override void SetIcon(SpellItemData modelData, SpellViewItem viewItem)
        {
            
        }
    }
}

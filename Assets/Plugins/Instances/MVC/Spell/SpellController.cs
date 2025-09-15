using GameBase.Inventorys;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.UI;

namespace Instance.MVC
{
    public class SpellController : MVController<SpellItemData, SpellViewItem, SpellViewPanel, SpellController>
    {
        public SpellModel spellModel;

        protected override IInventoryModel<SpellItemData> Model => spellModel;

        protected override SpellViewPanel View => SpellViewPanel.Instance;

        protected override IDataBase<SpellItemData> DataBase => throw new System.NotImplementedException();

        public override int AddItem(SpellItemData item)
        {
            var ret = base.AddItem(item);
            View[ret].viewInfo = new SpellViewInfo(spellModel.owner.GetSpell(ret));
            return ret;
        }

        public override int AddItem(SpellItemData item, int index)
        {
            var ret = base.AddItem(item, index);
            View[ret].viewInfo = new SpellViewInfo(spellModel.owner.GetSpell(ret));
            return ret;
        }

        protected override void SetIcon(SpellItemData modelData, SpellViewItem viewItem)
        {
            viewItem.iconTextureID = modelData.iconTextureID;
            View.SetIcon(viewItem);
        }

        public int LastClickedItemIndex => View.LastClickedItemIndex;
    }
}

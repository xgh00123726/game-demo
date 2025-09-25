using GameBase.Inventorys;
using GameBase.UI;
using System;
using GameBase.Creatures;

namespace Instance
{
    public struct SpellItemData
    {
        public Constructor.Spells.Main.Type type;
        public int id;
        public int iconTextureID;
    }
    public class SpellController : InventoryController<SpellViewItem, SpellItemData>
    {
        private new SpellViewPanel _view;
        private new DynInventory<SpellItemData> _model;
        public SpellController(DynInventory<SpellItemData> model) : base(SpellViewPanel.Instance, model)
        {
            _view = SpellViewPanel.Instance;
            _model = model;
        }

        public Action<int> OnClickedItem
        {
            get => _view.OnClickedItem;
            set => _view.OnClickedItem = value;
        }
        public int LastClickedItemIndex => _view.LastClickedItemIndex;
    }
}

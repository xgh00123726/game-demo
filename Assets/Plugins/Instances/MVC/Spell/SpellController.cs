using GameBase.Inventorys;
using GameBase.UI;
using System;
using GameBase.UI.MVC;
using GameBase.Creatures;

namespace Instance.UI.MVC
{
    public class SpellController : MVController<SpellItemData, SpellViewItem, SpellViewPanel, SpellController>
    {
        private SpellModel _model = new();
        public Action<int> OnClickedItem
        {
            get => View.OnClickedItem;
            set => View.OnClickedItem = value;
        }

        protected override IMVCModel<SpellItemData> Model => _model;

        protected override SpellViewPanel View => SpellViewPanel.Instance;

        public override int AddItem(SpellItemData item)
        {
            var ret = base.AddItem(item);
            return ret;
        }

        public override int AddItem(SpellItemData item, int index)
        {
            var ret = base.AddItem(item, index);
            return ret;
        }

        protected override void SetItem(SpellItemData modelData, SpellViewItem viewItem)
        {
            View.SetIcon(viewItem, modelData.iconTextureID);
        }

        public int LastClickedItemIndex => View.LastClickedItemIndex;

        public void SetOwner(Creature owner)
        {
            _model.owner = owner;
        }

        protected override void Update()
        {
            for (int i = 0; i < Model.Size; ++i)
            {
                View[i].coolingTimeRemain = _model.owner.spells[i].spellCoolingdown.CoolingRemain;
                View[i].coolingTimeSet = _model.owner.spells[i].spellCoolingdown.CoolingSet;
            }
        }
    }
}

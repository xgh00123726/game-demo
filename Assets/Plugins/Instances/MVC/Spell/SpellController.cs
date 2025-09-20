using GameBase.Inventorys;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.UI;
using System;

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

        protected override IDataBase<SpellItemData> DataBase => throw new System.NotImplementedException();

        public override int AddItem(SpellItemData item)
        {
            var ret = base.AddItem(item);
            View[ret].viewInfo = new SpellViewInfo(_model.owner.GetSpell(ret));
            return ret;
        }

        public override int AddItem(SpellItemData item, int index)
        {
            var ret = base.AddItem(item, index);
            View[ret].viewInfo = new SpellViewInfo(_model.owner.GetSpell(ret));
            return ret;
        }

        protected override void SetItem(SpellItemData modelData, SpellViewItem viewItem)
        {
            View.SetIcon(viewItem, modelData.iconTextureID);
        }

        public int LastClickedItemIndex => View.LastClickedItemIndex;

        public void SetOwner<T_Owner>(T_Owner owner) where T_Owner : ISpellModelOwner
        {
            _model.owner = owner;
        }
    }
}

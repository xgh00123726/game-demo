using GameBase.Inventorys;
using GameBase.Modify;
using GameBase.Tools;
using GameBase.UI;
using GameBase.UI.MVC;

namespace Instance.UI.MVC
{
    public class AttrController : MVController<int, AttrViewItem, AttrViewPanel, AttrController>
    {
        private AttrModel _model = new()
        {
            0,1,3,2,5,7,4,12,14,6,8,9
        };

        private IModifieder _owner;

        public AttrController()
        {
            _view = new AttrViewPanel();
            Size = 12;
            ForceRefreshView();
        }

        public override IMVCModel<int> Model => _model;
        private AttrViewPanel _view;

        public override AttrViewPanel View => _view;

        protected override void SetItem(int modelData, AttrViewItem viewItem)
        {
            viewItem.SetIconSprite(ModifyTable.GetIconTextureID(modelData)); 
        }

        protected override void Update()
        {
            if (_owner == null)
            {
                return;
            }
            for (int i = 0; i < Size; ++i)
            {
                float value = _owner.Modifyables[Model[i]];
                View[i].Value = value;
            }
        }

        public IModifieder Owner
        {
            get => _owner;
            set
            {
                _owner = value;
            }
        }
    }
}

using GameBase.Inventorys;
using GameBase.Modify;
using GameBase.Tools;
using GameBase.UI;

namespace Instance.UI.MVC
{
    public class AttrController : MVController<int, AttrViewItem, AttrViewPanel, AttrController>
    {
        private AttrModel _model = new()
        {
            0,1,2,3,4,5,6,7,
        };

        private IModifieder _owner;

        public AttrController()
        {
            Size = 8;
            ForceRefreshView();
        }

        protected override IMVCModel<int> Model => _model;

        protected override AttrViewPanel View => AttrViewPanel.Instance;

        protected override IDataBase<int> DataBase => throw new System.NotImplementedException();

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

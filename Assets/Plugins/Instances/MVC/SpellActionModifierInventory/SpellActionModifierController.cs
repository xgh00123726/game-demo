using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.UI;
using UnityEngine;
namespace Instance.MVC
{
    public class SpellActionModifierController : InventoryController<CommonItemData, 
        SpellActionModifierViewItem, 
        SpellActionModifierViewPanel, 
        SpellActionModifierController>
    {
        private SpellActionModifierInventoryModel _model = new();

        public SpellActionModifierController()
        {
            for (int i = 0; i < 5; ++i)
            {
                View.NewEntity();
            }
        }

        protected override IInventoryModel<CommonItemData> Model => _model;

        protected override SpellActionModifierViewPanel View => SpellActionModifierViewPanel.Instance;

        protected override IDataBase<CommonItemData> DataBase => throw new System.NotImplementedException();

        protected override void SetIcon(CommonItemData modelData, SpellActionModifierViewItem viewItem)
        {
            var texture = ResourcesLoader.GetTexture2D(modelData.iconTextureID);
            viewItem.IconSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}

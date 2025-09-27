using GameBase.Inventorys;
using GameBase.Tools;
using Instance;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class ShopViewPanel : BaseViewPanel<ShopViewItem>
    {
        private BaseUI _refreshIconScript;

        private static ShopViewPanel _instance = new ShopViewPanel(41, 40);
        public static ShopViewPanel Instance => _instance;

        public IEnterExitControl RefreshControl
        {
            set => _refreshIconScript.enterExitControl = value;
        }

        public ShopViewPanel(int prefabID = 41, int defaultObjID = 40) : base(prefabID, defaultObjID)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("instance has only one");
            }
            _refreshIconScript = panel.transform.Find("RefreshIcon").gameObject.AddComponent<BaseUI>();
        }

        protected override BaseUI InstantiateObj(ShopViewItem e)
        {
            var obj = base.InstantiateObj(e);

            e.identifyIconObj = e.obj.transform.Find("Identify").gameObject;

            var image = e.identifyIconObj.GetComponent<Image>();
            e.identifyImage = new SuperImage(image);

            e.priceText = e.obj.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();

            return obj;
        }

        public void UpdateInventoryItem(ShopInventory inventory, int index)
        {
            FillItem(inventory.Size);
            if (inventory.HasItem(index))
            {
                var info = inventory.GetItemInfoFromShoppingPosition(index);
                var viewItem = this[index];
                viewItem.triggerImage.SetColor(info.rarity);

                
                // 如果表中没有配置材质贴图，并且商品是背包物品，那就使用其物品的材质号
                if (info.iconTextureID < 0 && info.type == ShopItemType.InventoryItem)
                {
                    //viewItem.triggerImage.SetIcon(CommonInventoryDataBase.Get(info.secondID).iconTextureID);
                    viewItem.identifyImage.SetIcon(62);
                }
                // 如果是增益效果，那就使用在表格中配置的材质贴图
                else if (info.type == ShopItemType.Buff)
                {
                    viewItem.identifyImage.SetIcon(61);
                    viewItem.triggerImage.SetIcon(info.iconTextureID);
                }

                viewItem.Value = info.price; 
                viewItem.obj.SetActive(true);
                viewItem.triggerImage.SetColor(info.rarity);
                viewItem.triggerImage.Show();
            }
        }

        public void UpdateInventory(ShopInventory inventory)
        {
            for (int i = 0; i < inventory.Size; i++)
            {
                UpdateInventoryItem(inventory, i);
            }
        }
    }
}

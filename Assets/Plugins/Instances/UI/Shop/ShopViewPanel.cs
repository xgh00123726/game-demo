using GameBase.Buffs;
using GameBase.Inventorys;
using GameBase.Tools;
using Instance.UI.Shops;
using System;
using TMPro;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class ShopViewPanel : BaseViewPanel<ShopViewItem>
    {
        private BaseUI _refreshIconScript;

        private static ShopViewPanel _instance;
        public static ShopViewPanel Instance => _instance;

        private Action _OnRefreshIconPush;
        public Action OnRefreshIconPush
        {
            get => _OnRefreshIconPush;
            set
            {
                _OnRefreshIconPush = value;
                _refreshIconScript.OnPointerDown = (i) => _OnRefreshIconPush?.Invoke();
            }
        }

        public ShopViewPanel(int prefabID = 41, int defaultObjID = 40) : base(prefabID, defaultObjID)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("instance has only one");
            }
            _instance = this;
            _refreshIconScript = panel.transform.Find("RefreshIcon").gameObject.AddComponent<BaseUI>();
        }

        protected override BaseUI InstantiateObj(ShopViewItem e)
        {
            var obj = base.InstantiateObj(e);

            e.identifyIconObj = e.obj.transform.Find("Identify").gameObject;

            var image = e.identifyIconObj.GetComponent<Image>();
            e.identifyImage = new SuperImage(image);

            e.priceText = e.obj.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();

            e.identifyText = e.obj.transform.Find("IdentifyText").GetComponent<TextMeshProUGUI>();

            return obj;
        }

        public void UpdateItem(ShopInventory model, int index)
        {
            FillItem(model.Size);
            var viewItem = this[index];
            if (model.HasItem(index))
            {
                var info = model.GetItemInfoOfShoppingPosition(index);
                viewItem.triggerImage.SetColor(info.rarity);

                // 如果表中没有配置材质贴图，并且商品是背包物品，那就使用其物品的材质号
                if (info.iconTextureID < 0 && info.type == ShopItemType.InventoryItem)
                {
                    var eInfo = BuffDataBase.Instance[info.id];
                    viewItem.triggerImage.SetIcon(eInfo.iconTextureID);
                    viewItem.identifyText.text = $"装备";
                    viewItem.identifyImage.SetIcon(62);
                }
                // 如果是增益效果，那就使用在表格中配置的材质贴图
                else if (info.type == ShopItemType.Buff)
                {
                    viewItem.identifyImage.SetIcon(61);
                    viewItem.identifyText.text = $"Buff";
                    viewItem.triggerImage.SetIcon(info.iconTextureID);
                }
                else if (info.type == ShopItemType.Modifier)
                {
                    viewItem.identifyImage.SetIcon(61);
                    viewItem.identifyText.text = $"属性";
                    viewItem.triggerImage.SetIcon(info.iconTextureID);
                }

                viewItem.Value = info.price; 
                viewItem.obj.SetActive(true);
                viewItem.triggerImage.SetColor(info.rarity);
                viewItem.triggerImage.Show();
            }
            else
            {
                viewItem.HideValue();
                viewItem.obj.SetActive(false);
            }
        }

        public void UpdatePanel(ShopInventory inventory)
        {
            for (int i = 0; i < inventory.Size; i++)
            {
                UpdateItem(inventory, i);
            }
        }
    }
}

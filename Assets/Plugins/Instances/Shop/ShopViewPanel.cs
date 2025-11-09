using GameBase.Buffs;
using GameBase.Inventorys;
using GameBase.Tools;
using GameBase.UI;
using System;
using TMPro;
using UnityEngine.UI;

namespace Instance
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

        public ShopViewPanel(string prefabName = "Prefabs/UI/ShopPanel.prefab",
            string itemPrefabName = "Prefabs/UI/ShopItem.prefab") : base(prefabName, itemPrefabName)
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
                var goodID = model[index];
                var info = ShopDataBase.Instance[goodID];
                viewItem.triggerImage.SetColor(info.rarity);

                // 如果表中没有配置材质贴图，并且商品是背包物品，那就使用其物品的材质号
                if (info.textureName == null && info.type == ShopItemType.InventoryItem)
                {
                    var eInfo = BuffDataBase.Instance[info.typeID];
                    viewItem.triggerImage.SetIcon(eInfo.textureName);
                    viewItem.identifyText.text = $"装备";
                    viewItem.identifyImage.SetIcon("Textures/Equipment/base.png");
                }
                // 如果是增益效果，那就使用在表格中配置的材质贴图
                else if (info.type == ShopItemType.Buff)
                {
                    viewItem.identifyImage.SetIcon("Textures/Increasing/base.png");
                    viewItem.identifyText.text = $"Buff";
                    viewItem.triggerImage.SetIcon(info.textureName);
                }
                else if (info.type == ShopItemType.Modifier)
                {
                    viewItem.identifyImage.SetIcon("Textures/Increasing/base.png");
                    viewItem.identifyText.text = $"属性";
                    viewItem.triggerImage.SetIcon(info.textureName);
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

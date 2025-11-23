using GameBase.Buffs;
using GameBase.Equipments;
using GameBase.Inventorys;
using GameBase.Items;
using GameBase.Tools;
using GameBase.UI;
using System;
using TMPro;
using UnityEngine.UI;

namespace Instance
{
    public class ShopViewPanel : BaseViewPanel<ShopViewItem, ShopViewPanel>
    {
        protected override string PanelPrefabName => "Prefabs/UI/ShopPanel.prefab";
        protected override string ItemPrefabName => "Prefabs/UI/ShopItem.prefab";

        private BaseUI _refreshIconScript;
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

        public ShopViewPanel()
        {
            _refreshIconScript = panel.transform.Find("RefreshIcon").gameObject.AddComponent<BaseUI>();
        }

        protected override void OnGet(ShopViewItem e)
        {
            base.OnGet(e);
            e.identifyIconObj = e.Obj.transform.Find("Identify").gameObject;

            var image = e.identifyIconObj.GetComponent<Image>();
            e.identifyImage = new SuperImage(image);

            e.priceText = e.Obj.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();

            e.identifyText = e.Obj.transform.Find("IdentifyText").GetComponent<TextMeshProUGUI>();
        }

        public void UpdateItem(ShopInventory model, int index)
        {
            FillItem(model.Size);
            var viewItem = this[index];
            if (model.HasItem(index))
            {
                var goodID = model[index];
                var info = ShopDataBase.Instance[goodID];
                viewItem.TriggerImage.SetColor(info.Rarity);

                // 如果表中没有配置材质贴图，并且商品是背包物品，那就使用其物品的材质号
                if (info.TextureName == null && info.Type == ShopItemType.InventoryItem)
                {
                    var equipData = ItemDataMgr.Get<EquipmentData>(info.TypeID);
                    viewItem.TriggerImage.SetIcon(equipData.TextureName);
                    viewItem.identifyText.text = $"装备";
                    viewItem.identifyImage.SetIcon("Textures/Equipment/base.png");
                }
                // 如果是增益效果，那就使用在表格中配置的材质贴图
                else if (info.Type == ShopItemType.Buff)
                {
                    viewItem.identifyImage.SetIcon("Textures/Increasing/base.png");
                    viewItem.identifyText.text = $"Buff";
                    viewItem.TriggerImage.SetIcon(info.TextureName);
                }
                else if (info.Type == ShopItemType.Modifier)
                {
                    viewItem.identifyImage.SetIcon("Textures/Increasing/base.png");
                    viewItem.identifyText.text = $"属性";
                    viewItem.TriggerImage.SetIcon(info.TextureName);
                }

                viewItem.Value = info.Price; 
                viewItem.Obj.SetActive(true);
                viewItem.TriggerImage.SetColor(info.Rarity);
                viewItem.TriggerImage.Show();
            }
            else
            {
                viewItem.HideValue();
                viewItem.Obj.SetActive(false);
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

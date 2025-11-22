using GameBase.Buffs;
using GameBase.Creatures;
using GameBase.Equipments;
using GameBase.Inventorys;
using GameBase.Items;
using GameBase.Tools;
using GameBase.UI;
using TMPro;
using UnityEngine.UI;

namespace Instance
{
    public class AttrSelectPanel : BaseViewPanel<AttrSelectItem>
    {
        private static AttrSelectPanel _instance;
        public static AttrSelectPanel Instance => _instance;
        public AttrSelectPanel(string prefabName = "Prefabs/UI/AttrSelectPanel.prefab", string itemPrefabName = "Prefabs/UI/AttrSelectItem.prefab") : base(prefabName, itemPrefabName)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("error");
            }
            _instance = this;
        }

        protected override BaseUI InstantiateObj(AttrSelectItem e)
        {
            var obj = base.InstantiateObj(e);

            e.iconObj = e.Obj.transform.Find("Icon").gameObject;

            var image = e.iconObj.GetComponent<Image>();
            e.iconImage = new SuperImage(image);

            e.text = e.Obj.transform.Find("Text").GetComponent<TextMeshProUGUI>();

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

                // 如果表中没有配置材质贴图，并且商品是背包物品，那就使用其物品的材质号
                if (info.TextureName == null && info.Type == ShopItemType.InventoryItem)
                {
                    var equipData = ItemDataMgr.Get<EquipmentData>(info.TypeID);
                    viewItem.iconImage.SetIcon(equipData.TextureName);
                    viewItem.text.text = $"装备";
                }
                // 如果是增益效果，那就使用在表格中配置的材质贴图
                else if (info.Type == ShopItemType.Buff)
                {
                    viewItem.text.text = $"Buff";
                    viewItem.iconImage.SetIcon(info.TextureName);
                }
                else if (info.Type == ShopItemType.Modifier)
                {
                    viewItem.text.text = $"属性";
                    viewItem.iconImage.SetIcon(info.TextureName);
                }

                viewItem.Obj.SetActive(true);
                var color = ViewConfig.GetColor(info.Rarity);
                color.a = viewItem.TriggerImage.Color.a;
                viewItem.TriggerImage.Color = color;
                viewItem.iconImage.Show();
            }
            else
            {
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

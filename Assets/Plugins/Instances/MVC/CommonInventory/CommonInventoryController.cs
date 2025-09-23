using GameBase.Config;
using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.UI;
using GameBase.UI.MVC;
using UnityEngine;

namespace Instance.UI.MVC
{
    public class CommonInventoryController : InventoryController<InventoryData,
        CommonInventoryViewItem,
        CommonInventoryViewPanel,
        CommonInventoryController>
    {
        private bool _showFlag = false;

        private CommonInventoryModel _inventoryModel = new();
        protected override IMVCModel<InventoryData> Model => _inventoryModel;
        protected override CommonInventoryViewPanel View => CommonInventoryViewPanel.Instance;
        public bool IsShow => _showFlag;

        public CommonInventoryController()
        {
            View.DragableControl = new CommonDragableControl();
            View.DetailableControl = new CommonDetailControl();
            View.EnterExitControl = new CommonFixedDetailableControl();
            View.panel.SetActive(false);
            
            Size = InventoryConfig.Int.InventoryPageCapacity;
        }

        /// <summary>
        /// 尝试获取database中index位置的物品数据
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool TryGetDataFromDataBase(int index, out InventoryData data)
        {
            if (index >= InventoryDataBase.Count)
            {
                data = default;
                return false;
            }
            else
            {
                data = InventoryDataBase.Read(index);
                return true;
            }
        }

        /// <summary>
        /// 从数据库中添加物品到背包
        /// <list type="bullet">
        /// <item><param name="dataBaseIndex"><paramref name="dataBaseIndex"/>物品在数据库中的位置</param></item>
        /// <item><param name="index"><paramref name="index"/>需要添加到背包的位置</param></item>
        /// </list></summary>
        public int AddItem(int dataBaseIndex, int index)
        {
            if (TryGetDataFromDataBase(dataBaseIndex, out InventoryData item))
            {
                return AddItem(item, index);
            }
            return -1;
        }

        /// <summary>
        /// 从数据库中添加物品到背包，位置为最近的空位
        /// <list type="bullet">
        /// <item><param name="dataBaseIndex"><paramref name="dataBaseIndex"/>物品在数据库中的位置</param></item>
        /// </list></summary>
        public int AddItem(int dataBaseIndex)
        {
            if (TryGetDataFromDataBase(dataBaseIndex, out InventoryData item))
            {
                return AddItem(item);
            }

            return -1;
        }

        public override void Show()
        {
            _showFlag = true;
            View.panelXOffsetTarget = 0;
            View.panel.SetActive(true);
        }
        public override void Hide()
        {
            _showFlag = false;
            View.panelXOffsetTarget = InventoryConfig.Float.InventoryPanelHideOffsetX;
            View.panelXMoveSpeed = InventoryConfig.Float.InventoryPanelHideSpeed;
            CommonFixedDetailableShadowView.Instance.Hide();
        }
        public override void Toggle()
        {
            if (_showFlag)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }
}

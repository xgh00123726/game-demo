using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using System.Collections.Generic;
using UnityEngine;
namespace Instance.MVC
{
    public class SpellActionModifierController : InventoryController<InventoryData, 
        SpellActionModifierViewItem, 
        SpellActionModifierViewPanel, 
        SpellActionModifierController>
    {
        private List<SpellActionModifierInventoryModel> _models = new()
        {
            new() {Size = 10},
            new() {Size = 10},
            new() {Size = 10},
            new() {Size = 10},
        };

        public SpellActionModifierController()
        {
            View.FillItem(5);
        }

        protected override IInventoryModel<InventoryData> Model
        {
            get
            {
                if (SpellController.Instance.LastClickedItemIndex == -1)
                {
                    return _models[0];
                }
                else
                {
                    return _models[SpellController.Instance.LastClickedItemIndex];
                }
            }
        }

        protected override SpellActionModifierViewPanel View => SpellActionModifierViewPanel.Instance;

        protected override IDataBase<InventoryData> DataBase => throw new System.NotImplementedException();

        public void RefreshView()
        {

        }

        protected override void SetIcon(InventoryData modelData, SpellActionModifierViewItem viewItem)
        {
            
        }
    }
}

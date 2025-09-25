using GameBase.UI;
using Instance;
using Instance.UI.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    internal CommonInventoryController inventoryController;
    private void InventoryUIInit()
    {
        var view = CommonInventoryViewPanel.Instance;
        var model = new GameBase.Inventorys.DynInventory<CommonInventoryData>();
        inventoryController = new CommonInventoryController(model);
        view.layout = new DefaultLayout()
        {
            xInterval = 115,
            yInterval = 115,
            width = 1200,
            height = 0,
            align = AlignType.Left | AlignType.Top
        };
        view.detailableControl = new CommonDetailControl(inventoryController, view);
        view.EnterExitControl = new CommonFixedDetailableControl();
        view.dragableControl = new CommonDragableControl(inventoryController, view);

        inventoryController.Size = 50;
        for (int i = 0; i < CommonInventoryDataBase.Count; ++i)
        {
            inventoryController.Add(CommonInventoryDataBase.Get(i));
        }
    }
}

using GameBase.Inventorys;
using GameBase.UI;
using Instance.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellActionModifierController : InventoryController<CommonItemData, SpellActionModifierViewItem, SpellActionModifierViewPanel, SpellActionModifierController>
{
    protected override IInventoryModel<CommonItemData> Model => throw new System.NotImplementedException();

    protected override SpellActionModifierViewPanel View => throw new System.NotImplementedException();

    protected override IDataBase<CommonItemData> DataBase => throw new System.NotImplementedException();

    protected override void SetIcon(CommonItemData modelData, SpellActionModifierViewItem viewItem)
    {
        throw new System.NotImplementedException();
    }
}

using GameBase.UI;
using Instance.Inventory;
using UnityEngine;

public class CommonDetailControl : IDetailableControl<InventoryViewItem>
{
    bool IDetailableControl<InventoryViewItem>.IsDetail(InventoryViewItem e)
    {
        return e.Obj.EnterTime > 1f;
    }

    void IDetailableControl<InventoryViewItem>.OnDetail(InventoryViewItem e)
    {
        CommonInventoryController.Instance.DataOfUI(e);
    }

    void IDetailableControl<InventoryViewItem>.OnEnterDetail(InventoryViewItem e)
    {
        CommonDetailShadowView.SetPosition(Input.mousePosition);
    }

    void IDetailableControl<InventoryViewItem>.OnExitDetail(InventoryViewItem e)
    {
        CommonDetailShadowView.Hide();
    }
}

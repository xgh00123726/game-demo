using GameBase.UI;
using Instance.Inventory;
using UnityEngine;

public class CommonDetailControl : IDetailableControl<InventoryViewItem>
{
    bool IDetailableControl<InventoryViewItem>.IsDetail(InventoryViewItem e)
    {
        return e.Obj.EnterTime > 0.2f;
    }

    void IDetailableControl<InventoryViewItem>.OnDetail(InventoryViewItem e)
    {
        CommonDetailShadowView.SetPosition(Input.mousePosition);
    }

    void IDetailableControl<InventoryViewItem>.OnEnterDetail(InventoryViewItem e)
    {
        CommonDetailShadowView.SetPosition(Input.mousePosition);

        if (CommonInventoryController.Instance.TryGetDataOfView(e, out var data))
        {
            CommonDetailShadowView.SetText($"buff id:{data.buffID}\ntexture id:{data.iconTextureID}");
        }
        else
        {
            CommonDetailShadowView.SetText($"this pos has no item");
        }
    }

    void IDetailableControl<InventoryViewItem>.OnExitDetail(InventoryViewItem e)
    {
        CommonDetailShadowView.Hide();
    }
}

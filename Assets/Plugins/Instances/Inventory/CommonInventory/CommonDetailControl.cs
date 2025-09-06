using GameBase.UI;
using Instance.Inventory;
using UnityEngine;

public class CommonDetailControl : IDetailableControl<CommonInventoryViewItem>
{
    bool IDetailableControl<CommonInventoryViewItem>.IsDetail(CommonInventoryViewItem e)
    {
        return e.Obj.EnterTime > 0.2f;
    }

    void IDetailableControl<CommonInventoryViewItem>.OnDetail(CommonInventoryViewItem e)
    {
        CommonDetailShadowView.SetPosition(Input.mousePosition);
    }

    void IDetailableControl<CommonInventoryViewItem>.OnEnterDetail(CommonInventoryViewItem e)
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

    void IDetailableControl<CommonInventoryViewItem>.OnExitDetail(CommonInventoryViewItem e)
    {
        CommonDetailShadowView.Hide();
    }
}

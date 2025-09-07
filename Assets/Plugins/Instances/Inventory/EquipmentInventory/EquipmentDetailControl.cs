using GameBase.UI;
using Instance.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentDetailControl : IDetailableControl<EquipmentViewItem>
{
    protected DetailableShadowView<EquipmentViewItem> ShadowView => DetailableShadowView<EquipmentViewItem>.Instance;
    bool IDetailableControl<EquipmentViewItem>.IsDetail(EquipmentViewItem e)
    {
        return e.Obj.EnterTime > 0.2f;
    }

    void IDetailableControl<EquipmentViewItem>.OnDetail(EquipmentViewItem e)
    {
        ShadowView.SetPosition(Input.mousePosition);
    }

    void IDetailableControl<EquipmentViewItem>.OnEnterDetail(EquipmentViewItem e)
    {
        ShadowView.SetPosition(Input.mousePosition);

        var index = EquipmentInventoryController.Instance.IndexOfView(e);
        if (index >= 0 && EquipmentInventoryController.Instance.TryGetData(index, out var data))
        {
            ShadowView.SetText($"index:{index}\nbuff id:{data.buffID}\ntexture id:{data.iconTextureID}");
        }
        else
        {
            ShadowView.SetText($"this pos has no item");
        }
    }

    void IDetailableControl<EquipmentViewItem>.OnExitDetail(EquipmentViewItem e)
    {
        ShadowView.Hide();
    }
}

using GameBase.UI;
using Instance.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentDetailControl : IDetailableControl<EquipmentViewItem>
{
    bool IDetailableControl<EquipmentViewItem>.IsDetail(EquipmentViewItem e)
    {
        return e.Obj.EnterTime > 0.2f;
    }

    void IDetailableControl<EquipmentViewItem>.OnDetail(EquipmentViewItem e)
    {
        EquipmentDetailShadowView.SetPosition(Input.mousePosition);
    }

    void IDetailableControl<EquipmentViewItem>.OnEnterDetail(EquipmentViewItem e)
    {
        EquipmentDetailShadowView.SetPosition(Input.mousePosition);

        if (EquipmentInventoryController.Instance.TryGetDataOfView(e, out var data))
        {
            EquipmentDetailShadowView.SetText($"buff id:{data.buffID}\ntexture id:{data.iconTextureID}");
        }
        else
        {
            EquipmentDetailShadowView.SetText($"this position has no item");
        }
    }

    void IDetailableControl<EquipmentViewItem>.OnExitDetail(EquipmentViewItem e)
    {
        EquipmentDetailShadowView.Hide();
    }
}

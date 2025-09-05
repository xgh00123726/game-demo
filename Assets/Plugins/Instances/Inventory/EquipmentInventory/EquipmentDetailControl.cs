using GameBase.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentDetailControl : IDetailableControl<EquipmentViewItem>
{
    bool IDetailableControl<EquipmentViewItem>.IsDetail(EquipmentViewItem e)
    {
        return e.Obj.EnterTime > 1f;
    }

    void IDetailableControl<EquipmentViewItem>.OnDetail(EquipmentViewItem e)
    {

    }

    void IDetailableControl<EquipmentViewItem>.OnEnterDetail(EquipmentViewItem e)
    {
        EquipmentDetailShadowView.SetPosition(Input.mousePosition);
    }

    void IDetailableControl<EquipmentViewItem>.OnExitDetail(EquipmentViewItem e)
    {
        EquipmentDetailShadowView.Hide();
    }
}

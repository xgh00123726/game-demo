using GameBase.Buffs;
using GameBase.Spells;
using GameBase.UI;
using Instance.Buffs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private DetailUI equipmentDetailUI;

    private void ShowEquipmentUI(Buff eb)
    {
        var ee = EquipmentPanel.Instance.NewEntity((EquipmentItem ee) =>
        {
            ee.bindEquipment = new ViewableEquipment(eb);
        });

        equipmentDetailUI.detailables.Add(ee);
    }

    public void EquipmentUIInit()
    {
        var e = EquipmentPanel.Instance;
        equipmentDetailUI = DetailUISys.Instance.NewEntity();
    }
}

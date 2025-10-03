using GameBase.UI;
using Instance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private EquipmentPanel _equipmentPanel;

    private void EquipmentInit()
    {
        _equipmentPanel = new EquipmentPanel();
        _equipmentPanel.dragableControl = new EquipmentDragableControl(_equipmentPanel, character.Equipments, InventoryViewPanel.Instance);
        _equipmentPanel.detailableControl = new EquipmentDetailableControl(character.Equipments, _equipmentPanel, new DefaultDetailableView());

        _equipmentPanel.layout = new DefaultLayout()
        {
            xInterval = 95,
            yInterval = 90,
            width = 315,
        };
    }


    private void EquipmentUpdate()
    {
        _equipmentPanel.UpdateView(character);
    }
}

using GameBase.Equipments;
using GameBase.Items;
using GameBase.Tools;
using UnityEngine;

public class ItemWarpper
{
    public static void Init()
    {
        ItemDataMgr.RegisterType<EquipmentData, EquipmentYamlFactory>(EquipmentYamlFactory.Instance);

        var checkResult = ItemDataMgr.Check();
        if (checkResult.Count == 0)
        {
            XLogger.Instance.Color(Color.green).Log("Item check success");
        }
        else
        {
            var tips = $"Your items id has null id lists: ";
            foreach (var id in checkResult)
            {
                tips += $"<color=red>{id}</color> ";
            }
            XLogger.Instance.Level(XLogger.LogLevel.Error)
                .Log(tips);
        }
    }
}

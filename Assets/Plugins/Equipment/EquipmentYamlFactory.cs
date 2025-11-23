using GameBase.EntitySystem;
using GameBase.Items;
using GameBase.Modify;
using GameBase.Tools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameBase.Equipments
{
    [GenTemplate]
    public class EquipmentYamlFactory : YamlFactory<EquipmentData, Equipment, EquipmentYamlFactory>,
        IItemDataRegister<EquipmentData>
    {
        protected override string Folder => $"{Application.streamingAssetsPath}/Equipment";

        List<EquipmentData> IItemDataRegister<EquipmentData>.GetRegisteredItems()
        {
            return Instance._dataDict.Values.ToList();
        }

        protected override void OnInitYamlData(ref EquipmentData data)
        {
            if (!SuperEnum.TryParse(data.Tag, out EquipmentTag tag))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid enum string: {data.Tag}");
            }
            else
            {
                data.TagEnum = tag;
            }

            data.IntKeyModifiers = new();
            foreach (var ps in data.Modifiers)
            {
                data.IntKeyModifiers.Add(new()
                {
                    Key = ModifyTable.GetID(ps.Key),
                    Value = ps.Value
                });
            }
        }

        protected override Equipment GetEntity(EquipmentData data)
        {
            var e = EquipmentSys.Instance.NewEntity();
            e.TextureName = data.TextureName;
            e.Rarity = data.Rarity;
            e.IntKeyModifiers = data.IntKeyModifiers;
            e.Tag = data.TagEnum;
            e.ID = data.ID;

            return e;
        }
    }
}

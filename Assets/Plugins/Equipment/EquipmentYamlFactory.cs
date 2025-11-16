using GameBase.EntitySystem;
using GameBase.Modify;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Equipments
{
    [GenTemplate]
    public class EquipmentYamlFactory : YamlFactory<EquipmentData, Equipment, EquipmentYamlFactory>
    {
        protected override string YamlFolder => $"{Application.streamingAssetsPath}/Equipment";

        protected override void OnInitYamlData(EquipmentData data)
        {
            if (!SuperEnum.TryParse(data.tag, out data.tagEnum))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid enum string: {data.tag}");
            }

            data.iModifiers = new();
            foreach (var ps in data.modifiers)
            {
                data.iModifiers.Add(new(ModifyTable.GetID(ps.Key), ps.Value));
            }
        }

        protected override Equipment GetEntity(EquipmentData data)
        {
            var e = EquipmentSys.Instance.NewEntity();
            e.textureName = data.textureName;
            e.rarity = data.rarity;
            e.iModifiers = data.iModifiers;
            e.tag = data.tagEnum;

            return e;
        }
    }
}

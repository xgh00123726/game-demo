using GameBase.EntitySystem;
using GameBase.Modify;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Buffs
{
    [GenTemplate]
    public class BuffYamlFactory : YamlFactory<BuffData, Buff, BuffYamlFactory>
    {
        protected override string YamlFolder => $"{Application.streamingAssetsPath}/Buff";

        protected override BuffData GetTemplateData()
        {
            var data = new BuffData();
            data.modifiers = new ()
            {
                new("cooldown", 0.1f),
                new("speed", 0.2f),
                new("attack", 0.3f)
            };

            return data;
        }

        protected override void OnInitYamlData(BuffData data)
        {
            if (!SuperEnum.TryParse(data.tag, out data.tagEnum))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid enum string: {data.tag}");
            }

            data.iModifiers = new();
            foreach (var ps in data.modifiers)
            {
                data.iModifiers.Add(new (ModifyTable.GetID(ps.Key), ps.Value));
            }
        }

        protected override Buff GetEntity(BuffData data)
        {
            var buff = BuffSys.Instance.NewEntity();
            buff.textureName = data.textureName;
            buff.rarity = data.rarity;
            buff.iModifiers = data.iModifiers;
            buff.tag = data.tagEnum;

            return buff;
        }
    }
}

using GameBase.EntitySystem;
using GameBase.Modify;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Buffs
{
    [GenTemplate]
    public class BuffYamlFactory : YamlFactory<BuffData, Buff, BuffYamlFactory>
    {
        protected override string Folder => $"{Application.streamingAssetsPath}/Buff";

        protected override BuffData GetTemplateData()
        {
            var data = new BuffData();
            data.Modifiers = new ()
            {
                new("cooldown", 0.1f),
                new("speed", 0.2f),
                new("attack", 0.3f)
            };

            return data;
        }

        protected override void OnInitYamlData(ref BuffData data)
        {
            if (!SuperEnum.TryParse(data.Tag, out BuffTag tag))
            {
                data.TagEnum = tag;
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid enum string: {data.Tag}");
            }

            data.IntKeyModifiers = new();
            foreach (var ps in data.Modifiers)
            {
                data.IntKeyModifiers.Add(new (ModifyTable.GetID(ps.Key), ps.Value));
            }
        }

        protected override Buff GetEntity(BuffData data)
        {
            var buff = BuffSys.Instance.NewEntity();
            buff.TextureName = data.TextureName;
            buff.Rarity = data.Rarity;
            buff.IntKeyModifiers = data.IntKeyModifiers;
            buff.Tag = data.TagEnum;

            return buff;
        }
    }
}

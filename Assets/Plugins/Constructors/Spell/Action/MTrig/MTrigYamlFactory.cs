using Constructor.Triggers;
using GameBase.EntitySystem;
using GameBase.Tools;
using UnityEngine;

namespace Constructor.Spells
{
    [GenTemplate]
    public class MTrigYamlFactory : YamlFactory<MTrigData, MTrig, MTrigYamlFactory>
    {
        protected override string Folder => $"{Application.streamingAssetsPath}/Spell/Action/MTrig";

        protected override void OnInitYamlData(ref MTrigData data)
        {
            if (data == null || data.Trigger == null)
            {
                return;
            }

            if (!SuperEnum.TryParse(data.Trigger.Tag, out TriggerActionTag tag))
            {
                data.Trigger.TagEnum = tag;
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid enum string: {data.Trigger.Tag}");
            }
        }

        protected override MTrig GetEntity(MTrigData data)
        {
            return new MTrig()
            {
                Size = data.SlotNum,
                Data = data,
            };
        }
    }
}

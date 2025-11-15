using Constructor.Triggers;
using GameBase.EntitySystem;
using GameBase.Tools;
using UnityEngine;

namespace Constructor.Spells
{
    [GenTemplate]
    public class MTrigYamlFactory : YamlFactory<MTrigData, MTrig, MTrigYamlFactory>
    {
        protected override string YamlFolder => $"{Application.streamingAssetsPath}/ConstructorData/Spell/Action/MTrig";

        protected override void OnInitYamlData(MTrigData data)
        {
            if (data == null || data.trigger == null)
            {
                return;
            }

            if (!SuperEnum.TryParse(data.trigger.tag, out data.trigger.tagEnum))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid enum string: {data.trigger.tag}");
            }
        }

        protected override MTrig GetEntity(MTrigData data)
        {
            return new MTrig()
            {
                Size = data.slotNum,
                data = data,
            };
        }
    }
}

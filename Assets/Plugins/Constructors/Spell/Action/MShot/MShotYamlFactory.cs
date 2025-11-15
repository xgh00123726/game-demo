using GameBase.EntitySystem;
using GameBase.Spells;
using GameBase.Tools;
using UnityEngine;

namespace Constructor.Spells
{
    public enum SpellActionType
    {
        MTriggerOnHit,
        MBuffSelf,
        MShot,

        MAreaFixedDis,
        MNearestTarget,
        MTriggerOnly,
    }
    [GenTemplate]
    public class MShotYamlFactory : YamlFactory<MShotData, ISpellAction, MShotYamlFactory>
    {
        protected override string YamlFolder => $"{Application.streamingAssetsPath}/ConstructorData/Spell/Action/MShot";

        protected override void OnInitYamlData(MShotData data)
        {
            if (data == null || data.projectile == null)
            {
                return;
            }
            if (!SuperEnum.TryParse(data.projectile.tag, out data.projectile.tagEnum))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid enum string: {data.projectile.tag}");
            }
        }

        protected override ISpellAction GetEntity(MShotData data)
        {
            return new MShot()
            {
                data = data,
                Size = data.slotNum,
            };
        }
    }
}

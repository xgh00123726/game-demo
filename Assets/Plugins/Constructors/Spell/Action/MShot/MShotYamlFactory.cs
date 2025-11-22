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
        protected override string Folder => $"{Application.streamingAssetsPath}/Spell/Action/MShot";

        protected override void OnInitYamlData(ref MShotData data)
        {
            if (data == null || data.Projectile == null)
            {
                return;
            }
            if (!SuperEnum.TryParse(data.Projectile.Tag, out GameBase.Projectiles.Tag tag))
            {
                data.Projectile.TagEnum = tag;
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid enum string: {data.Projectile.Tag}");
            }
        }

        protected override ISpellAction GetEntity(MShotData data)
        {
            return new MShot()
            {
                Data = data,
                Size = data.SlotNum,
            };
        }
    }
}

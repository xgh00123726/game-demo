using GameBase.AI;
using GameBase.Animations;
using GameBase.Creatures;
using GameBase.EntitySystem;
using UnityEngine;

namespace Constructor.Creatures
{
    public class CreatureYamlFactory : YamlFactory<CreatureData, Creature, CreatureYamlFactory>
    {
        protected override string YamlFolder => $"{Application.streamingAssetsPath}/ConstructorData/Creatures";

        protected override Creature GetEntity(CreatureData data)
        {
            var e = CreatureSys.Instance.NewEntity(data.prefabName);
            e.camp = data.camp;
            e.healthBarOffset = data.healthBarOffset;

            e.modifyables.Set("universal", 0f);
            e.modifyables.Set("moveSpeed", data.moveSpeed);
            e.modifyables.Set("strength", 0f);
            e.modifyables.Set("attackSpeed", 1f);
            e.modifyables.Set("agility", 0f);
            e.modifyables.Set("defense", 0f);
            e.modifyables.Set("intelligence", 0f);
            e.modifyables.Set("damage", data.damage);

            e.modifyables.Set("healthRegen", data.healthRegen);
            e.modifyables.Set("currHP", data.maxHP);
            e.modifyables.Set("maxHP", data.maxHP);
            e.modifyables.Set("currMana", 100f);
            e.modifyables.Set("maxMana", 100f);
            e.modifyables.Set("coolingAccelerate", 0f);
            e.modifyables.Set("rotateSpeed", 720f);
            e.modifyables.Set("attackRange", data.attackRange);
            AnimControllerFactory.Get(data.animType)?.AddTo(e);
            if (data.collideEnable)
            {
                e.AddCollider();
            }
            AIFactory.Get(data.aiType)?.AddTo(e);

            return e;
        }
    }
}

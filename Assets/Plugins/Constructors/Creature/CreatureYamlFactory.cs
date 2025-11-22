using GameBase.AI;
using GameBase.Animations;
using GameBase.Creatures;
using GameBase.EntitySystem;
using UnityEngine;

namespace Constructor.Creatures
{
    public class CreatureYamlFactory : YamlFactory<CreatureData, Creature, CreatureYamlFactory>
    {
        protected override string Folder => $"{Application.streamingAssetsPath}/Creatures";

        protected override Creature GetEntity(CreatureData data)
        {
            var e = CreatureSys.Instance.NewEntity(data.PrefabName);
            e.Camp = data.Camp;
            e.HealthBarOffset = data.HealthBarOffset;

            e.Modifyables.Set("universal", 0f);
            e.Modifyables.Set("moveSpeed", data.MoveSpeed);
            e.Modifyables.Set("strength", 0f);
            e.Modifyables.Set("attackSpeed", 1f);
            e.Modifyables.Set("agility", 0f);
            e.Modifyables.Set("defense", 0f);
            e.Modifyables.Set("intelligence", 0f);
            e.Modifyables.Set("damage", data.Damage);

            e.Modifyables.Set("healthRegen", data.HealthRegen);
            e.Modifyables.Set("currHP", data.MaxHP);
            e.Modifyables.Set("maxHP", data.MaxHP);
            e.Modifyables.Set("currMana", 100f);
            e.Modifyables.Set("maxMana", 100f);
            e.Modifyables.Set("coolingAccelerate", 0f);
            e.Modifyables.Set("rotateSpeed", 720f);
            e.Modifyables.Set("attackRange", data.AttackRange);
            AnimControllerFactory.Get(data.AnimType)?.AddTo(e);
            if (data.CollideEnable)
            {
                e.AddCollider();
            }
            AIFactory.Get(data.AIType)?.AddTo(e);

            return e;
        }
    }
}

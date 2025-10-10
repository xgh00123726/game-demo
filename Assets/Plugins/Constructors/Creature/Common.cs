using GameBase.AI;
using GameBase.Animations;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Tools;

namespace Constructor.Creatures
{
    public struct CommonData
    {
        public int ObjID;
        public CreatureTag tag;
        public AnimType animType;
        public AIType aiType;
        public float damage;
        public float moveSpeed;
        public float maxHP;
        public float attackRange;
        public float healthBarXOffset;
        public float healthBarYOffset;
        public float healthBarZOffset;
        public bool collideEnable;
    }
    public class Common : EntityConstructor<CommonData, Creature, CreatureSys, Common>
    {
        protected override string RelativePath => "Creatures/Common.csv";

        protected override CreatureSys SysInstance => CreatureSys.Instance;

        protected override void ESet(Creature e, in CommonData data)
        {
            e.ObjID = data.ObjID;
            e.tag = data.tag;
            e.healthBarOffset = new UnityEngine.Vector3(
                data.healthBarXOffset,
                data.healthBarYOffset,
                data.healthBarZOffset
                );

            e.modifyables.Set("universal", 0f);
            e.modifyables.Set("moveSpeed", data.moveSpeed);
            e.modifyables.Set("strength", 0f);
            e.modifyables.Set("attackSpeed", 1f);
            e.modifyables.Set("agility", 0f);
            e.modifyables.Set("defense", 0f);
            e.modifyables.Set("intelligence", 0f);
            e.modifyables.Set("damage", data.damage);

            e.modifyables.Set("currHP", data.maxHP);
            e.modifyables.Set("maxHP", data.maxHP);
            e.modifyables.Set("currMana", 100f);
            e.modifyables.Set("maxMana", 100f);
            e.modifyables.Set("coolingAccelerate", 0f);
            e.modifyables.Set("rotateSpeed", 720f);
            e.modifyables.Set("attackRange", data.attackRange);
        }

        protected override void Set(Creature e, in CommonData data)
        {
            base.Set(e, data);
            AnimControllerFactory.Get(data.animType)?.AddTo(e);
            if (data.collideEnable)
            {
                e.AddCollider();
            }
            AIFactory.Get(data.aiType)?.AddTo(e);
        }
    }
}

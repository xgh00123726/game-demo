using GameBase.Creatures;
using GameBase.EntitySystem;

namespace Constructor.Creatures
{
    public struct CommonData
    {
        public int ObjID;
        public CreatureTag tag;
        public float healthBarXOffset;
        public float healthBarYOffset;
        public float healthBarZOffset;
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

            e.Modifyables.Set("universal", 0f);
            e.Modifyables.Set("moveSpeed", 5f);
            e.Modifyables.Set("strength", 0f);
            e.Modifyables.Set("attackSpeed", 1f);
            e.Modifyables.Set("agility", 0f);
            e.Modifyables.Set("defense", 0f);
            e.Modifyables.Set("intelligence", 0f);
            e.Modifyables.Set("damage", 100f);

            e.Modifyables.Set("currHP", 100f);
            e.Modifyables.Set("maxHP", 100f);
            e.Modifyables.Set("currMana", 100f);
            e.Modifyables.Set("maxMana", 100f);
            e.Modifyables.Set("coolingAccelerate", 0f);
            e.Modifyables.Set("rotateSpeed", 720f);
            e.Modifyables.Set("attackRange", 20f);
        }
    }
}

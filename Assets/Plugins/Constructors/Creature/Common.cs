using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Modify;
using NReco.Csv;

namespace Constructor.Creatures
{
    public struct CommonData
    {
        public int ObjID;
        public int tag;
    }
    public class Common : EntityConstructor<CommonData, Creature, SimpleEntityContainer, CreatureSys, Common>
    {
        protected override string RelativePath => "Creatures/Common.csv";

        protected override CreatureSys SysInstance => CreatureSys.Instance;

        protected override void Parse(CsvReader line, ref CommonData data)
        {
            data.ObjID = int.Parse(line[1]);
            data.tag = int.Parse(line[2]);
        }

        protected override void Set(Creature e, in CommonData data)
        {
            e.ObjID = data.ObjID;
            e.tag = (GameBase.Creatures.Tag)(data.tag);

            e.ModifyableContainer["universal"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(0f);   // 0£¬universal
            e.ModifyableContainer["moveSpeed"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(5f);   // 1, moveSpeed
            e.ModifyableContainer["strength"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(0f);   // 2, strength
            e.ModifyableContainer["attackSpeed"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(1f);   // 3, attackSpeed
            e.ModifyableContainer["agility"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(0f);   // 4, agility
            e.ModifyableContainer["defense"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(0f);   // 5, defense
            e.ModifyableContainer["intelligence"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(0f);   // 6, intelligence
            e.ModifyableContainer["damage"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(100f); // 7, damage

            e.ModifyableContainer["currHP"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(100f); // 8, currHP
            e.ModifyableContainer["maxHP"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(100f); // 9, maxHP
            e.ModifyableContainer["currMana"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(100f); // 10,currMana
            e.ModifyableContainer["maxMana"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(100f); // 11,maxMana  
            e.ModifyableContainer["coolingAccelerate"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(0f);   // 12,coolingAccelerate
            e.ModifyableContainer["rotateSpeed"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(720f); // 13,coolingAccelerate
            e.ModifyableContainer["attackRange"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(10f); // 14,attackRange
        }
    }
}

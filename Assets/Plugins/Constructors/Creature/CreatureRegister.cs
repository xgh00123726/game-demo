using GameBase.Modify;
using GameBase.Move;
using GameBase.UI;
using Instance.Creatures;

public class CreatureRegister
{
    private static GameCreature Gen_0()
    {
        var e = CreatureSys.Instance.NewEntity<GameCreature>();

        e.AfterInstantiateObj += () =>
        {
            var em = MoveSys.Instance.NewEntity<Mover>();
            em.owner = e;

            var er = RotateSys.Instance.NewEntity<Rotater>();
            er.owner = e;

            var eh = HealthBarSys.Instance.NewEntity<HealthBar>();
            eh.owner = e;
            eh.ObjID = 6;
        };

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

        return e;
    }

    internal static void RegisterGenerator()
    {
        CreatureSys.Instance.RegisterEntityGenerateDeletate(0, Gen_0);
    }
}

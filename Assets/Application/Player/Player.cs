using GameBase.Spells;
using GameBase.Tools;
using UnityEngine;
using GameBase.GCamera;
using GameBase.UI;
using GameBase.Buffs;
using Instance.Spells;
using GameBase.Infos;
using GameBase.Animations;
using GameBase.Indicators;
using Instance.Indicators;
using GameBase.Creatures;

public partial class Player : MonoBehaviour,
    IPlayerGlobal
{
    public Creature charater;
    public PlayerAnimController2 animCtrler2;

    Vector3 IPlayerGlobal.Position => transform.position;



    private void SpellInit()
    {
        charater.SpellContainer[0] = SpellSys.Instance.NewEntity((Spell e) =>
        {
            e.interactive = new CommonInteractive(KeyFunction.Spell1, IndicatorSys.Instance.NewEntity<CircleIndicator>());
            e.coolingTimeSet = 5f;
            e.speller = charater;
            e.actionInterface = new AreaProjAction();
        });

        charater.SpellContainer[1] = SpellSys.Instance.NewEntity((Spell e) =>
        {
            e.interactive = new CommonInteractive(KeyFunction.Spell2, IndicatorSys.Instance.NewEntity<LinearIndicator>());
            e.coolingTimeSet = 5f;
            e.speller = charater;
            e.actionInterface = new TraceProjAction();
        });

        SpellSys.Instance.NewEntity((Spell e) =>
        {
            e.interactive = new CommonInteractive(KeyFunction.Aim, IndicatorSys.Instance.NewEntity<CircleIndicator>());
            e.coolingTimeSet = 1f;
            e.speller = charater;
            e.actionInterface = new TraceProjAction();
        });

    }

    private void EpicBarInit()
    {
        var epicHealthBar = EpicBarSys.Instance.NewEntity<EpicBar>();
        epicHealthBar.CurrHP = charater.ModifyableContainer["currHP"].Value;
        epicHealthBar.MaxHP = charater.ModifyableContainer["maxHP"].Value;

        var epicManaBar = EpicBarSys.Instance.NewEntity<EpicBar>();
        epicManaBar.CurrHP = charater.ModifyableContainer["currMana"].Value;
        epicManaBar.MaxHP = charater.ModifyableContainer["maxMana"].Value;
        epicManaBar.ObjID = 8;
    }

    private void PassiveInit()
    {
        for (int i = 0; i < 10; ++i)
        {
            var buff = BuffSys.Instance.NewEntity<Buff>();
            buff.durationSet = 9999;
            buff.owner = charater;
        }
    }

    private void EquipmentInit()
    {
        for (int i = 0; i < 6; ++i)
        {
            var equipment = BuffSys.Instance.NewEntity<Buff>();
            equipment.owner = charater;
            equipment.durationSet = 9999;
        }
    }

    private void AnimatorInit()
    {
        animCtrler2 = new PlayerAnimController2(charater);
    }

    private void GlobalInfoInit()
    {
        Globals.RegisterPlayer(this);
    }

    private void Start()
    {
        SpellInit();
        EpicBarInit();
        PassiveInit();
        EquipmentInit();
        AnimatorInit();
        GlobalInfoInit();

        AttrUIInit();
        SpellUIInit();
        BuffUIInit();
    }

    private void Update()
    {
        if (Inputs.GetKeyDown(KeyFunction.MoveTo))
        {
            charater.Dest = CameraSys.MouseHitPosition;
            charater.Dir = CameraSys.MouseHitPosition - charater.Obj.transform.position;
        }

        animCtrler2.Update();
    }
}

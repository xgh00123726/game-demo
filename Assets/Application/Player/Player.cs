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
        var spell1 = Constructor.Spells.Main.Common.Instance.Get(0);
        spell1.speller = charater;
        ((Constructor.Spells.Interactive.KeyInteractive)spell1.interactive).readyKey = KeyFunction.Spell1;
        charater.SpellContainer[0] = spell1;

        var spell2 = Constructor.Spells.Main.Common.Instance.Get(1);
        spell2.speller = charater;
        ((Constructor.Spells.Interactive.KeyInteractive)spell2.interactive).readyKey = KeyFunction.Spell2;
        charater.SpellContainer[1] = spell2;

        var spell3 = Constructor.Spells.Main.Common.Instance.Get(3);
        spell3.speller = charater;
        ((Constructor.Spells.Interactive.KeyInteractive)spell3.interactive).readyKey = KeyFunction.Spell3;
        charater.SpellContainer[2] = spell3;

        var spella = Constructor.Spells.Main.Common.Instance.Get(2);
        spella.speller = charater;
        ((Constructor.Spells.Interactive.KeyInteractive)spella.interactive).readyKey = KeyFunction.Aim;
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

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
using GameBase.Creatures;
using Instance.Move;
using Instance.Inventory;

public partial class Player : MonoBehaviour,
    IPlayerGlobal
{
    private Player _instance;

    public Creature charater;
    public PlayerAnimController2 animCtrler2;

    public Player Instance => _instance;
    Vector3 IPlayerGlobal.Position => transform.position;



    private void SpellInit()
    {
        var indicatorMutex = new IndicatorMutex();

        var spell1 = Constructor.Spells.Main.Common.Instance.Get(0);
        spell1.speller = charater;
        var interactive1 = (Constructor.Spells.Interactive.KeyInteractive)spell1.interactive;
        interactive1.readyKey = KeyFunction.Spell1;
        indicatorMutex.Add(interactive1.indicator);
        charater.SpellContainer[0] = spell1;

        var spell2 = Constructor.Spells.Main.Common.Instance.Get(1);
        spell2.speller = charater;
        var interactive2 = (Constructor.Spells.Interactive.KeyInteractive)spell2.interactive;
        interactive2.readyKey = KeyFunction.Spell2;
        indicatorMutex.Add(interactive2.indicator);
        charater.SpellContainer[1] = spell2;

        var spell3 = Constructor.Spells.Main.Common.Instance.Get(3);
        spell3.speller = charater;
        var interactive3 = (Constructor.Spells.Interactive.KeyInteractive)spell3.interactive;
        interactive3.readyKey = KeyFunction.Spell3;
        indicatorMutex.Add(interactive3.indicator);
        charater.SpellContainer[2] = spell3;

        var spell4 = Constructor.Spells.Main.Common.Instance.Get(4);
        spell4.speller = charater;
        var interactive4 = (Constructor.Spells.Interactive.KeyInteractive)spell4.interactive;
        interactive4.readyKey = KeyFunction.Spell4;
        indicatorMutex.Add(interactive4.indicator);
        charater.SpellContainer[3] = spell4;

        var spella = Constructor.Spells.Main.Common.Instance.Get(2);
        spella.speller = charater;
    }

    private void EpicBarInit()
    {
        var epicHealthBar = EpicBarSys.Instance.NewEntity();
        epicHealthBar.CurrHP = charater.ModifyableContainer["currHP"].Value;
        epicHealthBar.MaxHP = charater.ModifyableContainer["maxHP"].Value;

        var epicManaBar = EpicBarSys.Instance.NewEntity((EpicBar e) =>
        {
            e.ObjID = 8;
        });
        epicManaBar.CurrHP = charater.ModifyableContainer["currMana"].Value;
        epicManaBar.MaxHP = charater.ModifyableContainer["maxMana"].Value;
    }

    private void PassiveInit()
    {
    }

    private void EquipmentUIInit()
    {
        var e = EquipmentInventoryController.Instance;
        e.owner = charater;
    }

    private void EquipmentInit()
    {
    }

    private void InventoryUIInit()
    {
        for (int i = 0; i < 11; ++i)
        {
            CommonInventoryController.Instance.AddItem(i, 20 + i);
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
        AnimatorInit();
        GlobalInfoInit();

        AttrUIInit();
        SpellUIInit();
        BuffUIInit();
        EquipmentUIInit();
        InventoryUIInit();

        EquipmentInit();

        _instance = this;
    }

    private void Update()
    {
        if (Inputs.GetKeyDown(KeyFunction.MoveTo, "mover"))
        {
            charater.Dest = CameraSys.MouseHitPosition;
            charater.Dir = CameraSys.MouseHitPosition - charater.Obj.transform.position;
            MoveIndicator.Show(CameraSys.MouseHitPosition);
        }

        if (Inputs.GetKeyDown(KeyFunction.ToggleAttrPanel, "inventory"))
        {
            CommonInventoryController.Instance.Toggle();
            if (CommonInventoryController.Instance.IsShow)
            {
                Inputs.LockOthers("inventory");
            }
            else
            {
                Inputs.ReleaseAll();
            }
        }

        animCtrler2.Update();
    }
}

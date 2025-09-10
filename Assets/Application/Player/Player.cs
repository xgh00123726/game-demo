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
using Instance.MVC;
using Instance.Shops;
using GameBase.Shops;
using Constructor.Spells.Action.Modifyables.Modifier;

public partial class Player : MonoBehaviour,
    IPlayerGlobal,
    IShoper
{
    private Player _instance;

    public Creature character;
    public PlayerAnimController2 animCtrler2;

    public Player Instance => _instance;
    Vector3 IPlayerGlobal.Position => transform.position;

    public int Gold {  get; set; }

    Vector3 IShoper.Position => character.Position;

    bool IShoper.OpenShop => Inputs.GetKeyDown(KeyFunction.OpenShop, "shopping");
    bool IShoper.CloseShop => Inputs.GetKeyDown(KeyFunction.CloseShop, "shopping");

    private void SpellInit()
    {
        var spell1 = Constructor.Spells.Main.Common.Instance.Get(0);
        spell1.speller = character;
        var interactive1 = (Constructor.Spells.Interactive.KeyCommon)spell1.interactive;
        interactive1.readyKey = KeyFunction.Spell1;
        character.spells[0] = spell1;

        var spell2 = Constructor.Spells.Main.Common.Instance.Get(1);
        spell2.speller = character;
        var interactive2 = (Constructor.Spells.Interactive.KeyCommon)spell2.interactive;
        interactive2.readyKey = KeyFunction.Spell2;
        character.spells[1] = spell2;

        var spell3 = Constructor.Spells.Main.Common.Instance.Get(5);
        spell3.speller = character;
        var interactive3 = (Constructor.Spells.Interactive.KeyCommon)spell3.interactive;
        interactive3.readyKey = KeyFunction.Spell3;
        character.spells[2] = spell3;

        var spell4 = Constructor.Spells.Main.Common.Instance.Get(4);
        spell4.speller = character;
        var interactive4 = (Constructor.Spells.Interactive.KeyCommon)spell4.interactive;
        interactive4.readyKey = KeyFunction.Spell4;
        character.spells[3] = spell4;

        var spella = Constructor.Spells.Main.Common.Instance.Get(2);
        spella.speller = character;

        new FlyingNumModifier(4).ModifyTo(spell3);
        new FireDisfuseModifier(180).ModifyTo(spell3);
        new MultipleModifier(2).ModifyTo(spell3);

        Constructor.Spells.Action.Modifyables.Modifier.Factory.Instance.Get(
            Constructor.Spells.Action.Modifyables.Modifier.Type.MultipleModifier, -1).ModifyTo(spell3);
    }

    private void EpicBarInit()
    {
        var epicHealthBar = EpicBarSys.Instance.NewEntity();
        epicHealthBar.CurrHP = character.ModifyableContainer["currHP"].Value;
        epicHealthBar.MaxHP = character.ModifyableContainer["maxHP"].Value;

        var epicManaBar = EpicBarSys.Instance.NewEntity((EpicBar e) =>
        {
            e.ObjID = 8;
        });
        epicManaBar.CurrHP = character.ModifyableContainer["currMana"].Value;
        epicManaBar.MaxHP = character.ModifyableContainer["maxMana"].Value;
    }

    private void PassiveInit()
    {
    }

    private void EquipmentUIInit()
    {
        var e = EquipmentInventoryController.Instance;
        e.owner = character;
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
        animCtrler2 = new PlayerAnimController2(character);
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
            character.Dest = CameraSys.MouseHitPosition;
            character.Dir = CameraSys.MouseHitPosition - character.Obj.transform.position;
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

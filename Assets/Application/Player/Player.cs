using GameBase.Spells;
using GameBase.Tools;
using UnityEngine;
using GameBase.GCamera;
using GameBase.UI;
using GameBase.Buffs;
using GameBase.Infos;
using GameBase.Animations;
using GameBase.Indicators;
using GameBase.Creatures;
using Instance.Move;
using Instance.UI.Shops;
using Instance.UI.MVC;

public partial class Player : MonoBehaviour,
    IPlayerGlobal
{
    private static Player _instance;

    public Creature character;
    public PlayerAnimController2 animCtrler2;

    public static Player Instance => _instance;
    Vector3 IPlayerGlobal.Position => transform.position;

    private void EpicBarInit()
    {
        var epicHealthBar = EpicBarSys.Instance.NewEntity();
        epicHealthBar.CurrHP = character.Modifyables["currHP"];
        epicHealthBar.MaxHP = character.Modifyables["maxHP"];

        var epicManaBar = EpicBarSys.Instance.NewEntity((EpicBar e) =>
        {
            e.ObjID = 8;
        });
        epicManaBar.CurrHP = character.Modifyables["currMana"];
        epicManaBar.MaxHP = character.Modifyables["maxMana"];
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
        AnimatorInit();
        GlobalInfoInit();
        AttrInit();
        //AttrUIInit();

        BuffUIInit();
        //EquipmentUIInit();
        InventoryUIInit();

        //EquipmentInit();

        GenerateInitShop();

        _instance = this;
    }

    private void Update()
    {
        if (Inputs.GetKeyDown(KeyFunction.MoveTo, "mover"))
        {
            character.Mover.MoveTo(CameraSys.MouseHitPosition);
            character.Dir = CameraSys.MouseHitPosition - character.Obj.transform.position;
            MoveIndicator.Show(CameraSys.MouseHitPosition);
        }

        if (Inputs.GetKeyDown(KeyFunction.ToggleAttrPanel, "inventory"))
        {
            inventoryController.Toggle();
            if (inventoryController.IsShow)
            {
                Inputs.LockOthers("inventory");
            }
            else
            {
                Inputs.ReleaseAll();
                SpellActionModifierController.Instance.Hide();
            }
        }

        ShopUpdate();
        AttrUpdate();
        SpellUpdate();
        animCtrler2.Update();
    }
}

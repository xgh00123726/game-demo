using GameBase.Creatures;
using GameBase.Move;
using GameBase.Spells;
using GameBase.Flyings;
using GameBase.Tools;
using UnityEngine;
using GameBase.GCamera;
using GameBase.UI;
using GameBase.Modify;
using GameBase.Buffs;
using Instance.Buffs;
using Instance.Modify;
using Instance.Spells;
using GameBase.Infos;
using GameBase.Animations;
using GameBase.Math;
using GameBase.Indicators;
using Instance.Indicators;
using Combines.Projectiles;
using Instance.Creatures;

public partial class Player : MonoBehaviour,
    IPlayerGlobal
{
    public GameCreature charater;
    public PlayerAnimController2 animCtrler2;

    Vector3 IPlayerGlobal.Position => transform.position;



    private void SpellInit()
    {
        var spell_1 = SpellSys.Instance.NewEntity<IndicatorSpell>();
        spell_1.readyKey = KeyFunction.Spell1;
        spell_1.coolingTimeSet = 5f;
        spell_1.targetable = true;
        spell_1.type = IndicatorSpell.IndicatorType.Circle;
        spell_1.speller = charater;
        spell_1.CastAction += SpellActions.FallingStone;
        charater.SpellContainer[0] = spell_1;

        var spell_2 = SpellSys.Instance.NewEntity<IndicatorSpell>();
        spell_2.readyKey = KeyFunction.Spell2;
        spell_2.coolingTimeSet = 5f;
        spell_2.speller = charater;
        spell_2.targetable = true;
        spell_2.type = IndicatorSpell.IndicatorType.Linear;
        spell_2.indicatorLength = 6;
        spell_2.CastAction += SpellActions.FrisbeeEmiter;
        charater.SpellContainer[1] = spell_2;

        var spell_3 = SpellSys.Instance.NewEntity<IndicatorSpell>();
        spell_3.readyKey = KeyFunction.Spell3;
        spell_3.coolingTimeSet = 5f;
        spell_3.speller = charater;
        spell_3.CastAction += SpellActions.Frisbeeing;
        charater.SpellContainer[2] = spell_3;

        var spell_4 = SpellSys.Instance.NewEntity<IndicatorSpell>();
        spell_4.readyKey = KeyFunction.Spell4;
        spell_4.coolingTimeSet = 0f;
        spell_4.speller = charater;
        spell_4.CastAction += SpellActions.Faster;
        charater.SpellContainer[3] = spell_4;

        var spell_a = SpellSys.Instance.NewEntity<IndicatorSpell>();
        spell_a.targetable = true;
        spell_a.coolingTimeSet = 1f;
        spell_a.speller = charater;
        spell_a.readyKey = KeyFunction.Aim;
        spell_a.type = IndicatorSpell.IndicatorType.Circle;
        spell_a.CastAction += SpellSys.Instance.GetSpellAction(1);
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

using GameBase.Creature;
using GameBase.Move;
using GameBase.Instance;
using GameBase.Projectile;
using GameBase.Spell;
using GameBase.Tools;
using UnityEngine;
using GameBase.GCamera;
using GameBase.UI;
using GameBase.Modify;
using GameBase.Buffs;
using System.Collections.Generic;
using Instance.Buffs;

public class Player1 : MonoBehaviour,
    IProjectileOwner,
    ISpeller,
    IBuffOwner,
    IModifyOwner<float>
{
    public Mover mover;
    public Rotater rotater;

    public List<float> attrs = new List<float>();

    private BuffContainer _buffContainer = new BuffContainer();
    private ModifyableContainer<float> _modifyables = new ModifyableContainer<float>();

    BuffContainer IBuffOwner.Buffs => _buffContainer;

    Vector3 IProjectileOwner.HandPostion => transform.position + new Vector3(0, 2, 0);

    float ISpeller.CoolingAccelerate => _modifyables[0].Value;

    GameObject ISpeller.GameObject => gameObject;

    ModifyableContainer<float> IModifyOwner<float>.Modifyables => _modifyables;

    private void Start()
    {
        mover = MoveSys.Instance.NewEntity<Mover>();
        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            mover.RigidyBody = gameObject.AddComponent<Rigidbody>();
        }
        mover.body = gameObject;
        mover.speed = 6f;

        rotater = RotateSys.Instance.NewEntity<Rotater>();
        rotater.body = gameObject;
        rotater.turnSpeed = 720;

        _modifyables[0] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>();

        //_modifyables[0] = ModifySys<float>.Instance.NewEntity<Modifyable<float>>();
        //_modifyables[0].AddModify((float val) => { return val + 100f; });

        var spell_1 = SpellSys.Instance.NewEntity<ViewableSpell>();
        spell_1.textureID = 1;
        spell_1.hotKey = KeyFunction.Spell1;
        spell_1.coolingTimeSet = 5f;
        spell_1.speller = PossibleObj<ISpeller>.New(this);
        spell_1.CastAction = SpellActions.Frisbee;

        var spell_2 = SpellSys.Instance.NewEntity<ViewableSpell>();
        spell_2.hotKey = KeyFunction.Spell2;
        spell_2.coolingTimeSet = 5f;
        spell_2.speller = PossibleObj<ISpeller>.New(this);
        spell_2.CastAction = SpellActions.FrisbeeEmiter;

        var spell_3 = SpellSys.Instance.NewEntity<ViewableSpell>();
        spell_3.hotKey = KeyFunction.Spell3;
        spell_3.coolingTimeSet = 5f;
        spell_3.speller = PossibleObj<ISpeller>.New(this);
        spell_3.CastAction = SpellActions.Ezreal_E;

        var spell_4 = SpellSys.Instance.NewEntity<ViewableSpell>();
        spell_4.hotKey = KeyFunction.Spell4;
        spell_4.coolingTimeSet = 5f;
        spell_4.speller = PossibleObj<ISpeller>.New(this);
        spell_4.CastAction = SpellActions.Faster;

        for (int i = 0; i < 10; ++i)
        {
            var buff = BuffSys.Instance.NewEntity<ViewablePassive>();
            buff.durationSet = 9999;
        }
    }

    private void Update()
    {
        if (Inputs.GetKeyDown(KeyFunction.MoveTo))
        {
            mover.Dest = CameraSys.MouseHitPosition;
            rotater.Dir = CameraSys.MouseHitPosition - rotater.body.transform.position;
        }

        if (Inputs.GetKeyDown(KeyFunction.Spell6))
        {
            var c = CreatureSys.Instance.NewEntity<Enemy1>();
            c.genPos = CameraSys.MouseHitPosition;
            c.ObjID = 0;
        }

        attrs.Clear();
        for (int i = 0; i < 1; i++)
        {
            attrs.Add(_modifyables[0].Value);
            attrs.Add(_modifyables[0].ValueSet);
        }
    }
}

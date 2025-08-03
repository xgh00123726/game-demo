using GameBase.Creature;
using GameBase.Move;
using GameBase.Instance;
using GameBase.Projectile;
using GameBase.Spell;
using GameBase.Tools;
using UnityEngine;
using GameBase.GCamera;
using GameBase.UI;

public class Player1 : MonoBehaviour,
    IProjectileOwner,
    ISpeller
{
    public Mover mover;
    public Rotater rotater;

    Vector3 IProjectileOwner.HandPostion => transform.position + new Vector3(0, 2, 0);

    float ISpeller.CoolingAccelerate => 0f;

    GameObject ISpeller.GameObject => gameObject;

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



        var spell_1 = SpellSys.Instance.NewEntity<PanelSpell>();
        spell_1.hotKey = KeyFunction.Spell1;
        spell_1.coolingTimeSet = 5f;
        spell_1.speller = PossibleObj<ISpeller>.New(this);
        spell_1.CastAction = SpellActions.Frisbee;
        var item_1 = SpellPanel.AddItem();
        item_1.SetIcon(0);
        item_1.spell = spell_1;

        var spell_2 = SpellSys.Instance.NewEntity<PanelSpell>();
        spell_2.hotKey = KeyFunction.Spell2;
        spell_2.speller = PossibleObj<ISpeller>.New(this);
        spell_2.CastAction = SpellActions.FrisbeeEmiter;

        var spell_3 = SpellSys.Instance.NewEntity<PanelSpell>();
        spell_3.hotKey = KeyFunction.Spell3;
        spell_3.speller = PossibleObj<ISpeller>.New(this);
        spell_3.CastAction = SpellActions.Ezreal_E;

        var spell_4 = SpellSys.Instance.NewEntity<PanelSpell>();
        spell_4.hotKey = KeyFunction.Spell4;
        spell_4.speller = PossibleObj<ISpeller>.New(this);
        spell_4.CastAction = SpellActions.Frisbeeing;
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
            c.bodyID = 0;
        }
    }
}

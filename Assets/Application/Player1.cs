using GameBase.Creature;
using GameBase.Move;
using GameBase.Instance;
using GameBase.Projectile;
using GameBase.Spell;
using GameBase.Tools;
using UnityEngine;

public class Player1 : MonoBehaviour,
    IProjectileOwner,
    ISpeller
{
    public Mover mover;
    public Rotater rotater;
    public Camera cam;
    public GameObject projectileTargetObject;
    public IProjectileTarget projectileTarget;

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

        projectileTarget = projectileTargetObject.GetComponent<ProjectileTestObj>();

        var spell_1 = SpellSys.Instance.NewEntity<PanelSpell>();
        spell_1.hotKey = KeyFunction.Spell1;

        var spell_3 = SpellSys.Instance.NewEntity<PanelSpell>();
        spell_3.hotKey = KeyFunction.Spell3;
        spell_3.speller = PossibleObj<ISpeller>.New(this);
        spell_3.CastAction = SpellActions.Ezreal_E;
    }

    private void Update()
    {
        if (Inputs.GetKeyDown(KeyFunction.MoveTo))
        {
            mover.Dest = Inputs.MouseHitPostion(cam);
            rotater.Dir = Inputs.MouseHitPostion(cam) - rotater.body.transform.position;
        }

        if (Inputs.GetKeyDown(KeyFunction.Spell6))
        {
            var c = CreatureSys.Instance.NewEntity<Creature>();
            c.genPos = Inputs.MouseHitPostion(cam);
            c.bodyID = 0;
        }
    }
}

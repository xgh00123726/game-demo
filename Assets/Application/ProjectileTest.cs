using System.Collections;
using System.Collections.Generic;
using GameBase.Projectile;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

public class ProjectileTest : MonoBehaviour
{
    public KeyFunction trig1 = KeyFunction.Spell7;
    public GameObject owner;
    public GameObject target;
    public float speed = 10f;

    private void Start()
    {
        var e = TextSys.Instance.NewEntity<FloatText>();
        e.bodyID = 1;
        e.showPosition = new Vector3(999, 999, 999);
    }

    private void Update()
    {
        if (GameBase.Tools.Inputs.GetKeyDown(trig1))
        {
            GameBase.Tools.XLogger.Instance.Log("trig1 down");

            if (target != null)
            {
                IProjectileTarget pTarget = target.GetComponent<ProjectileTestObj>();
                PossibleObj<IProjectileTarget> ppTarget = PossibleObj<IProjectileTarget>.New(pTarget);
                var proj = ProjectileSys.Instance.NewEntity<Projectile>();
                proj.bodyID = 0;
                proj.src = owner.transform.position;
                proj.dest = target.transform.position;
                proj.target = ppTarget;
                proj.curveType = CurveFactory.CurveType.Tracer;
                proj.speed = speed;
            }
        }
    }
}

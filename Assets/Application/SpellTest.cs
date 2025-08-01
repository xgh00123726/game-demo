using System.Collections;
using System.Collections.Generic;
using GameBase.Effects;
using GameBase.Tools;
using UnityEngine;

public class SpellTest : MonoBehaviour
{
    public KeyFunction trig1 = KeyFunction.Spell1;
    public Camera cam;
    public GameObject target;
    public CircleIndicator indicator;

    private void Awake()
    {
    }

    private void Update()
    {
        if (GameBase.Tools.Inputs.GetKeyDown(trig1))
        {
            GameBase.Tools.XLogger.Instance.Log(Inputs.MouseHitPostion(cam));
        }
    }
}

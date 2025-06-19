using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;
using GameBase.Tools;
using GameBase.Resources;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    protected void Start()
    {
        //Debug.Log(ResourcesLoader._pathDict["prefab"]["MoveIndicatorEffect"].ToString());
        //Debug.Log($"load:{ResourcesLoader.LoadFrefab("MoveIndicatorEffect")}");
        //Debug.Log($"load:{Resources.Load("prefabs/CFXR Water Ripples")}");
    }

    // Update is called once per frame
    protected void Update()
    {
        if (GameBase.Tools.Inputs.GetKeyDown(KeyFunction.Blink))
        {
            Timer.AddTask(1, 1, false, () =>
            {
                Debug.Log("you push space");
            });
        }
    }
}

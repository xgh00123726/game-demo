using GameBase.Tools;
using GameBase.Tools.Transforms;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Timer.AddTask(5, () =>
        {
            XLogger.Instance.Log("move this obj");
            transform.DOMoveVec(new Vector3(10, 0, 0), 10).Then(() =>
            {
                XLogger.Instance.Log("move over");
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

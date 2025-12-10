using GameBase.Network;
using GameBase.Tools;
using System;
using System.IO;
using System.Net;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class Main : MonoBehaviour
{
    void Awake()
    {
        ItemRegisterWarpper.Init();
        CommandInstances.Register();
        NetworkMgr.Instance.SetActive(true);
        NetworkMgr.Instance.SetFixedActive(true);
        LuaMgr.Init();

        DontDestroyOnLoad(this);
    }

    void Update()
    {
        LuaMgr.Update();
    }

    void OnDestroy()
    {
        LuaMgr.Dispose();
    }
}

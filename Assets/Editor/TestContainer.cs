using GameBase.Inventorys;
using GameBase.Move;
using GameBase.Texts;
using GameBase.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class TestContainer
{
    public struct TestStruct
    {
        public int a;
        public int b;
    }

    public class TestClass
    {
        public int a;
        public int b;
    }

    [MenuItem("Tools/TimerTest", false)]
    public static void Test()
    {
        TextMgr.Lang = "zh-cn";
        TextMgr.LoadAll();
    }

}
using Constructor.AttrAmplify;
using CsvHelper.Configuration;
using GameBase.Equipments;
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
    public class A
    {
        public string AA {  get; set; }
    }
    public class B : A
    {
        public int b;
    }
    [MenuItem("Tools/TimerTest", false)]
    public static void Test()
    {
        var a = General.CreateNotNullInstance<EquipmentData>();
        XLogger.Instance.Log(a);
    }

}
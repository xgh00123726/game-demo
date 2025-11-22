using Constructor.AttrAmplify;
using CsvHelper.Configuration;
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
        public int a;
    }
    public class B : A
    {
        public int b;
    }
    [MenuItem("Tools/TimerTest", false)]
    public static void Test()
    {
        using var fileWriter = new StreamWriter($"{Application.streamingAssetsPath}/AttrAmplifier/Template.csv");
        using var csvHelper = new CsvHelper.CsvWriter(fileWriter, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
        var datas = new AttrAmplifierData[1];
        datas[0] = new AttrAmplifierData()
        {
            ID = 1,
            Name = "a",
            Modifier = new List<ModifierPair> { new ModifierPair("key", 1, 2) }
        };
        csvHelper.WriteRecords(datas);
        XLogger.Instance.Log("write ok");
    }

}
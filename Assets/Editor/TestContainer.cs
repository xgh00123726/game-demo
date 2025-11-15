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
    public static void PlayModeUseFirstScene()
    {
        HashSet<TestStruct> hash1 = new();
        hash1.Add(new TestStruct { a = 1, b = 2 });
        hash1.Add(new TestStruct { a = 1, b = 2 });
        Debug.Log($"hash1 len:{hash1.Count}, is contain:{hash1.Contains(new TestStruct { a = 1, b = 2 })}");

        HashSet<TestClass> hash2 = new();
        hash2.Add(new TestClass { a = 1, b = 2 });
        hash2.Add(new TestClass { a = 1, b = 2 });
        Debug.Log($"hash2 len:{hash2.Count}, is contain:{hash2.Contains(new TestClass { a = 1, b = 2 })}");
    }

}
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;

namespace GameBase.Resources
{

    public static class ResourcesLoader
    {
        public static JToken _pathDict;
        static List<string> _typeName = new List<string>();
        // 静态初始化，用于初始化游戏开始前需要加载的资源
        static ResourcesLoader()
        {
            string pathStr = File.ReadAllText($"{Application.streamingAssetsPath}/public/ResourcesPath.json", System.Text.Encoding.UTF8);
            _pathDict = JObject.Parse(pathStr);
            foreach (var e in Enum.GetValues(typeof(PrefabType)))
            {
                _typeName.Add(e.ToString());
            }
        }

        public static GameObject LoadFrefab(PrefabType type, string name, string subFolder)
        {
            string prefabName;
            // 从名称映射json文件中读取名称，json文件中没有该名字，则使用类名作为索引
            if (_pathDict["prefab"][name] != null)
            {
                prefabName = _pathDict["prefab"][name].ToString();
            }
            else
            {
                prefabName = name;
            }
            
            if (subFolder != null && subFolder != "" && subFolder.Length != 0)
            {
                return UnityEngine.Resources.Load<GameObject>($"prefabs/{_typeName[(int)type]}/{subFolder}/{prefabName}");
            }
            return UnityEngine.Resources.Load<GameObject>($"prefabs/{_typeName[(int)type]}/{prefabName}");
        }

        public static Material LoadMaterial(string name)
        {
            return UnityEngine.Resources.Load<Material>($"materials/{_pathDict["material"][name]}");
        }

        public static void Load(string name)
        {
            
        }

    }
}

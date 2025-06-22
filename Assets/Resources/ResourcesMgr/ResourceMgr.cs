using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
namespace GameBase.Resources
{
    public class ResourceMgr
    {
        private static Dictionary<string, GameObject> _prefabDict = new Dictionary<string, GameObject> { };
        private static Dictionary<string, Material> _materialDict = new Dictionary<string, Material> { };

        // ´Ó×ÖµäÀï
        public static GameObject LoadPrefab(PrefabType type, string name)
        {
            if (_prefabDict.ContainsKey(name))
            {
                return _prefabDict[name];
            }
            GameObject go = ResourcesLoader.LoadFrefab(type, name);
            if (go == null)
            {
                Debug.LogWarning($"The prefab your load is null, prefab type:{type.ToString()}, prefab name:{name}");
            }

            _prefabDict[name] = go;
            return go;
        }

        public static Material LoadMaterial(string name)
        {
            if (_materialDict.ContainsKey(name))
            {
                return _materialDict[name];
            }
            Material m = ResourcesLoader.LoadMaterial(name);
            _materialDict[name] = m;
            return m;
        }

        public static GameObject InstaniatePrefab(PrefabType type, string name)
        {
            GameObject go = LoadPrefab(type, name);
            return GameObject.Instantiate(go);
        }

        public static Material InstaniateMaterial(string name)
        {
            Material m = LoadMaterial(name);
            return Material.Instantiate(m);
        }
    }
}

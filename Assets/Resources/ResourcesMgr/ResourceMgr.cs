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
        public static GameObject LoadPrefab(string name)
        {
            if (_prefabDict.ContainsKey(name))
            {
                return _prefabDict[name];
            }
            GameObject go = ResourcesLoader.LoadFrefab(name);
            if (go == null)
            {
                Debug.Log("the prefab your load is null, please validate your prefab name and project tag");
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

        public static GameObject InstaniatePrefab(string name)
        {
            GameObject go = LoadPrefab(name);
            return GameObject.Instantiate(go);
        }

        public static Material InstaniateMaterial(string name)
        {
            Material m = LoadMaterial(name);
            return Material.Instantiate(m);
        }
    }
}

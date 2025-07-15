using System.Collections.Generic;
using GameBase.Tools;
using UnityEngine;
using Logger = GameBase.Tools.Logger;
namespace GameBase.Resources
{
    /// <summary>
    /// 管理prefab, 可以从resource中导入prefab
    /// </summary>
    public class ResourceMgr
    {
        private static Dictionary<string, GameObject> _prefabDict = new Dictionary<string, GameObject> { };
        private static Dictionary<string, Material> _materialDict = new Dictionary<string, Material> { };
        private static Dictionary<string, Sprite> _spriteDict = new Dictionary<string, Sprite> { };

        /// <summary>
        /// 从resouce的指定文件夹中读取指定的prefab
        /// 已被读取的prefab会被存储, 下次读取时可以直接从字典中取得
        /// <list type="bullet">
        /// <item><param name="type"><paramref name="type"/>:PrefabType参数, 其枚举名一级文件夹名一致</param></item>
        /// <item><param name="name"><paramref name="name"/>:prefab名, 可以为prefab取别名</param></item>
        /// <item><param name="subFolder"><paramref name="subFolder"/>:子文件夹名,可以传入/分隔的多级目录</param></item>
        /// </list>
        /// </summary>
        /// <returns>prefab数组</returns>
        public static GameObject LoadPrefab(PrefabType type, string name, string subFolder = null)
        {
            if (_prefabDict.ContainsKey(name))
            {
                return _prefabDict[name];
            }
            GameObject go = ResourcesLoader.LoadFrefab(type, name, subFolder);
            if (go == null)
            {
                Logger.Instance.Level(Logger.LogLevel.Warning)
                    .Log($"The prefab your load is null, prefab type:{type.ToString()}, prefab name:{name}");
            }

            _prefabDict[name] = go;
            return go;
        }

        /// <summary>
        /// 从resouce的指定文件夹中读取所有的prefab, 并存储在字典中
        /// <list type="bullet">
        /// <item><param name="type"><paramref name="type"/>:PrefabType参数, 其枚举名与一级文件夹名一致</param></item>
        /// <item><param name="subFolder"><paramref name="subFolder"/>:子文件夹名,可以传入/分隔的多级目录</param></item>
        /// </list>
        /// </summary>
        /// <returns>prefab数组</returns>
        public static GameObject[] LoadAllPrefab(PrefabType type, string subFolder)
        {
            GameObject[] gos = ResourcesLoader.LoadAllPrefab(type, subFolder);
            if (gos == null)
            {
                Debug.LogWarning($"The prefab your load is null, prefab type:{type.ToString()}, prefab path:{subFolder}");
            }
            foreach (var go in gos)
            {
                if (_prefabDict.ContainsKey(go.name)) continue;
                _prefabDict[go.name] = go;
            }
            return gos;
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

        /// <summary>
        /// 从指定目录下载入Sprite
        /// <list type="bullet">
        /// <item><param name="name"><paramref name="name"/>:Sprite名</param></item>
        /// </list>
        /// <returns>载入的Sprite</returns>
        /// </summary>
        public static Sprite LoadSprite(string name)
        {
            if (_spriteDict.ContainsKey(name)) 
            {
                return _spriteDict[name];
            }
            Sprite sprite = ResourcesLoader.LoadSprite(name);
            _spriteDict[name] = sprite;
            return sprite;
        }
        /// <summary>
        /// 从指定目录下实例化prefab
        /// <list type="bullet">
        /// <item><param name="type"><paramref name="type"/>:PrefabType参数, 其枚举名与一级文件夹名一致</param></item>
        /// <item><param name="name"><paramref name="name"/>:prefab名,可以为prefab取别名</param></item>
        /// <item><param name="subFolder"><paramref name="subFolder"/>:子文件夹名,可以传入/分隔的多级目录</param></item>
        /// </list>
        /// <returns>实例化出的gameobject</returns>
        /// </summary>
        public static GameObject InstaniatePrefab(PrefabType type, string name, string subFolder = null)
        {
            GameObject go = LoadPrefab(type, name, subFolder);
            return GameObject.Instantiate(go);
        }

        public static Material InstaniateMaterial(string name)
        {
            Material m = LoadMaterial(name);
            return Material.Instantiate(m);
        }

        /// <summary>
        /// 从指定目录下实例化Sprite
        /// <list type="bullet">
        /// <item><param name="name"><paramref name="name"/>:Sprite名</param></item>
        /// </list>
        /// <returns>实例化的Sprite</returns>
        /// </summary>
        public static Sprite InstantiateSprite(string name)
        {
            Sprite go = LoadSprite(name);
            return Sprite.Instantiate(go);
        }
    }
}

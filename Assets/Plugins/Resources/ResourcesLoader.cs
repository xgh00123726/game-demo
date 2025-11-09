using UnityEngine;
using System.IO;
using UnityEngine.AddressableAssets;
using NReco.Csv;
using GameBase.Tools;
using System.Collections.Generic;

namespace GameBase.Resources
{

    public partial class ResourcesLoader
    {
        public static Dictionary<string, GameObject> _prefabs;
        public static Dictionary<string, Sprite> _sprites;
        public static Dictionary<string, Texture2D> _textures;

        public static GameObject GetPrefab(string name)
        {
            if (_prefabs.ContainsKey(name))
            {
                return _prefabs[name];
            }
            else
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid name: {name}");
            }

            return null;
        }

        public static GameObject InstantiateGameObject(string name)
        {
            return GameObject.Instantiate(GetPrefab(name));
        }

        public static Texture2D GetTexture2D(string name)
        {
            if (_textures.ContainsKey(name))
            {
                return _textures[name];
            }
            else
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid name: {name}");
            }

            return null;
        }

        public static Sprite GetSprite(string name)
        {
            if (_sprites.ContainsKey(name))
            {
                return _sprites[name];
            }
            else
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid name: {name}");
            }

            return null;
        }

        private static void CopyTexturesToSprite()
        {
            _sprites = new();
            foreach (var kvp in _textures)
            {
                var texture = kvp.Value;
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                _sprites.Add(kvp.Key, sprite);
            }
        }

        private static void LoadCsvAsset<T>(string csvPath, out Dictionary<string, T> container)
        {
            StreamReader reader = File.OpenText(csvPath);
            CsvReader csvReader = new CsvReader(reader);
            string path;
            container = new ();
            while (csvReader.Read())
            {
                path = csvReader[0];
                container[path] = Addressables.LoadAssetAsync<T>(path).WaitForCompletion();
            }

            reader.Close();
        }

        public static T LoadAddressable<T>(string filePath)
        {
            return Addressables.LoadAssetAsync<T>(filePath).WaitForCompletion();
        }

        public static void LoadAllAsset()
        {
            LoadCsvAsset($"{Application.streamingAssetsPath}/public/PrefabIDDictionary.csv", out _prefabs);
            LoadCsvAsset($"{Application.streamingAssetsPath}/public/Texture2DIDDictionary.csv", out _textures);

            CopyTexturesToSprite();
        }

    }
}

using UnityEngine;
using System.IO;
using UnityEngine.AddressableAssets;
using NReco.Csv;
using GameBase.Tools;

namespace GameBase.Resources
{

    public partial class ResourcesLoader
    {
        public static GameObject[] _prefabs;
        public static Sprite[] _sprites;
        public static Texture2D[] _textures;

        public static GameObject GetPrefab(int id)
        {
            if (id >= _prefabs.Length || id < 0)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"resource id out of the bound, id:{id}, max:{_prefabs.Length}");
            }
            return _prefabs[id];
        }

        public static GameObject InstantiateGameObject(int id)
        {
            return GameObject.Instantiate(GetPrefab(id));
        }

        public static Sprite GetSpriteFromTextureID(int id)
        {
            if (id >= _sprites.Length || id < 0)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"resource id out of the bound, id:{id}, max:{_textures.Length}");
            }

            var sprite = _sprites[id];

            if (sprite == null)
            {
                var texture = ResourcesLoader.GetTexture2D(id);
                sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                _sprites[id] = sprite;
            }
            return sprite;
        }

        public static Texture2D GetTexture2D(int id)
        {
            if (id >= _textures.Length || id < 0)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"resource id out of the bound, id:{id}, max:{_textures.Length}");
            }
            return _textures[id];
        }

        private static void CopyTexturesToSprite()
        {
            for (int i = 0; i < _textures.Length; i++)
            {
                var texture = ResourcesLoader.GetTexture2D(i);
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                _sprites[i] = sprite;
            }
        }

        private static void LoadCsvAsset<T>(string csvPath, out T[] container)
        {
            StreamReader reader = File.OpenText(csvPath);
            CsvReader csvReader = new CsvReader(reader);
            int id;
            string path;
            csvReader.Read();
            container = new T[int.Parse(csvReader[0])];
            for (int i = 0; i < container.Length; i++)
            {
                csvReader.Read();
                id = int.Parse(csvReader[0]);
                path = csvReader[1];
                container[id] = Addressables.LoadAssetAsync<T>(path).WaitForCompletion();
            }

            reader.Close();
        }

        public static void LoadAllAsset()
        {
            LoadCsvAsset($"{Application.streamingAssetsPath}/public/PrefabIDDictionary.csv", out _prefabs);
            LoadCsvAsset($"{Application.streamingAssetsPath}/public/Texture2DIDDictionary.csv", out _textures);

            _sprites = new Sprite[_textures.Length];
            CopyTexturesToSprite();
        }

    }
}

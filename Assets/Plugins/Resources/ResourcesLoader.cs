using UnityEngine;
using System.IO;
using UnityEngine.AddressableAssets;
using NReco.Csv;

namespace GameBase.Resources
{

    public partial class ResourcesLoader
    {
        public static GameObject[] _prefabs;
        public static Sprite[] _sprites;
        public static int PrefabCount => _prefabs.Length;
        public static int SpriteCount => _sprites.Length;

        public static GameObject GetPrefab(int id)
        {
            return _prefabs[id];
        }

        public static Sprite GetSprite(int id)
        {
            return _sprites[id];
        }

        private static void LoadCsvAsset<T>(string csvPath, out T[] container)
        {
            StreamReader reader = File.OpenText(csvPath);
            CsvReader csvReader = new CsvReader(reader);
            int id;
            string path;
            csvReader.Read();
            container = new T[int.Parse(csvReader[0])];
            while (csvReader.Read())
            {
                id = int.Parse(csvReader[0]);
                path = csvReader[1];
                container[id] = Addressables.LoadAssetAsync<T>(path).WaitForCompletion();
            }
        }

        public static void LoadAllAsset()
        {
            LoadCsvAsset($"{Application.streamingAssetsPath}/public/PrefabIDDictionary.csv", out _prefabs);
            LoadCsvAsset($"{Application.streamingAssetsPath}/public/SpriteIDDictionary.csv", out _sprites);
        }

    }
}

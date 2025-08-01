using UnityEngine;

namespace GameBase.Resources
{
    public partial class ResourcesLoader
    {
        private static GameObject[] _creaturePrefabs;
        public static GameObject GetCreaturePrefab(int id)
        {
            return _creaturePrefabs[id];
        }
        public static int CreaturePrefabCount => _creaturePrefabs.Length;

        public static void LoadAllCreaturePrefab()
        {
            LoadCsvAsset($"{Application.streamingAssetsPath}/public/CreaturePrefabIDDictionary.csv", out _creaturePrefabs);
        }
    }
}

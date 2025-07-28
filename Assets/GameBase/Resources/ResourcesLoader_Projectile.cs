using NReco.Csv;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameBase.Resources
{
    public partial class ResourcesLoader
    {
        private static GameObject[] _projectileBodyPrefabs;
        public static GameObject GetProjectileBodyPrefab(int id)
        {
            return _projectileBodyPrefabs[id];
        }
        public static int ProjectileBodyPrefabCount => _projectileBodyPrefabs.Length;

        public static void LoadAllProjectileBodyPrefab()
        {
            LoadCsvAsset($"{Application.streamingAssetsPath}/public/ProjectileBodyPrefabIDDictionary.csv", out _projectileBodyPrefabs);
        }
    }
}

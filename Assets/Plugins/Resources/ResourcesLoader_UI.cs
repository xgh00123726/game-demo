using NReco.Csv;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameBase.Resources
{
    public partial class ResourcesLoader
    {
        private static GameObject[] _UIPrefabs;
        public static GameObject GetUIPrefab(int id)
        {
            return _UIPrefabs[id];
        }
        public static int UIPrefabCount => _UIPrefabs.Length;

        public static void LoadAllUIPrefab()
        {
            LoadCsvAsset($"{Application.streamingAssetsPath}/public/UIPrefabIDDictionary.csv", out _UIPrefabs);
        }
    }
}

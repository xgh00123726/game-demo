using GameBase.Tools;
using NReco.Csv;
using System.Collections.Generic;
using System.IO;
using UnityEngine.AddressableAssets;

namespace GameBase.Resources
{
    public class ResourceLoader<T>
    {
        internal Dictionary<string, T> _resources = new();

        public ResourceLoader(string preloadFilePath = null)
        {
            LoadAsset(preloadFilePath);
        }

        protected virtual T Instantiate(string name)
        {
            return Addressables.LoadAssetAsync<T>(name).WaitForCompletion();
        }

        protected virtual void LoadAsset(string preloadFilePath)
        {
            if (preloadFilePath == null)
            {
                return;
            }

            using var reader = File.OpenText(preloadFilePath);
            var csvReader = new CsvReader(reader);
            string name = null;
            while (csvReader.Read())
            {
                name = csvReader[0];
                _resources[name] = Addressables.LoadAssetAsync<T>(name).WaitForCompletion();
            }
        }

        public T Get(string name)
        {
            if (name == null)
            {
                return default;
            }

            if (_resources.ContainsKey(name))
            {
                return _resources[name];
            }
            else
            {
                var res = Instantiate(name);
                if (res == null)
                {
                    XLogger.Instance.Level(XLogger.LogLevel.Error)
                        .Log($"invalid name: {name}");
                }
                else
                {
                    _resources.Add(name, res);
                }

                return res;
            }
        }
    }
}

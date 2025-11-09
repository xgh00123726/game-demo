using GameBase.Resources;
using GameBase.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.UI
{
    public class RootCanvas : Singleton<RootCanvas>
    {
        internal AutoFillList<Transform> layers = new();
        internal GameObject obj;

        public RootCanvas()
        {
            obj = GameObject.Instantiate(ResourcesLoader.GetPrefab("Prefabs/UI/RootCanvs.prefab"));
        }

        public Transform Layer(int level)
        {
            if (level >= layers.Count)
            {
                int count = layers.Count;
                for (int i = count; i < level + 1; ++i)
                {
                    var layer = new GameObject($"layer{i}").transform;
                    layer.SetParent(obj.transform, false);
                    layers.Add(layer, i);
                }
                return layers[level];
            }
            else
            {
                return layers[level];
            }
        }
    }
}

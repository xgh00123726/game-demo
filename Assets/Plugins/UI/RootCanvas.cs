using GameBase.Resources;
using GameBase.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.UI
{
    public class RootCanvas : MonoBehaviour
    {
        internal static RootCanvas instance;
        internal AutoFillList<Transform> layers = new();
        public static RootCanvas Instance => instance;

        public Transform Layer(int level)
        {
            if (level >= layers.Count)
            {
                int count = layers.Count;
                for (int i = count; i < level + 1; ++i)
                {
                    var layer = new GameObject($"layer{i}").transform;
                    layer.SetParent(transform, false);
                    layers.Add(layer, i);
                }
            }
            else
            {
                return layers[level];
            }

            return transform;
        }

        void Awake()
        {
            instance = this;
        }
    }
}

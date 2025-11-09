using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.UI
{
    public class WorldCanvs : Singleton<WorldCanvs>
    {
        internal GameObject obj;
        public WorldCanvs()
        {
            obj = GameObject.Instantiate(ResourcesLoader.GetPrefab("Prefabs/UI/WorldCanvas.prefab"));
        }

        public Transform transform
        {
            get => obj.transform;
        }
    }
}

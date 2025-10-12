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
            obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(50));
        }

        public Transform transform
        {
            get => obj.transform;
        }
    }
}

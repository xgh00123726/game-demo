using GameBase.Resources;
using UnityEngine;

namespace GameBase.UI
{
    public class RootCanvas : BaseUI
    {
        internal static RootCanvas instance;
        public static RootCanvas Instance => instance;
        void Awake()
        {
            instance = this;

            //// 8,Prefabs/UI/AttrPanel
            //GameObject.Instantiate(ResourcesLoader.GetPrefab(8)).AddComponent<AttrPanel>().transform.SetParent(transform, false);
        }
    }
}

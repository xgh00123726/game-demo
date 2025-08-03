using GameBase.Resources;
using UnityEngine;

namespace GameBase.UI
{
    public class RootCanvas : BaseUI
    {
        void Awake()
        {
            // 8,Prefabs/UI/AttrPanel
            GameObject.Instantiate(ResourcesLoader.GetPrefab(8)).AddComponent<AttrPanel>().transform.SetParent(transform, false);

            // 10,Prefabs/UI/BuffPanel
            GameObject.Instantiate(ResourcesLoader.GetPrefab(10)).AddComponent<BuffPanel>().transform.SetParent(transform, false);

            // 12,Prefabs/UI/SpellPanel
            GameObject.Instantiate(ResourcesLoader.GetPrefab(12)).AddComponent<SpellPanel>().transform.SetParent(transform, false);
        }
    }
}

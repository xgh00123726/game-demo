using GameBase.Resources;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameBase.UI
{
    public class RootCanvas : BaseUI
    {
        void Awake()
        {
            if (PrefabMgr.Instance == null)
            {
                Debug.LogError("prefab manager must instantiate before rootcanvas!!!!!");
                Debug.LogWarning("please check weather start without start scene!!!");
            }
            AttrPanel._instance = PrefabMgr.Instance.GetNotfromPool<AttrPanel>(PrefabType.UI);
            UIMgr.AttachToRoot(AttrPanel._instance.transform);

            SpellPanel._instance = PrefabMgr.Instance.GetNotfromPool<SpellPanel>(PrefabType.UI);
            UIMgr.AttachToRoot(SpellPanel._instance.transform);

            BuffPanel._instance = PrefabMgr.Instance.GetNotfromPool<BuffPanel>(PrefabType.UI);
            UIMgr.AttachToRoot(BuffPanel._instance.transform);
        }
    }
}

using GameBase.Resources;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameBase.UI
{
    public class RootCanvas : BaseUI
    {
        void Awake()
        {
            AttrPanel._instance = PoolablePrefabMgr.GetNotfromPool<AttrPanel>(PrefabType.UI);
            UIMgr.AttachToRoot(AttrPanel._instance.transform);

            SpellPanel._instance = PoolablePrefabMgr.GetNotfromPool<SpellPanel>(PrefabType.UI);
            UIMgr.AttachToRoot(SpellPanel._instance.transform);

            BuffPanel._instance = PoolablePrefabMgr.GetNotfromPool<BuffPanel>(PrefabType.UI);
            UIMgr.AttachToRoot(BuffPanel._instance.transform);
        }
    }
}

using GameBase.Resources;

namespace GameBase.UI
{
    public class RootCanvas : BaseUI
    {
        void Awake()
        {
            AttrPanel._instance = PrefabMgr.Instance.GetNotfromPool<AttrPanel>();
            UIMgr.AttachToRoot(AttrPanel._instance.transform);

            SpellPanel._instance = PrefabMgr.Instance.GetNotfromPool<SpellPanel>();
            UIMgr.AttachToRoot(SpellPanel._instance.transform);
        }
    }
}

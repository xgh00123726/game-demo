using UnityEngine;

namespace GameBase.UI
{
    public class FreeViewItem : ITodoTarget, ITodoView
    {
        public enum CanvasType
        {
            Root,
            World,
        }
        public GameObject Obj { get; private set; }
        public FreeViewItem(string prefabName, CanvasType canvasType = CanvasType.Root, int layer = 1, string objName = null)
        {
            Obj = GameObject.Instantiate(Resources.ResourceMgr.Prefab.Get(prefabName));
            if (objName != null)
            {
                Obj.name = objName;
            }
            if (canvasType == CanvasType.Root)
            {
                Obj.transform.SetParent(RootCanvas.Instance.Layer(layer));
            }
            else
            {
                Obj.transform.SetParent(WorldCanvs.Instance.Transform);
            }
        }

        public bool IsShow
        {
            get => Obj.activeSelf;
            set => Obj.SetActive(value);
        }
        public Vector3 Position
        {
            get => Obj.transform.position;
            set => Obj.transform.position = value;
        }
        public void Show()
        {
            Obj.SetActive(true);
        }
        public void Hide()
        {
            Obj.SetActive(false);
        }
        public void Toggle()
        {
            Obj.SetActive(!Obj.activeSelf);
        }
    }
}

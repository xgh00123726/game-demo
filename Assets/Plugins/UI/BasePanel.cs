using GameBase.Resources;
using GameBase.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public abstract class BasePanel<T, T_Instance> : UObjEntitySys<T, SimpleEntityContainer, GameObject, T_Instance>
        where T : BasePanelItem, new()
        where T_Instance : BasePanel<T, T_Instance>, new()
    {
        public enum Align
        {
            Left = 0, Right, Center
        }


        public GameObject panel;
        public PanelShadow shadow;
        private int itemIterIdx = 0;

        internal abstract float ItemWidth { get; }
        internal abstract float ItemHeight { get; }
        internal abstract float XInterval { get; }
        internal abstract float YInterval { get; }
        internal abstract float MaxPanelWidth { get; }
        internal abstract float PanelX { get; }
        internal abstract float PanelY { get; }
        internal abstract int PanelObjID {  get; }
        internal abstract int MaskTexureID { get; }
        internal abstract int ItemAlign { get; }
        internal int ItemIterIdx => itemIterIdx;

        protected virtual void GetItemNumXYStyle(int index, out int itemPerLine, out int x, out int y)
        {
            itemPerLine = (int)Mathf.Floor(MaxPanelWidth / (ItemWidth + XInterval));
            x = index % itemPerLine;
            y = index / itemPerLine;
        }

        protected virtual Vector3 GetItemLocalPosition(int index)
        {
            GetItemNumXYStyle(index, out int itemPerLine, out int x, out int y);

            float vx = 0f;
            if (ItemAlign == (int)Align.Left)
            {
                vx = (ItemWidth + XInterval) * x;
            }
            else if (ItemAlign == (int)Align.Center)
            {
                vx = (ItemWidth + XInterval) * x;
                float remainWidth = itemPerLine * (ItemWidth + XInterval);
                vx -= remainWidth / 2;
            }
                float vy = (ItemHeight + YInterval) * y;


            return new Vector3(vx, vy, 0);
        }

        protected override void AfterInstantiateEUObject(T e)
        {
            var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(e.IconTexureID));
            var mask = GameObject.Instantiate(ResourcesLoader.GetTexture2D(MaskTexureID));

            e.iconMaterial.SetTexture("_Shape", mask);
            e.iconMaterial.SetTexture("_Target", texture);
            e.Obj.SetActive(true);
        }

        protected override void BeforeReleaseEUObject(T e)
        {
            e.Obj.SetActive(false);
        }

        protected override GameObject InstantiateObj(T e)
        {
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));

            obj.transform.SetParent(panel.transform, false);

            var image = obj.transform.Find("Icon").GetComponent<Image>();
            if (image == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }

            e.iconMaterial = new Material(image.material);
            image.material = e.iconMaterial;

            return obj;
        }

        protected override void UpdateEntity(T e)
        {
            panel.transform.localPosition = new Vector3(PanelX, PanelY, 0);
            e.Obj.transform.localPosition = GetItemLocalPosition(itemIterIdx);

            if (++itemIterIdx >= _entities.Count)
            {
                itemIterIdx = 0;
            }

            shadow.panelObjID = PanelObjID;
            shadow.maskTexureID = MaskTexureID;
            shadow.itemHeight = ItemHeight;
            shadow.itemWidth = ItemWidth;
            shadow.maxPanelWidth = MaxPanelWidth;
            shadow.xInterval = XInterval;
            shadow.yInterval = YInterval;
        }

        protected override void Awake()
        {
            base.Awake();

            panel = GameObject.Instantiate(ResourcesLoader.GetPrefab(PanelObjID));
            panel.transform.SetParent(RootCanvas.Instance.transform, false);
            shadow = panel.AddComponent<PanelShadow>();
        }
    }
}

using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public abstract class BasePanel<T, T_Instance> : UObjEntitySys<T, SimpleEntityContainer, BaseUI, T_Instance>
        where T : BasePanelItem, new()
        where T_Instance : BasePanel<T, T_Instance>, new()
    {
        public enum Align
        {
            Left = 0, Right, Center
        }


        public GameObject panel;
        private int itemIterIdx = 0;

        internal abstract float ItemWidth { get; }
        internal abstract float ItemHeight { get; }
        internal abstract float XInterval { get; }
        internal abstract float YInterval { get; }
        internal abstract float MaxPanelWidth { get; }
        internal abstract float PanelX { get; }
        internal abstract float PanelY { get; }
        internal abstract int PanelObjID {  get; }
        internal abstract int ShapeTexureID { get; }
        internal abstract int ContourTexureID { get; }
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
            var shape = GameObject.Instantiate(ResourcesLoader.GetTexture2D(ShapeTexureID));
            var contour = GameObject.Instantiate(ResourcesLoader.GetTexture2D(ContourTexureID));

            e.iconMaterial.SetTexture("_Shape", shape);
            e.iconMaterial.SetTexture("_Contour", contour);
            e.iconMaterial.SetTexture("_Target", texture);

            e.AfterInstantiateUObjectDelegate?.Invoke(e);
            
            e.Obj.gameObject.SetActive(true);
        }

        protected override void BeforeReleaseEUObject(T e)
        {
            e.Obj.gameObject.SetActive(false);
        }

        protected override BaseUI InstantiateObj(T e)
        {
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));
            var ui = obj.AddComponent<BaseUI>();

            if (e is IEnterExist eee)
            {
                ui.OnPointerEnter = eee.OnPointerEnter;
                ui.OnPointerExit = eee.OnPointerExist;
            }

            if (e is ISwitchable se)
            {
                ui.OnSwitchOn = se.OnSwitchOn;
                ui.OnSwitchOff = se.OnSwitchOff;
            }

            obj.transform.SetParent(panel.transform, false);

            var image = obj.transform.Find("Icon").GetComponent<Image>();
            if (image == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }

            e.iconMaterial = new Material(image.material);
            image.material = e.iconMaterial;

            e.iconMaterial.SetFloat("_Dir1", -1f);
            e.iconMaterial.SetFloat("_Dir2", -1f);

            return ui;
        }

        protected override void UpdateEntity(T e)
        {
            panel.transform.localPosition = new Vector3(PanelX, PanelY, 0);
            e.Obj.transform.localPosition = GetItemLocalPosition(itemIterIdx);

            if (++itemIterIdx >= _entities.Count)
            {
                itemIterIdx = 0;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            panel = GameObject.Instantiate(ResourcesLoader.GetPrefab(PanelObjID));
            panel.transform.SetParent(RootCanvas.Instance.transform, false);
        }
    }
}

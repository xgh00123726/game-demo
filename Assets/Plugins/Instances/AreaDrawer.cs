using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;

namespace Instance
{
    public class AreaDrawer : SingletonInstance<AreaDrawer>
    {
        private static GameObject _lineRendererObj;
        private static LineRenderer _lineRenderer;
        private static Rect _drawArea;

        public static int lineRendererObjID = 48;
        public AreaDrawer()
        {
            _lineRendererObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(lineRendererObjID));
            _lineRenderer = _lineRendererObj.transform.Find("Line").GetComponent<LineRenderer>();
        }

        public static void SetDrawArea(Rect area, float drawY)
        {
            _drawArea = area;
            _lineRenderer.DrawRect(area, drawY);
        }

        protected override void Update()
        {
            if (Inputs.GetKey(KeyFunction.ExtraInfo, "areaDrawer"))
            {
                _lineRendererObj.SetActive(true);
            }
            else
            {
                _lineRendererObj.SetActive(false);
            }
        }
    }
}

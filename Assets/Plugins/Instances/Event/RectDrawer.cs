using GameBase.EntitySystem;
using GameBase.GCamera;
using GameBase.Resources;
using GameBase.Tools;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Instance
{
    public class RectDrawer : SingletonInstance<RectDrawer>
    {
        private static LineRenderer _lineRenderer;
        private static GameObject _lineRendererObj;
        private static float _selectBeginTime;
        private static Vector3 _selectBeginPos;
        private static bool _selectEnable;

        public static float drawY = -7;
        public static float trigTime = 0.1f;
        public static int lineRendererObjID = 48;

        public static Action OnDrawBegin;
        public static Action OnDrawEnd;
        public static Action<Rect> OnDraw;

        public RectDrawer()
        {
            _lineRendererObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(lineRendererObjID));
            _lineRenderer = _lineRendererObj.transform.Find("Line").GetComponent<LineRenderer>();
        }

        protected override void Update()
        {
            if (Inputs.GetKeyDown(KeyFunction.DrawRectTrig, "rectDrawer"))
            {
                if (!_selectEnable)
                {
                    OnDrawBegin?.Invoke();
                    _selectEnable = true;
                }
                _selectBeginTime = Time.time;
                _selectBeginPos = CameraSys.MouseHitPosition;
            }

            if (!Inputs.GetKey(KeyFunction.DrawRectTrig, "rectDrawer"))
            {
                if (_selectEnable)
                {
                    OnDrawEnd?.Invoke();
                    _selectEnable = false;
                }
            }

            if (_selectEnable && Time.time > _selectBeginTime + trigTime)
            {
                var pos = CameraSys.MouseHitPosition;
                float beginX = _selectBeginPos.x;
                float beginZ = _selectBeginPos.z;
                float endX = pos.x;
                float endZ = pos.z;

                _lineRendererObj.SetActive(true);
                _lineRenderer.DrawRect(Rect.MinMaxRect(beginX, beginZ, endX, endZ), drawY);

                float minX = Mathf.Min(beginX, endX);
                float maxX = Mathf.Max(beginX, endX);
                float minZ = Mathf.Min(beginZ, endZ);
                float maxZ = Mathf.Max(beginZ, endZ);

                OnDraw?.Invoke(Rect.MinMaxRect(minX, minZ, maxX, maxZ));
            }
            else
            {
                _lineRendererObj.SetActive(false);
            }
        }
    }
}

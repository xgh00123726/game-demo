using System.Collections;
using System.Collections.Generic;
using GameBase.Resources;
using UnityEngine;

namespace GameBase.UI
{
    public class UIMgr : MonoBehaviour
    {
        public float _yFactorAMin = -10;
        public float _yFactorAMax = -10;
        public float _yFactorBMin = 0;
        public float _yFactorBMax = 100;
        public float _yFactorCMin = 0;
        public float _yFactorCMax = 10;
        public float _horizontalSpeedMin = 0;
        public float _horizontalSpeedMax = 10;
        public float _durationMin = 0;
        public float _durationMax = 10;

        private static UIMgr _instance;
        public static UIMgr Instance => _instance;

        private static Transform _rootCanvas;
        public static Transform RootCanvas
        {
            get
            {
                if (_rootCanvas == null)
                {
                    _rootCanvas = GameObject.FindGameObjectWithTag("RootCanvas").transform;
                }
                return _rootCanvas;
            }
        }

        private void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// 将制定的UI放在rootcanvas下
        /// <list type="bullet">
        /// <item><param name="ui"><paramref name="ui"/>:UI的transform</param></item>
        /// </list></summary>
        public static void AttachToRoot(Transform ui)
        {
            ui.SetParent(RootCanvas, false);
        }
    }
}

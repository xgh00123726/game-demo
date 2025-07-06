using System.Collections;
using System.Collections.Generic;
using GameBase.Resources;
using UnityEngine;

namespace GameBase.UI
{
    public class UIMgr : MonoBehaviour
    {
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

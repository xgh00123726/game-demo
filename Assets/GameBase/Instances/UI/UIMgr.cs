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

        public static void AttachToRoot(Transform ui)
        {
            ui.SetParent(RootCanvas, false);
        }
    }
}

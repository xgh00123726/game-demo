using GameBase.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.GCamera
{
    public class CameraSys : MonoBehaviour
    {
        protected static Camera _main;
        protected static LinkedList<Camera> _cameras = new LinkedList<Camera>();

        private static Vector3 _mouseHitPosition;
        public static Vector3 MouseHitPosition => _mouseHitPosition;

        public static Camera Main
        {
            get => _main;
            set => _main = value;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            _mouseHitPosition = Inputs.MouseHitPostion(_main);
        }
    }
}

using GameBase.EntitySystem;
using GameBase.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.GCamera
{
    public static class CameraUtil
    {
        public static void LookAt(this Camera camera, Vector3 position)
        {
            XLogger.Instance.Log($"look at: {position}");
        }
    }

    public class CameraSys : SingletonInstance<CameraSys>
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

        protected override void Update()
        {
            _mouseHitPosition = Inputs.MouseHitPostion(_main);
        }
    }
}

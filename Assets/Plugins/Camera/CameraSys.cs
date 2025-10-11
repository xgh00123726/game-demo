using GameBase.EntitySystem;
using GameBase.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.GCamera
{
    public static class CameraUtil
    {
        public static void LookAt(this Camera c, Vector3 position)
        {
            float deltaY = c.transform.position.y - position.y;
            float angle = 90 - c.transform.rotation.eulerAngles.x;

            float deltaX = 0;
            float deltaZ = -Mathf.Tan(angle * Mathf.Deg2Rad) * deltaY;

            c.transform.position = new Vector3(position.x + deltaX, c.transform.position.y, position.z + deltaZ);
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

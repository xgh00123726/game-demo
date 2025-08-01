using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.GCamera
{
    public class CameraBase : MonoBehaviour
    {
        protected Camera _camera;
        public bool enablePositionTestLog = true;
        public float cameraHeight = 10f;
        public float testDepth;

        public static CameraBase Main => Camera.main.GetComponent<CameraBase>();

        public Vector2 MouseDirTo(GameObject go)
        {
            return (Input.mousePosition - _camera.WorldToScreenPoint(go.transform.position)).normalized;
        }
        public Vector3 MouseWorldPos()
        {
            Vector3 pos = Input.mousePosition;
            pos.z = cameraHeight;
            return _camera.ScreenToWorldPoint(pos);
        }
        public Vector3 MouseWorldPos(float z)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = z;
            return _camera.ScreenToWorldPoint(pos);
        }

        // Start is called before the first frame update
        protected virtual void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (enablePositionTestLog && Tools.Inputs.GetKeyDown(Tools.KeyFunction.TestPosKey))
            {
                Debug.Log($"your test position is:{MouseWorldPos(testDepth)}");
            }
        }
    }
}

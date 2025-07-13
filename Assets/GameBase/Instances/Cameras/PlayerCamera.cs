using UnityEngine;
using GameBase.Tools;
using GameBase.GCamera;
using Logger = GameBase.Tools.Logger;

namespace GameBase.GCamera
{
    public class PlayerCamera : CameraBase
    {
        public RotateComponent rotateComponent;
        public GameObject playerGo;

        public float pitchSetSpeedFactor = 2f;
        public float yawSetSpeed = 20f;
        public float yawSetFastFactor = 3f;
        private bool frozenYawSet = false;

        public float posXOffset = 0f;
        public float posYOffset = 0f;
        public float posZOffset = 0f;

        public Ray moveRay;
        public RaycastHit rayHit;
        private static Vector3 _mouseHitPoint;
        public static Vector3 MouseHitPoint => _mouseHitPoint;

        private void CameraHeightSetDetect()
        {
            if (GameBase.Tools.Inputs.GetKey(KeyFunction.LockCamHeight))
            {
                _camera.fieldOfView -= UnityEngine.Input.mouseScrollDelta.y;
            }
        }

        private void CameraRotateSetDetect()
        {
            // pitch设置，视角俯仰控制
            if (GameBase.Tools.Inputs.GetKey(KeyFunction.SettingPitch))
            {
                rotateComponent.RotateX(UnityEngine.Input.mouseScrollDelta.y * pitchSetSpeedFactor);
                posZOffset = -Mathf.Tan((90 - rotateComponent._eulerX) * Mathf.Deg2Rad) * cameraHeight;
                Debug.Log($"{Mathf.Tan((90 - rotateComponent._eulerX) * Mathf.Deg2Rad)}, {cameraHeight}");
            }

            if (GameBase.Tools.Inputs.GetKey(KeyFunction.ResetView))
            {
                rotateComponent.ResetEuler();
                // 每次重置视角旋转0.5s后，不允许旋转视角，否则抬起时刻不一致会导致视角有微小转动
                frozenYawSet = true;
                Timer.AddTask(0.5f, () =>
                {
                    frozenYawSet = false;
                });
            }

            // yaw设置，视角平面旋转设置
            if (frozenYawSet) return;
            if (GameBase.Tools.Inputs.GetKey(KeyFunction.SettingYawClockWiseFaster))
            {
                rotateComponent.RotateZ(-yawSetSpeed * Time.deltaTime * yawSetFastFactor);
            }
            else if (GameBase.Tools.Inputs.GetKey(KeyFunction.SettingYawClockWise))
            {
                rotateComponent.RotateZ(-yawSetSpeed * Time.deltaTime);
            }
            if (GameBase.Tools.Inputs.GetKey(KeyFunction.SettingYawAntiClockWiseFaster))
            {
                rotateComponent.RotateZ(yawSetSpeed * Time.deltaTime * yawSetFastFactor);
            }
            else if (GameBase.Tools.Inputs.GetKey(KeyFunction.SettingYawAntiClockWise))
            {
                rotateComponent.RotateZ(yawSetSpeed * Time.deltaTime);
            }
        }

        private void CameraPositionSetDetect()
        {
            float camY = playerGo.transform.position.y + cameraHeight + posYOffset;
            float camX = playerGo.transform.position.x + posXOffset;
            float camZ = playerGo.transform.position.z + posZOffset;
            transform.position = new Vector3(camX, camY, camZ);
        }

        protected override void Start()
        {
            cameraHeight = 25.0f;

            rotateComponent = GetComponent<RotateComponent>();
            if (rotateComponent == null)
            {
                Debug.Log("no rotate component exist in camera, some function maybe unusable");
            }
            rotateComponent.rotateSpeed = new Vector3(100, 100, 100);
        }

        private void RecordInputInfo()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Logger.Instance.Log($"Lmouse:{_mouseHitPoint}");
            }
        }

        // Update is called once per frame
        protected override void Update()
        {
            CameraPositionSetDetect();
            CameraHeightSetDetect();
            CameraRotateSetDetect();

            moveRay = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            Physics.Raycast(moveRay, out rayHit);
            _mouseHitPoint = rayHit.point;

#if UNITY_EDITOR
            RecordInputInfo();
#endif
        }
    }
}

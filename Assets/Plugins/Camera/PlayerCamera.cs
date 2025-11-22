using GameBase.Tools;
using GameBase.Infos;
using UnityEngine;
namespace GameBase.GCamera
{
    public class PlayerCamera : MonoBehaviour
    {
        private bool _inDrag = false;
        private Vector3 _dragCameraPosition;
        private Vector3 _dragMousePosition;

        public bool EnableEdgeAutoDrag { get; set; } = true;
        public bool EnableScrollToChangeFOV { get; set; } = true;

        public float DragFactor { get; set; } = 100f;
        public float XMoveSpeed { get; set; } = 10f;
        public float YMoveSpeed { get; set; } = 10f;
        public float XBorder { get; set; } = 20f;
        public float YBorder { get; set; } = 20f;
        public float XMax { get; set; } = 1920;
        public float YMax { get; set; } = 1080;

        // Start is called before the first frame update
        void Start()
        {
            CameraSys.Instance.SetActive(true);
            CameraSys.Main = GetComponent<Camera>();
            Command.Register("enableScrollChangeFOV", () =>
            {
                EnableScrollToChangeFOV = true;
            });
            Command.Register("disableScrollChangeFOV", () =>
            {
                EnableScrollToChangeFOV = false;
            });
        }

        private void OnDrawGizmosSelected()
        {
            Ray r = new Ray(CameraSys.Main.transform.position, CameraSys.Main.transform.forward);
            Physics.Raycast(r, out RaycastHit rayHit);
            for (int i = 0; i < 10; ++i)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(CameraSys.Main.transform.position, rayHit.point);
            }
        }

        private Vector3 DirUp()
        {
            return new Vector3(0, 0, 1f);
            //return new Vector3(-1.414f, 0, 1.414f);
        }

        private Vector3 DirRight()
        {
            return new Vector3(1f, 0, 0);
            //return new Vector3(1.414f, 0, 1.414f);
        }

        private Vector3 MouseDirToWorldDir(Vector3 dir)
        {
            float x = dir.x;
            float y = dir.y;

            var dirRet = x * DirRight() + y * DirUp();
            return dirRet;
        }

        private void CameraReset()
        {
        }

        private void DragUpdate()
        {
            if (Inputs.GetKeyDown(KeyFunction.DragScreen, "camera"))
            {
                _inDrag = true;
                _dragCameraPosition = transform.position;
                _dragMousePosition = Input.mousePosition;
            }
            else if (Inputs.GetKeyUp(KeyFunction.DragScreen, "camera"))
            {
                _inDrag = false;
            }

            if (_inDrag)
            {
                var position = transform.position;
                var mousePosition = Input.mousePosition;
                var mouseDir = mousePosition - _dragMousePosition;
                var worldDir = MouseDirToWorldDir(mouseDir) / DragFactor;
                Vector3 currPosition = _dragCameraPosition - worldDir;
                currPosition.y = transform.position.y;
                transform.position = currPosition;
            }
        }

        private void EdgeAutoUpdate()
        {
            Vector2 mousePos = Input.mousePosition;
            float x = mousePos.x;
            float y = mousePos.y;

            Vector3 moveDir = Vector3.zero;

            if (x > XMax - XBorder && x <= XMax)
            {
                moveDir += DirRight() * XMoveSpeed;
            }
            else if (x < XBorder && x >= 0)
            {
                moveDir -= DirRight() * XMoveSpeed;
            }
            if (y > YMax - YBorder && y <= YMax)
            {
                moveDir += DirUp() * YMoveSpeed;
            }
            else if (y < YBorder && y >= 0)
            {
                moveDir -= DirUp() * YMoveSpeed;
            }

            transform.position = transform.position + moveDir * Time.deltaTime;
        }

        private void FOVUpdate()
        {
            CameraSys.Main.fieldOfView -= Input.mouseScrollDelta.y;
        }

        private void Update()
        {
            DragUpdate();
            if (EnableEdgeAutoDrag)
            {
                EdgeAutoUpdate();
            }

            if (Inputs.GetKey(KeyFunction.ResetView))
            {
                CameraReset();
            }

            if (EnableScrollToChangeFOV)
            {
                FOVUpdate();
            }
        }
    }
}

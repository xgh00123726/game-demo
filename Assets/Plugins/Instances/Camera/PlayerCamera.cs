using GameBase.GCamera;
using GameBase.Tools;
using GameBase.Infos;
using UnityEngine;
namespace GameBase.Instance
{
    public class PlayerCamera : MonoBehaviour
    {
        private bool _inDrag = false;
        private Vector3 _dragCameraPosition;
        private Vector3 _dragMousePosition;

        public float dragFactor = 100f;
        public bool enableEdgeAutoDrag = true;
        public float xMoveSpeed = 10f;
        public float yMoveSpeed = 10f;
        public float xBorder = 20f;
        public float yBorder = 20f;
        public float xMax = 1920;
        public float yMax = 1080;

        // Start is called before the first frame update
        void Start()
        {
            CameraSys.Main = GetComponent<Camera>();
        }

        private Vector3 DirUp()
        {
            return new Vector3(-1.414f, 0, 1.414f);
        }

        private Vector3 DirRight()
        {
            return new Vector3(1.414f, 0, 1.414f);
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
            var position = Globals.GetPlayerPosition(0);
            position.y = transform.position.y;
            transform.position = position;
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
                var worldDir = MouseDirToWorldDir(mouseDir) / dragFactor;
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

            if (x > xMax - xBorder && x <= xMax)
            {
                moveDir += DirRight() * xMoveSpeed;
            }
            else if (x < xBorder && x >= 0)
            {
                moveDir -= DirRight() * xMoveSpeed;
            }
            if (y > yMax - yBorder && y <= yMax)
            {
                moveDir += DirUp() * yMoveSpeed;
            }
            else if (y < yBorder && y >= 0)
            {
                moveDir -= DirUp() * yMoveSpeed;
            }

            transform.position = transform.position + moveDir * Time.deltaTime;
        }

        private void Update()
        {
            DragUpdate();
            if (enableEdgeAutoDrag)
            {
                EdgeAutoUpdate();
            }
        }
    }
}

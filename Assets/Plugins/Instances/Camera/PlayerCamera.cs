using GameBase.GCamera;
using GameBase.Tools;
using UnityEngine;
namespace GameBase.Instance
{
    public class PlayerCamera : MonoBehaviour
    {
        private bool _inDrag = false;
        private Vector3 _dragPosition;

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

        private void DragUpdate()
        {
            if (Inputs.GetKeyDown(KeyFunction.DragScreen, "camera"))
            {
                _inDrag = true;
                _dragPosition = CameraSys.MouseHitPosition;
            }
            else if (Inputs.GetKeyUp(KeyFunction.DragScreen, "camera"))
            {
                _inDrag = false;
            }

            if (_inDrag)
            {
                var position = CameraSys.MouseHitPosition;
                Vector3 currPosition = _dragPosition * 2 - position;
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
            EdgeAutoUpdate();
        }
    }
}

using GameBase.GCamera;
using UnityEngine;
namespace GameBase.Instance
{
    public class PlayerCamera : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            CameraSys.Main = GetComponent<Camera>();
        }
    }
}

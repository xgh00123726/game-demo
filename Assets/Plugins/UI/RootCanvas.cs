using GameBase.Resources;
using UnityEngine;

namespace GameBase.UI
{
    public class RootCanvas : MonoBehaviour
    {
        internal static RootCanvas instance;
        public static RootCanvas Instance => instance;
        void Awake()
        {
            instance = this;
        }
    }
}

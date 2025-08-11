using GameBase.Resources;
using UnityEngine;

namespace GameBase.UI
{
    public class WorldCanvs : MonoBehaviour
    {
        internal static WorldCanvs instance;
        public static WorldCanvs Instance => instance;
        void Awake()
        {
            instance = this;
        }
    }
}

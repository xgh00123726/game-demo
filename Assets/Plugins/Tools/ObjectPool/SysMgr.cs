using System.Collections.Generic;
using UnityEngine;
namespace GameBase.Tools
{
    internal class SysMgr : MonoBehaviour
    {
        private static LinkedList<IBaseSys> systems = new LinkedList<IBaseSys>();
        public static void AddSys(IBaseSys sys)
        {
            sys.Awake();
            systems.AddLast(sys);
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            foreach (IBaseSys sys in systems)
            {
                sys.Update();
            }
        }
    }
}

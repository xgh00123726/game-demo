using System.Collections.Generic;
using UnityEngine;
namespace GameBase.Tools
{
    public class SysMgr : MonoBehaviour
    {
        public static List<string> sysNames = new List<string>();
        public List<string> sysNamesCopy;
        private static LinkedList<IBaseSys> systems = new LinkedList<IBaseSys>();
        public static void AddSys(IBaseSys sys)
        {
            sysNames.Add(sys.GetType().Name);
            sys.Awake();
            systems.AddLast(sys);
        }

        private void Awake()
        {
            sysNamesCopy = sysNames;
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

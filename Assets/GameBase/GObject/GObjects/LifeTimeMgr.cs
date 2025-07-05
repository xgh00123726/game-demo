using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameBase.Object
{
    public class LifeTimeMgr : MonoBehaviour
    {
        public string startScene = "DemoCopy";
        public int managerNum = 0;
        private static List<IManager> _managers = new List<IManager>();

        public static void AddMgr(IManager manager)
        {
            _managers.Add(manager);
        }
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(startScene);
        }

        void Update()
        {
            foreach (IManager manager in _managers)
            {
                manager.Update();
            }
            managerNum = _managers.Count;
        }
    }
}

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

        private static int _updateTick = 0;
        private static int _fixedUpdateTick = 0;
        public static int UpdateTick => _updateTick;
        public static int FixedUpdateTick => _fixedUpdateTick;

        public static void RegisterMgr(IManager manager)
        {
            _managers.Add(manager);
        }
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(startScene);
        }

        private void FixedUpdate()
        {
            ++_fixedUpdateTick;
        }

        void Update()
        {
            ++_updateTick;
            foreach (IManager manager in _managers)
            {
                manager.Update();
            }
            managerNum = _managers.Count;
        }
    }
}

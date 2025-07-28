using System.Collections.Generic;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = GameBase.Tools.Logger;

namespace GameBase.LifeTime
{
    public class LifeTimeMgr : MonoBehaviour
    {
        public string managerScene = "Managers";
        public string startScene = "DemoCopy";
        public bool _sceneLoaded = false;
        public int managerNum = 0;
        private static List<string> _managerNames = new List<string>();
        public List<string> managerNames = _managerNames;
        private static LinkedList<IManager> _managersWillAdd = new LinkedList<IManager>();
        private static LinkedList<IManager> _managers = new LinkedList<IManager>();

        private static int _updateTick = 0;
        private static int _fixedUpdateTick = 0;
        public static int UpdateTick => _updateTick;
        public static int FixedUpdateTick => _fixedUpdateTick;

        public static void RegisterMgr(IManager manager)
        {
            _managersWillAdd.AddLast(manager);
            _managerNames.Add(manager.GetType().Name);
        }
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            ResourcesLoader.LoadAllAsset();
            SceneManager.LoadScene(managerScene);
            SceneManager.LoadScene(startScene);
            Tools.Logger.Instance.Log($"id 0:{ResourcesLoader._prefabs[0]}, id 1:{ResourcesLoader._prefabs[1]}");
        }


        private void FixedUpdate()
        {
            ++_fixedUpdateTick;
        }

        void Update()
        {
            ++_updateTick;
            foreach (IManager manager in _managersWillAdd)
            {
                _managers.AddLast(manager);
            }
            _managersWillAdd.Clear();
            foreach (IManager manager in _managers)
            {
                manager.Update();
            }
            managerNum = _managers.Count;
        }
    }
}

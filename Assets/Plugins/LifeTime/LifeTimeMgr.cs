using System;
using System.Collections.Generic;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = GameBase.Tools.XLogger;

namespace GameBase.LifeTime
{
    public class LifeTimeMgr : MonoBehaviour
    {
        public string managerScene = "Managers";
        public List<string> backgroundScenes = new List<string> { "BackGround" };
        public List<string> entityScenes = new List<string>{ "ProjTextTest" };
        public bool _sceneLoaded = false;

        private static int _updateTick = 0;
        private static int _fixedUpdateTick = 0;
        
        private static List<Action> _updatesNeedAdd = new List<Action>();
        private static List<Action> _updates = new List<Action>();

        public static int UpdateTick => _updateTick;
        public static int FixedUpdateTick => _fixedUpdateTick;

        public static void AddUpdate(Action action)
        {
            _updatesNeedAdd.Add(action);
        }


        void Start()
        {
            DontDestroyOnLoad(gameObject);

            ResourcesLoader.LoadAllAsset();

            SceneManager.LoadScene(managerScene);
            if (backgroundScenes.Count > 0)
            {
                SceneManager.LoadScene(backgroundScenes[0]);
            }
            for (int i = 1; i < backgroundScenes.Count; i++)
            {
                SceneManager.LoadScene(backgroundScenes[i], LoadSceneMode.Additive);
            }
            foreach (var s in entityScenes)
            {
                SceneManager.LoadScene(s, LoadSceneMode.Additive);
            }
        }


        private void FixedUpdate()
        {
            ++_fixedUpdateTick;
        }

        void Update()
        {
            ++_updateTick;
            foreach (var update in _updatesNeedAdd)
            {
                _updates.Add(update);
            }
            _updatesNeedAdd.Clear();

            foreach (var update in _updates)
            {
                update?.Invoke();
            }
        }
    }
}

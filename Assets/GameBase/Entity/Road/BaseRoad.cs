using System.Collections.Generic;
using System.Linq;
using GameBase.Math;
using GameBase.Resources;
using UnityEngine;

namespace GameBase.Entity
{
    public class BaseRoad : MonoBehaviour
    {
        static List<GameObject> _roadPrefabs = new List<GameObject>();
        static bool _classInit = false;

        GameObject _roadObject;
        bool _selfInit = false;

        public float widthFactor = 1f;

        Vector3 _internalDir;
        internal Vector3 InternalDir
        {
            get => _internalDir;
            set
            {
                _internalDir = value.normalized;
            }
        }
        float _width = 1f;
        internal float Width
        {
            get => _width;
            set
            {
                Vector3 vertical = Quaternion.Euler(0, 90, 0) * InternalDir;
                float amplify = value / _width;
                float xAmp = amplify * vertical.x;
                float zAmp = amplify * vertical.z;
                
                float x = transform.localScale.x * xAmp;
                float z = transform.localScale.z * zAmp;
                transform.localScale = new Vector3(x, transform.localScale.y, z);

                _width = value;
            }
        }

        internal void Init()
        {
            if (!_classInit)
            {
                _classInit = true;
                _roadPrefabs = ResourceMgr.LoadAllPrefab(PrefabType.Terrain, "Mound").ToList<GameObject>();
            }
            if (_selfInit) return;
            _selfInit = true;
            int index = Random.Range(0, _roadPrefabs.Count - 1);
            _roadObject = GameObject.Instantiate(_roadPrefabs[index], transform);
        }
        private void Awake()
        {
            Init();
        }
    }
}

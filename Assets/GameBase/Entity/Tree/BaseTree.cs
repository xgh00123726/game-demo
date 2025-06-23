using System.Collections.Generic;
using System.Linq;
using GameBase.Entity;
using GameBase.Resources;
using UnityEngine;

public class BaseTree : BaseEntity
{
    static List<GameObject> _foliagePrefabs = new List<GameObject>();
    static List<GameObject> _trunkPrefabs = new List<GameObject>();
    static bool _initFlag = false;

    GameObject _foliageObj;
    GameObject _trunkObj;
    private void Awake()
    {
        if (!_initFlag)
        {
            _initFlag = true;
            _foliagePrefabs = ResourceMgr.LoadAllPrefab(PrefabType.Vegetation, "Tree/Modular/Foliage").ToList<GameObject>();
            _trunkPrefabs = ResourceMgr.LoadAllPrefab(PrefabType.Vegetation, "Tree/Modular/Trunk").ToList<GameObject>();
        }

        int foliageIndex = Random.Range(0, _foliagePrefabs.Count - 1);
        int trunkIndex = Random.Range(0, _trunkPrefabs.Count - 1);
        _foliageObj = GameObject.Instantiate<GameObject>(_foliagePrefabs[foliageIndex]);
        _trunkObj = GameObject.Instantiate<GameObject>(_trunkPrefabs[trunkIndex]);
        _foliageObj.transform.parent = transform;
        _trunkObj.transform.parent = transform;
    }
}

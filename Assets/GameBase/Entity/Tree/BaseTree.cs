using System.Collections.Generic;
using System.Linq;
using GameBase.Entity;
using GameBase.Resources;
using UnityEngine;

// BaseTree: 主要完成BaseTree的组件组装工作
// 当一个Basetree Gameobject被实例化之后，该类的awake函数就会自动从Resource里寻找树叶和树干组装它的身体
public class BaseTree : BaseEntity
{
    static List<GameObject> _foliagePrefabs = new List<GameObject>();
    static List<GameObject> _trunkPrefabs = new List<GameObject>();
    static bool _classInit = false;

    bool _selfInit = false;
    GameObject _foliageObj;
    GameObject _trunkObj;

    internal void Init()
    {
        if (!_classInit)
        {
            _classInit = true;
            _foliagePrefabs = ResourceMgr.LoadAllPrefab(PrefabType.Vegetation, "Tree/Modular/Foliage").ToList<GameObject>();
            _trunkPrefabs = ResourceMgr.LoadAllPrefab(PrefabType.Vegetation, "Tree/Modular/Trunk").ToList<GameObject>();
        }
        if (_selfInit) return;

        _selfInit = true;
        int foliageIndex = Random.Range(0, _foliagePrefabs.Count - 1);
        int trunkIndex = Random.Range(0, _trunkPrefabs.Count - 1);
        _foliageObj = GameObject.Instantiate<GameObject>(_foliagePrefabs[foliageIndex], transform);
        _trunkObj = GameObject.Instantiate<GameObject>(_trunkPrefabs[trunkIndex], transform);
    }

    private void Awake()
    {
        Init();
    }
}

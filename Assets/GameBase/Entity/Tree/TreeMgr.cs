using System.Collections.Generic;
using GameBase.Editable;
using GameBase.Resources;
using UnityEngine;

public class TreeMgr : MonoBehaviour
{
    public delegate Vector3 PositionGetter();
    public static TreeMgr Instance => _instance;
    private static TreeMgr _instance;
    public static List<BaseTree> _treeList = new List<BaseTree>();
    private static List<EditableShape> _childs = new List<EditableShape>();
    // 创建树，并在Mgr中保存引用
    public static BaseTree CreateTree(Vector3? position = null, Transform parent = null, string name = "Tree")
    {
        BaseTree tree = PoolablePrefabMgr.GetNotfromPool<BaseTree>(PrefabType.Vegetation, "Tree");
        tree.Init();
        _treeList.Add(tree);
        if (parent != null)
        {
            tree.transform.parent = parent;
        }
        if (position != null)
        {
            tree.transform.position = (Vector3)position;
        }
        return tree;
    }

    // 批量创建树，并在Mgr中保存引用
    public static List<BaseTree> CreateTrees(int count, Vector3? position = null, Transform parent = null, string name = "Tree")
    {
        List<BaseTree> trees = new List<BaseTree>();
        for (int i = 0; i < count; i++)
        {
            trees.Add(CreateTree(position, parent, name));
        }
        return trees;
    }
    // 批量创建树，并在Mgr中保存引用，使用position getter替换position，使得批量创建的树坐标可以自定义
    public static List<BaseTree> CreateTrees(int count, PositionGetter GetPosition = null, Transform parent = null, string name = "Tree")
    {
        List<BaseTree> trees = new List<BaseTree>();
        for (int i = 0; i < count; i++)
        {
            trees.Add(CreateTree(GetPosition?.Invoke(), parent, name));
        }
        return trees;
    }

    private void Init()
    {
        _instance = _instance != null ? _instance : this;
        _childs.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var shape = child.GetComponent<EditableShape>();
            if (shape == null)
            {
                Debug.LogWarning($"child:{child.name} of TreeMgr must has a EditableShape component");
            }
            _childs.Add(shape);
        }
    }

    private void Awake()
    {
        Init();
        int treeMax = 20;
        int treeMin = 10;
        foreach (var child in _childs)
        {
            int treeNum = Random.Range(treeMin, treeMax);
            CreateTrees(treeNum, () => child.ShapeSpcaceToWorldVec3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), child.transform);
        }
    }
}

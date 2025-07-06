using System.Collections;
using System.Collections.Generic;
using GameBase.Editable;
using GameBase.Entity;
using GameBase.Resources;
using UnityEngine;

public class RoadMgr : MonoBehaviour
{
    public static RoadMgr Instance => _instance;
    private static RoadMgr _instance;
    public static List<BaseRoad> _roadList = new List<BaseRoad>();
    private static List<EditableCurve> _childs = new List<EditableCurve>();
    // 创建树，并在Mgr中保存引用
    public static BaseRoad CreateRoad(Vector3 begin, Vector3 end, float width = 1f, Transform parent = null, string name = "RoadClip")
    {
        BaseRoad road = PoolablePrefabMgr.GetNotfromPool<BaseRoad>(PrefabType.Terrain);
        road.Init();
        _roadList.Add(road);
        if (parent != null)
        {
            road.transform.parent = parent;
        }
        road.transform.localPosition = (begin + end) / 2;
        road.InternalDir = end - begin;
        road.Width = width;
        return road;
    }

    // 批量创建树，并在Mgr中保存引用
    public static List<BaseRoad> CreateRoads(int count, Vector3 begin, Vector3 end, float width = 1f, Transform parent = null, string name = "RoadClip")
    {
        List<BaseRoad> trees = new List<BaseRoad>();
        Vector3 from = begin;
        Vector3 to = begin;
        for (int i = 1; i <= count; i++)
        {
            to = Vector3.Lerp(begin, end, i / (float)count);
            trees.Add(CreateRoad(from, to, width, parent, name));
            from = to;
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
            var shape = child.GetComponent<EditableCurve>();
            if (shape == null)
            {
                Debug.LogWarning($"child:{child.name} of TreeMgr must has a EditableCurve component");
            }
            _childs.Add(shape);
        }
    }

    private void Awake()
    {
        Init();
    }
}

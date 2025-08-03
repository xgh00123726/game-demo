using GameBase.Creature;
using GameBase.Math;
using GameBase.Resources;
using GameBase.Tools;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSys : UObjEntitySys<Creature, CSObjectPool<Creature>, GameObject, UObjectPool<GameObject>>
{
    public delegate bool CreatureFilter(Creature e);

    public static float infDis = 9999f;

    protected override int ContainerCapacity => ResourcesLoader.PrefabCount;

    /// <summary>
    /// 返回指定位置最近的游戏实体
    /// <list type="bullet">
    /// <item><param name="position"><paramref name="position"/>:指定的位置</param></item>
    /// <item><param name="id"><paramref name="id"/>:限定的ID，负数表示无限制</param></item>
    /// <item><param name="filter"><paramref name="filter"/>:寻找过滤器</param></item>
    /// <item><param name="rangeLimit"><paramref name="rangeLimit"/>:只会寻找到rangeLimit距离内的实体，负数表示无穷</param></item>
    /// </list></summary>
    /// <returns>符合条件最近的实体，没有实体满足条件则返回null</returns>
    public static T NearestEntity<T>(Vector3 position, CreatureFilter filter = null, int id = -1, float rangeLimit = -1) where T : Creature
    {
        Creature ret = null;
        var sys = Instance as CreatureSys;
        float minDistance = float.PositiveInfinity;
        bool hasFilter = filter != null;

        if (rangeLimit < 0)
        {
            rangeLimit = infDis;
        }

        foreach (var e in sys._activeEntities)
        {
            if (hasFilter && !filter(e)) continue; // 不满足过滤需求

            if (id >= 0 && e.ObjID != id)
            {
                continue;
            }

            float dis = (e.Obj.transform.position - position).magnitude;

            if (dis > rangeLimit) continue;

            if (dis < minDistance)
            {
                minDistance = dis;
                ret = e;
            }
        }

        return ret as T;
    }

    /// <summary>
    /// 返回指定范围内所有实体
    /// <list type="bullet">
    /// <item><param name="IShape2D"><paramref name="position"/>:范围</param></item>
    /// <item><param name="filter"><paramref name="filter"/>:寻找过滤器</param></item>
    /// </list></summary>
    /// <returns>指定范围内所有实体，没有实体满足条件则返回null</returns>
    public static LinkedList<Creature> CreaturesInShape(IShape2D shape, CreatureFilter filter = null)
    {
        var ret = new LinkedList<Creature>();
        var sys = Instance as CreatureSys;
        bool hasFilter = filter != null;
        foreach (var e in sys._activeEntities)
        {
            if (hasFilter && !filter(e)) continue;

            float x = e.body.transform.position.x;
            float y = e.body.transform.position.z;
            if (shape.Contains(x, y))
            {
                ret.AddLast(e);
            }
        }

        return ret;
    }

    protected override GameObject InstantiateObj(Creature e)
    {
        return GameObject.Instantiate(ResourcesLoader.GetPrefab(e.bodyID));
    }

    protected override void OnInstantiateUObject(Creature e)
    {
        e.body.transform.position = e.genPos;

        e.body.SetActive(true);
    }

    protected override void OnReleaseUObject(Creature e)
    {
        e.body.SetActive(false);
    }

    protected override void UpdateEntity(Creature e)
    {
        
    }
}

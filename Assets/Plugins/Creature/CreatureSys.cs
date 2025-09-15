using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Move;
using GameBase.Resources;
using GameBase.UI;
using System;
using UnityEngine;

public class CreatureSys : UObjEntitySys<Creature, GameObject, CreatureSys>
{
    public static float infDis = 9999f;

    /// <summary>
    /// 返回指定位置最近的游戏实体
    /// <list type="bullet">
    /// <item><param name="position"><paramref name="position"/>:指定的位置</param></item>
    /// <item><param name="id"><paramref name="id"/>:限定的ID，负数表示无限制</param></item>
    /// <item><param name="filter"><paramref name="filter"/>:寻找过滤器</param></item>
    /// <item><param name="rangeLimit"><paramref name="rangeLimit"/>:只会寻找到rangeLimit距离内的实体，负数表示无穷</param></item>
    /// </list></summary>
    /// <returns>符合条件最近的实体，没有实体满足条件则返回null</returns>
    public T NearestEntity<T>(Vector3 position, Func<Creature, bool> filter = null, int id = -1, float rangeLimit = -1) where T : Creature
    {
        Creature ret = null;
        var sys = Instance as CreatureSys;
        float minDistance = float.PositiveInfinity;
        bool hasFilter = filter != null;

        if (rangeLimit < 0)
        {
            rangeLimit = infDis;
        }

        foreach (var e in sys.Entities)
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

    protected override GameObject InstantiateObj(Creature e)
    {
        var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));

        e.animator = obj.GetComponent<Animator>();
        e.Obj = obj;

        MoveSys.Instance.NewEntity((Mover em) =>
        {
            em.owner = e;
        });
        RotateSys.Instance.NewEntity((Rotater er) =>
        {
            er.owner = e;
        });
        HealthBarSys.Instance.NewEntity((HealthBar eh) =>
        {
            eh.ObjID = 6;
            eh.owner = e;
        });

        return obj;
    }

    protected override void AfterInstantiateEUObject(Creature e)
    {
        e.Alive = true;

        e.Obj.SetActive(true);
    }

    protected override void BeforeReleaseEUObject(Creature e)
    {
        e.Alive = false;

        e.Obj.SetActive(false);
    }

    protected override void UpdateEntity(Creature e)
    {
        if (e.ReleaseTrigger)
        {
            RemoveEntity(e);
        }
    }

    public void RemoveAll(Tag tag)
    {
        foreach (var e in Entities)
        {
            if (e.tag == tag)
            {
                RemoveEntity(e);
            }
        }
    }

    public void RemoveAll<T_EntityType>() where T_EntityType : Creature
    {
        foreach (var c in Entities)
        {
            if (c.GetType() == typeof(T_EntityType))
            {
                RemoveEntity(c);
            }
        }
    }
}

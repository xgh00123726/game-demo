using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Move;
using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSys : UObjEntitySys<Creature, GameObject, CreatureSys>
{
    internal int instanceNum = 0;
    internal Dictionary<int, Creature> _creatrues = new();

    /// <summary>
    /// 返回指定位置最近的游戏实体
    /// <list type="bullet">
    /// <item><param name="position"><paramref name="position"/>:指定的位置</param></item>
    /// <item><param name="id"><paramref name="id"/>:限定的ID，负数表示无限制</param></item>
    /// <item><param name="filter"><paramref name="filter"/>:寻找过滤器</param></item>
    /// <item><param name="rangeLimit"><paramref name="rangeLimit"/>:只会寻找到rangeLimit距离内的实体</param></item>
    /// </list></summary>
    /// <returns>符合条件最近的实体，没有实体满足条件则返回null</returns>
    public Creature NearestEntity(Vector3 position, CreatureTag tag, float rangeLimit = 10)
    {
        Creature c = null;
        float minDistance = float.PositiveInfinity;

        foreach (var e in Entities)
        {
            if ((e.tag & tag) == 0)
            {
                continue;
            }
            

            float dis = (e.Obj.transform.position - position).magnitude;

            if (dis > rangeLimit) continue;

            if (dis < minDistance)
            {
                minDistance = dis;
                c = e;
            }
        }

        return c;
    }

    protected override GameObject InstantiateObj(Creature e)
    {
        var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));

        e.animator = obj.GetComponent<Animator>();
        e.Obj = obj;

        e.mover = MoveSys.Instance.NewEntity();
        e.mover.owner = e;

        e.rotater = RotateSys.Instance.NewEntity();
        e.rotater.owner = e;

        e.animController = new GameBase.Animations.HumanAnimController(e);

        e.instanceID = instanceNum++;

        return obj;
    }

    protected override void AfterInstantiateEUObject(Creature e)
    {
        e.Alive = true;

        e.healthBar = HealthBarSys.Instance.NewEntity((HealthBar eh) =>
        {
            eh.ObjID = 6;
        });
        e.healthBar.owner = e;

        _creatrues.Add(e.instanceID, e);

        e.Obj.SetActive(true);
    }

    protected override void BeforeReleaseEUObject(Creature e)
    {
        e.Alive = false;

        _creatrues.Remove(e.instanceID);

        e.Obj.SetActive(false);
    }

    protected override void UpdateEntity(Creature e)
    {
        if (e.ReleaseTrigger)
        {
            RemoveEntity(e);
        }

        e.animController.Update();
    }

    public bool Exist(int  instanceID)
    {
        return _creatrues.ContainsKey(instanceID);
    }

    public Creature GetCreature(int instanceID)
    {
        if (_creatrues.ContainsKey(instanceID))
        {
            return _creatrues[instanceID];
        }

        return null;
    }

    public void RemoveAll(CreatureTag tag)
    {
        List<Creature> needRemove = new();

        foreach (var e in Entities)
        {
            if (e.tag == tag)
            {
                needRemove.Add(e);
            }
        }

        foreach(var e in needRemove)
        {
            RemoveEntity(e);
        }
    }
}

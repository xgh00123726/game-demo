using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Move;
using GameBase.Resources;
using GameBase.Spells;
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

        return obj;
    }

    protected override void AfterInstantiateEUObject(Creature e)
    {
        e.Alive = true;

        e.instanceID = instanceNum++;

        e.healthBar = HealthBarSys.Instance.NewEntity((HealthBar eh) =>
        {
            eh.ObjID = 6;
        });
        e.healthBar.owner = e;

        e.mover = MoveSys.Instance.NewEntity();
        e.mover.owner = e;

        e.rotater = RotateSys.Instance.NewEntity();
        e.rotater.owner = e;

        e.animator = e.Obj.GetComponent<Animator>();

        _creatrues.Add(e.instanceID, e);

        e.Obj.SetActive(true);
    }

    protected override void BeforeReleaseEUObject(Creature e)
    {
        e.Alive = false;

        _creatrues.Remove(e.instanceID);

        MoveSys.Instance.RemoveEntity(e.mover);
        RotateSys.Instance.RemoveEntity(e.rotater);
        if (e.collider != null)
        {
            CollideSys.Instance.RemoveEntity(e.collider);
        }

        for(int i = 0; i < e.spells.Size; i++)
        {
            var spell = e.spells[i];
            e.spells.Remove(i);
            SpellSys.Instance.RemoveEntity(spell);
        }

        e.Obj.SetActive(false);
    }

    protected override void UpdateEntity(Creature e)
    {
        if (e.modifyables["currHP"] <= 0)
        {
            e.OnDead?.Invoke(e);
            RemoveEntity(e);
            return;
        }
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

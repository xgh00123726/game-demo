using GameBase.AI;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Move;
using GameBase.Resources;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.UI;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSys : KeyEntitySys<int, Creature, CreatureSys>
{
    protected override Creature CtorT(int k)
    {
        var e = new Creature();
        var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(k));
        e.obj = obj;
        return e;
    }

    protected override void OnGet(Creature e)
    {
        e.mover = MoveSys.Instance.NewEntity();
        e.mover.owner = e;

        e.rotater = RotateSys.Instance.NewEntity();
        e.rotater.owner = e;

        e.animator = e.obj.GetComponent<Animator>();
    }

    protected override void EntityStart(Creature e)
    {
        e.Alive = true;

        e.healthBar = HealthBarSys.Instance.NewEntity(6);
        e.healthBar.owner = e;

        e.HighLevelAttrInit();

        e.obj.SetActive(true);
    }

    protected override void OnRelease(Creature e)
    {
        e.Alive = false;

        e.HighLevelAttrDispose();

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

        AISys.Instance.RemoveEntity(e.ai);

        e.obj.SetActive(false);
    }

    protected override void UpdateEntity(Creature e)
    {
        e.HighLevelAttrUpdate();

        float currHP = e.modifyables["currHP"].Value;
        if (currHP <= 0)
        {
            e.OnDead?.Invoke(e);
            RemoveEntity(e);
            return;
        }

        float maxHP = e.modifyables["maxHP"].Value;
        float healthRegen = e.modifyables["healthRegen"].Value * Time.deltaTime;

        healthRegen = Mathf.Min(healthRegen, maxHP - currHP);

        e.modifyables.ForceModify(8, healthRegen); // 8： currHP
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


            float dis = (e.obj.transform.position - position).magnitude;

            if (dis > rangeLimit) continue;

            if (dis < minDistance)
            {
                minDistance = dis;
                c = e;
            }
        }

        return c;
    }
}

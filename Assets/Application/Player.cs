using System.Collections.Generic;
using GameBase.Object;
using GameBase.Tools;
using GameBase.Effects;
using GameBase.Spell;
using GameBase.GCamera;
using GameBase.UI;
using GameBase.Modify;
using GameBase.Buff;
using UnityEngine;
using GameBase.Resources;
using GameBase.Entity;
using Logger = GameBase.Tools.Logger;

public class Player : GameEntity
{
    public bool isSkillIndicatorPlay = false;
    public AttrItem coolAccPanelItem;
    public AttrItem HPMaxPanelItem;
    public AttrItem MPMaxPanelItem;
    public AttrItem damagePanelItem;
    public AttrItem defensePanelItem;

    protected override void Awake()
    {
        base.Awake();
        camp = Camp.Friendly;
    }

    protected override void Start()
    {
        base.Start();
        moveComponent.moveSpeed = 10.0f;
        attrs.damage = 100f;
        Logger.Instance.Log($"damage:{attrs.damage.Value}");

        animationComponent.SetDefaultClip("HumanIdle");
        animationComponent.EnableClipLoop("HumanRun");

        moveComponent.OnStop = () =>
        {
            animationComponent.ResetEnable = true;
            animationComponent.ForceSetClipRun("HumanIdle");
        };
        // 箭雨，发射指定数目的箭矢射向指定地点
        AddSpell(new ArrowOfRain(this, PoolablePrefabMgr.GetNotfromPool<SpellCircleIndicator>(PrefabType.Effect))
        {
            AttackAction = (ArrowOfRain skill) =>
            {
                animationComponent.ForceSetClipRun("HumanDrawArrow");
                animationComponent.ResetEnable = true;
                skill.src = transform.position;
                skill.dest = PlayerCamera.MouseHitPoint;
            },
            ChooseCondition = (GSpell skill) => Inputs.GetKeyDown(KeyFunction.Spell1),
            CancelCondition = (GSpell skill) => Inputs.GetKeyDown(KeyFunction.Cancel),
            InvokeCondition = (GSpell skill) => skill.IsChoosing && Inputs.GetKeyDown(KeyFunction.MouseConfirm),
            damage = attrs.damage * 0.5f
        });
        // 普攻，发射一根箭射向指定地点
        AddSpell(new Attack(this)
        {
            AttackAction = (Attack skill) =>
            {
                animationComponent.ForceSetClipRun("HumanDrawArrow");
                animationComponent.ResetEnable = true;
                skill.src = transform.position;
                skill.dest = PlayerCamera.MouseHitPoint;
            },
            ChooseCondition = (GSpell skill) => Inputs.GetKeyDown(KeyFunction.Aim),
            CancelCondition = (GSpell skill) => Inputs.GetKeyDown(KeyFunction.Cancel),
            InvokeCondition = (GSpell skill) => skill.IsChoosing && Inputs.GetKeyDown(KeyFunction.MouseConfirm),
            damage = attrs.damage
        }, false);
        // 为自己添加一个buff
        AddSpell(new SpellNoTarget(this)
        {
            spellAction = () =>
            {
                var buff = new BuffModify(this, new ModifyLite() { coolingAcclerate = 10 }) { duration = 5 };
                buff.CastTo(this);
                BuffPanel.Instance.ShowBuff(buff);
            },
            ChooseCondition = (GSpell spell) => Inputs.GetKeyDown(KeyFunction.Spell2),
            CancelCondition = (GSpell spell) => Inputs.GetKeyDown(KeyFunction.Cancel),
            InvokeCondition = (GSpell skill) => skill.IsChoosing && Inputs.GetKeyDown(KeyFunction.Spell2),
            coolingTimeSet = 10f,
        });
        // 向指定目标（游戏物体）发射一根追踪箭
        AddSpell(new TracerArrow(this, PoolablePrefabMgr.GetNotfromPool<SpellCircleIndicator>(PrefabType.Effect))
        {
            Radius = 1f,
            CastAction = (TracerArrow arrow) =>
            {
                var entity = EntityMgr.NearestEntity(PlayerCamera.MouseHitPoint, arrow.Radius);
                arrow.target = entity;
            },
            ChooseCondition = (GSpell skill) => Inputs.GetKeyDown(KeyFunction.Spell3),
            CancelCondition = (GSpell skill) => Inputs.GetKeyDown(KeyFunction.Cancel),
            InvokeCondition = (GSpell skill) => skill.IsChoosing
                && Inputs.GetKeyDown(KeyFunction.MouseConfirm)
                && EntityMgr.NearestEntity(PlayerCamera.MouseHitPoint, 1) != null,
            damage = attrs.damage * 0.75f
        });

        coolAccPanelItem = AttrPanel.Instance.AddItem();
        coolAccPanelItem.KeyText = "spell accelerate";
        
        HPMaxPanelItem = AttrPanel.Instance.AddItem();
        HPMaxPanelItem.KeyText = "HP Max";

        MPMaxPanelItem = AttrPanel.Instance.AddItem();
        MPMaxPanelItem.KeyText = "MP Max";

        damagePanelItem = AttrPanel.Instance.AddItem();
        damagePanelItem.KeyText = "damage";

        defensePanelItem = AttrPanel.Instance.AddItem();
        defensePanelItem.KeyText = "defense";

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Inputs.GetKeyDown(KeyFunction.MoveTo))
        {
            moveComponent.MoveTo(PlayerCamera.MouseHitPoint);
            MoveIndicator.Instance.PlayAt(PlayerCamera.MouseHitPoint);
            animationComponent.SetClipRun("HumanRun");
            animationComponent.ResetEnable = false;
        }

        if (Inputs.GetKeyDown(KeyFunction.Stop))
        {
            moveComponent.Stop();
            rotateComponent.Stop();
        }

        if (Inputs.GetKeyDown(KeyFunction.ToggleAttrPanel))
        {
            AttrPanel.Instance.ToggleShow();
        }

        coolAccPanelItem.ValueText = attrs.coolingAcclerate.Value.ToString();
        HPMaxPanelItem.ValueText = attrs.HPMax.Value.ToString();
        MPMaxPanelItem.ValueText = "XXX";
        damagePanelItem.ValueText = attrs.damage.Value.ToString();
        defensePanelItem.ValueText = attrs.defense.Value.ToString();
    }
}

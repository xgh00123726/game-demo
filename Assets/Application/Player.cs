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

public class Player : GameEntity
{
    public bool isSkillIndicatorPlay = false;
    

    protected override void Start()
    {
        base.Start();
        moveComponent.moveSpeed = 10.0f;

        animationComponent.SetDefaultClip("HumanIdle");
        animationComponent.EnableClipLoop("HumanRun");

        moveComponent.OnStop = () =>
        {
            animationComponent.ResetEnable = true;
            animationComponent.ForceSetClipRun("HumanIdle");
        };
        AddSpell(new ArrowOfRain(this, PrefabMgr.Instance.GetNotfromPool<SpellCircleIndicator>(PrefabType.Effect))
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
            InvokeCondition = (GSpell skill) => skill.IsChoosing && Inputs.GetKeyDown(KeyFunction.MouseConfirm)
        });
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
            InvokeCondition = (GSpell skill) => skill.IsChoosing && Inputs.GetKeyDown(KeyFunction.MouseConfirm)
        }, false);
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
            coolingTimeSet = 10f
        });

        AttrPanel.Instance.AddItem();
        AttrPanel.Instance.Childs[0].KeyText = "spell accelerate";
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

        AttrPanel.Instance.Childs[0].ValueText = attrs.coolingAcclerate.Value.ToString();
    }
}

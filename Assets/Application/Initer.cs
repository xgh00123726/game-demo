using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using GameBase.Entity;
using GameBase.GCamera;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;
public class Initer : MonoBehaviour
{
    AttrItem itemX;
    AttrItem itemY;
    AttrItem itemZ;
    AttrItem entityMgrObjectNum;
    void Start()
    {
#if UNITY_EDITOR
        itemX = AttrPanel.Instance.AddItem();
        itemY = AttrPanel.Instance.AddItem();
        itemZ = AttrPanel.Instance.AddItem();

        var builder = new BehaviorTreeBuilder();

        builder.Repeat(3)
                    .Sequence()
                        .Log("this is test for behavior tree builder")
                    .Back()
                .End();
        builder.Tree.Tick();

        entityMgrObjectNum = AttrPanel.Instance.AddItem();
#endif
    }

    private void Update()
    {
#if UNITY_EDITOR
        itemX.KeyText = "x";
        itemY.KeyText = "y";
        itemZ.KeyText = "z";
        itemX.ValueText = PlayerCamera.MouseHitPoint.x.ToString();
        itemY.ValueText = PlayerCamera.MouseHitPoint.y.ToString();
        itemZ.ValueText = PlayerCamera.MouseHitPoint.z.ToString();

        entityMgrObjectNum.KeyText = "eMgr objNum";
        int[] nums = EntityMgr.ObjectNumOf("Enemy1");
        entityMgrObjectNum.ValueText = $"a:{nums[0]} r:{nums[1]}";
#endif
    }
}

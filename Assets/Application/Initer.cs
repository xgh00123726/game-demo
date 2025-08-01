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

        AttrPanel.Instance.Hide();

        Physics.gravity = new Vector3(0, -100, 0);

#endif
    }

    private void Update()
    {
#if UNITY_EDITOR
        itemX.KeyText = "x";
        itemY.KeyText = "y";
        itemZ.KeyText = "z";
#endif
    }
}

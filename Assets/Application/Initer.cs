using System.Collections;
using System.Collections.Generic;
using GameBase.GCamera;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

public class Initer : MonoBehaviour
{
    AttrItem itemX;
    AttrItem itemY;
    AttrItem itemZ;
    void Start()
    {
#if UNITY_EDITOR
        itemX = AttrPanel.Instance.AddItem();
        itemY = AttrPanel.Instance.AddItem();
        itemZ = AttrPanel.Instance.AddItem();
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
#endif
    }
}

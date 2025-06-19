using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInfo
{
    public static Vector3 MousePosition(float height)
    {
        Vector3 pos = Input.mousePosition;
        pos.z = height;
        return Camera.main.ScreenToWorldPoint(pos);
    }
}

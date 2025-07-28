using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameBase.Tools
{
    public partial class Inputs
    {
        public static Vector3 MouseHitPostion(Camera cam)
        {
            Ray moveRay = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);
            Physics.Raycast(moveRay, out RaycastHit rayHit);
            return rayHit.point;
        }
    }
}

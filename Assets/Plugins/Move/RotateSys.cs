using GameBase.Math;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Move
{
    public class RotateSys : SimplestEntitySys<Rotater, EntityList<Rotater>, RotateSys>
    {
        public static float turnStopAngle = 1f;
        protected override void OnRegisterEntity(Rotater e)
        {
            
        }

        protected override void OnRemoveEntity(Rotater e)
        {
            
        }

        protected override void UpdateEntity(Rotater e)
        {
            e.dir = e.body.transform.forward;
            Vector3 dirSetTemp = new Vector3(e.dirSet.x, 0, e.dirSet.z);
            float angle = Vector3.Angle(e.dir, dirSetTemp);
            float crossY = Vector3.Cross(e.dir, dirSetTemp).y;
            
            if (angle < turnStopAngle)
            {
                return;
            }

            e.body.transform.Rotate(Vector3.up, e.turnSpeed * Time.deltaTime * Mathf.Sign(crossY), Space.Self);
        }
    }
}

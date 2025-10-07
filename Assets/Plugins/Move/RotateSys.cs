using GameBase.EntitySystem;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Move
{
    public class RotateSys : CommonEntitySys<Rotater, RotateSys>
    {
        public static float turnStopAngle = 1f;

        protected override void UpdateEntity(Rotater e)
        {
            var currDir = e.owner.Obj.transform.forward;
            Vector3 dirSetTemp = new Vector3(e.owner.Dir.x, 0, e.owner.Dir.z);
            float angle = Vector3.Angle(currDir, dirSetTemp);
            float crossY = Vector3.Cross(currDir, dirSetTemp).y;
            
            if (angle < turnStopAngle)
            {
                e.owner.IsRotating = false;
                return;
            }

            e.owner.IsRotating = true;
            e.owner.Obj.transform.Rotate(Vector3.up, e.owner.Speed * Time.deltaTime * Mathf.Sign(crossY), Space.Self);
        }
    }
}

using GameBase.EntitySystem;
using UnityEngine;

namespace GameBase.Move
{
    public class RotateSys : SimplestEntitySys<Rotater, SimpleEntityContainer, RotateSys>
    {
        public static float turnStopAngle = 1f;

        protected override void UpdateEntity(Rotater e)
        {
            var currDir = e.owner.GO.transform.forward;
            Vector3 dirSetTemp = new Vector3(e.owner.Dir.x, 0, e.owner.Dir.z);
            float angle = Vector3.Angle(currDir, dirSetTemp);
            float crossY = Vector3.Cross(currDir, dirSetTemp).y;
            
            if (angle < turnStopAngle)
            {
                e.owner.IsRotating = false;
                return;
            }

            e.owner.IsRotating = true;
            e.owner.GO.transform.Rotate(Vector3.up, e.owner.Speed * Time.deltaTime * Mathf.Sign(crossY), Space.Self);
        }
    }
}

using GameBase.EntitySystem;
using GameBase.Math;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Move
{
    public class MoveSys : SealedEntitySys<Mover, MoveSys>
    {
        public static float turnStopAngle = 1f;
        public GridAStar aStar;
        public MoveSys()
        {
            aStar = new GridAStar(CollideSys.Instance.grid, new AStar()
            {
                defaultMapWeight = CollideSys.Instance.grid.weightFactor
            });
        }

        private void MoveUpdate(Mover e)
        {
            if (!e.isMoving)
            {
                return;
            }

            Vector3 dir = e.dest - e.owner.Position;
            Vector3 delta = dir.normalized * e.owner.MoveSpeed * Time.deltaTime;
            Vector3 dest = e.owner.Position + delta;
            e.LookAt(dest);

            var collider = e.owner.Collider;

            if (collider != null)
            {
                if (collider.isCollide)
                {
                    dest += new Vector3(collider.force.x, 0, collider.force.y);
                }
            }

            if ((e.dest - e.owner.Position).magnitude <= delta.magnitude)
            {
                if (e.destQueue.Count > 0)
                {
                    e.dest = e.destQueue.Dequeue();
                    e.isMoving = true;
                    e.isArrive = false;
                    e.owner.Position = dest;
                }
                else
                {
                    e.isMoving = false;
                    e.isArrive = true;
                    e.owner.Position = e.dest;
                }
            }
            else
            {
                e.isArrive = false;
                e.isMoving = true;
                e.owner.Position = dest;
            }
        }

        private void RotateUpdate(Mover e)
        {
            var currDir = e.owner.Obj.transform.forward;
            Vector3 dirSetTemp = new Vector3(e.owner.Dir.x, 0, e.owner.Dir.z);
            float angle = Vector3.Angle(currDir, dirSetTemp);
            float crossY = Vector3.Cross(currDir, dirSetTemp).y;

            if (angle < turnStopAngle)
            {
                e.isRotating = false;
                return;
            }

            e.isRotating = true;
            e.owner.Obj.transform.Rotate(Vector3.up, e.owner.RotateSpeed * Time.deltaTime * Mathf.Sign(crossY), Space.Self);
        }
        protected override void UpdateEntity(Mover e)
        {
            MoveUpdate(e);
            RotateUpdate(e);
        }
    }
}

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
            CollideSys.Instance.OnGridDirty = OnGridDirty;
        }

        private void OnGridDirty(Grid grid)
        {
            foreach (var e in Entities)
            {
                if (e.isMoving)
                {
                    e.MoveTo(e.finalDest);
                }
            }
        }

        protected override void EntityStart(Mover e)
        {
            e.dest = e.owner.Position;
        }

        private void MoveUpdate(Mover e)
        {
            if (e.destQueue.Count > 0)
            {
                e.dest = e.destQueue.Peek();
            }

            Vector3 delta = Vector3.zero;
            Vector3 dest = e.owner.Position;
            if (e.isMoving)
            {
                Vector3 dir = e.dest - e.owner.Position;
                delta = e.owner.MoveSpeed * Time.deltaTime * dir.normalized;
                dest = e.owner.Position + delta;
                e.LookAt(dest);
            }

            var collider = e.owner.Collider;

            if (collider != null)
            {
                if (collider.isCollide)
                {
                    dest += new Vector3(collider.force.x, 0, collider.force.y);
                    
                    if (!e.isMoving)
                    {
                        e.dest = dest;
                    }
                }
            }

            if ((e.dest - e.owner.Position).magnitude <= delta.magnitude)
            {
                if (e.destQueue.Count > 0)
                {
                    e.destQueue.Dequeue();
                    dest = e.dest;
                }

                if (e.destQueue.Count > 0)
                {
                    e.isMoving = true;
                    e.isArrive = false;
                }
                else
                {
                    e.isMoving = false;
                    e.isArrive = true;
                }
            }

            if (e.isMoving || collider.isCollide)
            {
                e.owner.Position = dest;
            }
        }

        private void RotateUpdate(Mover e)
        {
            var currDir = e.owner.Dir;
            Vector3 dirSetTemp = new Vector3(e.targetDir.x, 0, e.targetDir.z);
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

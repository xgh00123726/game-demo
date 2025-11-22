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
            e.dest = e.Owner.Position;
        }

        private void MoveUpdate(Mover e)
        {
            if (e.destQueue.Count > 0)
            {
                e.dest = e.destQueue.Peek();
            }

            Vector3 delta = Vector3.zero;
            Vector3 dest = e.Owner.Position;
            if (e.isMoving)
            {
                Vector3 dir = e.dest - e.Owner.Position;
                delta = e.Owner.MoveSpeed * Time.deltaTime * dir.normalized;
                dest = e.Owner.Position + delta;
                e.LookAt(dest);
            }

            var collider = e.Owner.Collider;

            if (collider != null)
            {
                if (collider.IsCollide)
                {
                    dest += new Vector3(collider.Force.x, 0, collider.Force.y);
                    
                    if (!e.isMoving)
                    {
                        e.dest = dest;
                    }
                }
            }

            if ((e.dest - e.Owner.Position).magnitude <= delta.magnitude)
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

            if (e.isMoving || collider.IsCollide)
            {
                e.Owner.Position = dest;
            }
        }

        private void RotateUpdate(Mover e)
        {
            var currDir = e.Owner.Dir;
            Vector3 dirSetTemp = new Vector3(e.TargetDir.x, 0, e.TargetDir.z);
            float angle = Vector3.Angle(currDir, dirSetTemp);
            float crossY = Vector3.Cross(currDir, dirSetTemp).y;

            if (angle < turnStopAngle)
            {
                e.isRotating = false;
                return;
            }

            e.isRotating = true;
            e.Owner.Obj.transform.Rotate(Vector3.up, e.Owner.RotateSpeed * Time.deltaTime * Mathf.Sign(crossY), Space.Self);
        }
        protected override void UpdateEntity(Mover e)
        {
            MoveUpdate(e);
            RotateUpdate(e);
        }
    }
}

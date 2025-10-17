using GameBase.EntitySystem;
using GameBase.Math;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Move
{
    public class MoveSys : SealedEntitySys<Mover, MoveSys>
    {
        protected override void UpdateEntity(Mover e)
        {
            if (!e.isMoving)
            {
                return;
            }

            Vector3 dir = e.dest - e.owner.Position;
            Vector3 delta = dir.normalized * e.owner.Speed * Time.deltaTime;
            Vector3 dest = e.owner.Position + delta;

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
                e.isMoving = false;
                e.isArrive = true;
                e.owner.Position = e.dest;
                return;
            }

            e.isArrive = false;
            e.isMoving = true;
            e.owner.Position = dest;
        }
    }
}

using GameBase.Tools;
using UnityEngine;

namespace GameBase.Move
{
    public class MoveSys : SimplestEntitySys<Mover, SimpleEntityContainer, MoveSys>
    {
        public static float moveStopDis = 0.1f;

        protected override void OnRegisterEntityToActives(Mover e)
        {
            
        }

        protected override void OnRemoveEntityFromActives(Mover e)
        {
            
        }

        protected override void UpdateEntity(Mover e)
        {
            if (!e.destCommand && !e.targetCommand) return;

            Vector3 dir = e.Dest - e.owner.GO.transform.position;
            Vector3 delta = dir.normalized * e.owner.moveSpeed * Time.deltaTime;
            Vector3 dest = e.owner.GO.transform.position + delta;

            if ((e.Dest - e.owner.GO.transform.position).magnitude < moveStopDis)
            {
                e.isMoveing = false;
                return;
            }

            if (e.rigidbody.Exist)
            {
                e.rigidbody.Get().MovePosition(dest);
            }
            else
            {
                e.owner.GO.transform.position = dest;
            }
        }
    }
}

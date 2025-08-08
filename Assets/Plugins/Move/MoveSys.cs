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

            Vector3 dir = e.Dest - e.body.transform.position;
            Vector3 delta = dir.normalized * e.speed * Time.deltaTime;
            Vector3 dest = e.body.transform.position + delta;

            if ((e.Dest - e.body.transform.position).magnitude < moveStopDis)
            {

                return;
            }

            if (e.rigidbody.Exist)
            {
                e.rigidbody.Get().MovePosition(dest);
            }
            else
            {
                e.body.transform.position = dest;
            }
        }
    }
}

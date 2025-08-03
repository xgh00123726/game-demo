using GameBase.Tools;
using UnityEngine;

namespace GameBase.Move
{
    public class MoveSys : SimplestEntitySys<Mover, EntityList<Mover>, MoveSys>
    {
        public static float moveStopDis = 0.1f;

        protected override void OnRegisterEntity(Mover e)
        {
            
        }

        protected override void OnRemoveEntity(Mover e)
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

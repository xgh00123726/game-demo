using GameBase.Tools;
using UnityEngine;

namespace GameBase.Move
{
    public class MoveSys : SimplestEntitySys<Mover, SimpleEntityContainer, MoveSys>
    {
        public static float moveStopDis = 0.1f;

        protected override void OnRegisterEntityToActives(Mover e)
        {
            e.owner.Dest = e.owner.GO.transform.position;
        }

        protected override void OnRemoveEntityFromActives(Mover e)
        {
            
        }

        protected override void UpdateEntity(Mover e)
        {
            Vector3 dir = e.owner.Dest - e.owner.GO.transform.position;
            Vector3 delta = dir.normalized * e.owner.Speed * Time.deltaTime;
            Vector3 dest = e.owner.GO.transform.position + delta;

            if ((e.owner.Dest - e.owner.GO.transform.position).magnitude < moveStopDis)
            {
                e.owner.IsMoving = false;
                return;
            }

            e.owner.IsMoving = true;
            e.owner.GO.transform.position = dest;
        }
    }
}

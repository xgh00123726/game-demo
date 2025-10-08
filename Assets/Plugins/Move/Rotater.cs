using UnityEngine;

namespace GameBase.Move
{
    public class Rotater
    {
        public IRotater owner;

        public void LookAt(Vector3 position)
        {
            owner.Dir = position - owner.Obj.transform.position;
        }
    }
}

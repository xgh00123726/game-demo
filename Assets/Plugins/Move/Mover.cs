using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Move
{
    public class Mover
    {
        internal Queue<Vector3> destQueue = new();
        internal bool isMoving;
        internal bool isRotating;
        internal Vector3 dest;
        internal bool isArrive;

        public IMover owner;

        public Queue<Vector3> DestQueue => destQueue;

        public bool IsMoving => isMoving;
        public bool IsRotating => isRotating;
        public Vector3 Dest => dest;

        public bool IsArrive => isArrive;
        public IMover Owner
        {
            get => owner;
            set
            {
                owner = value;
                dest = owner.Position;
            }
        }

        public void MoveTo(Vector3 position)
        {
            isMoving = true;
            destQueue.Clear();
            var aStar = MoveSys.Instance.aStar;
            var positions = aStar.GetWay(owner.Position, position);
            //aStar.LogWay();
            dest = positions[0];
            for (int i = 0; i < positions.Count; i++)
            {
                ThenMoveTo(positions[i]);
            }
        }

        public void ThenMoveTo(Vector3 position)
        {
            isMoving = true;
            destQueue.Enqueue(position);
        }

        public void LookAt(Vector3 position)
        {
            owner.Dir = position - owner.Obj.transform.position;
        }

        public void Stop()
        {
            isMoving = false;
            destQueue.Clear();
        }
    }
}

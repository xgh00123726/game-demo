using GameBase.Tools;
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
        internal Vector3 finalDest;

        public IMover Owner { get; set; }
        public Vector3 TargetDir { get; set; }
        public Queue<Vector3> DestQueue => destQueue;
        public bool IsMoving => isMoving;
        public bool IsRotating => isRotating;
        public Vector3 Dest => dest;
        public bool IsArrive => isArrive;

        public void MoveTo(Vector3 position)
        {
            finalDest = position;
            isMoving = true;
            destQueue.Clear();
            var aStar = MoveSys.Instance.aStar;
            var positions = aStar.GetWay(Owner.Position, position);
            for (int i = 0; i < positions.Count - 1; i++)
            {
                ThenMoveTo(positions[i]);
            }
            ThenMoveTo(finalDest);
        }

        public void ThenMoveTo(Vector3 position)
        {
            isMoving = true;
            destQueue.Enqueue(position);
        }

        public void LookAt(Vector3 position)
        {
            TargetDir = position - Owner.Obj.transform.position;
        }

        public void Interrupt()
        {
            isMoving = false;
            dest = Owner.Position;
            finalDest = Owner.Position;
            destQueue.Clear();
        }
    }
}

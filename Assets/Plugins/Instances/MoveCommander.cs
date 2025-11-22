using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.GCamera;
using GameBase.Move;
using GameBase.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Instance
{
    public class MoveCommander : SingletonInstance<MoveCommander>
    {
        private static List<Creature> _targets = new();
        private static List<Vector3> _disfusedDest = new();
        private static List<Coord> _disfusedCoords = new();

        public static Action OnMoveInput { get; set; }

        private static void GetDirs(int num, ref List<Coord> coords)
        {
            coords.Clear();
            if (num <= 0)
            {
                return;
            }
            coords.Add(new Coord(0, 0));
            int currLayer = 1;
            int currLayerPoint = 1;
            int iterPoint = 1;
            for (int i = 1; i < num; i++) 
            {
                if (iterPoint == currLayerPoint)
                {
                    currLayer += 2;
                    currLayerPoint = (currLayer - 1) * 4;
                    iterPoint = 0;
                }
                if (iterPoint < currLayerPoint)
                {
                    if (iterPoint == 0)
                    {
                        coords.Add(coords[i - 1] + new Coord(1, 0));
                    }
                    else if (iterPoint < (currLayer - 1))
                    {
                        coords.Add(coords[i - 1] + new Coord(0, -1));
                    }
                    else if (iterPoint < (currLayer - 1) * 2)
                    {
                        coords.Add(coords[i - 1] + new Coord(-1, 0));
                    }
                    else if (iterPoint < (currLayer - 1) * 3)
                    {
                        coords.Add(coords[i - 1] + new Coord(0, 1));
                    }
                    else
                    {
                        coords.Add(coords[i - 1] + new Coord(1, 0));
                    }
                    iterPoint++;
                }
            }
        }

        private static void Disfuse(Vector3 target, List<Creature> collideCreature, ref List<Vector3> output)
        {
            output.Clear();

            if (collideCreature.Count <= 0)
            {
                return;
            }

            GetDirs(collideCreature.Count, ref _disfusedCoords);

            for (int i = 0; i < collideCreature.Count; i++)
            {
                Coord c = _disfusedCoords[i];
                output.Add(target + new Vector3(c.x, 0, c.y) * collideCreature[i].Collider.r * 3);
            }
        }

        protected override void Update()
        {
            if (Inputs.GetKeyDown(KeyFunction.MoveTo, "mover"))
            {
                bool isImmediately = !Inputs.GetKey(KeyFunction.ShiftMoveMode, "mover");

                Disfuse(CameraSys.MouseHitPosition, _targets, ref _disfusedDest);
                for (int i = 0; i < _targets.Count; i++) 
                {
                    var c = _targets[i];
                    var dest = _disfusedDest[i];
                    if (isImmediately)
                    {
                        c.Mover.MoveTo(dest);
                    }
                    else
                    {
                        c.Mover.ThenMoveTo(dest);
                    }
                    c.AI?.Disable();
                }

                MoveIndicator.Show(CameraSys.MouseHitPosition);

                OnMoveInput?.Invoke();
            }

            if (Inputs.GetKeyDown(KeyFunction.Stop, "mover"))
            {
                foreach (var c in _targets)
                {
                    c.Interrupt();
                    c.AI?.Disable();
                }
            }
        }

        public static void SetTarget(Creature target)
        {
            _targets.Clear();
            _targets.Add(target);
        }

        public static void ClearTarget()
        {
            _targets.Clear();
        }

        public static void AddTarget(Creature target)
        {
            _targets.Add(target);
        }
    }
}

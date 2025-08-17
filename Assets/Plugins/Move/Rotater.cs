using GameBase.Tools;
using UnityEngine;

namespace GameBase.Move
{
    public class Rotater : IEntity
    {
        public IRotater owner;

        public int InstanceID { get; set; }
    }
}

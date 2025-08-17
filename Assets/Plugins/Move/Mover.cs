using GameBase.Tools;
using UnityEngine;

namespace GameBase.Move
{
    public class Mover : IEntity
    {
        public IMover owner;
        public int InstanceID { get; set; }
    }
}

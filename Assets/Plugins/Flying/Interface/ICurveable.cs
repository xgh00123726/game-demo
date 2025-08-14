using UnityEngine;

namespace GameBase.Flyings
{
    public interface ICurveable
    {
        public Vector3 Position { get; set; }
        public Vector3 Dir {  get; set; }
        public Vector3 Dest { get; }
        public Vector3 Src { get; }
        public float LifeTime { get; }
    }
}

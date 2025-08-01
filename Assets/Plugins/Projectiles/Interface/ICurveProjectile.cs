using UnityEngine;

namespace GameBase.Projectile
{
    public interface ICurveProjectile
    {
        public Vector3 Position { get; set; }
        public Vector3 Dir {  get; set; }
        public Vector3 Dest { get; }
        public Vector3 Src { get; }
        public float LifeTime { get; }
    }
}

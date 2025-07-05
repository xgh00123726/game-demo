using UnityEngine;

namespace GameBase.Projectile
{
    public interface ICurvableProjectile
    {
        Transform Transform { get; }
        Vector3 Target { get; } // 曲线目标
        Vector3 Start { get; }  // 曲线初始位置
        Vector3 Dir { set; get; }
        float LifeTime { get; } // 曲线当前持续时间
    }
}

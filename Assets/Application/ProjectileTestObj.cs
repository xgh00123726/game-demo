using UnityEngine;
using GameBase.Projectile;
using GameBase.UI;


public class ProjectileTestObj : MonoBehaviour,
    IProjectileTarget
{
    Vector3 IProjectileTarget.Center => transform.position;

    float IProjectileTarget.Radius => 1;

    void IProjectileTarget.GetDamage(float damage)
    {
        var e = TextSys.Instance.NewEntity<FloatText>();
        e.bodyID = 1;
        e.value = damage.ToString();
        e.showPosition = transform.position;
    }
}

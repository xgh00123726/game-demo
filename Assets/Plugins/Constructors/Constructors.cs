using Constructor.Buffs;
using Constructor.Flyings;
using Constructor.Projectiles;
using UnityEngine;
namespace Constructor
{
    public class Constructors : MonoBehaviour
    {
        private void Awake()
        {
            ProjectileRegister.RegisterProjectileGenerator();
            BuffRegister.RegisterBuffGenerator();
            FlyingRegister.RegisterGenerator();
        }
    }
}

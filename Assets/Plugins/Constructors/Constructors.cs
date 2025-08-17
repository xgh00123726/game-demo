using Constructor.Buffs;
using Constructor.Flyings;
using Constructor.Projectiles;
using GameBase.Spells;
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
            SpellActionRegister.RegisterGenerator();
            CreatureRegister.RegisterGenerator();
        }
    }
}

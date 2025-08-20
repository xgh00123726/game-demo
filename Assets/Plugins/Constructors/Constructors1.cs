using Constructor.Buffs;
using Constructor.Projectiles;
using GameBase.Projectiles;
using GameBase.Spells;
using UnityEngine;
namespace Constructor
{
    public class Constructors1 : MonoBehaviour
    {
        private void Awake()
        {
            BuffRegister.RegisterBuffGenerator();
            CreatureRegister.RegisterGenerator();
            TargetSetFactorary.RegisterTargetSet("Common", CommonTargetSet.Instance);
        }
    }
}

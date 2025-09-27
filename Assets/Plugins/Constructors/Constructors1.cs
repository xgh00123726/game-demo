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
            TargetSetFactorary.RegisterTargetSet("Common", CommonTargetSet.Instance);
        }
    }
}

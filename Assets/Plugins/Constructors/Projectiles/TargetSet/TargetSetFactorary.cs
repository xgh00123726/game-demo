using GameBase.Projectiles;
using GameBase.Tools;
using System.Collections.Generic;

namespace Constructor.Projectiles
{
    public enum TargetSetType
    {
        Common,
    }
    public class TargetSetFactorary
    {
        public static IProjectileTargetsSet Get(TargetSetType type)
        {
            if (type == TargetSetType.Common)
            {
                return CommonTargetSet.Instance;
            }

            return null;
        }
    }
}

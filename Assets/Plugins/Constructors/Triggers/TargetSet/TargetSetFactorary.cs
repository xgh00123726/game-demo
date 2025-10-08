using GameBase.Triggers;
using GameBase.Tools;
using System.Collections.Generic;

namespace Constructor.Triggers
{
    public enum TargetSetType
    {
        Common,
        TargetPlayer,
    }
    public class TargetSetFactorary
    {
        public static ITriggerTargetsSet Get(TargetSetType type)
        {
            if (type == TargetSetType.Common)
            {
                return CommonTargetSet.Instance;
            }
            if (type == TargetSetType.TargetPlayer)
            {
                return TargetPlayer.Instance;
            }

            return null;
        }
    }
}

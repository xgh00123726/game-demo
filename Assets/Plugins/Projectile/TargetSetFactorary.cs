using GameBase.Tools;
using System.Collections.Generic;

namespace GameBase.Projectiles
{
    public class TargetSetFactorary
    {
        private static Dictionary<string, IProjectileTargetsSet> _targetSets = new();
        public static void RegisterTargetSet(string name, IProjectileTargetsSet targetSet)
        {
            if (_targetSets.ContainsKey(name))
            {
                XLogger.Instance.Log($"duplicate targetSet:{name}");
            }
            else
            {
                _targetSets.Add(name, targetSet);
            }
        }

        public static IProjectileTargetsSet GetTargetSet(string name)
        {
            if (!_targetSets.ContainsKey(name))
            {
                XLogger.Instance.Log($"no registered target set:{name}");
                return null;
            }
            return _targetSets[name];
        }
    }
}

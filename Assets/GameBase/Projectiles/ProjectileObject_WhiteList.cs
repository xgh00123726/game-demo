using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Projectile
{
    public partial class ProjectileObject
    {
        protected LinkedList<IProjectileTarget> _whites = new LinkedList<IProjectileTarget>();

        public void AddWhite(IProjectileTarget target)
        {
            _whites.AddLast(target);
        }
    }
}

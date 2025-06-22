using System.Collections;
using System.Collections.Generic;
using GameBase.Object;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Projectile
{
    public class ProjectileMgr
    {
        private static ProjectileMgr _instance;
        public static ProjectileMgr Instance
        {
            get
            {
                _instance ??= new ProjectileMgr();
                return _instance;
            }
        }

        public T CreateProjectile<T>() where T : GProjectile
        {
            return PrefabMgr.Instance.GetFromPool<T>(PrefabType.Projectile);
        }
    }
}

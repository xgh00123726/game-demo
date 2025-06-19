using System.Collections;
using System.Collections.Generic;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.XCard
{
    public partial class XCardBase : PoolablePrefab
    {
        public void AnimateLoss()
        {
            Timer.DO(1f, (float time) =>
            {
                bodyMaterial.SetFloat("_Loss", time);
            });
        }
    }
}

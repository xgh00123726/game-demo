using System.Collections;
using System.Collections.Generic;
using TMPro;
using GameBase.GCamera;
using UnityEngine;
using UnityEngine.Assertions;
using GameBase.Resources;


namespace GameBase.XCard
{
    public partial class XCardBase : PoolablePrefab
    {
        protected void Awake()
        {
            CommonInit();
            MaterialInit();

            Suit = (SuitType)Random.Range(0, 4);
        }

        void Update()
        {
            if (_isFloat)
            {
                transform.position = CameraBase.Main.MouseWorldPos();
            }
        }
    }
}
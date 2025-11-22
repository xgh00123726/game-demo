using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Math;
using GameBase.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Instance
{
    public class ShopInteractive : SingletonInstance<ShopInteractive>
    {
        private static List<Shop> _shops = new();
        private static Creature _target = null;
        private static Shop _nearestShop = null;

        public static float ShopInteractiveDis { get; set; } = 100f;
        public static Action<Shop> OnTargetNearShop {  get; set; }
        public static Action<Shop> OnTargetFarFromShop {  get; set; }
        public static Action<Shop> OnShopActive { get; set; }
        public static Action<Shop> OnShopInActive { get; set; }
        public static Func<bool> ShopActiveCmd {  get; set; }
        public static Func<bool> ShopInActiveCmd { get; set; }

        public ShopInteractive()
        {
            ShopActiveCmd += DefaultShopActiveCmd;
            ShopInActiveCmd += DefaultShopInActiveCmd;
        }

        private static bool DefaultShopActiveCmd()
        {
            return Inputs.GetKeyDown(KeyFunction.OpenShop, "shopping");
        }

        private static bool DefaultShopInActiveCmd()
        {
            return Inputs.GetKeyDown(KeyFunction.CloseShop, "shopping") || Inputs.GetKeyDown(KeyFunction.Cancel, "shopping");
        }

        protected override void Update()
        {
            if (_target == null)
            {
                return;
            }

            float minDis = ShopInteractiveDis;
            _nearestShop = null;
            foreach (var shop in _shops)
            {
                var shopPos = shop.Obj.transform.position;

                var dis = GMath.GameDistance(_target.Position, shopPos);

                if (dis > shop.DetectRange)
                {
                    if (shop.isTargetNear)
                    {
                        OnTargetFarFromShop?.Invoke(shop);
                    }
                    shop.isTargetNear = false;
                    shop.isActive = false;
                }
                else
                {
                    if (!shop.isTargetNear)
                    {
                        OnTargetNearShop?.Invoke(shop);
                    }
                    shop.isTargetNear = true;

                    if (dis < minDis)
                    {
                        minDis = dis;
                        _nearestShop = shop;
                    }
                }
            }

            if (_nearestShop != null)
            {
                if (!_nearestShop.isActive && ShopActiveCmd?.Invoke() == true)
                {
                    OnShopActive?.Invoke(_nearestShop);
                    _nearestShop.isActive = true;
                }
                else if (_nearestShop.isActive && ShopInActiveCmd?.Invoke() == true)
                {
                    OnShopInActive?.Invoke(_nearestShop);
                    _nearestShop.isActive = false;
                }
            }
        }

        public static Shop NearestShop(Vector3 postion, float rangeLimit = 2)
        {
            return _nearestShop;
        }

        public static void RegisterShop(Shop shop)
        {
            _shops.Add(shop);
        }

        public static void SetTarget(Creature target)
        {
            _target = target;
        }

        public static Shop GetCurrentShop()
        {
            return _nearestShop;
        }
    }
}

using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.GCamera;
using GameBase.Resources;
using GameBase.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Instance
{
    public class CreatureSelector : SingletonInstance<CreatureSelector>
    {
        private static Dictionary<KeyFunction, float> _lastTrigTime = new();
        private static Dictionary<KeyFunction, Creature> _hotKeyTargets = new();

        private static float _lastPointerSelectTime;

        public static float doubleTrigInterval = 0.3f;
        public static float doublePointerSelectInterval = 0.3f;
        public static float pointerSelectRange = 0.5f;

        public static bool interactiveToCreatureRadiusDrawer = true;

        public static Action<Creature> OnSelected;
        public static Action OnFirstSelected;

        public CreatureSelector()
        {
            OnSelected = DefaultOnSelected;
            OnFirstSelected = DefaultOnFirstSelected;
            RectDrawer.OnDraw += OnDrawRect;
        }

        public static void AddHotKeyCreature(KeyFunction keyFunction, Creature c)
        {
            _hotKeyTargets[keyFunction] = c;
            _lastTrigTime[keyFunction] = 0;
        }

        private static void OnDrawRect(Rect rect)
        {
            var minX = rect.xMin;
            var maxX = rect.xMax;
            var minZ = rect.yMin;
            var maxZ = rect.yMax;
            int selectNum = 0;
            foreach (var c in CreatureSys.Instance.Entities)
            {
                float x = c.Position.x;
                float z = c.Position.z;
                if (x > minX && x < maxX && z > minZ && z < maxZ)
                {
                    if (selectNum++ == 0)
                    {
                        OnFirstSelectCreature();
                    }
                    OnSelectCreature(c);
                }
            }
        }

        private static void DefaultOnFirstSelected()
        {
            MoveCommander.ClearTarget();
        }

        private static void DefaultOnSelected(Creature c)
        {
            SpellCaster.SetTarget(c);
            MoveCommander.AddTarget(c);
            SpellUIInteractive.SetTarget(c);
            BuffUIInteractive.SetTarget(c);
            AttrUIChanger.SetTarget(c);
            EpicBarController.SetPlayerBarTarget(c);
            EquipmentUIInteractive.SetTarget(c);
        }

        private static void OnFirstSelectCreature()
        {
            OnFirstSelected?.Invoke();
            var keys = DrawCreatureRadius.colorSet.Keys.ToList();
            foreach (var c in keys)
            {
                DrawCreatureRadius.colorSet[c] = Color.white;
            }
        }

        private static void OnSelectCreature(Creature c)
        {
            OnSelected?.Invoke(c);
            if (interactiveToCreatureRadiusDrawer)
            {
                DrawCreatureRadius.colorSet[c] = Color.green;
            }
        }

        protected override void Update()
        {
            if (Inputs.GetKeyDown(KeyFunction.PointerSelect, "creatureSelector"))
            {
                if (Time.time < _lastPointerSelectTime + doublePointerSelectInterval)
                {
                    OnFirstSelectCreature();
                    var c = CreatureSys.Instance.NearestEntity(CameraSys.MouseHitPosition, CreatureTag.ALL, pointerSelectRange);
                    if (c != null)
                    {
                        OnSelectCreature(c);
                    }
                    return;
                }
                _lastPointerSelectTime = Time.time;
            }

            int selectNum = 0;

            foreach (var kvp in _hotKeyTargets)
            {
                if (Inputs.GetKeyDown(kvp.Key, "creatureSelector"))
                {
                    if (Time.time < _lastTrigTime[kvp.Key] + doubleTrigInterval)
                    {
                        CameraSys.Main.LookAt(kvp.Value.Position);
                    }

                    _lastTrigTime[kvp.Key] = Time.time; 
                    if (selectNum++ == 0)
                    {
                        OnFirstSelectCreature();
                    }
                    OnSelectCreature(kvp.Value);
                }
            }
        }
    }
}

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
        private Dictionary<KeyFunction, float> _lastTrigTime = new();
        private Dictionary<KeyFunction, Creature> _hotKeyTargets = new();

        private float _lastPointerSelectTime;

        public float doubleTrigInterval = 0.3f;
        public float doublePointerSelectInterval = 0.3f;
        public float pointerSelectRange = 0.5f;

        public bool interactiveToCreatureRadiusDrawer = true;

        public Action<Creature> OnSelected;
        public Action OnFirstSelected;
        public Creature currentSelect;

        public CreatureSelector()
        {
            OnSelected = DefaultOnSelected;
            OnFirstSelected = DefaultOnFirstSelected;
            RectDrawer.OnDraw += OnDrawRect;
        }

        public void AddHotKeyCreature(KeyFunction keyFunction, Creature c)
        {
            _hotKeyTargets[keyFunction] = c;
            _lastTrigTime[keyFunction] = 0;
        }

        private void OnDrawRect(Rect rect)
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
            SpellUIInteractive.Instance.Target = c;
            BuffUIInteractive.Instance.Target = c;
            AttrUIChanger.SetTarget(c);
            EpicBarController.SetPlayerBarTarget(c);
            EquipmentUIInteractive.Instance.Target = c;
            Instance.currentSelect = c;
        }

        private void OnFirstSelectCreature()
        {
            OnFirstSelected?.Invoke();
            var keys = DrawCreatureRadius.colorSet.Keys.ToList();
            foreach (var c in keys)
            {
                DrawCreatureRadius.colorSet[c] = Color.white;
            }
        }

        private void OnSelectCreature(Creature c)
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
                    var c = CreatureSys.Instance.NearestEntity(CameraSys.MouseHitPosition, CreatureTag.ALL, pointerSelectRange);
                    if (c != null)
                    {
                        OnFirstSelectCreature();
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

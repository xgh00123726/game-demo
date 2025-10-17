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
        private static LineRenderer _lineRenderer;
        private static GameObject _lineRendererObj;
        private static Dictionary<KeyFunction, float> _lastTrigTime = new();
        private static Dictionary<KeyFunction, Creature> _hotKeyTargets = new();

        private static float _selectBeginTime;
        private static Vector3 _selectBeginPos;
        private static bool _selectEnable;

        private static float _lastPointerSelectTime;

        public static float drawY = -7;
        public static float trigTime = 0.1f;
        public static float doubleTrigInterval = 0.3f;
        public static float doublePointerSelectInterval = 0.3f;
        public static float pointerSelectRange = 0.5f;
        public static int lineRendererObjID = 48;
        public static bool interactiveToCreatureRadiusDrawer = true;

        public static Action<Creature> OnSelected;
        public static Action OnFirstSelected;

        public CreatureSelector()
        {
            _lineRendererObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(lineRendererObjID));
            _lineRenderer = _lineRendererObj.transform.Find("Line").GetComponent<LineRenderer>();
            OnSelected = DefaultOnSelected;
            OnFirstSelected = DefaultOnFirstSelected;
        }

        public static void AddHotKeyCreature(KeyFunction keyFunction, Creature c)
        {
            _hotKeyTargets[keyFunction] = c;
            _lastTrigTime[keyFunction] = 0;
        }

        private static void DefaultOnFirstSelected()
        {
            PlayerMoveController.ClearTarget();
        }

        private static void DefaultOnSelected(Creature c)
        {
            PlayerSpellCaster.SetTarget(c);
            PlayerMoveController.AddTarget(c);
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
            if (Inputs.GetKeyDown(KeyFunction.SelectTrig, "creatureSelector"))
            {
                _selectBeginTime = Time.time;
                _selectBeginPos = CameraSys.MouseHitPosition;
                _selectEnable = true;
            }

            if (!Inputs.GetKey(KeyFunction.SelectTrig, "creatureSelector"))
            {
                _selectEnable = false;
            }

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
            

            if (_selectEnable && Time.time > _selectBeginTime + trigTime)
            {
                var pos = CameraSys.MouseHitPosition;
                float beginX = _selectBeginPos.x;
                float beginZ = _selectBeginPos.z;
                float endX = pos.x;
                float endZ = pos.z;

                _lineRendererObj.SetActive(true);
                _lineRenderer.DrawRect(Rect.MinMaxRect(beginX, beginZ, endX, endZ), drawY);

                float minX = Mathf.Min(beginX, endX);
                float maxX = Mathf.Max(beginX, endX);
                float minZ = Mathf.Min(beginZ, endZ);
                float maxZ = Mathf.Max(beginZ, endZ);

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
            else
            {
                _lineRendererObj.SetActive(false);
            }
        }
    }
}

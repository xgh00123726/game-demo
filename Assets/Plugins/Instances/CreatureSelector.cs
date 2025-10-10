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

        public static float drawY = -7;
        public static float trigTime = 0.1f;
        public static float doubleTrigInterval = 0.3f;
        public static int lineRendererObjID = 48;
        public static bool interactiveToCreatureRadiusDrawer = true;

        public static Action<Creature> OnSelected;

        public CreatureSelector()
        {
            _lineRendererObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(lineRendererObjID));
            _lineRenderer = _lineRendererObj.transform.Find("Line").GetComponent<LineRenderer>();
            OnSelected = DefaultOnSelected;
        }

        public static void AddHotKeyCreature(KeyFunction keyFunction, Creature c)
        {
            _hotKeyTargets[keyFunction] = c;
            _lastTrigTime[keyFunction] = 0;
        }

        private static void DefaultOnSelected(Creature c)
        {
            PlayerSpellCaster.SetTarget(c);
            PlayerMoveController.SetTarget(c);
            SpellUIInteractive.SetTarget(c);
            BuffUIInteractive.SetTarget(c);
            AttrUIChanger.SetTarget(c);
            EpicBarController.SetPlayerBarTarget(c);
            EquipmentUIInteractive.SetTarget(c);
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

            foreach (var kvp in _hotKeyTargets)
            {
                if (Inputs.GetKeyDown(kvp.Key, "creatureSelector"))
                {
                    if (Time.time < _lastTrigTime[kvp.Key] + doubleTrigInterval)
                    {
                        CameraSys.Main.LookAt(kvp.Value.Position);
                    }

                    _lastTrigTime[kvp.Key] = Time.time;
                    OnSelected?.Invoke(kvp.Value);
                    if (interactiveToCreatureRadiusDrawer)
                    {
                        var keys = DrawCreatureRadius.colorSet.Keys.ToList();
                        foreach (var c in keys)
                        {
                            DrawCreatureRadius.colorSet[c] = Color.white;
                        }
                        DrawCreatureRadius.colorSet[kvp.Value] = Color.green;
                    }
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

                _lineRenderer.SetPosition(0, new Vector3(beginX, drawY, beginZ));
                _lineRenderer.SetPosition(1, new Vector3(beginX, drawY, endZ  ));
                _lineRenderer.SetPosition(2, new Vector3(endX  , drawY, endZ  ));
                _lineRenderer.SetPosition(3, new Vector3(endX  , drawY, beginZ ));

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
                        OnSelected?.Invoke(c);
                        if (interactiveToCreatureRadiusDrawer)
                        {
                            DrawCreatureRadius.colorSet[c] = Color.green;
                        }
                    }
                    else
                    {
                        if (interactiveToCreatureRadiusDrawer)
                        {
                            DrawCreatureRadius.colorSet[c] = Color.white;
                        }
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

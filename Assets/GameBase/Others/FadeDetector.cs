using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameBase.Math;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
using UnityEngine.Rendering;
using static PlasticGui.WorkspaceWindow.Merge.MergeInProgress;

namespace GameBase.Others
{
    public class FadeDetector : MonoBehaviour
    {
        public static Material fadeMaterial;
        public BoxCollider fadeCollider;
        public List<MeshRenderer> meshs;
        public bool _lastTrig = false;
        public bool _trig = false;

        public List<GameObject> triggerGos;
        public Rect rect;
        public Vector2 x;
        public Vector2 y;
        // Start is called before the first frame update
        protected void Start()
        {
            fadeCollider = GetComponent<BoxCollider>();
            if (fadeCollider == null)
            {
                Debug.LogError("fade detector must has a box collider");
            }
            if (Physics.queriesHitTriggers)
            {
                Debug.LogWarning("fade detector is a trigger, physics ray should not hit a trigger");
                Physics.queriesHitTriggers = false;
            }

            if (fadeMaterial == null)
            {
                fadeMaterial = ResourceMgr.InstaniateMaterial("FadeMaterial");
            }

            if (transform.parent == null)
            {
                Debug.LogError("fade detector must be attached to a gameobject");
            }
            meshs = transform.parent.GetComponentsInChildren<MeshRenderer>().ToList();
            foreach (var mesh in meshs)
            {
                MaterialsModify.SetMaterialRenderingMode(mesh.material, MaterialsModify.RenderingMode.Fade);
            }
        }

        // Update is called once per frame
        protected void Update()
        {
            rect = GMath.BoundsRect(fadeCollider.bounds);
            x = new Vector2(rect.xMin, rect.xMax);
            y = new Vector2(rect.yMin, rect.yMax);

            _trig = false;
            foreach (var trigGo in triggerGos)
            {
                if (GMath.BoundsContains(fadeCollider.bounds, trigGo.transform.position))
                {
                    _trig = true;
                }
            }


            if (!_lastTrig && _trig)
            {
                foreach (var mesh in meshs)
                {
                    mesh.material.color = new Color(1, 1, 1, 0.1f);
                }
            }
            else if (_lastTrig && !_trig)
            {
                foreach (var mesh in meshs)
                {
                    mesh.material.color = Color.white;
                }
            }

            _lastTrig = _trig;
        }
    }
}


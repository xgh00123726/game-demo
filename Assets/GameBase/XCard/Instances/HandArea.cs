using System.Collections;
using System.Collections.Generic;
using GameBase.GCamera;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameBase.XCard
{
    public class HandArea : MonoBehaviour
    {
        public static bool IsMouseOn => _bounds.Contains(CameraBase.Main.MouseWorldPos());
        private static Rect _bounds;
        public Rect bounds;
        public bool isMouseOn;

        private void Awake()
        {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            Assert.IsNotNull(collider);

            Vector3 bmax = collider.bounds.max;
            Vector3 bmin = collider.bounds.min;
            _bounds = Rect.MinMaxRect(bmin.x, bmin.y, bmax.x, bmax.y);
        }

        private void OnEnable()
        {
            Vector3 bmax = GetComponent<BoxCollider2D>().bounds.max;
            Vector3 bmin = GetComponent<BoxCollider2D>().bounds.min;
            _bounds = Rect.MinMaxRect(bmin.x, bmin.y, bmax.x, bmax.y);
        }

        private void Update()
        {
            bounds = _bounds;
            isMouseOn = IsMouseOn;
        }
    }
}

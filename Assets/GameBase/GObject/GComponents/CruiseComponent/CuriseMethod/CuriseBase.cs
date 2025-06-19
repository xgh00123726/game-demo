using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Object
{

    public abstract class CuriseBase
    {
        public Vector3 start;
        public Vector3 end;
        protected Vector3 _dir;
        public float speed = 10f;
        public virtual void ReInit()
        {
            _dir = (end - start).normalized;
        }
        public abstract Vector3 TrackEquation(float time);
    }
}

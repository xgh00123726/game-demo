using GameBase.Flyings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTester : MonoBehaviour
{
    public bool enable = false;
    public GameObject srcObj;
    public GameObject destObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (srcObj != null && destObj != null && enable)
        {
            Vector3 src = srcObj.transform.position;
            Vector3 dest = destObj.transform.position;
            var flying = FlyingSys.Instance.NewEntity();
            flying.Src = src;
            flying.dest = dest;
            flying.ObjID = 22;
            enable = false;
        }
    }
}

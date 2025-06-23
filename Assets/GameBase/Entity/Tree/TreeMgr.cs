using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMgr : MonoBehaviour
{
    static GameObject CreateTree(string name = "Tree")
    {
        GameObject treeObj = new GameObject(name);
        treeObj.AddComponent<BaseTree>();
        return treeObj;
    }

    private void Awake()
    {
        for (int i = 0; i < 20; ++i)
        {
            CreateTree().transform.position = new Vector3(Random.value * 100 - 50, 1 + Random.value, Random.value * 100 - 50);
        }
    }
}

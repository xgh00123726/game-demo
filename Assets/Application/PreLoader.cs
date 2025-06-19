using System.Collections;
using System.Collections.Generic;
using GameBase.XCard;
using UnityEngine;

public class PreLoader : MonoBehaviour
{
    void Awake()
    {
        new Settings();
        new XCKeyWord();
        new XCContent();
    }
}

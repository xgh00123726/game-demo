using System.Collections;
using System.Collections.Generic;
using GameBase.Editable;
using UnityEditor;
using UnityEngine;

public partial class Generator
{
    public static EditableCurve selectCurve;
    [MenuItem("Tools/" + ToolBarTitle + "/Road", true)]
    public static bool ValidateGenerateRoad()
    {
        var selects = Selection.gameObjects;
        if (selects.Length == 0) return false;

        select = selects[0];
        selectCurve = select.GetComponent<EditableCurve>();
        return selectCurve != null;
    }

    [MenuItem("Tools/" + ToolBarTitle + "/Road", false)]
    public static void GenerateRoad()
    {
        RoadMgr.CreateRoad(selectCurve.beginPos, selectCurve.endPos, 1f, selectCurve.transform);
    }

    [MenuItem("Tools/" + ToolBarTitle + "/Roads", true)]
    public static bool ValidateGenerateRoads() => ValidateGenerateRoad();

    [MenuItem("Tools/" + ToolBarTitle + "/Roads", false)]
    public static void GenerateRoads()
    {
        RoadMgr.CreateRoads(10, selectCurve.beginPos, selectCurve.endPos, 1f, selectCurve.transform);
    }
}

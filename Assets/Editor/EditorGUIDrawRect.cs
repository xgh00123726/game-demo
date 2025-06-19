using GameBase.XCard;
using UnityEditor;
using UnityEngine;


// Uncomment the following line after replacing "MyScript" with your script name:
[CustomEditor(typeof(LivingBody))]
[CanEditMultipleObjects]
public class EditorGUIDrawRect : Editor
{
    //SerializedProperty _boundsProperty;
    //private void OnEnable()
    //{
    //    _boundsProperty = serializedObject.FindProperty("bounds");

    //    Debug.Log($"living body{serializedObject.FindProperty("gameObject")} on enable");
    //    Debug.Log($"current bounds is {_boundsProperty.rectValue}");
    //}
    //public override void OnInspectorGUI()
    //{
    //    base.OnInspectorGUI();
    //    serializedObject.Update();
    //    Handles.color = Color.green;
    //    //Handles.

    //    EditorGUI.DrawRect(_boundsProperty.rectValue, Color.green);

    //    serializedObject.ApplyModifiedProperties();
    //}
}

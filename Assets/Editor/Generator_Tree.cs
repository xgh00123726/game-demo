using GameBase.Editable;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public partial class Generator
{
    public const string ToolBarTitle = "Generate";
    public static GameObject select;
    public static EditableRect selectRect;
    [MenuItem("Tools/" + ToolBarTitle + "/Tree", true)]
    public static bool ValidateGenerateTree()
    {
        var selects = Selection.gameObjects;
        if (selects.Length == 0) return false;

        select = selects[0];
        selectRect = select.GetComponent<EditableRect>();
        return selectRect != null;
    }

    [MenuItem("Tools/" + ToolBarTitle + "/Tree", false)]
    public static void GenerateTree()
    {
        TreeMgr.CreateTree(selectRect.ShapeSpcaceToWorldVec3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), selectRect.transform);
    }

    [MenuItem("Tools/" + ToolBarTitle + "/Trees", true)]
    public static bool ValidateGenerateTrees() => ValidateGenerateTree();

    [MenuItem("Tools/" + ToolBarTitle + "/Trees", false)]
    public static void GenerateTrees()
    {
        for (int i = 0;i < 10; ++i)
        {
            TreeMgr.CreateTree(selectRect.ShapeSpcaceToWorldVec3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), selectRect.transform);
        }
    }
}

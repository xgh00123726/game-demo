using UnityEditor;
using UnityEditor.SceneManagement;

public class StartWithStartScene
{
    [MenuItem("Tools/PlayModeUseFirstScene", true)]
    public static bool ValidatePlayModeUseFirstScene()
    {
        return EditorSceneManager.playModeStartScene != null;
    }

    [MenuItem("Tools/PlayModeUseFirstScene", false)]
    public static void PlayModeUseFirstScene()
    {
        EditorSceneManager.playModeStartScene = null;
    }

    [MenuItem("Tools/PlayModeUseStartScene", true)]
    public static bool ValidatePlayerModeUseStartScene()
    {
        return EditorSceneManager.playModeStartScene == null;
    }

    [MenuItem("Tools/PlayModeUseStartScene", false)]
    public static void PlayerModeUseStartScene()
    {
        SceneAsset scene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/StartScene.unity");
        EditorSceneManager.playModeStartScene = scene;
    }
}

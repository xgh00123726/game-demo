using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeTimeMgr : MonoBehaviour
{
    public string startScene = "DemoCopy";
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(startScene);
    }

    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    public bool isMainScene
    {
        get
        {
            UnityEngine.SceneManagement.Scene curScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            return curScene.name == "MainScene";
        }
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeScene(string SceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName); 
    }
}

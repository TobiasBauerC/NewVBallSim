using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;
    public static SceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SceneManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(int sceneToChangeTo)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToChangeTo);
    }
}

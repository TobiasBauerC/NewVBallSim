using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManager : MonoBehaviour
{
    public void SceneManagerChangeToGameScene(int level)
    {
        SceneManager.Instance.ChangeScene(level);
    }

    public void SceneManagerChangeToMainMenu()
    {
        SceneManager.Instance.ChangeScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

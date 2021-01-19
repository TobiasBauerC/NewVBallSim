using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManager : MonoBehaviour
{
    public void SceneManagerChangeToGameScene(DifficultyData difficultyData)
    {
        DifficultyTracker.currentDifficulty = difficultyData;
        SceneManager.Instance.ChangeScene(1);
    }

    public void SceneManagerChangeToGameScene(int sceneIndex)
    {
        SceneManager.Instance.ChangeScene(sceneIndex);
    }

    public void SetDifficulty(DifficultyData difficultyData)
    {
        DifficultyTracker.currentDifficulty = difficultyData;
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

    public void PlayDifficultyAnnouncerLine()
    {
        SoundManager.Instance.PlayAnnouncerLineQueue(SoundManager.Instance.announcerDifficultyChoice);
    }

    public void PlayMenuMusic()
    {
        SoundManager.Instance.PlayMainMenuMusic();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManager : MonoBehaviour
{


    public void SceneManagerChangeToGameScene()
    {
        SceneManager.Instance.ChangeScene(1);
    }

    public void SceneManagerChangeToMainMenu()
    {
        SceneManager.Instance.ChangeScene(0);
    }

    

}

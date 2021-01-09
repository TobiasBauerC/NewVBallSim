using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerButtonOnKeyPress : MonoBehaviour
{
    public string actionName;
    public Button buttonToTrigger;

    void Update()
    {
        if (Input.GetButtonUp(actionName))
        {
            buttonToTrigger.onClick.Invoke();
        }
    }
}

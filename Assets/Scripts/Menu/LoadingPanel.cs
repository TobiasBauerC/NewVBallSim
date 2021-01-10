using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadingPanel : MonoBehaviour
{
    public UnityEvent eventsBeforeLoad;

    // Start is called before the first frame update
    void Start()
    {
        eventsBeforeLoad.Invoke();
        gameObject.SetActive(false);
    }

    public void EnableLoadingPanel()
    {
        gameObject.SetActive(true);
    }
}

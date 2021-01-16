using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public string volumeParamName;
    public AudioMixer mixer;

    void Start()
    {
        SetLevel(PlayerPrefs.GetFloat(volumeParamName, 1.0f));
    }

    public void SetLevel(float sliderValue)
    {
        float newVol = Mathf.Clamp((sliderValue / 100), 0.001f, 1.0f);
        mixer.SetFloat(volumeParamName, Mathf.Log10(newVol) * 20.0f);
        PlayerPrefs.SetFloat(volumeParamName, sliderValue);
    }
}

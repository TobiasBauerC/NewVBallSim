using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public string volumeParamName;
    public AudioMixer mixer;
    public Slider slider;
    public InputField inputField;

    public void Init()
    {
        SetLevel(PlayerPrefs.GetFloat(volumeParamName, 1.0f));
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(volumeParamName, Mathf.Log10(sliderValue) * 20.0f);
        inputField.text = Mathf.RoundToInt(sliderValue * 100).ToString();
        slider.SetValueWithoutNotify(sliderValue);
        PlayerPrefs.SetFloat(volumeParamName, sliderValue);
    }

    public void SetLevel(string inputFieldValue)
    {
        float newVol = Mathf.Clamp(float.Parse(inputFieldValue), 0, 100);
        newVol = newVol / 100.0f;
        newVol = Mathf.Clamp(newVol, 0.001f, 1.0f);
        SetLevel(newVol);
    }
}

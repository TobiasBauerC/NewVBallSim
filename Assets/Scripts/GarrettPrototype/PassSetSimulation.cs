using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class PassSetSimulation : MonoBehaviour
{
    [SerializeField] private Text messageText = null;

    private float _settingAbility = 100;
    public void SetSettingAbility(float value) { _settingAbility = value; }

    private int goodCounter = 0;
    private int medCounter = 0;
    private int badCounter = 0;

    [SerializeField] private Text goodText = null;
    [SerializeField] private Text medText = null;
    [SerializeField] private Text badText = null;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetCounters()
    {
        goodCounter = 0;
        medCounter = 0;
        badCounter = 0;
    }

    public void Simulate1000Sets3Pass()
    {
        ResetCounters();
        int index = 0;
        while (index < 1000)
        {
            SimulateSetOn3Pass();
            index++;
        }
        goodText.text = "Good Sets: " + goodCounter;
        medText.text = "Average Sets: " + medCounter;
        badText.text = "Bad Sets: " + badCounter;
        messageText.text = "On 3 Passes";
    }

    public void Simulate1000Sets2Pass()
    {
        ResetCounters();
        int index = 0;
        while (index < 1000)
        {
            SimulateSetOn2Pass();
            index++;
        }
        goodText.text = "Good Sets: " + goodCounter;
        medText.text = "Average Sets: " + medCounter;
        badText.text = "Bad Sets: " + badCounter;
        messageText.text = "On 2 Passes";
    }

    public void Simulate1000Sets1Pass()
    {
        ResetCounters();
        int index = 0;
        while (index < 1000)
        {
            SimulateSetOn1Pass();
            index++;
        }
        goodText.text = "Good Sets: " + goodCounter;
        medText.text = "Average Sets: " + medCounter;
        badText.text = "Bad Sets: " + badCounter;
        messageText.text = "On 1 Passes";
    }

    public void SimulateSetOn3Pass()
    {
        int set = GetOneSetQuality(3, _settingAbility);
        if (set == 3)
        {
            messageText.text = "Good Set!";
            goodCounter++;
        }
        else if (set == 2)
        {
            messageText.text = "Average Set.";
            medCounter++;
        }
        else if (set == 1)
        { 
            messageText.text = "Bad Set!";
            badCounter++;
        }
    }

    public void SimulateSetOn2Pass()
    {
        int set = GetOneSetQuality(2, _settingAbility);
        if (set == 3)
        {
            messageText.text = "Good Set!";
            goodCounter++;
        }
        else if (set == 2)
        {
            messageText.text = "Average Set.";
            medCounter++;
        }
        else if (set == 1)
        {
            messageText.text = "Bad Set!";
            badCounter++;
        }
    }

    public void SimulateSetOn1Pass()
    {
        int set = GetOneSetQuality(1, _settingAbility);
        if (set == 3)
        {
            messageText.text = "Good Set!";
            goodCounter++;
        }
        else if (set == 2)
        {
            messageText.text = "Average Set.";
            medCounter++;
        }
        else if (set == 1)
        {
            messageText.text = "Bad Set!";
            badCounter++;
        }
    }

    private int GetOneSetQuality(int passQuality, float settingAbility)
    {
        int setQuality = 1;
        // set quality scale
        // 1 is bad set
        // 2 is medium set
        // 3 is good set

        int setValue = Mathf.CeilToInt(UnityEngine.Random.Range(0, settingAbility));
        Debug.Log("Set Value was: " + setValue);

        int goodThreshold = 100 - (passQuality * 30);
        
        if(setValue >= goodThreshold)
        {
            Debug.Log("Good threshold was " + goodThreshold);
            // good set behaviour
            setQuality = 3;
            return setQuality;
        }
        int medThreshold = Mathf.CeilToInt(goodThreshold - (goodThreshold / 1.7f));
        if(setValue >= medThreshold)
        {
            Debug.Log("Medium threshold was " + medThreshold);
            // medium set behaviour
            setQuality = 2;
            return setQuality;
        }
        Debug.Log("Medium threshold was " + medThreshold);

        return setQuality;
    }

    public int GetSetNumber(int passNumber)
    {
        int setQuality = 1;
        // set quality scale
        // 1 is bad set
        // 2 is medium set
        // 3 is good set

        int setValue = Mathf.CeilToInt(UnityEngine.Random.Range(0, _settingAbility));
        // Debug.Log("Set value was " + setValue);

        int goodThreshold = 100 - (passNumber * 28);

        if (setValue >= goodThreshold)
        {
            // Debug.Log("Good threshold was " + goodThreshold);
            // good set behaviour
            setQuality = 3;
            return setQuality;
        }
        int medThreshold = Mathf.CeilToInt(goodThreshold - (goodThreshold / 1.7f));
        if (setValue >= medThreshold)
        {
            // Debug.Log("Medium threshold was " + medThreshold);
            // medium set behaviour
            setQuality = 2;
            return setQuality;
        }
        // Debug.Log("Medium threshold was " + medThreshold);

        return setQuality;
    }
}

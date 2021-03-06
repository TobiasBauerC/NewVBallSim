﻿using System.Collections;
using System.Collections.Generic;
// using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ServePassSimulation : MonoBehaviour
{
    private float _baseServeAbility = 100;
    public void SetServeAbility(float value) { _baseServeAbility = value; }
    private float _basePassAbility = 100;
    public void SetPassAbility(float value) { _basePassAbility = value; }

    [SerializeField] private Text messageText = null;

    [SerializeField] private Text aceText = null;
    [SerializeField] private Text missText = null;
    [SerializeField] private Text oneText = null;
    [SerializeField] private Text twoText = null;
    [SerializeField] private Text threeText = null;

    private int aceCounter = 0;
    private int missCounter = 0;
    private int oneCounter = 0;
    private int twoCounter = 0;
    private int threeCounter = 0;

    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void ResetSimulation()
    {
        aceCounter = 0;
        missCounter = 0;
        oneCounter = 0;
        twoCounter = 0;
        threeCounter = 0;
    }

    private void CalculatePassingAverage()
    {
        int totalPasses = 1000 - missCounter;
        float average = ((oneCounter * 1) + (twoCounter * 2) + (threeCounter * 3));
        average = average / totalPasses;
        Debug.Log(average);
        string averageString = average.ToString("#.##");
        messageText.text = "Passing Average: " + averageString;
    }

    private int CalculateServe(float serveAbility)
    {
        int serveValue = 0;
        // calculate the serve value
        serveValue = Mathf.CeilToInt(UnityEngine.Random.Range(0, serveAbility));
        Debug.Log("serve strength was " + serveValue);
        return serveValue;
    }

    // called from the rally manager
    public int GetServeNumber(Pawn servingPawn, bool isPlayerPawn)
    {
        int serveValue = 0;
        // calculate the serve value
        float serveSkill = SkillManager.Instance.GetPlayerSkillsFromPawn(servingPawn, isPlayerPawn).serve;
        // Debug.Log("Serve skill was determined to be " + serveSkill);
        serveValue = Mathf.CeilToInt(UnityEngine.Random.Range(0, serveSkill));
        // Debug.Log("returning serve value of " + serveValue);
        return serveValue;
    }

    private int CalculatePass(Pawn passingPawn, bool isPlayerPawn)
    {
        int passValue = 0;
        // calculate the pass value
        float passSkill = SkillManager.Instance.GetPlayerSkillsFromPawn(passingPawn, isPlayerPawn).pass;
        // Debug.Log("Pass skill was determined to be " + passSkill);
        passValue = Mathf.CeilToInt(UnityEngine.Random.Range(0, passSkill));
        // Debug.Log("returning pass value of " + passValue);
        return passValue;
    }

    public int GetPassNumber(int serveNumber, Pawn passingPawn, bool isPlayerPawn)
    {
        int passQuality = 3;
        int serve = serveNumber;
        int pass = CalculatePass(passingPawn, isPlayerPawn);

        if (serve < 5)
        {
            // missed serve

            return 4;
        }

        // look for an ace
        if ((serve - pass) > 60)
        {
            // ace
            passQuality = 0;
            return passQuality;
        }
        else if ((serve - pass) > 35)
        {
            // one pass
            passQuality = 1;
            return passQuality;
        }
        else if ((serve - pass) >= 5)
        {
            // two pass
            passQuality = 2;
            return passQuality;
        }

        passQuality = 3;
        return passQuality;
    }

    public int GetPassNumber(int serveNumber, int xDistance, int yDistance, Pawn passingPawn, bool isPlayerPawn)
    {
        int passQuality = 3;
        int serve = serveNumber;
        int pass = CalculatePass(passingPawn, isPlayerPawn);
        int distanceMod = 10; // moving distance mod from 8 to 10 to put more emphasis on serving farther away from players

        // account for passers distance from ball
        if(xDistance > 4 || yDistance > 4)
        {
            // ball is too far from the player, lands for an ace
            Debug.Log("Ball is too far from the player, lands for an ace");
            return 0;
        }
        // if its not an ace, alter the pass number based on the distance
        int newPass = pass - ((xDistance - 1) * Mathf.RoundToInt(distanceMod / 2)) - ((yDistance - 1) * distanceMod);
        // Debug.LogWarning("Closest player xDistance is " + xDistance + " and yDistance is " + yDistance);
        // Debug.LogWarning("Passers number modified from " + pass + " to " + newPass);
        pass = newPass;

        if (serve < 3)
        {
            // missed serve

            return 4;
        }

        // look for an ace
        if ((serve - pass) > 70) // making random aces less likely, moving mod from 55 to 70 
            // moving again from 70 to 90 to stop having as many aces
        {
            // ace
            passQuality = 0;
            return passQuality;
        }
        else if ((serve - pass) > 40) // moving from 30 to 40
        {
            // one pass
            passQuality = 1;
            return passQuality;
        }
        else if ((serve - pass) >= 0)
        {
            // two pass
            passQuality = 2;
            return passQuality;
        }

        passQuality = 3;
        return passQuality;
    }

    private int GetPassQuality(float serveAbility, float passAbility, Pawn passingPawn, bool isPlayerPawn)
    {
        int passQuality = 3;
        int serve = CalculateServe(_baseServeAbility);
        int pass = CalculatePass(passingPawn, isPlayerPawn);

        if(serve < 10)
        {
            // missed serve
            
            return 4;
        }

        // look for an ace
        if((serve - pass) > 50)
        {
            // ace
            passQuality = 0;
            return passQuality;
        }
        else if ((serve - pass) > 20)
        {
            // one pass
            passQuality = 1;
            return passQuality;
        }
        else if ((serve - pass) > -10)
        {
            // two pass
            passQuality = 2;
            return passQuality;
        }

        passQuality = 3;
        return passQuality;
    }

    private void ResolvePassQuality(int passQuality)
    {
        if(passQuality == 0)
        {
            // ace behaviour
            messageText.text = "Ace Serve";
            aceCounter++;
            aceText.text = "Aces: " + aceCounter;
            return;
        }
        if (passQuality == 1)
        {
            // one behaviour
            messageText.text = "One Pass";
            oneCounter++;
            oneText.text = "Ones: " + oneCounter;
            return;
        }
        if (passQuality == 2)
        {
            // two behaviour
            messageText.text = "Two Pass";
            twoCounter++;
            twoText.text = "Twos: " + twoCounter;
            return;
        }
        if (passQuality == 3)
        {
            // three behaviour
            messageText.text = "Three Pass";
            threeCounter++;
            threeText.text = "Threes: " + threeCounter;
            return;
        }
        if (passQuality == 4)
        {
            // missed serve behaviour
            messageText.text = "Missed Serve";
            missCounter++;
            missText.text = "Errors: " + missCounter;
            return;
        }
    }
}

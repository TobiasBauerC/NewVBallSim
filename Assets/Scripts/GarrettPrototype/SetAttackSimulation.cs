using System.Collections;
using System.Collections.Generic;
// using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class SetAttackSimulation : MonoBehaviour
{
    [SerializeField] private Text messageText = null;

    private float _attackingAbility = 100;
    public void SetAttackAbility(float value) { _attackingAbility = value; }

    private int greatCounter = 0;
    private int goodCounter = 0;
    private int medCounter = 0;
    private int badCounter = 0;
    private int terribleCounter = 0;

    [SerializeField] private Text greatText = null;
    [SerializeField] private Text goodText = null;
    [SerializeField] private Text medText = null;
    [SerializeField] private Text badText = null;
    [SerializeField] private Text terribleText = null;

    private int lastHitValue = 0;
    private int lastHitQuality = 0;
    public int GetLastHitValue() { return lastHitValue; }
    public int GetLastHitQuality() { return lastHitQuality; }

    [SerializeField] private RotationManager rotationManager = null;



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
        greatCounter = 0;
        goodCounter = 0;
        medCounter = 0;
        badCounter = 0;
        terribleCounter = 0;
    }

    public void Simulate1000HitsGoodSet()
    {
        ResetCounters();
        int index = 0;
        while (index < 1000)
        {
            SimulateHitOnGoodSet();
            index++;
        }
        greatText.text = "Great Hits: " + greatCounter;
        goodText.text = "Good Hits: " + goodCounter;
        medText.text = "Average Hits: " + medCounter;
        badText.text = "Bad Hits: " + badCounter;
        terribleText.text = "Terrible Hits: " + terribleCounter;
        messageText.text = "On Good Sets";
    }

    public void Simulate1000HitsMedSet()
    {
        ResetCounters();
        int index = 0;
        while (index < 1000)
        {
            SimulateHitOnMedSet();
            index++;
        }
        greatText.text = "Great Hits: " + greatCounter;
        goodText.text = "Good Hits: " + goodCounter;
        medText.text = "Average Hits: " + medCounter;
        badText.text = "Bad Hits: " + badCounter;
        terribleText.text = "Terrible Hits: " + terribleCounter;
        messageText.text = "On Average Sets";
    }

    public void Simulate1000HitsBadSet()
    {
        ResetCounters();
        int index = 0;
        while (index < 1000)
        {
            SimulateHitOnBadSet();
            index++;
        }
        greatText.text = "Great Hits: " + greatCounter;
        goodText.text = "Good Hits: " + goodCounter;
        medText.text = "Average Hits: " + medCounter;
        badText.text = "Bad Hits: " + badCounter;
        terribleText.text = "Terrible Hits: " + terribleCounter;
        messageText.text = "On Bad Sets";
    }

    public void SimulateHitOnGoodSet()
    {
        int hit = GetOneHitQuality(3, _attackingAbility);
        if(hit == 5)
        {
            messageText.text = "Great Hit!";
            greatCounter++;
        }
        else if (hit == 4)
        {
            messageText.text = "Good Hit!";
            goodCounter++;
        }
        else if (hit == 3)
        {
            messageText.text = "Average Hit.";
            medCounter++;
        }
        else if (hit == 2)
        {
            messageText.text = "Bad Hit!";
            badCounter++;
        }
        else if (hit == 1)
        {
            messageText.text = "Terrible Hit!";
            terribleCounter++;
        }
    }

    public void SimulateHitOnMedSet()
    {
        int hit = GetOneHitQuality(2, _attackingAbility);
        if (hit == 5)
        {
            messageText.text = "Great Hit!";
            greatCounter++;
        }
        else if (hit == 4)
        {
            messageText.text = "Good Hit!";
            goodCounter++;
        }
        else if (hit == 3)
        {
            messageText.text = "Average Hit.";
            medCounter++;
        }
        else if (hit == 2)
        {
            messageText.text = "Bad Hit!";
            badCounter++;
        }
        else if (hit == 1)
        {
            messageText.text = "Terrible Hit!";
            terribleCounter++;
        }
    }

    public void SimulateHitOnBadSet()
    {
        int hit = GetOneHitQuality(1, _attackingAbility);
        if (hit == 5)
        {
            messageText.text = "Great Hit!";
            greatCounter++;
        }
        else if (hit == 4)
        {
            messageText.text = "Good Hit!";
            goodCounter++;
        }
        else if (hit == 3)
        {
            messageText.text = "Average Hit.";
            medCounter++;
        }
        else if (hit == 2)
        {
            messageText.text = "Bad Hit!";
            badCounter++;
        }
        else if (hit == 1)
        {
            messageText.text = "Terrible Hit!";
            terribleCounter++;
        }
    }

    private int GetOneHitQuality(int setQuality, float hittingAbility)
    {
        // hit quality scale
        // number will be passed as 1-100 but for counting will use the following scale
        // 1 is terrible hit
        // 2 is bad hit
        // 3 is med hit
        // 4 is good hit
        // 5 is great hit

        int hitValue = Mathf.CeilToInt(UnityEngine.Random.Range(0, hittingAbility));
        Debug.Log("Hit Value was: " + hitValue);
        lastHitValue = hitValue;
        int hitQuality = 1;

        if (setQuality == 3)
        {
            if(hitValue >= 80)
            {
                // great hit
                hitQuality = 5;
                return hitQuality;
            }
            else if(hitValue >= 40)
            {
                // good hit
                hitQuality = 4;
                return hitQuality;
            }
            else if (hitValue >= 20)
            {
                // med hit
                hitQuality = 3;
                return hitQuality;
            }
            else if (hitValue >= 5)
            {
                // bad hit
                hitQuality = 2;
                return hitQuality;
            }
            else
            {
                // terrible hit
                hitQuality = 1;
                return hitQuality;
            }
        }
        else if (setQuality == 2)
        {
            if (hitValue >= 85)
            {
                // great hit
                hitQuality = 5;
                return hitQuality;
            }
            else if (hitValue >= 65)
            {
                // good hit
                hitQuality = 4;
                return hitQuality;
            }
            else if (hitValue >= 25)
            {
                // med hit
                hitQuality = 3;
                return hitQuality;
            }
            else if (hitValue >= 5)
            {
                // bad hit
                hitQuality = 2;
                return hitQuality;
            }
            else
            {
                // terrible hit
                hitQuality = 1;
                return hitQuality;
            }
        }
        else if (setQuality == 1)
        {
            if (hitValue >= 95)
            {
                // great hit
                hitQuality = 5;
                return hitQuality;
            }
            else if (hitValue >= 80)
            {
                // good hit
                hitQuality = 4;
                return hitQuality;
            }
            else if (hitValue >= 60)
            {
                // med hit
                hitQuality = 3;
                return hitQuality;
            }
            else if (hitValue >= 20)
            {
                // bad hit
                hitQuality = 2;
                return hitQuality;
            }
            else
            {
                // terrible hit
                hitQuality = 1;
                return hitQuality;
            }
        }
        return hitQuality;
    }

    public int GetAttackNumber(int setNumber)
    {
        int attackValue = Mathf.CeilToInt(UnityEngine.Random.Range(0, _attackingAbility));
        // Debug.Log("Attack value was " + attackValue);
        return attackValue;
    }

    public int GetAttackNumber(Pawn attackingPlayer, bool isPlayerPawn)
    {
        float attackMax = SkillManager.Instance.GetPlayerSkillsFromPawn(attackingPlayer, isPlayerPawn).attack;
        //Debug.Log("Attack skill was determined to be " + attackMax);
        if (rotationManager.IsPlayerPawnLocatedInBackRow(attackingPlayer))
            attackMax -= 20;
        int attackValue = Mathf.CeilToInt(UnityEngine.Random.Range(0, attackMax));
        //Debug.Log("returning attack value of " + attackValue);
        return attackValue;
    }

    public int GetAttackQuality(int setNumber, int attackNumber)
    {
        int hitQuality = 1;
        int setQuality = setNumber;
        int hitValue = attackNumber;

        if (setQuality == 3)
        {
            if (hitValue >= 80)
            {
                // great hit
                hitQuality = 5;
                return hitQuality;
            }
            else if (hitValue >= 40)
            {
                // good hit
                hitQuality = 4;
                return hitQuality;
            }
            else if (hitValue >= 20)
            {
                // med hit
                hitQuality = 3;
                return hitQuality;
            }
            else if (hitValue >= 3)
            {
                // bad hit
                hitQuality = 2;
                return hitQuality;
            }
            else
            {
                // terrible hit
                hitQuality = 1;
                return hitQuality;
            }
        }
        else if (setQuality == 2)
        {
            if (hitValue >= 85)
            {
                // great hit
                hitQuality = 5;
                return hitQuality;
            }
            else if (hitValue >= 65)
            {
                // good hit
                hitQuality = 4;
                return hitQuality;
            }
            else if (hitValue >= 35)
            {
                // med hit
                hitQuality = 3;
                return hitQuality;
            }
            else if (hitValue >= 5)
            {
                // bad hit
                hitQuality = 2;
                return hitQuality;
            }
            else
            {
                // terrible hit
                hitQuality = 1;
                return hitQuality;
            }
        }
        else if (setQuality == 1)
        {
            if (hitValue >= 95)
            {
                // great hit
                hitQuality = 5;
                return hitQuality;
            }
            else if (hitValue >= 80)
            {
                // good hit
                hitQuality = 4;
                return hitQuality;
            }
            else if (hitValue >= 60)
            {
                // med hit
                hitQuality = 3;
                return hitQuality;
            }
            else if (hitValue >= 8)
            {
                // bad hit
                hitQuality = 2;
                return hitQuality;
            }
            else
            {
                // terrible hit
                hitQuality = 1;
                return hitQuality;
            }
        }
        return hitQuality;
    }
}
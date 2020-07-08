﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class AttackDefenceSimulation : MonoBehaviour
{

    private float _attackAbility = 100;
    public void SetAttackAbility(float value) { _attackAbility = value; }
    private float _blockAbility = 100;
    public void SetBlockAbility(float value) { _blockAbility = value; }
    private float _defenceAbility = 100;
    public void SetDefenceAbility(float value) { _defenceAbility = value; }

    [SerializeField] private Text _messageText = null;

    [SerializeField] private Text _killsText = null;
    [SerializeField] private Text _toolsText = null;
    [SerializeField] private Text _blocksText = null;
    [SerializeField] private Text _errorsText = null;
    [SerializeField] private Text _digsText = null;

    private int index = 0;
    private int killCount = 0;
    private int toolCount = 0;
    private int blockCount = 0;
    private int errorCount = 0;
    private int digCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SimulateOneAttackGoodSet()
    {
        SimulateAttackVSDefence(_attackAbility, _blockAbility, _defenceAbility, 3);
    }

    public void Simulate1000AttackGoodSet()
    {
        ResetCounters();
        int index = 0;
        while (index < 1000)
        {
            SimulateAttackVSDefence(_attackAbility, _blockAbility, _defenceAbility, 3);
            index++;
        }
        _killsText.text = "Kills: " + killCount;
        _toolsText.text = "Tools: " + toolCount;
        _blocksText.text = "Blocks: " + blockCount;
        _errorsText.text = "Errors: " + errorCount;
        _digsText.text = "Digs: " + digCount;

        // calculate hitting average
        int totalHits = 1000;
        float average = ((killCount) + (toolCount) - (errorCount) - (blockCount));
        average = average / totalHits;
        Debug.Log(average);
        string averageString = average.ToString("#.##");
        _messageText.text = "On Good Sets Hitting average: " + averageString;
    }

    public void Simulate1000AttackMedSet()
    {
        ResetCounters();
        int index = 0;
        while (index < 1000)
        {
            SimulateAttackVSDefence(_attackAbility, _blockAbility, _defenceAbility, 2);
            index++;
        }
        _killsText.text = "Kills: " + killCount;
        _toolsText.text = "Tools: " + toolCount;
        _blocksText.text = "Blocks: " + blockCount;
        _errorsText.text = "Errors: " + errorCount;
        _digsText.text = "Digs: " + digCount;
        // calculate hitting average
        int totalHits = 1000;
        float average = ((killCount) + (toolCount) - (errorCount) - (blockCount));
        average = average / totalHits;
        Debug.Log(average);
        string averageString = average.ToString("#.##");
        _messageText.text = "On Average Sets Hitting average: " + averageString;
    }

    public void Simulate1000AttackBadSet()
    {
        ResetCounters();
        int index = 0;
        while (index < 1000)
        {
            SimulateAttackVSDefence(_attackAbility, _blockAbility, _defenceAbility, 1);
            index++;
        }
        _killsText.text = "Kills: " + killCount;
        _toolsText.text = "Tools: " + toolCount;
        _blocksText.text = "Blocks: " + blockCount;
        _errorsText.text = "Errors: " + errorCount;
        _digsText.text = "Digs: " + digCount;
        // calculate hitting average
        int totalHits = 1000;
        float average = ((killCount) + (toolCount) - (errorCount) - (blockCount));
        average = average / totalHits;
        Debug.Log(average);
        string averageString = average.ToString("#.##");
        _messageText.text = "On Bad Sets Hitting average: " + averageString;
    }

    private void SimulateAttackVSDefence(float attackAbility, float blockAbility, float defenceAbility, int setQuality)
    {
        // take attack value and quality
        int attackValue = GetAttackValue(attackAbility);
        int attackQuality = GetAttackQuality(setQuality, attackValue);
        float q = attackQuality * 1.0f;
        float v = attackValue * 1.0f;
        float attackStrength = v / (4 / q);
        int trueAttackStrength = Mathf.CeilToInt(attackStrength);

        // take block value and quality
        int blockValue = GetBlockValue(blockAbility);
        float blockQuality = GetBlockQuality();

        // check if hitter makes an error
        if(attackQuality == 1)
        {
            // error behaviour
            errorCount++;
            _messageText.text = "Hitting error";
            return;
        }

        // compare attack and block to find result
        // does it hit the block
        int blockChance = Mathf.CeilToInt(UnityEngine.Random.Range(0, blockAbility));
        bool contactBlock = false;
        float threshold = blockQuality * 10;
        if (blockChance < threshold)
            contactBlock = true;
        if (contactBlock)
        {
            Debug.Log("Ball contacts the block");
            // ball hit the block, solve for tool or block
            if(trueAttackStrength + 10 >= blockValue)
            {
                Debug.Log("Attack tools the block");
                toolCount++;
                _messageText.text = "Tool";
                return;
            }
            else if (trueAttackStrength + 10 < blockValue)
            {
                Debug.Log("Block stops the attack");
                blockCount++;
                _messageText.text = "Block";
                return;
            }

        }
        Debug.Log("Ball makes it past the block");

        // if attack beats block, continue to check defense
        int defenceValue = GetDefenceValue(_defenceAbility);

        // compare attack and defence value
        if(defenceValue > trueAttackStrength)
        {
            Debug.Log("Defender makes a dig");
            digCount++;
            _messageText.text = "Dig";
            return;
        }
        else if (trueAttackStrength >= defenceValue)
        {
            Debug.Log("Attacker beats the defender");
            killCount++;
            _messageText.text = "Kill";
        }
    }

    public int GetResultNumber(int attackValue, int attackQuality, int blockValue, float blockQuality, int defenceValue)
    {
        float q = attackQuality * 1.0f;
        float v = attackValue * 1.0f;
        float attackStrength = v / (3.0f / q);
        int trueAttackStrength = Mathf.CeilToInt(attackStrength);

        // compare attack and block to find result
        // does it hit the block
        int blockChance = Mathf.CeilToInt(UnityEngine.Random.Range(0, _blockAbility));
        bool contactBlock = false;
        float threshold = blockQuality * 15;
        if (blockChance < threshold)
            contactBlock = true;
        if (contactBlock)
        {
            // Debug.Log("Ball contacts the block");
            // ball hit the block, solve for tool or block
            if (trueAttackStrength + 20 >= blockValue)
            {
                // Debug.Log("Attack tools the block");
                return -1;
            }
            else if (trueAttackStrength + 20 < blockValue)
            {
                // Debug.Log("Block stops the attack");
                return 100;
            }

        }
        // Debug.Log("Ball makes it past the block");

        // if attack beats block, continue to check defense
        // compare attack and defence value
        int defenceMod = 20;
        if (defenceValue + defenceMod > trueAttackStrength)
        {
            // calculate the quality of dig
            if (defenceValue + defenceMod > trueAttackStrength + 60)
            {
                Debug.Log("Dig quality was a 3");
                return 3;
            }
            else if (defenceValue + defenceMod > trueAttackStrength + 30)
            {
                Debug.Log("Dig quality was a 2");
                return 2;
            }
            else
            {
                Debug.Log("Dig quality was a 1");
                return 1;
            }
        }
        else /*if (trueAttackStrength >= defenceValue)*/
        {
            // Debug.Log("Attacker beats the defender, Kill");
            return 0;
        }
        // return -2;
    }

    private int GetAttackValue(float attackAbility)
    {
        int attackValue = Mathf.CeilToInt(UnityEngine.Random.Range(0, attackAbility));
        Debug.Log("Attack value was " + attackValue);
        return attackValue;
    }

    private int GetAttackQuality(int setQuality, int attackValue)
    {
        int hitQuality = 1;

        if (setQuality == 3)
        {
            if (attackValue >= 80)
            {
                // great hit
                hitQuality = 5;
                return hitQuality;
            }
            else if (attackValue >= 40)
            {
                // good hit
                hitQuality = 4;
                return hitQuality;
            }
            else if (attackValue >= 20)
            {
                // med hit
                hitQuality = 3;
                return hitQuality;
            }
            else if (attackValue >= 5)
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
            if (attackValue >= 85)
            {
                // great hit
                hitQuality = 5;
                return hitQuality;
            }
            else if (attackValue >= 65)
            {
                // good hit
                hitQuality = 4;
                return hitQuality;
            }
            else if (attackValue >= 25)
            {
                // med hit
                hitQuality = 3;
                return hitQuality;
            }
            else if (attackValue >= 5)
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
            if (attackValue >= 95)
            {
                // great hit
                hitQuality = 5;
                return hitQuality;
            }
            else if (attackValue >= 80)
            {
                // good hit
                hitQuality = 4;
                return hitQuality;
            }
            else if (attackValue >= 60)
            {
                // med hit
                hitQuality = 3;
                return hitQuality;
            }
            else if (attackValue >= 20)
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

    private int GetBlockValue(float blockAbility)
    {
        int blockValue = Mathf.CeilToInt(UnityEngine.Random.Range(0, blockAbility));
        Debug.Log("Block value was: " + blockValue);
        return blockValue;
    }

    public int GetBlockNumber()
    {
        int blockValue = Mathf.CeilToInt(UnityEngine.Random.Range(0, _blockAbility));
        Debug.Log("Block number was " + blockValue);
        return blockValue;
    }

    public float GetBlockQuality()
    {
        int blockQuality = (Mathf.CeilToInt(UnityEngine.Random.Range(0.00000000001f, 4)) - 1) * 2; // block should be 0, 2, 4 or 6 for no, single, double or triple block
        return blockQuality;
    }


    private int GetDefenceValue(float defenceAbility)
    {
        int defenceValue = Mathf.CeilToInt(UnityEngine.Random.Range(0, defenceAbility));
        Debug.Log("Dig value was: " + defenceValue);
        return defenceValue;
    }

    public int GetDefenceNumber()
    {
        int defenceValue = Mathf.CeilToInt(UnityEngine.Random.Range(0, _defenceAbility));
        Debug.Log("Defence value was " + defenceValue);
        return defenceValue;
    }

    private void ResetCounters()
    {
        killCount = 0;
        toolCount = 0;
        blockCount = 0;
        digCount = 0;
        errorCount = 0;
    }
}
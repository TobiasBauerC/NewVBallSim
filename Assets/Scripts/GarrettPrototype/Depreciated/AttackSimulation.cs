using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSimulation : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 100.0f)] private float _attackStrength = 100;
    [SerializeField] [Range(0.0f, 100.0f)] private float _blockStrength = 100;
    [SerializeField] [Range(0.0f, 100.0f)] private float _defenceStrength = 100;

    [SerializeField] private Text _messageText = null;

    [SerializeField] private Text _killsText = null;
    [SerializeField] private Text _toolsText = null;
    [SerializeField] private Text _blocksText = null;
    [SerializeField] private Text _errorsText = null;
    [SerializeField] private Text _digsText = null;

    private string _toolResult = "Tool";
    private string _blockResult = "Block";
    private string _outResult = "Out of Bounds";
    private string _digResult = "Dig";
    private string _killResult = "Kill";

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
        Simulate();
    }


    public void ResetSimulation()
    {
        index = 0;
        killCount = 0;
        toolCount = 0;
        blockCount = 0;
        errorCount = 0;
        digCount = 0;
    }

    private void Simulate()
    {
        if (index < 1000)
        {
            AttackVSBlock();
            index++;
            _killsText.text = "Kills: " + killCount;
            _toolsText.text = "Tools: " + toolCount;
            _blocksText.text = "Blocks: " + blockCount;
            _errorsText.text = "Errors: " + errorCount;
            _digsText.text = "Digs: " + digCount;

            if (index == 1000)
            {
                float kills = killCount + toolCount;
                float errors = errorCount + blockCount;
                float totalAttacks = index;
                float percentage = (kills - errors) / totalAttacks;
                Debug.Log("Percentage is " + percentage);
                percentage = Mathf.Round(percentage * 1000);
                _messageText.text = "Kill percentage is " + percentage;
            }
        }
    }

    private void AttackVSBlock()
    {
        // Attacker rolls 0 - 100     50 or better makes it past the block, 10 or worse is an error, 10-49 the block has a chance
        // Blocker rolls 0 - 50       if blocker beats attacker its a block, else its a kill

        // calculate the attack score
        int attack = Mathf.CeilToInt(UnityEngine.Random.Range(0, _attackStrength));
        Debug.Log("attack was " + attack);

        // if attack is good, it makes it past the block
        if (attack > 50)
        {
            Debug.Log("attack was higher than 50");
            Debug.Log("attack makes it past the block");
            AttackVSDefence(attack);
            return;
        }
        // if attack is terrible, its a straight miss
        else if (attack < 11)
        {
            _messageText.text = _outResult;
            Debug.Log("hit out of bounds");
            errorCount++;
            return;
        }

        // calculate the block score
        int block = Mathf.CeilToInt(UnityEngine.Random.Range(0, _blockStrength/2));
        Debug.Log("block was " + block);

        // check if the attack beats the block
        if (attack >= block)
        {
            _messageText.text = _toolResult;
            Debug.Log("attack tools the block");
            toolCount++;
            return;
        }
        // otherwise the block is higher than the attack
        else
        {
            _messageText.text = _blockResult;
            Debug.Log("block beats the attack");
            blockCount++;
            return;
        }

    }

    private void AttackVSDefence(int attackScore)
    {
        // calculate the defence score
        int defence = Mathf.CeilToInt(UnityEngine.Random.Range(0, _defenceStrength));
        Debug.Log("defence was " + defence);

        if(defence > attackScore)
        {
            _messageText.text = _digResult;
            Debug.Log("Defence makes a dig");
            digCount++;
            return;
        }
        else
        {
            _messageText.text = _killResult;
            Debug.Log("Attack beats the defence");
            killCount++;
            return;
        }
    }
}

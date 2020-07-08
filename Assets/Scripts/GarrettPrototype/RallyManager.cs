using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RallyManager : MonoBehaviour
{

    private int AserveNumber = 0;
    private int ApassNumber = 0;
    private int AsetNumber = 0;
    private int AattackNumber = 0;
    private int AattackQuality = 0;
    private int AblockNumber = 0;
    private int AblockQuality = 0;
    private int AdefenceNumber = 0;
    private int AresultNumber = 0;

    [SerializeField] private ServePassSimulation AservePass;
    [SerializeField] private PassSetSimulation ApassSet;
    [SerializeField] private SetAttackSimulation AsetAttack;
    [SerializeField] private AttackDefenceSimulation AattackDefence;

    private int BserveNumber = 0;
    private int BpassNumber = 0;
    private int BsetNumber = 0;
    private int BattackNumber = 0;
    private int BattackQuality = 0;
    private int BblockNumber = 0;
    private int BblockQuality = 0;
    private int BdefenceNumber = 0;
    private int BresultNumber = 0;

    [SerializeField] private ServePassSimulation BservePass;
    [SerializeField] private PassSetSimulation BpassSet;
    [SerializeField] private SetAttackSimulation BsetAttack;
    [SerializeField] private AttackDefenceSimulation BattackDefence;




    [SerializeField] private Text _killsText = null;
    [SerializeField] private Text _toolsText = null;
    [SerializeField] private Text _blocksText = null;
    [SerializeField] private Text _errorsText = null;
    [SerializeField] private Text _digsText = null;
    [SerializeField] private Text _acesText = null;
    [SerializeField] private Text _serrorsText = null;

    private int index = 0;
    private int killCount = 0;
    private int toolCount = 0;
    private int blockCount = 0;
    private int errorCount = 0;
    private int digCount1 = 0;
    private int digCount2 = 0;
    private int digCount3 = 0;
    private int aceCount = 0;
    private int serrorCount = 0;

    private int resultNumber = 0;

    private int bCount = 0;
    private int aCount = 0;

    [SerializeField] private Text _aText = null;
    [SerializeField] private Text _bText = null;

    private bool isAteamServing = true;

    



    // Start is called before the first frame update
    void Start()
    {
        // SimulateRally();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SimulateRally1000()
    {
        ResetCounters();
        int index = 0;
        while (index < 1000)
        {
            SimulateRallyAServing();
            index++;
        }
        _killsText.text = "Kills: " + killCount;
        _toolsText.text = "Tools: " + toolCount;
        _blocksText.text = "Blocks: " + blockCount;
        _errorsText.text = "Hitting Errors: " + errorCount;
        _digsText.text = "Digs:     1's: " + digCount1 + "   2's: " + digCount2 + "   3's: " + digCount3;
        _acesText.text = "Aces: " + aceCount;
        _serrorsText.text = "Service Errors: " + serrorCount;
        _aText.text = "Servers Points: " + bCount;
        _bText.text = "Recievers Points: " + aCount;
    }

    private void ResetCounters()
    {
        index = 0;
        killCount = 0;
        toolCount = 0;
        blockCount = 0;
        errorCount = 0;
        digCount1 = 0;
        digCount2 = 0;
        digCount3 = 0;
        aceCount = 0;
        serrorCount = 0;
        bCount = 0;
        aCount = 0;
    }

    public void SimulateOneRally()
    {
        SimulateRallyAServing();
    }

    public bool SimulateRallyAServing()
    {
        isAteamServing = true;
        // SERVE PASS
        Debug.Log("A serves");
        AserveNumber = AservePass.GetServeNumber();
        BpassNumber = BservePass.GetPassNumber(AserveNumber);
        // check for aces or misses
        if(BpassNumber == 4)
        {
            Debug.Log("A Miss Serve");
            serrorCount++;
            bCount++;
            return false;
        }
        else if (BpassNumber == 0)
        {
            Debug.Log("A Ace");
            aceCount++;
            aCount++;
            return true;
        }
        Debug.Log("B Passed " + BpassNumber);

        // PASS SET
        // get the set quality based on the pass
        BsetNumber = BpassSet.GetSetNumber(BpassNumber);
        Debug.Log("B Set " + BsetNumber);

        // SET ATTACK
        // get the attack quality based on the set
        BattackNumber = BsetAttack.GetAttackNumber(BsetNumber);
        BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
        Debug.Log("B Hit " + BattackQuality);
        if(BattackQuality == 1)
        {
            Debug.Log("B Hitting Error");
            aCount++;
            errorCount++;
            return true;
        }

        // ATTACK DEFENCE
        // get the block and defence values
        AblockNumber = AattackDefence.GetBlockNumber();
        AblockQuality = AattackDefence.GetBlockQuality();
        AdefenceNumber = AattackDefence.GetDefenceNumber();
        // compare the attack values to the defence values
        resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber);
        if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
        {
            bool result = CompareResultsB(resultNumber);
            return result;
        }
        else
        {
            bool result2 = ContinueRallyAServing(resultNumber);
            return result2;
        }
    }

    private bool ContinueRallyAServing(int digNumber)
    {
        while (resultNumber == 2 || resultNumber == 1 || resultNumber == 3)
        {
            // A SIDE WITH A DIG
            Debug.Log("A Dug " + digNumber);
            if (digNumber == 1)
                digCount1++;
            if (digNumber == 2)
                digCount2++;
            if (digNumber == 3)
                digCount3++;
            // PASS SET
            // get the set quality based on the pass
            AsetNumber = ApassSet.GetSetNumber(digNumber);
            Debug.Log("A Set " + AsetNumber);

            // SET ATTACK
            // get the attack quality based on the set
            AattackNumber = AsetAttack.GetAttackNumber(AsetNumber);
            AattackQuality = AsetAttack.GetAttackQuality(AsetNumber, AattackNumber);
            Debug.Log("A Hit " + AattackQuality);
            if (AattackQuality == 1)
            {
                Debug.Log("A Hitting Error");
                bCount++;
                errorCount++;
                return false;
            }
            // ATTACK DEFENCE
            // get the block and defence values
            BblockNumber = BattackDefence.GetBlockNumber();
            BblockQuality = BattackDefence.GetBlockQuality();
            BdefenceNumber = BattackDefence.GetDefenceNumber();
            // compare the attack values to the defence values
            resultNumber = BattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber);
            if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
            {
                bool result3 = CompareResultsA(resultNumber);
                return result3;
            }
                



            // B SIDE WITH A DIG
            Debug.Log("B Dug " + digNumber);
            if (resultNumber == 1)
                digCount1++;
            if (resultNumber == 2)
                digCount2++;
            if (resultNumber == 3)
                digCount3++;
            // PASS SET
            // get the set quality based on the pass
            BsetNumber = BpassSet.GetSetNumber(digNumber);
            Debug.Log("B Set " + BsetNumber);

            // SET ATTACK
            // get the attack quality based on the set
            BattackNumber = BsetAttack.GetAttackNumber(BsetNumber);
            BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
            Debug.Log("B Hit " + BattackQuality);
            if (BattackQuality == 1)
            {
                Debug.Log("B Hitting Error");
                aCount++;
                errorCount++;
                return true;
            }
            // ATTACK DEFENCE
            // get the block and defence values
            AblockNumber = AattackDefence.GetBlockNumber();
            AblockQuality = AattackDefence.GetBlockQuality();
            AdefenceNumber = AattackDefence.GetDefenceNumber();
            // compare the attack values to the defence values
            resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber);
            if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
            {
                bool result4 = CompareResultsB(resultNumber);
                return result4;
            }
        }
        Debug.LogWarning("Didn't want it to get here");
        return true;
    }

    public bool SimulateRallyBServing()
    {
        isAteamServing = false;
        // SERVE PASS
        Debug.Log("B serves");
        BserveNumber = BservePass.GetServeNumber();
        ApassNumber = AservePass.GetPassNumber(BserveNumber);
        // check for aces or misses
        if (ApassNumber == 4)
        {
            Debug.Log("B Miss Serve");
            serrorCount++;
            aCount++;
            return false;
        }
        else if (ApassNumber == 0)
        {
            Debug.Log("B Ace");
            aceCount++;
            bCount++;
            return true;
        }
        Debug.Log("A Passed " + ApassNumber);

        // PASS SET
        // get the set quality based on the pass
        AsetNumber = ApassSet.GetSetNumber(ApassNumber);
        Debug.Log("A Set " + AsetNumber);

        // SET ATTACK
        // get the attack quality based on the set
        AattackNumber = AsetAttack.GetAttackNumber(AsetNumber);
        AattackQuality = AsetAttack.GetAttackQuality(AsetNumber, AattackNumber);
        Debug.Log("A Hit " + AattackQuality);
        if (AattackQuality == 1)
        {
            Debug.Log("A Hitting Error");
            bCount++;
            errorCount++;
            return true;
        }

        // ATTACK DEFENCE
        // get the block and defence values
        BblockNumber = BattackDefence.GetBlockNumber();
        BblockQuality = BattackDefence.GetBlockQuality();
        BdefenceNumber = BattackDefence.GetDefenceNumber();
        // compare the attack values to the defence values
        resultNumber = AattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber);
        if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
        {
            bool result = CompareResultsA(resultNumber);
            return result;
        }
        else
        {
            bool result2 = ContinueRallyBServing(resultNumber);
            return result2;
        }
    }

    private bool ContinueRallyBServing(int digNumber)
    {
        while (resultNumber == 2 || resultNumber == 1 || resultNumber == 3)
        {
            // A SIDE WITH A DIG
            Debug.Log("B Dug " + digNumber);
            if (digNumber == 1)
                digCount1++;
            if (digNumber == 2)
                digCount2++;
            if (digNumber == 3)
                digCount3++;
            // PASS SET
            // get the set quality based on the pass
            BsetNumber = BpassSet.GetSetNumber(digNumber);
            Debug.Log("B Set " + AsetNumber);

            // SET ATTACK
            // get the attack quality based on the set
            BattackNumber = BsetAttack.GetAttackNumber(BsetNumber);
            BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
            Debug.Log("B Hit " + BattackQuality);
            if (BattackQuality == 1)
            {
                Debug.Log("B Hitting Error");
                aCount++;
                errorCount++;
                return false;
            }
            // ATTACK DEFENCE
            // get the block and defence values
            AblockNumber = AattackDefence.GetBlockNumber();
            AblockQuality = AattackDefence.GetBlockQuality();
            AdefenceNumber = AattackDefence.GetDefenceNumber();
            // compare the attack values to the defence values
            resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber);
            if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
            {
                bool result3 = CompareResultsB(resultNumber);
                return result3;
            }




            // B SIDE WITH A DIG
            Debug.Log("A Dug " + digNumber);
            if (resultNumber == 1)
                digCount1++;
            if (resultNumber == 2)
                digCount2++;
            if (resultNumber == 3)
                digCount3++;
            // PASS SET
            // get the set quality based on the pass
            AsetNumber = ApassSet.GetSetNumber(digNumber);
            Debug.Log("A Set " + AsetNumber);

            // SET ATTACK
            // get the attack quality based on the set
            AattackNumber = AsetAttack.GetAttackNumber(AsetNumber);
            AattackQuality = AsetAttack.GetAttackQuality(AsetNumber, AattackNumber);
            Debug.Log("A Hit " + AattackQuality);
            if (AattackQuality == 1)
            {
                Debug.Log("A Hitting Error");
                bCount++;
                errorCount++;
                return true;
            }
            // ATTACK DEFENCE
            // get the block and defence values
            BblockNumber = BattackDefence.GetBlockNumber();
            BblockQuality = BattackDefence.GetBlockQuality();
            BdefenceNumber = BattackDefence.GetDefenceNumber();
            // compare the attack values to the defence values
            resultNumber = AattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber);
            if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
            {
                bool result4 = CompareResultsA(resultNumber);
                return result4;
            }
        }
        Debug.LogWarning("Didn't want it to get here");
        return true;
    }

    private bool CompareResultsA(int result)
    {
        if (result == 100)
        {
            Debug.Log("B Block");
            bCount++;
            blockCount++;
            // check who's serving, returning true for a serving team won point, false for the recieving team winning the point
            if (isAteamServing)
                return false;
            else return true;
        }
        else if(result == -1)
        {
            Debug.Log("A Tool");
            toolCount++;
            aCount++;
            if (isAteamServing)
                return true;
            else return false;
        }
        else if(result == 2 || result == 3 || result == 1)
        {
            Debug.Log("Dig, rally continues");
            if (result == 1)
                digCount1++;
            if (result == 2)
                digCount2++;
            if (result == 3)
                digCount3++;
            //return;
        }
        else if(result == 0)
        {
            Debug.Log("A Attack Lands for a Kill");
            aCount++;
            killCount++;
            if (isAteamServing)
                return true;
            else return false;
        }
        else
        {
            Debug.Log("Something weird happened");
            // return;
        }
        Debug.LogWarning("Shouldn't have gotten here");
        return false;
    }

    private bool CompareResultsB(int result)
    {
        if (result == 100)
        {
            Debug.Log("A Block");
            aCount++;
            blockCount++;
            if (isAteamServing)
                return true;
            else return false;
        }
        else if (result == -1)
        {
            Debug.Log("B Tool");
            bCount++;
            toolCount++;
            if (isAteamServing)
                return false;
            else return true;
        }
        else if (result == 2 || result == 3 || result == 1)
        {
            Debug.Log("Dig, rally continues");
            if (result == 1)
                digCount1++;
            if (result == 2)
                digCount2++;
            if (result == 3)
                digCount3++;
            //return;
        }
        else if (result == 0)
        {
            Debug.Log("B Attack Lands for a Kill");
            killCount++;
            bCount++;
            if (isAteamServing)
                return false;
            else return true;
        }
        else
        {
            Debug.Log("Something weird happened");
            //return;
        }
        Debug.LogWarning("Shouldn't have gotten here");
        return false;
    }
}

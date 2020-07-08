using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class RallyManagerV2 : MonoBehaviour
{

    [SerializeField] private SkillManager skillManager;

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

    private int resultNumber = 0;

    private bool isAteamServing = true;

    [SerializeField] private GameObject setDecisionButtons;
    [SerializeField] private GameObject setLeftSideButton;
    [SerializeField] private GameObject setMiddleButton;
    [SerializeField] private GameObject setRightSideButton;
    private bool playerSetDecision = false;
    private SkillManager.PlayerSkills setChoiceSkills;
    private SkillManager.PlayerSkills AIsetChoiceSkills;

    [SerializeField] private Text messageText;



    // Start is called before the first frame update
    void Start()
    {
        // SimulateRally();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SimulateOneRallyPlayerServe()
    {
        StartCoroutine(SimulateRallyAServing());
    }

    public void SimulateOneRallyAIServe()
    {
        StartCoroutine(SimulateRallyBServing());
    }

    //private IEnumerator PlayerSetDecision()
    //{
    //    playerSetDecision = false;
    //    setDecisionButtons.SetActive(true);
    //    yield return new WaitUntil(() => playerSetDecision);
        
    //    setDecisionButtons.SetActive(false);
    //    yield return null;
    //}

    public void PlayerSetLeftSide()
    {
        Debug.Log("Setting left side");
        playerSetDecision = true;
        setChoiceSkills = skillManager.PlayerP1;
    }

    public void PlayerSetMiddle()
    {
        Debug.Log("Setting middle");
        playerSetDecision = true;
        setChoiceSkills = skillManager.PlayerM1;
    }

    public void PlayerSetRightSide()
    {
        Debug.Log("Setting right side");
        playerSetDecision = true;
        setChoiceSkills = skillManager.PlayerRS;
    }

    public IEnumerator SimulateRallyAServing()
    {
        // set the players team skills to whats on the sliders
        skillManager.SetPlayersTeamSkills();


        isAteamServing = true;
        // SERVE PASS
        Debug.Log("A serves");
        AservePass.SetServeAbility(skillManager.AIM2.serve);
        Debug.Log("Servers skill is: " + skillManager.AIM2.serve);
        AserveNumber = AservePass.GetServeNumber();

        BservePass.SetPassAbility(skillManager.AIP2.pass);
        Debug.Log("Passers skill is: " + skillManager.AIP2.pass);
        BpassNumber = BservePass.GetPassNumber(AserveNumber);

        messageText.text = "Player serves";
        yield return new WaitForSeconds(1);

        // check for aces or misses
        if (BpassNumber == 4)
        {
            messageText.text = "Player crushed it into the net";
            Debug.Log("A Miss Serve");
            yield return false;
            yield break;
        }
        else if (BpassNumber == 0)
        {
            messageText.text = "Player rips an ace";
            Debug.Log("A Ace");
            yield return true;
            yield break;
        }
        Debug.Log("B Passed " + BpassNumber);
        messageText.text = "AI passes it up";
        yield return new WaitForSeconds(1);

        // PASS SET
        // SET CHOICE
        AIsetChoiceSkills = AISetSelection(BpassNumber);

        messageText.text = "AI making a set choice";
        yield return new WaitForSeconds(1);


        // get the set quality based on the pass
        BpassSet.SetSettingAbility(skillManager.AIS.set);
        Debug.Log("Setters Skill is: " + skillManager.AIS.set);
        BsetNumber = BpassSet.GetSetNumber(BpassNumber);
        Debug.Log("B Set " + BsetNumber);

        // SET ATTACK
        // get the attack quality based on the set
        BsetAttack.SetAttackAbility(AIsetChoiceSkills.attack);
        Debug.Log("Attackers skill is: " + AIsetChoiceSkills.attack);
        BattackNumber = BsetAttack.GetAttackNumber(BsetNumber);
        BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
        Debug.Log("B Hit " + BattackQuality);
        if (BattackQuality == 1)
        {
            messageText.text = "AI pummels it into the net";
            Debug.Log("B Hitting Error");
            yield return true;
            yield break;
        }

        // ATTACK DEFENCE
        // get the block and defence values
        AattackDefence.SetBlockAbility(skillManager.PlayerM1.block);
        Debug.Log("Blockers skill is: " + skillManager.PlayerM1.block);
        AblockNumber = AattackDefence.GetBlockNumber();
        AblockQuality = AattackDefence.GetBlockQuality();
        AattackDefence.SetDefenceAbility(skillManager.PlayerP2.defence);
        Debug.Log("Defenders skill is: " + skillManager.PlayerP2.defence);
        AdefenceNumber = AattackDefence.GetDefenceNumber();
        // compare the attack values to the defence values
        resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber);
        if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
        {
            bool result = CompareResultsB(resultNumber);
            yield return result;
            yield break;
        }
        else
        {
            // bool result2 = ContinueRallyAServing(resultNumber);
            // return result2;
            while (resultNumber == 2 || resultNumber == 1 || resultNumber == 3)
            {
                int digNumber = resultNumber;
                // A SIDE WITH A DIG
                Debug.Log("A Dug " + digNumber);
                messageText.text = "Player digs it up";
                yield return new WaitForSeconds(1);

                // PASS SET

                // SET CHOICE
                playerSetDecision = false;
                PlayerSetChoiceButtonsActivate(digNumber);
                messageText.text = "Player choose who to set";
                yield return new WaitUntil(() => playerSetDecision);
                setLeftSideButton.SetActive(false);
                setMiddleButton.SetActive(false);
                setRightSideButton.SetActive(false);

                // get the set quality based on the pass
                ApassSet.SetSettingAbility(skillManager.PlayerS.set);
                Debug.Log("Setters Skill is: " + skillManager.PlayerS.set);
                AsetNumber = ApassSet.GetSetNumber(digNumber);
                Debug.Log("A Set " + AsetNumber);

                messageText.text = "Player sets it up";
                yield return new WaitForSeconds(1);

                // SET ATTACK
                // get the attack quality based on the set
                AsetAttack.SetAttackAbility(setChoiceSkills.attack);
                Debug.Log("Attacker skill is: " + setChoiceSkills.attack);
                AattackNumber = AsetAttack.GetAttackNumber(AsetNumber);
                AattackQuality = AsetAttack.GetAttackQuality(AsetNumber, AattackNumber);
                Debug.Log("A Hit " + AattackQuality);
                if (AattackQuality == 1)
                {
                    Debug.Log("A Hitting Error");
                    messageText.text = "Player hits it 10 bricks up the wall";
                    yield return false;
                    yield break;
                }
                // ATTACK DEFENCE
                // get the block and defence values
                BattackDefence.SetBlockAbility(skillManager.AIM1.block);
                Debug.Log("Blockers skill is: " + skillManager.AIM1.block);
                BblockNumber = BattackDefence.GetBlockNumber();
                BblockQuality = BattackDefence.GetBlockQuality();
                BattackDefence.SetDefenceAbility(skillManager.AIP2.defence);
                Debug.Log("Defenders skill is: " + skillManager.AIP2.defence);
                BdefenceNumber = BattackDefence.GetDefenceNumber();

                messageText.text = "Player attacking against a " + BblockQuality / 2 + " person block";
                yield return new WaitForSeconds(1);

                // compare the attack values to the defence values
                resultNumber = BattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber);
                if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
                {
                    bool result3 = CompareResultsA(resultNumber);
                    yield return result3;
                    yield break;
                }
                digNumber = resultNumber;



                // B SIDE WITH A DIG
                Debug.Log("B Dug " + digNumber);
                messageText.text = "AI digs it up";
                yield return new WaitForSeconds(1);

                // PASS SET
                // SET CHOICE
                AIsetChoiceSkills = AISetSelection(digNumber);
                messageText.text = "AI making a set choice";
                yield return new WaitForSeconds(1);

                // get the set quality based on the pass
                BpassSet.SetSettingAbility(skillManager.AIS.set);
                Debug.Log("Setters Skill is: " + skillManager.AIS.set);
                BsetNumber = BpassSet.GetSetNumber(digNumber);
                Debug.Log("B Set " + BsetNumber);

                // SET ATTACK
                // get the attack quality based on the set
                BsetAttack.SetAttackAbility(AIsetChoiceSkills.attack);
                Debug.Log("Attackers skill is: " + AIsetChoiceSkills.attack);
                BattackNumber = BsetAttack.GetAttackNumber(BsetNumber);
                BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
                Debug.Log("B Hit " + BattackQuality);
                if (BattackQuality == 1)
                {
                    Debug.Log("B Hitting Error");
                    messageText.text = "AI pummels it into the net";
                    yield return true;
                    yield break;
                }
                // ATTACK DEFENCE
                // get the block and defence values
                AattackDefence.SetBlockAbility(skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                Debug.Log("Blockers skill is: " + skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                AblockNumber = AattackDefence.GetBlockNumber();
                AblockQuality = AattackDefence.GetBlockQuality();
                AattackDefence.SetDefenceAbility(skillManager.P2Sliders.transform.Find("DefenceSlider").GetComponent<Slider>().value);
                Debug.Log("Defenders skill is: " + skillManager.P2Sliders.transform.Find("DefenceSlider").GetComponent<Slider>().value);
                AdefenceNumber = AattackDefence.GetDefenceNumber();

                messageText.text = "AI attacking against a " + AblockQuality / 2 + " person block";
                yield return new WaitForSeconds(1);

                // compare the attack values to the defence values
                resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber);
                if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
                {
                    bool result4 = CompareResultsB(resultNumber);
                    yield return result4;
                    yield break;
                }
            }
            Debug.LogWarning("Didn't want it to get here");
            yield return true;
        }
        
    }

    public IEnumerator SimulateRallyBServing()
    {
        // set the players team skills to whats on the sliders
        skillManager.SetPlayersTeamSkills();

        isAteamServing = false;
        // SERVE PASS
        Debug.Log("B serves");
        BservePass.SetServeAbility(skillManager.AIM2.serve);
        Debug.Log("Servers skill is: " + skillManager.AIM2.serve);
        BserveNumber = BservePass.GetServeNumber();
        AservePass.SetPassAbility(skillManager.PlayerP2.pass);
        Debug.Log("Passers skill is: " + skillManager.PlayerP2.pass);
        ApassNumber = AservePass.GetPassNumber(BserveNumber);

        messageText.text = "AI serves";
        yield return new WaitForSeconds(1);

        // check for aces or misses
        if (ApassNumber == 4)
        {
            messageText.text = "AI crushed it into the net";
            Debug.Log("B Miss Serve");
            yield return false;
            yield break;
        }
        else if (ApassNumber == 0)
        { 
            messageText.text = "AI rips an ace";
            Debug.Log("B Ace");
            yield return true;
            yield break;
        }
        Debug.Log("A Passed " + ApassNumber);
        messageText.text = "Player passes it up";
        yield return new WaitForSeconds(1);
        // PASS SET

        // SET CHOICE
        playerSetDecision = false;
        PlayerSetChoiceButtonsActivate(ApassNumber);
        
        messageText.text = "Player choose who to set";
        yield return new WaitUntil(() => playerSetDecision);

        setLeftSideButton.SetActive(false);
        setMiddleButton.SetActive(false);
        setRightSideButton.SetActive(false);

        // get the set quality based on the pass
        ApassSet.SetSettingAbility(skillManager.PlayerS.set);
        Debug.Log("Setters Skill is: " + skillManager.PlayerS.set);
        AsetNumber = ApassSet.GetSetNumber(ApassNumber);
        Debug.Log("A Set " + AsetNumber);

        // SET ATTACK
        messageText.text = "Player sets it up";
        yield return new WaitForSeconds(1);

        // get the attack quality based on the set
        AsetAttack.SetAttackAbility(setChoiceSkills.attack);
        Debug.Log("Attacker skill is: " + setChoiceSkills.attack);
        AattackNumber = AsetAttack.GetAttackNumber(AsetNumber);
        AattackQuality = AsetAttack.GetAttackQuality(AsetNumber, AattackNumber);
        Debug.Log("A Hit " + AattackQuality);
        if (AattackQuality == 1)
        {
            messageText.text = "Player pounds it out of bounds";
            Debug.Log("A Hitting Error");
            yield return true;
            yield break;
        }

        // ATTACK DEFENCE
        // get the block and defence values
        BattackDefence.SetBlockAbility(skillManager.AIM1.block);
        Debug.Log("Blockers skill is: " + skillManager.AIM1.block);
        BblockNumber = BattackDefence.GetBlockNumber();
        BblockQuality = BattackDefence.GetBlockQuality();
        BattackDefence.SetDefenceAbility(skillManager.AIP2.defence);
        Debug.Log("Defenders skill is: " + skillManager.AIP2.defence);
        BdefenceNumber = BattackDefence.GetDefenceNumber();

        messageText.text = "Player attacking against a " + BblockQuality/2 + " person block";
        yield return new WaitForSeconds(1);

        // compare the attack values to the defence values
        resultNumber = AattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber);
        if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
        {
            bool result = CompareResultsA(resultNumber);
            yield return result;
            yield break;
        }
        else
        {
            //bool result2 = ContinueRallyBServing(resultNumber);
            //return result2;
            
            while (resultNumber == 2 || resultNumber == 1 || resultNumber == 3)
            {
                int digNumber = resultNumber;
                // A SIDE WITH A DIG
                Debug.Log("B Dug " + digNumber);
                messageText.text = "AI digs it up";
                yield return new WaitForSeconds(1);

                // PASS SET
                // SET CHOICE
                AIsetChoiceSkills = AISetSelection(digNumber);

                messageText.text = "AI making a set choice";
                yield return new WaitForSeconds(1);


                // get the set quality based on the pass
                BpassSet.SetSettingAbility(skillManager.AIS.set);
                Debug.Log("Setters Skill is: " + skillManager.AIS.set);
                BsetNumber = BpassSet.GetSetNumber(digNumber);
                Debug.Log("B Set " + AsetNumber);

                // SET ATTACK
                // get the attack quality based on the set
                BsetAttack.SetAttackAbility(AIsetChoiceSkills.attack);
                Debug.Log("Attackers skill is: " + AIsetChoiceSkills.attack);
                BattackNumber = BsetAttack.GetAttackNumber(BsetNumber);
                BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
                Debug.Log("B Hit " + BattackQuality);



                if (BattackQuality == 1)
                {
                    Debug.Log("B Hitting Error");
                    messageText.text = "AI pummels it into the net";
                    yield return false;
                    yield break;
                }
                // ATTACK DEFENCE
                // get the block and defence values
                AattackDefence.SetBlockAbility(skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                Debug.Log("Blockers skill is: " + skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                AblockNumber = AattackDefence.GetBlockNumber();
                AblockQuality = AattackDefence.GetBlockQuality();
                AattackDefence.SetDefenceAbility(skillManager.P2Sliders.transform.Find("DefenceSlider").GetComponent<Slider>().value);
                Debug.Log("Defenders skill is: " + skillManager.P2Sliders.transform.Find("DefenceSlider").GetComponent<Slider>().value);
                AdefenceNumber = AattackDefence.GetDefenceNumber();

                messageText.text = "AI attacking against a " + AblockQuality / 2 + " person block";
                yield return new WaitForSeconds(1);

                // compare the attack values to the defence values
                resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber);
                if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
                {
                    bool result3 = CompareResultsB(resultNumber);
                    yield return result3;
                    yield break;
                }



                digNumber = resultNumber;
                // B SIDE WITH A DIG
                Debug.Log("A Dug " + digNumber);
                messageText.text = "Player digs it up";
                yield return new WaitForSeconds(1);
                // PASS SET

                // SET CHOICE
                playerSetDecision = false;
                PlayerSetChoiceButtonsActivate(ApassNumber);
                messageText.text = "Player choose who to set";
                yield return new WaitUntil(() => playerSetDecision);

                setLeftSideButton.SetActive(false);
                setMiddleButton.SetActive(false);
                setRightSideButton.SetActive(false);

                // get the set quality based on the pass
                ApassSet.SetSettingAbility(skillManager.SetterSliders.transform.Find("SetSlider").GetComponent<Slider>().value);
                Debug.Log("Setters Skill is: " + skillManager.SetterSliders.transform.Find("SetSlider").GetComponent<Slider>().value);
                AsetNumber = ApassSet.GetSetNumber(digNumber);
                Debug.Log("A Set " + AsetNumber);

                messageText.text = "Player sets it up";
                yield return new WaitForSeconds(1);

                // SET ATTACK
                // get the attack quality based on the set
                AsetAttack.SetAttackAbility(setChoiceSkills.attack);
                Debug.Log("Attacker skill is: " + setChoiceSkills.attack);
                AattackNumber = AsetAttack.GetAttackNumber(AsetNumber);
                AattackQuality = AsetAttack.GetAttackQuality(AsetNumber, AattackNumber);
                Debug.Log("A Hit " + AattackQuality);
                if (AattackQuality == 1)
                {
                    Debug.Log("A Hitting Error");
                    messageText.text = "Player hits it 10 bricks up the wall";
                    yield return true;
                    yield break;
                }
                // ATTACK DEFENCE
                // get the block and defence values
                BattackDefence.SetBlockAbility(skillManager.AIM1.block);
                Debug.Log("Blockers skill is: " + skillManager.AIM1.block);
                BblockNumber = BattackDefence.GetBlockNumber();
                BblockQuality = BattackDefence.GetBlockQuality();
                BattackDefence.SetDefenceAbility(skillManager.AIP2.defence);
                Debug.Log("Defenders skill is: " + skillManager.AIP2.defence);
                BdefenceNumber = BattackDefence.GetDefenceNumber();

                messageText.text = "Player attacking against a " + BblockQuality / 2 + " person block";
                yield return new WaitForSeconds(1);

                // compare the attack values to the defence values
                resultNumber = AattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber);
                if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
                {
                    bool result4 = CompareResultsA(resultNumber);
                    yield return result4;
                    yield break;
                }
            }
            Debug.LogWarning("Didn't want it to get here");
            yield return true;
        }
    }
    

    private bool CompareResultsA(int result)
    {
        if (result == 100)
        {
            Debug.Log("B Block");
            messageText.text = "AI slams the player";
            // check who's serving, returning true for a serving team won point, false for the recieving team winning the point
            if (isAteamServing)
                return false;
            else return true;
        }
        else if (result == -1)
        {
            messageText.text = "Player tools the block hard";
            Debug.Log("A Tool");
            if (isAteamServing)
                return true;
            else return false;
        }
        else if (result == 2 || result == 3 || result == 1)
        {
            Debug.Log("Dig, rally continues");
            //return;
        }
        else if (result == 0)
        {
            messageText.text = "Player pounds it past the defence for a point";
            Debug.Log("A Attack Lands for a Kill");
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
            messageText.text = "Player slams the AI";
            Debug.Log("A Block");
            if (isAteamServing)
                return true;
            else return false;
        }
        else if (result == -1)
        {
            messageText.text = "AI tools the player easily";
            Debug.Log("B Tool");
            if (isAteamServing)
                return false;
            else return true;
        }
        else if (result == 2 || result == 3 || result == 1)
        {
            Debug.Log("Dig, rally continues");
            //return;
        }
        else if (result == 0)
        {
            messageText.text = "AI bounces it on the player";
            Debug.Log("B Attack Lands for a Kill");
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




    private SkillManager.PlayerSkills AISetSelection(int digNumber)
    {
        if (digNumber == 1)
        {
            Debug.Log("AI forced to set left side");
            return skillManager.AIP1;
        }
        else if (digNumber == 2)
        {
            int setChoice = Mathf.CeilToInt(UnityEngine.Random.Range(0, 2));
            if (setChoice == 1)
            {
                Debug.Log("AI sets left side");
                return skillManager.AIP1;
            }
            else
            {
                Debug.Log("AI sets right side");
                return skillManager.AIRS;
            }
        }
        else if (digNumber == 3)
        {
            int setChoice = Mathf.CeilToInt(UnityEngine.Random.Range(0, 3));
            if (setChoice == 1)
            {
                Debug.Log("AI sets left side");
                return skillManager.AIP1;
            }
            else if (setChoice == 2)
            {
                Debug.Log("AI sets right side");
                return skillManager.AIRS;
            }
            else
            {
                Debug.Log("AI sets middle");
                return skillManager.AIM1;
            }
        }
        return skillManager.AIP1;
    }

    private void PlayerSetChoiceButtonsActivate(int passNumber)
    {
        if (passNumber > 0)
        {
            setLeftSideButton.SetActive(true);
        }
        if(passNumber > 1)
        {
            setRightSideButton.SetActive(true);
        }
        if(passNumber > 2)
        {
            setMiddleButton.SetActive(true);
        }
        Debug.Log("Activated the buttons fine");
        
    }















































    public IEnumerator SimulateRallyAServing(Action trueCallback, Action falseCallback)
    {
        // set the players team skills to whats on the sliders
        skillManager.SetPlayersTeamSkills();


        isAteamServing = true;
        // SERVE PASS
        Debug.Log("A serves");
        AservePass.SetServeAbility(skillManager.AIM2.serve);
        Debug.Log("Servers skill is: " + skillManager.AIM2.serve);
        AserveNumber = AservePass.GetServeNumber();

        BservePass.SetPassAbility(skillManager.AIP2.pass);
        Debug.Log("Passers skill is: " + skillManager.AIP2.pass);
        BpassNumber = BservePass.GetPassNumber(AserveNumber);

        messageText.text = "Player serves";
        yield return new WaitForSeconds(1);

        // check for aces or misses
        if (BpassNumber == 4)
        {
            messageText.text = "Player crushed it into the net";
            Debug.Log("A Miss Serve");
            falseCallback();
            yield return false;
            yield break;
        }
        else if (BpassNumber == 0)
        {
            messageText.text = "Player rips an ace";
            Debug.Log("A Ace");
            trueCallback();
            yield return true;
            yield break;
        }
        Debug.Log("B Passed " + BpassNumber);
        messageText.text = "AI passes it up";
        yield return new WaitForSeconds(1);

        // PASS SET
        // SET CHOICE
        AIsetChoiceSkills = AISetSelection(BpassNumber);

        messageText.text = "AI making a set choice";
        yield return new WaitForSeconds(1);


        // get the set quality based on the pass
        BpassSet.SetSettingAbility(skillManager.AIS.set);
        Debug.Log("Setters Skill is: " + skillManager.AIS.set);
        BsetNumber = BpassSet.GetSetNumber(BpassNumber);
        Debug.Log("B Set " + BsetNumber);

        // SET ATTACK
        // get the attack quality based on the set
        BsetAttack.SetAttackAbility(AIsetChoiceSkills.attack);
        Debug.Log("Attackers skill is: " + AIsetChoiceSkills.attack);
        BattackNumber = BsetAttack.GetAttackNumber(BsetNumber);
        BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
        Debug.Log("B Hit " + BattackQuality);
        if (BattackQuality == 1)
        {
            messageText.text = "AI pummels it into the net";
            Debug.Log("B Hitting Error");
            trueCallback();
            yield return true;
            yield break;
        }

        // ATTACK DEFENCE
        // get the block and defence values
        AattackDefence.SetBlockAbility(skillManager.PlayerM1.block);
        Debug.Log("Blockers skill is: " + skillManager.PlayerM1.block);
        AblockNumber = AattackDefence.GetBlockNumber();
        AblockQuality = AattackDefence.GetBlockQuality();
        AattackDefence.SetDefenceAbility(skillManager.PlayerP2.defence);
        Debug.Log("Defenders skill is: " + skillManager.PlayerP2.defence);
        AdefenceNumber = AattackDefence.GetDefenceNumber();

        messageText.text = "AI attacking against a " + AblockQuality / 2 + " person block";
        yield return new WaitForSeconds(1);

        // compare the attack values to the defence values
        resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber);
        if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
        {
            bool result = CompareResultsB(resultNumber);
            if (result)
                trueCallback();
            else falseCallback();
            yield return result;
            yield break;
        }
        else
        {
            // bool result2 = ContinueRallyAServing(resultNumber);
            // return result2;
            while (resultNumber == 2 || resultNumber == 1 || resultNumber == 3)
            {
                int digNumber = resultNumber;
                // A SIDE WITH A DIG
                Debug.Log("A Dug " + digNumber);
                messageText.text = "Player digs it up";
                yield return new WaitForSeconds(1);

                // PASS SET

                // SET CHOICE
                playerSetDecision = false;
                PlayerSetChoiceButtonsActivate(digNumber);
                messageText.text = "Player choose who to set";
                yield return new WaitUntil(() => playerSetDecision);
                setLeftSideButton.SetActive(false);
                setMiddleButton.SetActive(false);
                setRightSideButton.SetActive(false);

                // get the set quality based on the pass
                ApassSet.SetSettingAbility(skillManager.PlayerS.set);
                Debug.Log("Setters Skill is: " + skillManager.PlayerS.set);
                AsetNumber = ApassSet.GetSetNumber(digNumber);
                Debug.Log("A Set " + AsetNumber);

                messageText.text = "Player sets it up";
                yield return new WaitForSeconds(1);

                // SET ATTACK
                // get the attack quality based on the set
                AsetAttack.SetAttackAbility(setChoiceSkills.attack);
                Debug.Log("Attacker skill is: " + setChoiceSkills.attack);
                AattackNumber = AsetAttack.GetAttackNumber(AsetNumber);
                AattackQuality = AsetAttack.GetAttackQuality(AsetNumber, AattackNumber);
                Debug.Log("A Hit " + AattackQuality);
                if (AattackQuality == 1)
                {
                    Debug.Log("A Hitting Error");
                    messageText.text = "Player hits it 10 bricks up the wall";
                    falseCallback();
                    yield return false;
                    yield break;
                }
                // ATTACK DEFENCE
                // get the block and defence values
                BattackDefence.SetBlockAbility(skillManager.AIM1.block);
                Debug.Log("Blockers skill is: " + skillManager.AIM1.block);
                BblockNumber = BattackDefence.GetBlockNumber();
                BblockQuality = BattackDefence.GetBlockQuality();
                BattackDefence.SetDefenceAbility(skillManager.AIP2.defence);
                Debug.Log("Defenders skill is: " + skillManager.AIP2.defence);
                BdefenceNumber = BattackDefence.GetDefenceNumber();

                messageText.text = "Player attacking against a " + BblockQuality / 2 + " person block";
                yield return new WaitForSeconds(1);

                // compare the attack values to the defence values
                resultNumber = BattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber);
                if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
                {
                    bool result3 = CompareResultsA(resultNumber);
                    if (result3)
                        trueCallback();
                    else falseCallback();
                    yield return result3;
                    yield break;
                }
                digNumber = resultNumber;



                // B SIDE WITH A DIG
                Debug.Log("B Dug " + digNumber);
                messageText.text = "AI digs it up";
                yield return new WaitForSeconds(1);

                // PASS SET
                // SET CHOICE
                AIsetChoiceSkills = AISetSelection(digNumber);
                messageText.text = "AI making a set choice";
                yield return new WaitForSeconds(1);

                // get the set quality based on the pass
                BpassSet.SetSettingAbility(skillManager.AIS.set);
                Debug.Log("Setters Skill is: " + skillManager.AIS.set);
                BsetNumber = BpassSet.GetSetNumber(digNumber);
                Debug.Log("B Set " + BsetNumber);

                // SET ATTACK
                // get the attack quality based on the set
                BsetAttack.SetAttackAbility(AIsetChoiceSkills.attack);
                Debug.Log("Attackers skill is: " + AIsetChoiceSkills.attack);
                BattackNumber = BsetAttack.GetAttackNumber(BsetNumber);
                BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
                Debug.Log("B Hit " + BattackQuality);
                if (BattackQuality == 1)
                {
                    Debug.Log("B Hitting Error");
                    messageText.text = "AI pummels it into the net";
                    trueCallback();
                    yield return true;
                    yield break;
                }
                // ATTACK DEFENCE
                // get the block and defence values
                AattackDefence.SetBlockAbility(skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                Debug.Log("Blockers skill is: " + skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                AblockNumber = AattackDefence.GetBlockNumber();
                AblockQuality = AattackDefence.GetBlockQuality();
                AattackDefence.SetDefenceAbility(skillManager.P2Sliders.transform.Find("DefenceSlider").GetComponent<Slider>().value);
                Debug.Log("Defenders skill is: " + skillManager.P2Sliders.transform.Find("DefenceSlider").GetComponent<Slider>().value);
                AdefenceNumber = AattackDefence.GetDefenceNumber();

                messageText.text = "AI attacking against a " + AblockQuality / 2 + " person block";
                yield return new WaitForSeconds(1);

                // compare the attack values to the defence values
                resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber);
                if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
                {
                    bool result4 = CompareResultsB(resultNumber);
                    if (result4)
                        trueCallback();
                    else falseCallback();
                    yield return result4;
                    yield break;
                }
            }
            Debug.LogWarning("Didn't want it to get here");
            yield return true;
            yield break;
        }

    }


    public IEnumerator SimulateRallyBServing(Action trueCallback, Action falseCallback)
    {
        // set the players team skills to whats on the sliders
        skillManager.SetPlayersTeamSkills();

        isAteamServing = false;
        // SERVE PASS
        Debug.Log("B serves");
        BservePass.SetServeAbility(skillManager.AIM2.serve);
        Debug.Log("Servers skill is: " + skillManager.AIM2.serve);
        BserveNumber = BservePass.GetServeNumber();
        AservePass.SetPassAbility(skillManager.PlayerP2.pass);
        Debug.Log("Passers skill is: " + skillManager.PlayerP2.pass);
        ApassNumber = AservePass.GetPassNumber(BserveNumber);

        messageText.text = "AI serves";
        yield return new WaitForSeconds(1);

        // check for aces or misses
        if (ApassNumber == 4)
        {
            messageText.text = "AI crushed it into the net";
            Debug.Log("B Miss Serve");
            falseCallback();
            yield return false;
            yield break;
        }
        else if (ApassNumber == 0)
        {
            messageText.text = "AI rips an ace";
            Debug.Log("B Ace");
            trueCallback();
            yield return true;
            yield break;
        }
        Debug.Log("A Passed " + ApassNumber);
        messageText.text = "Player passes it up";
        yield return new WaitForSeconds(1);
        // PASS SET

        // SET CHOICE
        playerSetDecision = false;
        PlayerSetChoiceButtonsActivate(ApassNumber);

        messageText.text = "Player choose who to set";
        yield return new WaitUntil(() => playerSetDecision);

        setLeftSideButton.SetActive(false);
        setMiddleButton.SetActive(false);
        setRightSideButton.SetActive(false);

        // get the set quality based on the pass
        ApassSet.SetSettingAbility(skillManager.PlayerS.set);
        Debug.Log("Setters Skill is: " + skillManager.PlayerS.set);
        AsetNumber = ApassSet.GetSetNumber(ApassNumber);
        Debug.Log("A Set " + AsetNumber);

        // SET ATTACK
        messageText.text = "Player sets it up";
        yield return new WaitForSeconds(1);

        // get the attack quality based on the set
        AsetAttack.SetAttackAbility(setChoiceSkills.attack);
        Debug.Log("Attacker skill is: " + setChoiceSkills.attack);
        AattackNumber = AsetAttack.GetAttackNumber(AsetNumber);
        AattackQuality = AsetAttack.GetAttackQuality(AsetNumber, AattackNumber);
        Debug.Log("A Hit " + AattackQuality);
        if (AattackQuality == 1)
        {
            messageText.text = "Player pounds it out of bounds";
            Debug.Log("A Hitting Error");
            trueCallback();
            yield return true;
            yield break;
        }

        // ATTACK DEFENCE
        // get the block and defence values
        BattackDefence.SetBlockAbility(skillManager.AIM1.block);
        Debug.Log("Blockers skill is: " + skillManager.AIM1.block);
        BblockNumber = BattackDefence.GetBlockNumber();
        BblockQuality = BattackDefence.GetBlockQuality();
        BattackDefence.SetDefenceAbility(skillManager.AIP2.defence);
        Debug.Log("Defenders skill is: " + skillManager.AIP2.defence);
        BdefenceNumber = BattackDefence.GetDefenceNumber();

        messageText.text = "Player attacking against a " + BblockQuality / 2 + " person block";
        yield return new WaitForSeconds(1);

        // compare the attack values to the defence values
        resultNumber = AattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber);
        if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
        {
            bool result = CompareResultsA(resultNumber);
            if (result) trueCallback();
            else falseCallback();
            yield return result;
            yield break;
        }
        else
        {
            //bool result2 = ContinueRallyBServing(resultNumber);
            //return result2;

            while (resultNumber == 2 || resultNumber == 1 || resultNumber == 3)
            {
                int digNumber = resultNumber;
                // A SIDE WITH A DIG
                Debug.Log("B Dug " + digNumber);
                messageText.text = "AI digs it up";
                yield return new WaitForSeconds(1);

                // PASS SET
                // SET CHOICE
                AIsetChoiceSkills = AISetSelection(digNumber);

                messageText.text = "AI making a set choice";
                yield return new WaitForSeconds(1);


                // get the set quality based on the pass
                BpassSet.SetSettingAbility(skillManager.AIS.set);
                Debug.Log("Setters Skill is: " + skillManager.AIS.set);
                BsetNumber = BpassSet.GetSetNumber(digNumber);
                Debug.Log("B Set " + AsetNumber);

                // SET ATTACK
                // get the attack quality based on the set
                BsetAttack.SetAttackAbility(AIsetChoiceSkills.attack);
                Debug.Log("Attackers skill is: " + AIsetChoiceSkills.attack);
                BattackNumber = BsetAttack.GetAttackNumber(BsetNumber);
                BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
                Debug.Log("B Hit " + BattackQuality);



                if (BattackQuality == 1)
                {
                    Debug.Log("B Hitting Error");
                    messageText.text = "AI pummels it into the net";
                    falseCallback();
                    yield return false;
                    yield break;
                }
                // ATTACK DEFENCE
                // get the block and defence values
                AattackDefence.SetBlockAbility(skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                Debug.Log("Blockers skill is: " + skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                AblockNumber = AattackDefence.GetBlockNumber();
                AblockQuality = AattackDefence.GetBlockQuality();
                AattackDefence.SetDefenceAbility(skillManager.P2Sliders.transform.Find("DefenceSlider").GetComponent<Slider>().value);
                Debug.Log("Defenders skill is: " + skillManager.P2Sliders.transform.Find("DefenceSlider").GetComponent<Slider>().value);
                AdefenceNumber = AattackDefence.GetDefenceNumber();

                messageText.text = "AI attacking against a " + AblockQuality / 2 + " person block";
                yield return new WaitForSeconds(1);

                // compare the attack values to the defence values
                resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber);
                if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
                {
                    bool result3 = CompareResultsB(resultNumber);
                    if (result3) trueCallback();
                    else falseCallback();
                    yield return result3;
                    yield break;
                }



                digNumber = resultNumber;
                // B SIDE WITH A DIG
                Debug.Log("A Dug " + digNumber);
                messageText.text = "Player digs it up";
                yield return new WaitForSeconds(1);
                // PASS SET

                // SET CHOICE
                playerSetDecision = false;
                PlayerSetChoiceButtonsActivate(ApassNumber);
                messageText.text = "Player choose who to set";
                yield return new WaitUntil(() => playerSetDecision);

                setLeftSideButton.SetActive(false);
                setMiddleButton.SetActive(false);
                setRightSideButton.SetActive(false);

                // get the set quality based on the pass
                ApassSet.SetSettingAbility(skillManager.SetterSliders.transform.Find("SetSlider").GetComponent<Slider>().value);
                Debug.Log("Setters Skill is: " + skillManager.SetterSliders.transform.Find("SetSlider").GetComponent<Slider>().value);
                AsetNumber = ApassSet.GetSetNumber(digNumber);
                Debug.Log("A Set " + AsetNumber);

                messageText.text = "Player sets it up";
                yield return new WaitForSeconds(1);

                // SET ATTACK
                // get the attack quality based on the set
                AsetAttack.SetAttackAbility(setChoiceSkills.attack);
                Debug.Log("Attacker skill is: " + setChoiceSkills.attack);
                AattackNumber = AsetAttack.GetAttackNumber(AsetNumber);
                AattackQuality = AsetAttack.GetAttackQuality(AsetNumber, AattackNumber);
                Debug.Log("A Hit " + AattackQuality);
                if (AattackQuality == 1)
                {
                    Debug.Log("A Hitting Error");
                    messageText.text = "Player hits it 10 bricks up the wall";
                    trueCallback();
                    yield return true;
                    yield break;
                }
                // ATTACK DEFENCE
                // get the block and defence values
                BattackDefence.SetBlockAbility(skillManager.AIM1.block);
                Debug.Log("Blockers skill is: " + skillManager.AIM1.block);
                BblockNumber = BattackDefence.GetBlockNumber();
                BblockQuality = BattackDefence.GetBlockQuality();
                BattackDefence.SetDefenceAbility(skillManager.AIP2.defence);
                Debug.Log("Defenders skill is: " + skillManager.AIP2.defence);
                BdefenceNumber = BattackDefence.GetDefenceNumber();

                messageText.text = "Player attacking against a " + BblockQuality / 2 + " person block";
                yield return new WaitForSeconds(1);

                // compare the attack values to the defence values
                resultNumber = AattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber);
                if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
                {
                    bool result4 = CompareResultsA(resultNumber);
                    if (result4) trueCallback();
                    else falseCallback();
                    yield return result4;
                    yield break;
                }
            }
            Debug.LogWarning("Didn't want it to get here");
            yield return true;
        }
    }
}

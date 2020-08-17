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
    private float AblockQuality = 0;
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
    private float BblockQuality = 0;
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

    private bool waitingForPlayerInteraction = false;

    [SerializeField] private GameObject playerInteractionButton;

    [SerializeField] private PawnManager playerPawnManager;
    [SerializeField] private PawnManager AIPawnManager;

    private Pawn[] pawns;

    [SerializeField] private GridManager playerGridManager;
    [SerializeField] private GridManager aiGridManager;

    [SerializeField] private Ball ballScript;

    private PawnRole setChoiceRole;

    private const int playerBlockingRow = 8;
    private const int aiBlockingRow = 0;


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
        Debug.Log("Setting power 1");
        UnityEngine.Vector2 playerLocation = playerPawnManager.GetPawnGridPositon(PawnRole.Power1);
        int x = Mathf.RoundToInt(playerLocation.x);
        int y = Mathf.RoundToInt(playerLocation.y);
        ballScript.SetPosition(playerGridManager, x, y);
        playerSetDecision = true;
        setChoiceSkills = skillManager.PlayerP1;
    }

    public void PlayerSetMiddle()
    {
        Debug.Log("Setting middle 1");
        UnityEngine.Vector2 playerLocation = playerPawnManager.GetPawnGridPositon(PawnRole.Middle1);
        int x = Mathf.RoundToInt(playerLocation.x);
        int y = Mathf.RoundToInt(playerLocation.y);
        ballScript.SetPosition(playerGridManager, x, y);
        playerSetDecision = true;
        setChoiceSkills = skillManager.PlayerM1;
    }

    public void PlayerSetRightSide()
    {
        Debug.Log("Setting right side");
        UnityEngine.Vector2 playerLocation = playerPawnManager.GetPawnGridPositon(PawnRole.RightSide);
        int x = Mathf.RoundToInt(playerLocation.x);
        int y = Mathf.RoundToInt(playerLocation.y);
        ballScript.SetPosition(playerGridManager, x, y);
        playerSetDecision = true;
        setChoiceSkills = skillManager.PlayerRS;
    }

    public void PlayerInteractionDone()
    {
        waitingForPlayerInteraction = false;
        Debug.Log("Player interacted");
    }

    private Vector2Int UpdateServeLocationWithQuality(int serveNumber, Vector2Int serveLocation)
    {
        Vector2Int returnVector = Vector2Int.zero;
        int mod = 0;
        if (serveNumber > 60)
            mod = 0;
        else if (serveNumber > 30)
            mod = 1;
        else mod = 2;

        int xChange = Mathf.RoundToInt(UnityEngine.Random.Range(-mod, mod));
        int yChange = Mathf.RoundToInt(UnityEngine.Random.Range(-mod, mod));
        Vector2Int changeVector = new Vector2Int(xChange, yChange);

        returnVector = serveLocation + changeVector;

        return returnVector;
    }

    private Vector2Int UpdateAttackLocationWithQuality(int attackQuality, Vector2Int attackLocation)
    {
        Vector2Int returnVector = Vector2Int.zero;
        int mod = 0;
        if (attackQuality == 5 || attackQuality == 4)
            mod = 0;
        else if (attackQuality == 3)
            mod = 1;
        else mod = 2;

        int xChange = Mathf.RoundToInt(UnityEngine.Random.Range(-mod, mod));
        int yChange = Mathf.RoundToInt(UnityEngine.Random.Range(-mod, mod));
        Vector2Int changeVector = new Vector2Int(xChange, yChange);

        returnVector = attackLocation + changeVector;

        return returnVector;
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



    private void SetAIPositionsBasedOffPass(int passNumber)
    {
        if(passNumber == 1)
        {
            AIPawnManager.SetPositions(AIPawnManager.allPositionSets[4].positions);
        }
        else if(passNumber == 2)
        {
            AIPawnManager.SetPositions(AIPawnManager.allPositionSets[3].positions);
        }
        else if(passNumber == 3)
        {
            AIPawnManager.SetPositions(AIPawnManager.allPositionSets[2].positions);
        }
    }
    private SkillManager.PlayerSkills AISetSelection(int digNumber)
    {
        if (digNumber == 1)
        {
            // AIPawnManager.SetPositions(AIPawnManager.allPositionSets[4].positions);
            Debug.Log("AI forced to set Power 1");
            UnityEngine.Vector2 playerLocation = AIPawnManager.GetPawnGridPositon(PawnRole.Power1);
            ballScript.SetPosition(aiGridManager, Mathf.RoundToInt(playerLocation.x), Mathf.RoundToInt(playerLocation.y));
            return skillManager.AIP1;
        }
        else if (digNumber == 2)
        {
            // AIPawnManager.SetPositions(AIPawnManager.allPositionSets[3].positions);
            int setChoice = Mathf.CeilToInt(UnityEngine.Random.Range(0, 2));
            if (setChoice == 1)
            {
                Debug.Log("AI sets power 1");
                UnityEngine.Vector2 playerLocation = AIPawnManager.GetPawnGridPositon(PawnRole.Power1);
                ballScript.SetPosition(aiGridManager, Mathf.RoundToInt(playerLocation.x), Mathf.RoundToInt(playerLocation.y));
                return skillManager.AIP1;
            }
            else
            {
                Debug.Log("AI sets right side");
                UnityEngine.Vector2 playerLocation = AIPawnManager.GetPawnGridPositon(PawnRole.RightSide);
                ballScript.SetPosition(aiGridManager, Mathf.RoundToInt(playerLocation.x), Mathf.RoundToInt(playerLocation.y));
                return skillManager.AIRS;
            }
        }
        else if (digNumber == 3)
        {
            ballScript.SetPosition(aiGridManager, 1, 6);
            // AIPawnManager.SetPositions(AIPawnManager.allPositionSets[2].positions);
            int setChoice = Mathf.CeilToInt(UnityEngine.Random.Range(0, 3));
            if (setChoice == 1)
            {
                Debug.Log("AI sets power 1");
                UnityEngine.Vector2 playerLocation = AIPawnManager.GetPawnGridPositon(PawnRole.Power1);
                ballScript.SetPosition(aiGridManager, Mathf.RoundToInt(playerLocation.x), Mathf.RoundToInt(playerLocation.y));
                return skillManager.AIP1;
            }
            else if (setChoice == 2)
            {
                Debug.Log("AI sets right side");
                UnityEngine.Vector2 playerLocation = AIPawnManager.GetPawnGridPositon(PawnRole.RightSide);
                ballScript.SetPosition(aiGridManager, Mathf.RoundToInt(playerLocation.x), Mathf.RoundToInt(playerLocation.y));
                return skillManager.AIRS;
            }
            else
            {
                Debug.Log("AI sets middle 2");
                UnityEngine.Vector2 playerLocation = AIPawnManager.GetPawnGridPositon(PawnRole.Middle2);
                ballScript.SetPosition(aiGridManager, Mathf.RoundToInt(playerLocation.x), Mathf.RoundToInt(playerLocation.y));
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

    private void SetBallPositionOffDigPlayer(int digNumber)
    {
        if (digNumber == 1) {
            ballScript.SetPosition(playerGridManager, 4, 4);
            playerPawnManager.MoveSetter(4, 4);
        }
        else if (digNumber == 2) {
            ballScript.SetPosition(playerGridManager, 6, 3);
            playerPawnManager.MoveSetter(6, 3);
        }
        else if (digNumber == 3) {
            ballScript.SetPosition(playerGridManager, 8, 3);
            playerPawnManager.MoveSetter(8, 3);
        }
    }

    private void SetBallPositionOffDigAI(int digNumber)
    {
        if (digNumber == 1)
            ballScript.SetPosition(aiGridManager, 4, 4);
        else if (digNumber == 2)
            ballScript.SetPosition(aiGridManager, 2, 6);
        else if (digNumber == 3)
            ballScript.SetPosition(aiGridManager, 0, 6);
    }

    private SkillManager.PlayerSkills GetPlayerSkillFromAIPawn(Pawn pawn)
    {
        switch (pawn.pawnRole)
        {
            case PawnRole.Setter:
                return skillManager.AIS;
                break;
            case PawnRole.Middle1:
                return skillManager.AIM1;
                break;
            case PawnRole.Middle2:
                return skillManager.AIM2;
                break;
            case PawnRole.Power1:
                return skillManager.AIP1;
                break;
            case PawnRole.Power2:
                return skillManager.AIP2;
                break;
            case PawnRole.RightSide:
                return skillManager.AIRS;
                break;
        }
        Debug.LogWarning("Should not get here");
        return skillManager.AIP1;
    }

    private SkillManager.PlayerSkills GetPlayerSkillFromPlayerPawn(Pawn pawn)
    {
        switch (pawn.pawnRole)
        {
            case PawnRole.Setter:
                return skillManager.PlayerS;
                break;
            case PawnRole.Middle1:
                return skillManager.PlayerM1;
                break;
            case PawnRole.Middle2:
                return skillManager.PlayerM2;
                break;
            case PawnRole.Power1:
                return skillManager.PlayerP1;
                break;
            case PawnRole.Power2:
                return skillManager.PlayerP2;
                break;
            case PawnRole.RightSide:
                return skillManager.PlayerRS;
                break;
        }
        Debug.LogWarning("Should not get here");
        return skillManager.AIP1;
    }

    private int GetXDistanceFromBall(Pawn passingPlayer, Ball ball, GridManager gridManager)
    {
        int xDistance = 0;
        int playerX = Vector2Int.RoundToInt(gridManager.GetGridXYPosition(passingPlayer.transform.position)).x;
        int ballX = Vector2Int.RoundToInt(gridManager.GetGridXYPosition(ballScript.transform.position)).x;
        
        xDistance = Mathf.Abs(playerX - ballX);
        return xDistance;
    }

    private int GetYDistanceFromBall(Pawn passingPlayer, Ball ball, GridManager gridManager)
    {
        int yDistance = 0;
        int playerY = Vector2Int.RoundToInt(gridManager.GetGridXYPosition(passingPlayer.transform.position)).y;
        int ballY = Vector2Int.RoundToInt(gridManager.GetGridXYPosition(ballScript.transform.position)).y;
        yDistance = Mathf.Abs(playerY - ballY);
        return yDistance;
    }















































    public IEnumerator SimulateRallyAServing(Action trueCallback, Action falseCallback)
    {
        // set the players team skills to whats on the sliders
        skillManager.SetPlayersTeamSkills();
        isAteamServing = true;
        playerPawnManager.EnablePawnMove(false);
        AIPawnManager.SetPositions(AIPawnManager.allPositionSets[5].positions);

        // PLAYER INTERACTION
        waitingForPlayerInteraction = true;
        playerInteractionButton.SetActive(true);
        messageText.text = "Player chooses where to serve";
        playerPawnManager.SetPositions(playerPawnManager.allPositionSets[0].positions);
        // yield return new WaitUntil(() => !waitingForPlayerInteraction);
        Vector2Int serveLocation = Vector2Int.zero;
        while (waitingForPlayerInteraction)
        {
            if (Input.GetMouseButtonUp(0))
            {
                ballScript.SetPosition(aiGridManager, VBallTools.GetCursorPosition());
                serveLocation = Vector2Int.RoundToInt(aiGridManager.GetGridXYPosition(ballScript.transform.position));
            }
            yield return null;
        }
        playerInteractionButton.SetActive(false);

        messageText.text = "Serve location selected";
        yield return new WaitForSeconds(1);

        // SERVE PASS
        Debug.Log("A serves");
        AservePass.SetServeAbility(skillManager.AIM2.serve);
        Debug.Log("Servers skill is: " + skillManager.AIM2.serve);
        AserveNumber = AservePass.GetServeNumber();
        // serve location may change based on serve quality
        serveLocation = UpdateServeLocationWithQuality(AserveNumber, serveLocation);
        ballScript.SetPosition(aiGridManager, serveLocation.x, serveLocation.y);
        messageText.text = "Player serves";
        playerPawnManager.SetPositions(playerPawnManager.allPositionSets[1].positions);
        yield return new WaitForSeconds(2);

        // need to determine the closest passer and use their ability
        // also need to impact the pass value based on distance from the ball
        Pawn passingPawn = AIPawnManager.GetClosestPawn(ballScript.transform.position, false, aiBlockingRow);
        SkillManager.PlayerSkills passerSkills = GetPlayerSkillFromAIPawn(passingPawn);
        BservePass.SetPassAbility(passerSkills.pass);
        Debug.Log("Passers skill is: " + passerSkills.pass);
        // get player's distance from ball
        int passersXDistance = GetXDistanceFromBall(passingPawn, ballScript, aiGridManager);
        int passersYDistance = GetYDistanceFromBall(passingPawn, ballScript, aiGridManager);
        BpassNumber = BservePass.GetPassNumber(AserveNumber,passersXDistance, passersYDistance);



        // check for aces or misses
        if(serveLocation.x >  8 || serveLocation.x < 0 || serveLocation.y > 8 || serveLocation.y < 0)
        {
            messageText.text = "Player crushed it just out of bounds";
            ballScript.SetPosition(playerGridManager, VBallTools.GetCursorPosition());
            Debug.Log("A Miss Serve");
            falseCallback();
            yield return false;
            yield break;
        }
        if (BpassNumber == 4)
        {
            messageText.text = "Player crushed it into the net";
            ballScript.SetPosition(playerGridManager, VBallTools.GetCursorPosition());
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
        SetBallPositionOffDigAI(BpassNumber);
        SetAIPositionsBasedOffPass(BpassNumber);
        yield return new WaitForSeconds(1);

        // PLAYER INTERACTION
        waitingForPlayerInteraction = true;
        playerInteractionButton.SetActive(true);
        playerPawnManager.EnablePawnMove(true);
        messageText.text = "Player chooses where to set up their defence";
        yield return new WaitUntil(() => !waitingForPlayerInteraction);
        playerInteractionButton.SetActive(false);
        playerPawnManager.EnablePawnMove(false);

        // PASS SET
        // SET CHOICE
        AIsetChoiceSkills = AISetSelection(BpassNumber); // ai set selection also sets the ai attack position, and the ball position
        messageText.text = "AI making a set choice";
        yield return new WaitForSeconds(1);


        // get the set quality based on the pass
        BpassSet.SetSettingAbility(skillManager.AIS.set);
        Debug.Log("Setters Skill is: " + skillManager.AIS.set);
        BsetNumber = BpassSet.GetSetNumber(BpassNumber);
        Debug.Log("B Set " + BsetNumber);

        // PLAYER INTERACTION
        waitingForPlayerInteraction = true;
        playerInteractionButton.SetActive(true);
        playerPawnManager.EnablePawnMove(true);
        messageText.text = "Player has a chance to have their blockers react";
        yield return new WaitUntil(() => !waitingForPlayerInteraction);
        playerInteractionButton.SetActive(false);
        playerPawnManager.EnablePawnMove(false);

        // SET ATTACK
        // get the attack quality based on the set
        BsetAttack.SetAttackAbility(AIsetChoiceSkills.attack);
        Debug.Log("Attackers skill is: " + AIsetChoiceSkills.attack);
        BattackNumber = BsetAttack.GetAttackNumber(BsetNumber);
        BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
        Debug.Log("B Hit " + BattackQuality);
        // AI chooses where to attack
        messageText.text = "AI swinging to attack here";
        int aix = Mathf.CeilToInt((UnityEngine.Random.Range(0, 7)));
        int aiy = Mathf.CeilToInt((UnityEngine.Random.Range(0, 7)));
        Vector2Int aiAttackLocation = new Vector2Int(aix, aiy);
        ballScript.SetPosition(playerGridManager, aix, aiy);
        yield return new WaitForSeconds(1);

        if (BattackQuality == 1)
        {
            messageText.text = "AI pummels it into the net";
            Debug.Log("B Hitting Error");
            trueCallback();
            yield return true;
            yield break;
        }

        // AI attack changes location based on attack quality
        aiAttackLocation = UpdateAttackLocationWithQuality(BattackQuality, aiAttackLocation);
        ballScript.SetPosition(playerGridManager, aiAttackLocation.x, aiAttackLocation.y);
        messageText.text = "AI attack going towards here";
        yield return new WaitForSeconds(2);

        

        // ATTACK DEFENCE
        // get the block and defence values
        AattackDefence.SetBlockAbility(skillManager.PlayerM1.block);
        Debug.Log("Blockers skill is: " + skillManager.PlayerM1.block);
        AblockNumber = AattackDefence.GetBlockNumber();
        AblockQuality = AattackDefence.GetBlockQuality();

        Pawn diggingPawn = playerPawnManager.GetClosestPawn(ballScript.transform.position, true, playerBlockingRow);
        SkillManager.PlayerSkills diggerSkills = GetPlayerSkillFromPlayerPawn(diggingPawn);
        AattackDefence.SetDefenceAbility(diggerSkills.defence);
        Debug.Log("Defenders skill is: " + diggerSkills.defence);
        AdefenceNumber = AattackDefence.GetDefenceNumber();

        messageText.text = "AI attacking against a " + AblockQuality / 2 + " person block";
        yield return new WaitForSeconds(1);

        // compare the attack values to the defence values
        resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber);
        if ((resultNumber == 0 || resultNumber == 1 || resultNumber == 2 || resultNumber== 3)  && (aiAttackLocation.x > 8 || aiAttackLocation.x < 0 || aiAttackLocation.y > 8 || aiAttackLocation.y < 0))
        {
            messageText.text = "AI attacks it just out of bounds";
            Debug.Log("B Hitting Error");
            trueCallback();
            yield return true;
            yield break;
        }
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
                ballScript.SetPosition(playerGridManager, 4, 4);
                Debug.Log("A Dug " + digNumber);
                messageText.text = "Player digs it up";
                SetBallPositionOffDigPlayer(digNumber);
                yield return new WaitForSeconds(1);
                AIPawnManager.SetPositions(AIPawnManager.allPositionSets[1].positions);

                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                playerPawnManager.EnablePawnMove(true);
                messageText.text = "Player can transition their players to offensive positions";
                yield return new WaitUntil(() => !waitingForPlayerInteraction);
                playerInteractionButton.SetActive(false);
                playerPawnManager.EnablePawnMove(false);

                // PASS SET

                // SET CHOICE
                // PLAYER INTERACTION
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

                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                messageText.text = "Player chooses where to attack";
                Vector2Int playerAttackLocation = Vector2Int.zero;
                while (waitingForPlayerInteraction)
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        ballScript.SetPosition(aiGridManager, VBallTools.GetCursorPosition());
                        playerAttackLocation = Vector2Int.RoundToInt(aiGridManager.GetGridXYPosition(ballScript.transform.position));
                    }
                    yield return null;
                }
                playerInteractionButton.SetActive(false);

                messageText.text = "Attack location selected";
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
                    messageText.text = "Player hits it into the bottom of the net";
                    falseCallback();
                    yield return false;
                    yield break;
                }

                // player attack changes location based on attack quality
                playerAttackLocation = UpdateAttackLocationWithQuality(AattackQuality, playerAttackLocation);
                ballScript.SetPosition(aiGridManager, playerAttackLocation.x, playerAttackLocation.y);
                messageText.text = "Player attack going towards here";
                yield return new WaitForSeconds(2);

                // ATTACK DEFENCE
                // get the block and defence values
                BattackDefence.SetBlockAbility(skillManager.AIM1.block);
                Debug.Log("Blockers skill is: " + skillManager.AIM1.block);
                BblockNumber = BattackDefence.GetBlockNumber();
                BblockQuality = BattackDefence.GetBlockQuality();

                diggingPawn = AIPawnManager.GetClosestPawn(ballScript.transform.position, true, aiBlockingRow);
                diggerSkills = GetPlayerSkillFromAIPawn(diggingPawn);
                BattackDefence.SetDefenceAbility(diggerSkills.defence);
                Debug.Log("Defenders skill is: " + skillManager.AIP2.defence);
                BdefenceNumber = BattackDefence.GetDefenceNumber();

                messageText.text = "Player attacking against a " + BblockQuality / 2 + " person block";
                yield return new WaitForSeconds(1);

                // compare the attack values to the defence values
                resultNumber = BattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber);
                if ((resultNumber == 0 || resultNumber == 1 || resultNumber == 2 || resultNumber == 3) && (playerAttackLocation.x > 8 || playerAttackLocation.x < 0 || playerAttackLocation.y > 8 || playerAttackLocation.y < 0))
                {
                    Debug.Log("A Hitting Error");
                    messageText.text = "Player hits out of bounds";
                    falseCallback();
                    yield return false;
                    yield break;
                }
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
                SetBallPositionOffDigAI(digNumber);
                SetAIPositionsBasedOffPass(digNumber);
                yield return new WaitForSeconds(1);
                // AIPawnManager.SetPositions(AIPawnManager.allPositionSets[2].positions);
                // ballScript.SetPosition(aiGridManager, 4, 4);

                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                playerPawnManager.EnablePawnMove(true);
                playerPawnManager.SetPositions(playerPawnManager.allPositionSets[1].positions);
                messageText.text = "Player has a chance to transition to defensive positions";
                yield return new WaitUntil(() => !waitingForPlayerInteraction);
                playerInteractionButton.SetActive(false);
                playerPawnManager.EnablePawnMove(false);

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

                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                playerPawnManager.EnablePawnMove(true);
                messageText.text = "Player has a chance to have their blockers react";
                yield return new WaitUntil(() => !waitingForPlayerInteraction);
                playerInteractionButton.SetActive(false);
                playerPawnManager.EnablePawnMove(false);

                // SET ATTACK
                // get the attack quality based on the set
                BsetAttack.SetAttackAbility(AIsetChoiceSkills.attack);
                Debug.Log("Attackers skill is: " + AIsetChoiceSkills.attack);
                BattackNumber = BsetAttack.GetAttackNumber(BsetNumber);
                BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
                Debug.Log("B Hit " + BattackQuality);
                // AI chooses where to attack
                messageText.text = "AI swinging to attack here";
                aix = Mathf.CeilToInt((UnityEngine.Random.Range(0, 7)));
                aiy = Mathf.CeilToInt((UnityEngine.Random.Range(0, 7)));
                aiAttackLocation = new Vector2Int(aix, aiy);
                ballScript.SetPosition(playerGridManager, aix, aiy);
                yield return new WaitForSeconds(1);

                if (BattackQuality == 1)
                {
                    Debug.Log("B Hitting Error");
                    messageText.text = "AI pummels it into the net";
                    trueCallback();
                    yield return true;
                    yield break;
                }

                // AI attack changes location based on attack quality
                aiAttackLocation = UpdateAttackLocationWithQuality(BattackQuality, aiAttackLocation);
                ballScript.SetPosition(playerGridManager, aiAttackLocation.x, aiAttackLocation.y);
                messageText.text = "AI attack going towards here";
                yield return new WaitForSeconds(2);

                // ATTACK DEFENCE
                // get the block and defence values
                AattackDefence.SetBlockAbility(skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                Debug.Log("Blockers skill is: " + skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                AblockNumber = AattackDefence.GetBlockNumber();
                AblockQuality = AattackDefence.GetBlockQuality();

                diggingPawn = playerPawnManager.GetClosestPawn(ballScript.transform.position, true, playerBlockingRow);
                diggerSkills = GetPlayerSkillFromPlayerPawn(diggingPawn);
                AattackDefence.SetDefenceAbility(diggerSkills.defence);
                Debug.Log("Defenders skill is: " + diggerSkills.defence);
                AdefenceNumber = AattackDefence.GetDefenceNumber();

                messageText.text = "AI attacking against a " + AblockQuality / 2 + " person block";
                yield return new WaitForSeconds(1);

                // compare the attack values to the defence values
                resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber);
                if ((resultNumber == 0 || resultNumber == 1 || resultNumber == 2 || resultNumber == 3) && (aiAttackLocation.x > 8 || aiAttackLocation.x < 0 || aiAttackLocation.y > 8 || aiAttackLocation.y < 0))
                {
                    messageText.text = "AI attacks it just out of bounds";
                    Debug.Log("B Hitting Error");
                    trueCallback();
                    yield return true;
                    yield break;
                }
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
        playerPawnManager.EnablePawnMove(false);
        AIPawnManager.SetPositions(AIPawnManager.allPositionSets[0].positions);
        ballScript.SetPosition(aiGridManager, 8, 8);

        // PLAYER INTERACTION
        waitingForPlayerInteraction = true;
        playerInteractionButton.SetActive(true);
        playerPawnManager.EnablePawnMove(true);
        messageText.text = "Player has a chance to set up their reception positions";
        playerPawnManager.SetPositions(playerPawnManager.allPositionSets[2].positions);
        yield return new WaitUntil(() => !waitingForPlayerInteraction);
        playerInteractionButton.SetActive(false);
        playerPawnManager.EnablePawnMove(false);

        // SERVE PASS
        messageText.text = "AI up to serve";
        int x = Mathf.CeilToInt((UnityEngine.Random.Range(0, 6)));
        int y = Mathf.CeilToInt((UnityEngine.Random.Range(0, 7)));
        Vector2Int serveLocation = new Vector2Int(x, y);
        ballScript.SetPosition(playerGridManager, x, y);
        yield return new WaitForSeconds(1);

        Debug.Log("B serves");
        BservePass.SetServeAbility(skillManager.AIM2.serve);
        Debug.Log("Servers skill is: " + skillManager.AIM2.serve);
        BserveNumber = BservePass.GetServeNumber();
        // serve location may change based on serve quality
        serveLocation = UpdateServeLocationWithQuality(BserveNumber, serveLocation);
        ballScript.SetPosition(playerGridManager, serveLocation.x, serveLocation.y);
        messageText.text = "AI serve goes here";
        yield return new WaitForSeconds(2);


        // also need to impact the pass value based on distance from the ball
        Pawn passingPawn = playerPawnManager.GetClosestPawn(ballScript.transform.position, false, playerBlockingRow);
        SkillManager.PlayerSkills passerSkills = GetPlayerSkillFromPlayerPawn(passingPawn);
        AservePass.SetPassAbility(passerSkills.pass);
        Debug.Log("Passers skill is: " + skillManager.PlayerP2.pass);
        // account for passers distance from the ball
        int passersXDistance = GetXDistanceFromBall(passingPawn, ballScript, playerGridManager);
        int passersYDistance = GetYDistanceFromBall(passingPawn, ballScript, playerGridManager);
        ApassNumber = AservePass.GetPassNumber(BserveNumber, passersXDistance, passersYDistance);

        AIPawnManager.SetPositions(AIPawnManager.allPositionSets[1].positions);



        // check for aces or misses
        if (serveLocation.x > 8 || serveLocation.x < 0 || serveLocation.y > 8 || serveLocation.y < 0)
        {
            messageText.text = "AI crushed it just out of bounds";
            Debug.Log("B Miss Serve");
            falseCallback();
            yield return false;
            yield break;
        }
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
        SetBallPositionOffDigPlayer(ApassNumber);
        yield return new WaitForSeconds(1);
        


        // PASS SET
        // PLAYER INTERACTION
        waitingForPlayerInteraction = true;
        playerInteractionButton.SetActive(true);
        playerPawnManager.EnablePawnMove(true);
        messageText.text = "Player has a chance to move their attackers into position";
        yield return new WaitUntil(() => !waitingForPlayerInteraction);
        playerInteractionButton.SetActive(false);
        playerPawnManager.EnablePawnMove(false);

        // SET CHOICE
        // PLAYER INTERACTION
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

        // PLAYER INTERACTION
        waitingForPlayerInteraction = true;
        playerInteractionButton.SetActive(true);
        messageText.text = "Player chooses where to attack";
        Vector2Int playerAttackLocation = Vector2Int.zero;
        while (waitingForPlayerInteraction)
        {
            if (Input.GetMouseButtonUp(0))
            {
                ballScript.SetPosition(aiGridManager, VBallTools.GetCursorPosition());
                playerAttackLocation = Vector2Int.RoundToInt(aiGridManager.GetGridXYPosition(ballScript.transform.position));
            }
            yield return null;
        }
        playerInteractionButton.SetActive(false);
        messageText.text = "Attack location selected";
        yield return new WaitForSeconds(1);

        // get the attack quality based on the set
        AsetAttack.SetAttackAbility(setChoiceSkills.attack);
        Debug.Log("Attacker skill is: " + setChoiceSkills.attack);
        AattackNumber = AsetAttack.GetAttackNumber(AsetNumber);
        AattackQuality = AsetAttack.GetAttackQuality(AsetNumber, AattackNumber);
        Debug.Log("A Hit " + AattackQuality);
        if (AattackQuality == 1)
        {
            messageText.text = "Player hits it into the bottom of the net";
            Debug.Log("A Hitting Error");
            trueCallback();
            yield return true;
            yield break;
        }
        // player attack changes location based on attack quality
        playerAttackLocation = UpdateAttackLocationWithQuality(AattackQuality, playerAttackLocation);
        ballScript.SetPosition(aiGridManager, playerAttackLocation.x, playerAttackLocation.y);
        messageText.text = "Player attack going towards here";
        yield return new WaitForSeconds(2);

        // ATTACK DEFENCE
        // get the block and defence values
        BattackDefence.SetBlockAbility(skillManager.AIM1.block);
        Debug.Log("Blockers skill is: " + skillManager.AIM1.block);
        BblockNumber = BattackDefence.GetBlockNumber();
        BblockQuality = BattackDefence.GetBlockQuality();

        Pawn diggingPawn = AIPawnManager.GetClosestPawn(ballScript.transform.position, true,aiBlockingRow);
        SkillManager.PlayerSkills diggerSkills = GetPlayerSkillFromAIPawn(diggingPawn);
        BattackDefence.SetDefenceAbility(diggerSkills.defence);
        Debug.Log("Defenders skill is: " + skillManager.AIP2.defence);
        BdefenceNumber = BattackDefence.GetDefenceNumber();

        messageText.text = "Player attacking against a " + BblockQuality / 2 + " person block";
        yield return new WaitForSeconds(1);

        // compare the attack values to the defence values
        resultNumber = AattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber);
        if ((resultNumber == 0 || resultNumber == 1 || resultNumber == 2 || resultNumber == 3) && (playerAttackLocation.x > 8 || playerAttackLocation.x < 0 || playerAttackLocation.y > 8 || playerAttackLocation.y < 0))
        {
            messageText.text = "Player hits it out of bounds";
            Debug.Log("A Hitting Error");
            trueCallback();
            yield return true;
            yield break;
        }
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
                SetBallPositionOffDigAI(digNumber);
                SetAIPositionsBasedOffPass(BpassNumber);
                yield return new WaitForSeconds(1);
                // AIPawnManager.SetPositions(AIPawnManager.allPositionSets[2].positions);


                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                playerPawnManager.EnablePawnMove(true);
                messageText.text = "Player has a chance to have transition to defensive positions";
                playerPawnManager.SetPositions(playerPawnManager.allPositionSets[1].positions);
                yield return new WaitUntil(() => !waitingForPlayerInteraction);
                playerInteractionButton.SetActive(false);
                playerPawnManager.EnablePawnMove(false);

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

                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                playerPawnManager.EnablePawnMove(true);
                messageText.text = "Player has a chance to have their blockers react";
                yield return new WaitUntil(() => !waitingForPlayerInteraction);
                playerInteractionButton.SetActive(false);
                playerPawnManager.EnablePawnMove(false);

                // SET ATTACK
                // get the attack quality based on the set
                BsetAttack.SetAttackAbility(AIsetChoiceSkills.attack);
                Debug.Log("Attackers skill is: " + AIsetChoiceSkills.attack);
                BattackNumber = BsetAttack.GetAttackNumber(BsetNumber);
                BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
                Debug.Log("B Hit " + BattackQuality);
                // AI chooses where to attack
                messageText.text = "AI swinging to attack here";
                int aix = Mathf.CeilToInt((UnityEngine.Random.Range(0, 7)));
                int aiy = Mathf.CeilToInt((UnityEngine.Random.Range(0, 7)));
                Vector2Int aiAttackLocation = new Vector2Int(aix, aiy);
                ballScript.SetPosition(playerGridManager, aix, aiy);
                yield return new WaitForSeconds(1);

                if (BattackQuality == 1)
                {
                    Debug.Log("B Hitting Error");
                    messageText.text = "AI pummels it into the net";
                    falseCallback();
                    yield return false;
                    yield break;
                }

                // AI attack changes location based on attack quality
                aiAttackLocation = UpdateAttackLocationWithQuality(BattackQuality, aiAttackLocation);
                ballScript.SetPosition(playerGridManager, aiAttackLocation.x, aiAttackLocation.y);
                messageText.text = "AI attack going towards here";
                yield return new WaitForSeconds(2);

                // ATTACK DEFENCE
                // get the block and defence values
                AattackDefence.SetBlockAbility(skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                Debug.Log("Blockers skill is: " + skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                AblockNumber = AattackDefence.GetBlockNumber();
                AblockQuality = AattackDefence.GetBlockQuality();

                diggingPawn = playerPawnManager.GetClosestPawn(ballScript.transform.position, true, playerBlockingRow);
                diggerSkills = GetPlayerSkillFromPlayerPawn(diggingPawn);
                AattackDefence.SetDefenceAbility(diggerSkills.defence);
                Debug.Log("Defenders skill is: " + diggerSkills.defence);
                AdefenceNumber = AattackDefence.GetDefenceNumber();

                messageText.text = "AI attacking against a " + AblockQuality / 2 + " person block";
                yield return new WaitForSeconds(1);

                // compare the attack values to the defence values
                resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber);
                if ((resultNumber == 0 || resultNumber == 1 || resultNumber == 2 || resultNumber == 3) && (aiAttackLocation.x > 8 || aiAttackLocation.x < 0 || aiAttackLocation.y > 8 || aiAttackLocation.y < 0))
                {
                    Debug.Log("B Hitting Error");
                    messageText.text = "AI hits it out of bounds";
                    falseCallback();
                    yield return false;
                    yield break;
                }
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
                SetBallPositionOffDigPlayer(digNumber);
                yield return new WaitForSeconds(1);

                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                playerPawnManager.EnablePawnMove(true);
                messageText.text = "Player transitions to attack positions";
                yield return new WaitUntil(() => !waitingForPlayerInteraction);
                playerInteractionButton.SetActive(false);
                playerPawnManager.EnablePawnMove(false);

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

                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                messageText.text = "Player chooses where to attack";
                playerAttackLocation = Vector2Int.zero;
                while (waitingForPlayerInteraction)
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        ballScript.SetPosition(aiGridManager, VBallTools.GetCursorPosition());
                        playerAttackLocation = Vector2Int.RoundToInt(aiGridManager.GetGridXYPosition(ballScript.transform.position));
                    }
                    yield return null;
                }
                playerInteractionButton.SetActive(false);
                messageText.text = "Attack location selected";
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
                    messageText.text = "Player hits it into the bottom of the net";
                    trueCallback();
                    yield return true;
                    yield break;
                }
                // player attack changes location based on attack quality
                playerAttackLocation = UpdateAttackLocationWithQuality(AattackQuality, playerAttackLocation);
                ballScript.SetPosition(aiGridManager, playerAttackLocation.x, playerAttackLocation.y);
                messageText.text = "Player attack going towards here";
                yield return new WaitForSeconds(2);

                // ATTACK DEFENCE
                // get the block and defence values
                BattackDefence.SetBlockAbility(skillManager.AIM1.block);
                Debug.Log("Blockers skill is: " + skillManager.AIM1.block);
                BblockNumber = BattackDefence.GetBlockNumber();
                BblockQuality = BattackDefence.GetBlockQuality();

                diggingPawn = AIPawnManager.GetClosestPawn(ballScript.transform.position, true, aiBlockingRow);
                diggerSkills = GetPlayerSkillFromAIPawn(diggingPawn);
                BattackDefence.SetDefenceAbility(diggerSkills.defence);
                Debug.Log("Defenders skill is: " + diggerSkills.defence);
                BdefenceNumber = BattackDefence.GetDefenceNumber();

                messageText.text = "Player attacking against a " + BblockQuality / 2 + " person block";
                yield return new WaitForSeconds(1);

                // compare the attack values to the defence values
                resultNumber = AattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber);
                if ((resultNumber == 0 || resultNumber == 1 || resultNumber == 2 || resultNumber == 3) && (playerAttackLocation.x > 8 || playerAttackLocation.x < 0 || playerAttackLocation.y > 8 || playerAttackLocation.y < 0))
                {
                    messageText.text = "Player hits it out of bounds";
                    Debug.Log("A Hitting Error");
                    trueCallback();
                    yield return true;
                    yield break;
                }
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

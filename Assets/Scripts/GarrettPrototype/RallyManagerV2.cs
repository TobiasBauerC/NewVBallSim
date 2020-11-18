using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class RallyManagerV2 : MonoBehaviour
{

    [SerializeField] private SkillManager skillManager = null;

    private int AserveNumber = 0;
    private int ApassNumber = 0;
    private int AsetNumber = 0;
    private int AattackNumber = 0;
    private int AattackQuality = 0;
    private int AblockNumber = 0;
    private float AblockQuality = 0;
    private int AdefenceNumber = 0;
    //  int AresultNumber = 0;

    [SerializeField] private ServePassSimulation AservePass = null;
    [SerializeField] private PassSetSimulation ApassSet = null;
    [SerializeField] private SetAttackSimulation AsetAttack = null;
    [SerializeField] private AttackDefenceSimulation AattackDefence = null;

    private int BserveNumber = 0;
    private int BpassNumber = 0;
    private int BsetNumber = 0;
    private int BattackNumber = 0;
    private int BattackQuality = 0;
    private int BblockNumber = 0;
    private float BblockQuality = 0;
    private int BdefenceNumber = 0;
    //private int BresultNumber = 0;

    [SerializeField] private ServePassSimulation BservePass = null;
    [SerializeField] private PassSetSimulation BpassSet = null;
    [SerializeField] private SetAttackSimulation BsetAttack = null;
    [SerializeField] private AttackDefenceSimulation BattackDefence = null;

    private int resultNumber = 0;

    private bool isAteamServing = true;

    // [SerializeField] private GameObject setDecisionButtons = null;
    // [SerializeField] private GameObject setLeftSideButton = null;
    // [SerializeField] private GameObject setMiddleButton = null;
    // [SerializeField] private GameObject setRightSideButton = null;
    private bool playerSetDecision = false;
    private SkillManager.PlayerSkills setChoiceSkills;
    private SkillManager.PlayerSkills AIsetChoiceSkills;

    [SerializeField] private Text messageText = null;

    private bool waitingForPlayerInteraction = false;

    [SerializeField] private GameObject playerInteractionButton = null;

    [SerializeField] private PawnManager playerPawnManager = null;
    [SerializeField] private PawnManager AIPawnManager = null;

    // private Pawn[] pawns;

    [SerializeField] private GridManager playerGridManager = null;
    [SerializeField] private GridManager aiGridManager = null;

    [SerializeField] private Ball ballScript = null;
    [SerializeField] private Ball playerBallIndicator = null;
    [SerializeField] private Ball aiBallIndicator = null;

    // private PawnRole setChoiceRole;

    private const int playerBlockingColumn = 8;
    private const int aiBlockingColumn = 0;

    private int attackersRow = 0;
    private Vector2 attackerPosition = Vector2.zero;

    [SerializeField] private RotationManager rotationManager = null;

    private Vector3Int playerOutOfBoundsVectorUp = new Vector3Int(1, 1, 0);
    private Vector3Int playerOutOfBoundsVectorDown = new Vector3Int(1, -1, 0);
    private Vector3Int aiOutOfBoundsVectorUp = new Vector3Int(-1, 1, 0);
    private Vector3Int aiOutOfBoundsVectorDown = new Vector3Int(-1, -1, 0);


    // Start is called before the first frame update
    void Start()
    {
        // SimulateRally();
    }

    // Update is called once per frame
    void Update()
    {
       //  Debug.DrawRay(attackerPosition, new Vector2(ballScript.transform.position.x - attackerPosition.x, ballScript.transform.position.y - attackerPosition.y), Color.red, Vector2.Distance(attackerPosition, ballScript.transform.position));
    }

    //private IEnumerator PlayerSetDecision()
    //{
    //    playerSetDecision = false;
    //    setDecisionButtons.SetActive(true);
    //    yield return new WaitUntil(() => playerSetDecision);
        
    //    setDecisionButtons.SetActive(false);
    //    yield return null;
    //}

    public void PlayerSetLeftSide1()
    {
        //Debug.Log("Setting Left Side 1");
        UnityEngine.Vector2 playerLocation = playerPawnManager.GetPawnGridPositon(PawnRole.Power1);
        int x = Mathf.RoundToInt(playerLocation.x);
        int y = Mathf.RoundToInt(playerLocation.y);
        StartCoroutine(ballScript.SetPosition(playerGridManager, x, y, 1, SoundManager.Instance.volleyballSetSounds));
        playerSetDecision = true;
        setChoiceSkills = skillManager.PlayerP1;
    }

    public void PlayerSetLeftSide2()
    {
        //Debug.Log("Setting Left Side 2");
        UnityEngine.Vector2 playerLocation = playerPawnManager.GetPawnGridPositon(PawnRole.Power2);
        int x = Mathf.RoundToInt(playerLocation.x);
        int y = Mathf.RoundToInt(playerLocation.y);
        StartCoroutine(ballScript.SetPosition(playerGridManager, x, y, 1, SoundManager.Instance.volleyballSetSounds));
        playerSetDecision = true;
        setChoiceSkills = skillManager.PlayerP2;
    }

    public void PlayerSetMiddle1()
    {
        //Debug.Log("Setting middle 1");
        UnityEngine.Vector2 playerLocation = playerPawnManager.GetPawnGridPositon(PawnRole.Middle1);
        int x = Mathf.RoundToInt(playerLocation.x);
        int y = Mathf.RoundToInt(playerLocation.y);
        StartCoroutine(ballScript.SetPosition(playerGridManager, x, y, 1, SoundManager.Instance.volleyballSetSounds));
        playerSetDecision = true;
        setChoiceSkills = skillManager.PlayerM1;
    }

    public void PlayerSetMiddle2()
    {
        //Debug.Log("Setting middle 2");
        UnityEngine.Vector2 playerLocation = playerPawnManager.GetPawnGridPositon(PawnRole.Middle2);
        int x = Mathf.RoundToInt(playerLocation.x);
        int y = Mathf.RoundToInt(playerLocation.y);
        StartCoroutine(ballScript.SetPosition(playerGridManager, x, y, 1, SoundManager.Instance.volleyballSetSounds));
        playerSetDecision = true;
        setChoiceSkills = skillManager.PlayerM2;
    }

    public void PlayerSetRightSide()
    {
        //Debug.Log("Setting right side");
        UnityEngine.Vector2 playerLocation = playerPawnManager.GetPawnGridPositon(PawnRole.RightSide);
        int x = Mathf.RoundToInt(playerLocation.x);
        int y = Mathf.RoundToInt(playerLocation.y);
        StartCoroutine(ballScript.SetPosition(playerGridManager, x, y, 1, SoundManager.Instance.volleyballSetSounds));
        playerSetDecision = true;
        setChoiceSkills = skillManager.PlayerRS;
    }

    public void PlayerSetSetterDump()
    {
        //Debug.Log("Setting setter dump");
        UnityEngine.Vector2 playerLocation = playerPawnManager.GetPawnGridPositon(PawnRole.Setter);
        int x = Mathf.RoundToInt(playerLocation.x);
        int y = Mathf.RoundToInt(playerLocation.y);
        StartCoroutine(ballScript.SetPosition(playerGridManager, x, y, 1, SoundManager.Instance.volleyballSetSounds));
        playerSetDecision = true;
        setChoiceSkills = skillManager.PlayerS;
    }

    public void PlayerSetChoice(int player)
    {
        switch (player)
        {
            case 6:
                PlayerSetSetterDump();
                break;
            case 3:
                PlayerSetMiddle1();
                break;
            case 4:
                PlayerSetMiddle2();
                break;
            case 1:
                PlayerSetLeftSide1();
                break;
            case 2:
                PlayerSetLeftSide2();
                break;
            case 5:
                PlayerSetRightSide();
                break;
        }
    }

    public void PlayerInteractionDone()
    {
        waitingForPlayerInteraction = false;
        // Debug.Log("Player interacted");
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

    
    

    private bool CompareResultsA(int result, Vector2Int attackLocation, Vector2Int attackerGridLocation)
    {
        // Debug.Log("Comparing results");
        if (result == 100)
        {
            // Debug.Log("B Block");
            messageText.text = "AI slams the player";
            // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, playerGridManager.GetGridPosition(6, 4), 1f, SoundManager.Instance.volleyballSetSounds, SoundManager.Instance.volleyballBounceSounds));
            StartCoroutine(ballScript.SetPosition(playerGridManager, 6, 4, 1, SoundManager.Instance.volleyballSetSounds, SoundManager.Instance.volleyballBounceSounds));
            // check who's serving, returning true for a serving team won point, false for the recieving team winning the point
            AICoach.Instance.StatAIBlock();
            if (isAteamServing)
                return false;
            else return true;
        }
        else if (result == -1)
        {
            // Debug.Log("A Tool");
            messageText.text = "Player tools the block hard";
            int toolModifier = 8;
            if (aiGridManager.GetGridXYPosition(ballScript.transform.position).y < 5)
                toolModifier = 0;
            // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, aiGridManager.GetGridPosition(3, 3) + toolModifier, 1f, SoundManager.Instance.volleyballSetSounds, SoundManager.Instance.volleyballToolSounds));
            StartCoroutine(ballScript.SetPosition(aiGridManager, 0, toolModifier, 1, SoundManager.Instance.volleyballSetSounds, SoundManager.Instance.volleyballToolSounds));
            AICoach.Instance.StatPlayerTool(attackerGridLocation.y);
            if (isAteamServing)
                return true;
            else return false;
        }
        else if (result == 2 || result == 3 || result == 1)
        {
            //Debug.Log("Dig, rally continues");
            //return;
        }
        else if (result == 0)
        {
            // Debug.Log("A Attack Lands for a Kill");
            messageText.text = "Player pounds it past the defence for a point";
            // StartCoroutine(Movement.MoveFromAtoBWithEndSound(ballScript.transform, ballScript.transform.position, aiGridManager.GetGridPosition(attackLocation.x, attackLocation.y), 1f, SoundManager.Instance.volleyballBounceSounds));
            StartCoroutine(ballScript.SetPosition(aiGridManager, attackLocation.x, attackLocation.y, 1, null, SoundManager.Instance.volleyballBounceSounds));
            AICoach.Instance.StatPlayerKill(attackerGridLocation.y);
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

    private bool CompareResultsB(int result, Vector2Int attackLocation)
    {
        // Debug.Log("Comparing results");
        if (result == 100)
        {
            // Debug.Log("A Block");
            messageText.text = "Player slams the AI";
            // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, aiGridManager.GetGridPosition(3,3), 1f, SoundManager.Instance.volleyballSetSounds, SoundManager.Instance.volleyballBounceSounds));
            StartCoroutine(ballScript.SetPosition(aiGridManager, 3, 3, 1, SoundManager.Instance.volleyballSetSounds, SoundManager.Instance.volleyballBounceSounds));
            AICoach.Instance.StatPlayerBlock();
            if (isAteamServing)
                return true;
            else return false;
        }
        else if (result == -1)
        {
            // Debug.Log("B Tool");
            messageText.text = "AI tools the player easily";
            int toolModifier = 8;
            if (playerGridManager.GetGridXYPosition(ballScript.transform.position).y < 5)
                toolModifier = 0;
            // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, playerGridManager.GetGridPosition(3, 3) + toolModifier, 1f, SoundManager.Instance.volleyballSetSounds, SoundManager.Instance.volleyballToolSounds));
            StartCoroutine(ballScript.SetPosition(playerGridManager, 8, toolModifier, 1, SoundManager.Instance.volleyballSetSounds, SoundManager.Instance.volleyballToolSounds));
            AICoach.Instance.StatAITool();
            if (isAteamServing)
                return false;
            else return true;
        }
        else if (result == 2 || result == 3 || result == 1)
        {
            //Debug.Log("Dig, rally continues");
            //return;
        }
        else if (result == 0)
        {
            // Debug.Log("B Attack Lands for a Kill");
            messageText.text = "AI bounces it on the player";
            // StartCoroutine(Movement.MoveFromAtoBWithEndSound(ballScript.transform, ballScript.transform.position, playerGridManager.GetGridPosition(attackLocation.x, attackLocation.y), 1f, SoundManager.Instance.volleyballBounceSounds));
            StartCoroutine(ballScript.SetPosition(playerGridManager, attackLocation.x, attackLocation.y, 1, null, SoundManager.Instance.volleyballBounceSounds));
            AICoach.Instance.StatAIKill();
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



    private void SetAIPositionsBasedOffPass(int passNumber, float travelTime, Pawn diggingPawn)
    {
        //if(passNumber == 1)
        //{
        //    AIPawnManager.SetPositions(AIPawnManager.allPositionSets[4].positions);
        //}
        //else if(passNumber == 2)
        //{
        //    AIPawnManager.SetPositions(AIPawnManager.allPositionSets[3].positions);
        //}
        //else if(passNumber == 3)
        //{
        //    AIPawnManager.SetPositions(AIPawnManager.allPositionSets[2].positions);
        //}

        rotationManager.SetAIOffensePositions(passNumber, travelTime, diggingPawn);
    }
    private SkillManager.PlayerSkills AISetSelection(int digNumber, Pawn diggingPawn)
    {
        Pawn newSetter = null;
        if (diggingPawn.pawnRole == PawnRole.Setter)
        {
            for (int i = 0; i < rotationManager.aiPositionsArray.Length; i++)
            {
                if (rotationManager.aiPositionsArray[i].pawnRole == PawnRole.RightSide)
                    newSetter = rotationManager.aiPositionsArray[i];
            }
        }
            
        if (digNumber == 1)
        {
            // Debug.Log("AI forced to set Power 1");
            UnityEngine.Vector2 playerLocation;
            if (rotationManager.aiPositionsArray[4].pawnRole != PawnRole.Setter && rotationManager.aiPositionsArray[4] != newSetter)
                playerLocation = AIPawnManager.GetPawnGridPositon(rotationManager.aiPositionsArray[4]);
            else playerLocation = AIPawnManager.GetPawnGridPositon(rotationManager.aiPositionsArray[5]);
            StartCoroutine(ballScript.SetPosition(aiGridManager, Mathf.RoundToInt(playerLocation.x), Mathf.RoundToInt(playerLocation.y), 1, SoundManager.Instance.volleyballSetSounds));
            return skillManager.AIP1;
        }
        else if (digNumber == 2)
        {
            // AIPawnManager.SetPositions(AIPawnManager.allPositionSets[3].positions);
            int setChoice = Mathf.CeilToInt(UnityEngine.Random.Range(0, 2));
            if (setChoice == 1)
            {
                // Debug.Log("AI sets power 1");
                UnityEngine.Vector2 playerLocation;
                if (rotationManager.aiPositionsArray[4].pawnRole != PawnRole.Setter && rotationManager.aiPositionsArray[4] != newSetter)
                    playerLocation = AIPawnManager.GetPawnGridPositon(rotationManager.aiPositionsArray[4]);
                else playerLocation = AIPawnManager.GetPawnGridPositon(rotationManager.aiPositionsArray[5]);
                StartCoroutine(ballScript.SetPosition(aiGridManager, Mathf.RoundToInt(playerLocation.x), Mathf.RoundToInt(playerLocation.y), 1, SoundManager.Instance.volleyballSetSounds));
                
                return skillManager.AIP1;
            }
            else
            {
                // Debug.Log("AI sets right side");
                UnityEngine.Vector2 playerLocation;
                if (rotationManager.aiPositionsArray[2].pawnRole != PawnRole.Setter && rotationManager.aiPositionsArray[2] != newSetter)
                    playerLocation = AIPawnManager.GetPawnGridPositon(rotationManager.aiPositionsArray[2]);
                else playerLocation = AIPawnManager.GetPawnGridPositon(rotationManager.aiPositionsArray[1]);
                StartCoroutine(ballScript.SetPosition(aiGridManager, Mathf.RoundToInt(playerLocation.x), Mathf.RoundToInt(playerLocation.y), 1, SoundManager.Instance.volleyballSetSounds));
                return skillManager.AIRS;
            }
        }
        else if (digNumber == 3)
        {
            // ballScript.SetPosition(aiGridManager, 1, 6);
            // AIPawnManager.SetPositions(AIPawnManager.allPositionSets[2].positions);
            int setChoice = Mathf.CeilToInt(UnityEngine.Random.Range(0, 3));
            if (setChoice == 1)
            {
                // Debug.Log("AI sets power 1");
                UnityEngine.Vector2 playerLocation;
                if (rotationManager.aiPositionsArray[4].pawnRole != PawnRole.Setter && rotationManager.aiPositionsArray[4] != newSetter)
                    playerLocation = AIPawnManager.GetPawnGridPositon(rotationManager.aiPositionsArray[4]);
                else playerLocation = AIPawnManager.GetPawnGridPositon(rotationManager.aiPositionsArray[5]);
                StartCoroutine(ballScript.SetPosition(aiGridManager, Mathf.RoundToInt(playerLocation.x), Mathf.RoundToInt(playerLocation.y), 1, SoundManager.Instance.volleyballSetSounds));
                return skillManager.AIP1;
            }
            else if (setChoice == 2)
            {
                //Debug.Log("AI sets right side");
                UnityEngine.Vector2 playerLocation;
                if (rotationManager.aiPositionsArray[2].pawnRole != PawnRole.Setter && rotationManager.aiPositionsArray[2] != newSetter)
                    playerLocation = AIPawnManager.GetPawnGridPositon(rotationManager.aiPositionsArray[2]);
                else playerLocation = AIPawnManager.GetPawnGridPositon(rotationManager.aiPositionsArray[1]);
                StartCoroutine(ballScript.SetPosition(aiGridManager, Mathf.RoundToInt(playerLocation.x), Mathf.RoundToInt(playerLocation.y), 1, SoundManager.Instance.volleyballSetSounds));
                return skillManager.AIRS;
            }
            else
            {
                //Debug.Log("AI sets middle 2");
                UnityEngine.Vector2 playerLocation;
                if (rotationManager.aiPositionsArray[3].pawnRole != PawnRole.Setter && rotationManager.aiPositionsArray[3] != newSetter)
                    playerLocation = AIPawnManager.GetPawnGridPositon(rotationManager.aiPositionsArray[3]);
                else
                {
                    if(rotationManager.aiPositionsArray[0] != newSetter && rotationManager.aiPositionsArray[0].pawnRole != PawnRole.Setter)
                        playerLocation = AIPawnManager.GetPawnGridPositon(rotationManager.aiPositionsArray[0]);
                    else
                        playerLocation = AIPawnManager.GetPawnGridPositon(rotationManager.aiPositionsArray[1]);
                }
                StartCoroutine(ballScript.SetPosition(aiGridManager, Mathf.RoundToInt(playerLocation.x), Mathf.RoundToInt(playerLocation.y), 1, SoundManager.Instance.volleyballSetSounds));
                return skillManager.AIM1;
            }
        }
        Debug.LogError("AI Set selection didn't work, something broke. Likely no valid set options were available");
        return skillManager.AIP1;
    }

    private void PlayerSetChoiceButtonsActivate(int passNumber, Pawn diggingPawn)
    {
        //if (passNumber > 0)
        //{
        //    setLeftSideButton.SetActive(true);
        //}
        //if(passNumber > 1)
        //{
        //    setRightSideButton.SetActive(true);
        //}
        //if(passNumber > 2)
        //{
        //    setMiddleButton.SetActive(true);
        //}
        //Debug.Log("Activated the buttons fine");

        rotationManager.ActivateSetButtonsBasedOnPassDigNumber(passNumber, diggingPawn);
        
    }

    private void SetBallPositionOffDigPlayer(int digNumber, float time, Pawn diggingPawn)
    {
        if (digNumber == 1) {
            StartCoroutine(ballScript.SetPosition(playerGridManager, 4, 4, time, SoundManager.Instance.volleyballPassSounds));
            // playerPawnManager.MoveSetter(4, 4);
        }
        else if (digNumber == 2) {
            StartCoroutine(ballScript.SetPosition(playerGridManager, 6, 3, time, SoundManager.Instance.volleyballPassSounds));
            // playerPawnManager.MoveSetter(6, 3);
        }
        else if (digNumber == 3) {
            StartCoroutine(ballScript.SetPosition(playerGridManager, 8, 3, time, SoundManager.Instance.volleyballPassSounds));
            // playerPawnManager.MoveSetter(8, 3);
        }

        rotationManager.SetPlayerOffensePositions(digNumber, time, diggingPawn);
    }

    private void SetBallPositionOffDigAI(int digNumber, float time)
    {
        if (digNumber == 1)
            StartCoroutine(ballScript.SetPosition(aiGridManager, 4, 4, time, SoundManager.Instance.volleyballPassSounds));
        else if (digNumber == 2)
            StartCoroutine(ballScript.SetPosition(aiGridManager, 2, 6, time, SoundManager.Instance.volleyballPassSounds));
        else if (digNumber == 3)
            StartCoroutine(ballScript.SetPosition(aiGridManager, 0, 6, time, SoundManager.Instance.volleyballPassSounds));
    }

    private SkillManager.PlayerSkills GetPlayerSkillFromAIPawn(Pawn pawn)
    {
        switch (pawn.pawnRole)
        {
            case PawnRole.Setter:
                return skillManager.AIS;
            case PawnRole.Middle1:
                return skillManager.AIM1;
            case PawnRole.Middle2:
                return skillManager.AIM2;
            case PawnRole.Power1:
                return skillManager.AIP1;
            case PawnRole.Power2:
                return skillManager.AIP2;
            case PawnRole.RightSide:
                return skillManager.AIRS;
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
            case PawnRole.Middle1:
                return skillManager.PlayerM1;
            case PawnRole.Middle2:
                return skillManager.PlayerM2;
            case PawnRole.Power1:
                return skillManager.PlayerP1;
            case PawnRole.Power2:
                return skillManager.PlayerP2;
            case PawnRole.RightSide:
                return skillManager.PlayerRS;
        }
        Debug.LogWarning("Should not get here");
        return skillManager.AIP1;
    }

    private int GetXDistanceFromBall(Pawn passingPlayer, Ball ball, GridManager gridManager)
    {
        int xDistance = 0;
        int playerX = Vector2Int.RoundToInt(gridManager.GetGridXYPosition(passingPlayer.transform.position)).x;
        int ballX = Vector2Int.RoundToInt(gridManager.GetGridXYPosition(ball.transform.position)).x;
        
        xDistance = Mathf.Abs(playerX - ballX);
        return xDistance;
    }

    private int GetYDistanceFromBall(Pawn passingPlayer, Ball ball, GridManager gridManager)
    {
        int yDistance = 0;
        int playerY = Vector2Int.RoundToInt(gridManager.GetGridXYPosition(passingPlayer.transform.position)).y;
        int ballY = Vector2Int.RoundToInt(gridManager.GetGridXYPosition(ball.transform.position)).y;
        yDistance = Mathf.Abs(playerY - ballY);
        return yDistance;
    }















































    public IEnumerator SimulateRallyAServing(Action trueCallback, Action falseCallback)
    {
        ballScript.SetPosition(playerGridManager, 0, 0);
        // set the players team skills to whats on the sliders
        skillManager.SetPlayersTeamSkills();
        isAteamServing = true;
        playerPawnManager.serveRecieve = false;
        playerPawnManager.EnablePawnMove(false);
        rotationManager.SetAIRecievePositions(0);
        // set all the sprites to neutral
        AIPawnManager.SetAllPawnSprites(Pawn.Sprites.neutral);
        playerPawnManager.SetAllPawnSprites(Pawn.Sprites.neutral);

        // PLAYER INTERACTION
        waitingForPlayerInteraction = true;
        playerInteractionButton.SetActive(true);
        messageText.text = "Player chooses where to serve";
        
        playerBallIndicator.SetPosition(aiGridManager, 4, 4);

        rotationManager.SetPlayerServicePositions(0);
        // yield return new WaitUntil(() => !waitingForPlayerInteraction);
        Vector2Int serveLocation = new Vector2Int(4,4);
        playerBallIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.001f);
        // StartCoroutine(AIMovements.MoveSingleAITowardsTarget(rotationManager.aiPositionsArray[1], 1, 5, aiGridManager.GetGridPosition(0, 0), aiGridManager, AIPawnManager, rotationManager, 2f));
        while (waitingForPlayerInteraction)
        {
            if (Input.GetMouseButtonUp(0))
            {
                playerBallIndicator.SetPosition(aiGridManager, VBallTools.GetCursorPosition());
                serveLocation = Vector2Int.RoundToInt(aiGridManager.GetGridXYPosition(playerBallIndicator.transform.position));
            }
            yield return null;
        }
        playerInteractionButton.SetActive(false);

        messageText.text = "Serve location selected";
        playerBallIndicator.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        // SERVE PASS
        // Debug.Log("A serves");
        AservePass.SetServeAbility(skillManager.AIM2.serve);
        // Debug.Log("Servers skill is: " + skillManager.AIM2.serve);
        AserveNumber = AservePass.GetServeNumber();
        // serve location may change based on serve quality
        serveLocation = UpdateServeLocationWithQuality(AserveNumber, serveLocation);


        // need to determine the closest passer and use their ability
        // also need to impact the pass value based on distance from the ball
        // Pawn passingPawn = AIPawnManager.GetClosestPawn(playerBallIndicator.transform.position, false, aiBlockingColumn);
        Pawn passingPawn = AIPawnManager.GetClosestPawn(aiGridManager.GetGridPosition(serveLocation.x, serveLocation.y), false, aiBlockingColumn);
        SkillManager.PlayerSkills passerSkills = GetPlayerSkillFromAIPawn(passingPawn);
        BservePass.SetPassAbility(passerSkills.pass);
        // Debug.Log("Passers skill is: " + passerSkills.pass);
        // get player's distance from ball
        int passersXDistance = GetXDistanceFromBall(passingPawn, playerBallIndicator, aiGridManager);
        int passersYDistance = GetYDistanceFromBall(passingPawn, playerBallIndicator, aiGridManager);
        BpassNumber = BservePass.GetPassNumber(AserveNumber, passersXDistance, passersYDistance);
        // Debug.Log("Serve location is " + serveLocation);

        // check for aces or misses
        if(serveLocation.x >  8 || serveLocation.x < 0 || serveLocation.y > 8 || serveLocation.y < 0)
        {
            messageText.text = "Player crushed it just out of bounds";
            Vector3Int modVector = Vector3Int.zero;
            if (serveLocation.y > 4)
                modVector = playerOutOfBoundsVectorUp;
            else modVector = playerOutOfBoundsVectorDown;
            // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, aiGridManager.GetGridPosition(serveLocation.x, serveLocation.y) + modVector, 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            StartCoroutine(ballScript.SetPosition(aiGridManager, serveLocation.x + modVector.x, serveLocation.y + modVector.y, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            // Debug.Log("A Miss Serve");
            yield return new WaitForSeconds(1.001f);
            AICoach.Instance.StatPlayerServiceError();
            falseCallback();
            yield return false;
            yield break;
        }
        if (BpassNumber == 4)
        {
            messageText.text = "Player crushed it into the net";
            // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, playerGridManager.GetGridPosition(8, 4), 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            StartCoroutine(ballScript.SetPosition(playerGridManager, 8, 4, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            // Debug.Log("A Miss Serve");
            yield return new WaitForSeconds(1.001f);
            AICoach.Instance.StatPlayerServiceError();
            falseCallback();
            yield return false;
            yield break;
        }
        else if (BpassNumber == 0)
        {
            messageText.text = "Player rips an ace";
            // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, aiGridManager.GetGridPosition(serveLocation.x, serveLocation.y), 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballBounceSounds));
            StartCoroutine(ballScript.SetPosition(aiGridManager, serveLocation.x, serveLocation.y, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballBounceSounds));
            // Debug.Log("A Ace");
            yield return new WaitForSeconds(1.001f);
            AICoach.Instance.StatPlayerAce();
            trueCallback();
            yield return true;
            yield break;
        }

        float serveTravelTime = 1.5f;
        aiGridManager.SetCellOccupied(passingPawn.transform.position, false); // setting the passing pawn's tile occupation to false before moving towards the ball
        StartCoroutine(Movement.MoveFromAtoB(passingPawn.transform, passingPawn.transform.position, aiGridManager.ForceGetGridPosition(serveLocation.x, serveLocation.y), serveTravelTime));
        StartCoroutine(ballScript.SetPosition(aiGridManager, serveLocation.x, serveLocation.y, serveTravelTime, SoundManager.Instance.volleyballSpikeSounds));
        messageText.text = "Player serves";
        rotationManager.SetPlayerDefensivePositions(serveTravelTime);
        yield return new WaitForSeconds(serveTravelTime + .001f);

        // Debug.Log("B Passed " + BpassNumber);
        messageText.text = "AI passes it up";
        AICoach.Instance.StatAIPass(BpassNumber);
        float AIPassDigTime = 1;
        SetBallPositionOffDigAI(BpassNumber, AIPassDigTime);
        passingPawn.SetSprite(Pawn.Sprites.dig);
        yield return new WaitForSeconds(0.2f);
        passingPawn.SetSprite(Pawn.Sprites.neutral);
        SetAIPositionsBasedOffPass(BpassNumber, AIPassDigTime, passingPawn);
        yield return new WaitForSeconds(AIPassDigTime);
        

        // PLAYER INTERACTION
        waitingForPlayerInteraction = true;
        playerInteractionButton.SetActive(true);
        playerPawnManager.EnablePawnMove(true);
        messageText.text = "Player chooses where to set up their defence";
        yield return new WaitUntil(() => !waitingForPlayerInteraction);
        playerInteractionButton.SetActive(false);
        playerPawnManager.EnablePawnMove(false);
        playerPawnManager.SetBlockersAndDefendersSprites(playerBlockingColumn);

        // PASS SET
        // SET CHOICE
        AIsetChoiceSkills = AISetSelection(BpassNumber, passingPawn); // ai set selection also sets the ai attack position, and the ball position
        yield return new WaitForSeconds(1); // ball travel time wait
        messageText.text = "AI making a set choice";
        attackersRow = ballScript.GetGridPosition().y;
        attackerPosition = ballScript.transform.position;
        AIPawnManager.GetClosestPawn(ballScript.transform.position, false, 10).SetSprite(Pawn.Sprites.spike);
        yield return new WaitForSeconds(0.2f);


        // get the set quality based on the pass
        BpassSet.SetSettingAbility(skillManager.AIS.set);
        // Debug.Log("Setters Skill is: " + skillManager.AIS.set);
        BsetNumber = BpassSet.GetSetNumber(BpassNumber);
        // Debug.Log("B Set " + BsetNumber);

        // PLAYER INTERACTION
        waitingForPlayerInteraction = true;
        playerInteractionButton.SetActive(true);
        playerPawnManager.EnableLimitedMove(1, 1);
        playerPawnManager.EnablePawnMove(true);
        messageText.text = "Player has a chance to have their blockers react";
        yield return new WaitUntil(() => !waitingForPlayerInteraction);
        playerPawnManager.SetBlockersAndDefendersSprites(playerBlockingColumn);
        playerInteractionButton.SetActive(false);
        playerPawnManager.DisableLimitedMove();
        playerPawnManager.EnablePawnMove(false);

        // SET ATTACK
        // get the attack quality based on the set
        BsetAttack.SetAttackAbility(AIsetChoiceSkills.attack);
        // Debug.Log("Attackers skill is: " + AIsetChoiceSkills.attack);
        BattackNumber = BsetAttack.GetAttackNumber(BsetNumber, AIPawnManager.GetClosestPawn(ballScript.transform.position));
        BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
        // Debug.Log("B Hit " + BattackQuality);
        // AI chooses where to attack
        messageText.text = "AI swinging to attack here";
        int aix = Mathf.CeilToInt((UnityEngine.Random.Range(0, 7)));
        int aiy = Mathf.CeilToInt((UnityEngine.Random.Range(0, 7)));
        Vector2Int aiAttackLocation = new Vector2Int(aix, aiy);
        aiBallIndicator.SetPosition(playerGridManager, aix, aiy);
        yield return new WaitForSeconds(0.1f);

        if (BattackQuality == 1)
        {
            messageText.text = "AI pummels it into the net";
            // Debug.Log("B Hitting Error");
            // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, aiGridManager.GetGridPosition(0, 4), 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            StartCoroutine(ballScript.SetPosition(aiGridManager, 0, 4, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            yield return new WaitForSeconds(1 + .001f);
            AICoach.Instance.StatAIHittingError();
            trueCallback();
            yield return true;
            yield break;
        }

        // AI attack changes location based on attack quality
        aiAttackLocation = UpdateAttackLocationWithQuality(BattackQuality, aiAttackLocation);
        aiBallIndicator.SetPosition(playerGridManager, aiAttackLocation.x, aiAttackLocation.y);
        messageText.text = "AI attack going towards here";
        yield return new WaitForSeconds(0.2f);

        

        // ATTACK DEFENCE
        // get the block and defence values
        AattackDefence.SetBlockAbility(skillManager.PlayerM1.block);
        // Debug.Log("Blockers skill is: " + skillManager.PlayerM1.block);
        AblockNumber = AattackDefence.GetBlockNumber();
        AblockQuality = AattackDefence.GetBlockQuality(playerPawnManager, attackerPosition, playerGridManager.GetGridPosition(aiAttackLocation.x, aiAttackLocation.y), true);

        // Pawn diggingPawn = playerPawnManager.GetClosestPawn(aiBallIndicator.transform.position, true, playerBlockingColumn);
        Pawn diggingPawn = playerPawnManager.GetClosestPawn(playerGridManager.GetGridPosition(aiAttackLocation.x, aiAttackLocation.y), true, playerBlockingColumn);
        SkillManager.PlayerSkills diggerSkills = GetPlayerSkillFromPlayerPawn(diggingPawn);
        AattackDefence.SetDefenceAbility(diggerSkills.defence);
        // Debug.Log("Defenders skill is: " + diggerSkills.defence);
        AdefenceNumber = AattackDefence.GetDefenceNumber();

        messageText.text = "AI attacking against a " + AblockQuality / 2 + " person block";
        yield return new WaitForSeconds(0.1f);

        int diggersXDistance = GetXDistanceFromBall(diggingPawn, aiBallIndicator, playerGridManager);
        int diggersYDistance = GetYDistanceFromBall(diggingPawn, aiBallIndicator, playerGridManager);

        // compare the attack values to the defence values
        resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber, diggersXDistance, diggersYDistance);
        if ((resultNumber == 0 || resultNumber == 1 || resultNumber == 2 || resultNumber== 3)  && (aiAttackLocation.x > 8 || aiAttackLocation.x < 0 || aiAttackLocation.y > 8 || aiAttackLocation.y < 0))
        {
            messageText.text = "AI attacks it just out of bounds";
            // Debug.Log("B Hitting Error");
            Vector3Int modVector = Vector3Int.zero;
            if (aiAttackLocation.y > 4)
                modVector = aiOutOfBoundsVectorUp;
            else modVector = aiOutOfBoundsVectorDown;
            // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, playerGridManager.GetGridPosition(aiAttackLocation.x, aiAttackLocation.y) + modVector, 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            StartCoroutine(ballScript.SetPosition(playerGridManager, aiAttackLocation.x + modVector.x, aiAttackLocation.y + modVector.y, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            yield return new WaitForSeconds(1.001f);
            AICoach.Instance.StatAIHittingError();
            trueCallback();
            yield return true;
            yield break;
        }
        if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
        {
            StartCoroutine(Movement.MoveFromAtoBWithStartSound(ballScript.transform, ballScript.transform.position, ballScript.transform.position + new Vector3(-0.5f, 0, 0), 0.2f, SoundManager.Instance.volleyballSpikeSounds));
            yield return new WaitForSeconds(0.2f);
            bool result = CompareResultsB(resultNumber, aiAttackLocation);
            yield return new WaitForSeconds(1 + .001f);
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
                playerGridManager.SetCellOccupied(diggingPawn.transform.position, false); // setting the digging pawn's tile occupation to false before moving towards the ball
                StartCoroutine(Movement.MoveFromAtoB(diggingPawn.transform, diggingPawn.transform.position, playerGridManager.ForceGetGridPosition(aiAttackLocation.x, aiAttackLocation.y), 1));
                // StartCoroutine(Movement.MoveFromAtoBWithStartSound(ballScript.transform, ballScript.transform.position, aiBallIndicator.transform.position, 1, SoundManager.Instance.volleyballSpikeSounds));
                StartCoroutine(ballScript.SetPosition(playerGridManager, aiAttackLocation.x, aiAttackLocation.y, 1, null));
                yield return new WaitForSeconds(1 + .001f);
                int digNumber = resultNumber;
                // A SIDE WITH A DIG
                // Debug.Log("A Dug " + digNumber);
                messageText.text = "Player digs it up";
                AICoach.Instance.StatPlayerDig();
                diggingPawn.SetSprite(Pawn.Sprites.dig);
                float playerPassDigTravelTime = 1;
                SetBallPositionOffDigPlayer(digNumber, playerPassDigTravelTime, diggingPawn);
                yield return new WaitForSeconds(0.2f);
                playerPawnManager.SetAllPawnSprites(Pawn.Sprites.neutral);
                rotationManager.SetAIDefensivePositions(playerPassDigTravelTime);
                yield return new WaitForSeconds(playerPassDigTravelTime);
                // AIPawnManager.SetPositions(AIPawnManager.allPositionSets[1].positions);
                AIPawnManager.SetBlockersAndDefendersSprites(aiBlockingColumn);
                

                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                playerPawnManager.EnablePawnMoveMinusSetter(true, diggingPawn);
                messageText.text = "Player can transition their players to offensive positions";
                yield return new WaitUntil(() => !waitingForPlayerInteraction);
                playerInteractionButton.SetActive(false);
                playerPawnManager.EnablePawnMove(false);

                float blockersReactTime = 0.2f;
                StartCoroutine(rotationManager.LineUpBlockers(blockersReactTime));
                yield return new WaitForSeconds(blockersReactTime);

                // SET CHOICE
                // PLAYER INTERACTION
                playerSetDecision = false;
                PlayerSetChoiceButtonsActivate(digNumber, diggingPawn);
                messageText.text = "Player choose who to set";
                yield return new WaitUntil(() => playerSetDecision);
                rotationManager.TurnOffAllSetButtons();
                yield return new WaitForSeconds(1); // waiting for ball to travel
                //setLeftSideButton.SetActive(false);
                //setMiddleButton.SetActive(false);
                //setRightSideButton.SetActive(false);
                
                attackersRow = ballScript.GetGridPosition().y;
                attackerPosition = ballScript.transform.position;
                playerPawnManager.GetClosestPawn(ballScript.transform.position, false, 10).SetSprite(Pawn.Sprites.spike);

                // get the set quality based on the pass
                ApassSet.SetSettingAbility(skillManager.PlayerS.set);
                // Debug.Log("Setters Skill is: " + skillManager.PlayerS.set);
                AsetNumber = ApassSet.GetSetNumber(digNumber);
                // Debug.Log("A Set " + AsetNumber);

                messageText.text = "Player sets it up";
                yield return new WaitForSeconds(0.001f);

                blockersReactTime = 0.2f;
                StartCoroutine(AIMovements.BlockersReactToPlayerSetChoice(aiGridManager.ForceGetGridPosition(Mathf.RoundToInt(aiGridManager.GetGridXYPosition(ballScript.transform.position).x), Mathf.RoundToInt(aiGridManager.GetGridXYPosition(attackerPosition).y)), aiGridManager, playerGridManager, AIPawnManager, rotationManager, blockersReactTime));
                yield return new WaitForSeconds(blockersReactTime);

                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                messageText.text = "Player chooses where to attack";
                playerBallIndicator.SetPosition(aiGridManager, 4, 4);
                Vector2Int playerAttackLocation = Vector2Int.zero;
                playerAttackLocation = Vector2Int.RoundToInt(aiGridManager.GetGridXYPosition(playerBallIndicator.transform.position));
                playerBallIndicator.gameObject.SetActive(true);
                while (waitingForPlayerInteraction)
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        playerBallIndicator.SetPosition(aiGridManager, VBallTools.GetCursorPosition());
                        playerAttackLocation = Vector2Int.RoundToInt(aiGridManager.GetGridXYPosition(playerBallIndicator.transform.position));
                    }
                    yield return null;
                }
                playerInteractionButton.SetActive(false);
                playerBallIndicator.gameObject.SetActive(false);

                messageText.text = "Attack location selected";
                yield return new WaitForSeconds(0.1f);


                // SET ATTACK
                // get the attack quality based on the set
                AsetAttack.SetAttackAbility(setChoiceSkills.attack);
                // Debug.Log("Attacker skill is: " + setChoiceSkills.attack);
                AattackNumber = AsetAttack.GetAttackNumber(AsetNumber, playerPawnManager.GetClosestPawn(ballScript.transform.position));
                AattackQuality = AsetAttack.GetAttackQuality(AsetNumber, AattackNumber);
                // Debug.Log("A Hit " + AattackQuality);
                if (AattackQuality == 1)
                {
                    // Debug.Log("A Hitting Error");
                    messageText.text = "Player hits it into the bottom of the net";
                    // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, playerGridManager.GetGridPosition(8, 4), 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    StartCoroutine(ballScript.SetPosition(playerGridManager, 8, 4, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    yield return new WaitForSeconds(1.001f);
                    AICoach.Instance.StatPlayerHittingError();
                    falseCallback();
                    yield return false;
                    yield break;
                }

                // player attack changes location based on attack quality
                playerAttackLocation = UpdateAttackLocationWithQuality(AattackQuality, playerAttackLocation);
                playerBallIndicator.SetPosition(aiGridManager, playerAttackLocation.x, playerAttackLocation.y);
                messageText.text = "Player attack going towards here";
                yield return new WaitForSeconds(0.2f);

                // ATTACK DEFENCE
                // get the block and defence values
                BattackDefence.SetBlockAbility(skillManager.AIM1.block);
                // Debug.Log("Blockers skill is: " + skillManager.AIM1.block);
                BblockNumber = BattackDefence.GetBlockNumber();
                BblockQuality = BattackDefence.GetBlockQuality(AIPawnManager, attackerPosition, aiGridManager.GetGridPosition(playerAttackLocation.x, playerAttackLocation.y), false);

                // diggingPawn = AIPawnManager.GetClosestPawn(playerBallIndicator.transform.position, true, aiBlockingColumn);
                diggingPawn = AIPawnManager.GetClosestPawn(aiGridManager.GetGridPosition(playerAttackLocation.x, playerAttackLocation.y), true, aiBlockingColumn);
                diggerSkills = GetPlayerSkillFromAIPawn(diggingPawn);
                BattackDefence.SetDefenceAbility(diggerSkills.defence);
                // Debug.Log("Defenders skill is: " + skillManager.AIP2.defence);
                BdefenceNumber = BattackDefence.GetDefenceNumber();

                messageText.text = "Player attacking against a " + BblockQuality / 2 + " person block";
                yield return new WaitForSeconds(0.1f);

                diggersXDistance = GetXDistanceFromBall(diggingPawn, playerBallIndicator, aiGridManager);
                diggersYDistance = GetYDistanceFromBall(diggingPawn, playerBallIndicator, aiGridManager);

                // compare the attack values to the defence values
                resultNumber = BattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber, diggersXDistance, diggersYDistance);
                if ((resultNumber == 0 || resultNumber == 1 || resultNumber == 2 || resultNumber == 3) && (playerAttackLocation.x > 8 || playerAttackLocation.x < 0 || playerAttackLocation.y > 8 || playerAttackLocation.y < 0))
                {
                    // Debug.Log("A Hitting Error");
                    messageText.text = "Player hits it out of bounds";
                    Vector3Int modVector = Vector3Int.zero;
                    if (playerAttackLocation.y > 4)
                        modVector = playerOutOfBoundsVectorUp;
                    else modVector = playerOutOfBoundsVectorDown;
                    // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, aiGridManager.GetGridPosition(playerAttackLocation.x, playerAttackLocation.y) + modVector, 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    StartCoroutine(ballScript.SetPosition(aiGridManager, playerAttackLocation.x + modVector.x, playerAttackLocation.y + modVector.y, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    yield return new WaitForSeconds(1.001f);
                    AICoach.Instance.StatPlayerHittingError();
                    falseCallback();
                    yield return false;
                    yield break;
                }
                if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
                {
                    StartCoroutine(Movement.MoveFromAtoBWithStartSound(ballScript.transform, ballScript.transform.position, ballScript.transform.position + new Vector3(0.5f, 0, 0), 0.2f, SoundManager.Instance.volleyballSpikeSounds));
                    yield return new WaitForSeconds(0.2f + .001f);
                    bool result3 = CompareResultsA(resultNumber, playerAttackLocation, playerAttackLocation);
                    yield return new WaitForSeconds(1 + .001f);
                    if (result3)
                        trueCallback();
                    else falseCallback();
                    yield return result3;
                    yield break;
                }
                aiGridManager.SetCellOccupied(diggingPawn.transform.position, false); // setting the digging pawn's tile occupation to false before moving towards the ball
                StartCoroutine(Movement.MoveFromAtoB(diggingPawn.transform, diggingPawn.transform.position, aiGridManager.ForceGetGridPosition(playerAttackLocation.x, playerAttackLocation.y), 1));
                // StartCoroutine(Movement.MoveFromAtoBWithStartSound(ballScript.transform, ballScript.transform.position, playerBallIndicator.transform.position, 1, SoundManager.Instance.volleyballSpikeSounds));
                StartCoroutine(ballScript.SetPosition(aiGridManager, playerAttackLocation.x, playerAttackLocation.y, 1, null));
                yield return new WaitForSeconds(1 + .001f);
                digNumber = resultNumber;

                // B SIDE WITH A DIG
                // Debug.Log("B Dug " + digNumber);
                messageText.text = "AI digs it up";
                AICoach.Instance.StatAIDig();
                diggingPawn.SetSprite(Pawn.Sprites.dig);
                SetBallPositionOffDigAI(digNumber, AIPassDigTime);
                yield return new WaitForSeconds(0.2f);
                AIPawnManager.SetAllPawnSprites(Pawn.Sprites.neutral);
                SetAIPositionsBasedOffPass(digNumber, AIPassDigTime, diggingPawn);
                rotationManager.SetPlayerDefensivePositions(AIPassDigTime);
                yield return new WaitForSeconds(AIPassDigTime);
                playerPawnManager.SetBlockersAndDefendersSprites(playerBlockingColumn);
                

                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                playerPawnManager.EnablePawnMove(true);
                // playerPawnManager.SetPositions(playerPawnManager.allPositionSets[1].positions);
                
                // yield return new WaitForSeconds(playerDefenceMoveTime);
                messageText.text = "Player has a chance to transition to defensive positions";
                yield return new WaitUntil(() => !waitingForPlayerInteraction);
                playerInteractionButton.SetActive(false);
                playerPawnManager.EnablePawnMove(false);
                playerPawnManager.SetBlockersAndDefendersSprites(playerBlockingColumn);

                // PASS SET
                // SET CHOICE
                AIsetChoiceSkills = AISetSelection(digNumber, diggingPawn);
                yield return new WaitForSeconds(1); // ball travel time wait
                messageText.text = "AI making a set choice";
                attackersRow = ballScript.GetGridPosition().y;
                attackerPosition = ballScript.transform.position;
                AIPawnManager.GetClosestPawn(ballScript.transform.position, false, 10).SetSprite(Pawn.Sprites.spike);
                yield return new WaitForSeconds(0.2f);

                // get the set quality based on the pass
                BpassSet.SetSettingAbility(skillManager.AIS.set);
                // Debug.Log("Setters Skill is: " + skillManager.AIS.set);
                BsetNumber = BpassSet.GetSetNumber(digNumber);
                // Debug.Log("B Set " + BsetNumber);

                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                playerPawnManager.EnableLimitedMove(1, 1);
                playerPawnManager.EnablePawnMove(true);
                messageText.text = "Player has a chance to have their blockers react";
                yield return new WaitUntil(() => !waitingForPlayerInteraction);
                playerPawnManager.SetBlockersAndDefendersSprites(playerBlockingColumn);
                playerInteractionButton.SetActive(false);
                playerPawnManager.DisableLimitedMove();
                playerPawnManager.EnablePawnMove(false);

                // SET ATTACK
                // get the attack quality based on the set
                BsetAttack.SetAttackAbility(AIsetChoiceSkills.attack);
                // Debug.Log("Attackers skill is: " + AIsetChoiceSkills.attack);
                BattackNumber = BsetAttack.GetAttackNumber(BsetNumber, AIPawnManager.GetClosestPawn(ballScript.transform.position));
                BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
                // Debug.Log("B Hit " + BattackQuality);
                // AI chooses where to attack
                messageText.text = "AI swinging to attack here";
                aix = Mathf.CeilToInt((UnityEngine.Random.Range(0, 7)));
                aiy = Mathf.CeilToInt((UnityEngine.Random.Range(0, 7)));
                aiAttackLocation = new Vector2Int(aix, aiy);
                aiBallIndicator.SetPosition(playerGridManager, aix, aiy);
                yield return new WaitForSeconds(0.1f);

                if (BattackQuality == 1)
                {
                    // Debug.Log("B Hitting Error");
                    messageText.text = "AI pummels it into the net";
                    // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, aiGridManager.GetGridPosition(0, 4), 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    StartCoroutine(ballScript.SetPosition(aiGridManager, 0, 4, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    yield return new WaitForSeconds(1.001f);
                    AICoach.Instance.StatAIHittingError();
                    trueCallback();
                    yield return true;
                    yield break;
                }

                // AI attack changes location based on attack quality
                aiAttackLocation = UpdateAttackLocationWithQuality(BattackQuality, aiAttackLocation);
                aiBallIndicator.SetPosition(playerGridManager, aiAttackLocation.x, aiAttackLocation.y);
                messageText.text = "AI attack going towards here";
                yield return new WaitForSeconds(0.2f);

                // ATTACK DEFENCE
                // get the block and defence values
                AattackDefence.SetBlockAbility(skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                // Debug.Log("Blockers skill is: " + skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                AblockNumber = AattackDefence.GetBlockNumber();
                AblockQuality = AattackDefence.GetBlockQuality(playerPawnManager, attackerPosition, playerGridManager.GetGridPosition(aiAttackLocation.x, aiAttackLocation.y), true);

                // diggingPawn = playerPawnManager.GetClosestPawn(aiBallIndicator.transform.position, true, playerBlockingColumn);
                diggingPawn = playerPawnManager.GetClosestPawn(playerGridManager.GetGridPosition(aiAttackLocation.x, aiAttackLocation.y), true, playerBlockingColumn);
                diggerSkills = GetPlayerSkillFromPlayerPawn(diggingPawn);
                AattackDefence.SetDefenceAbility(diggerSkills.defence);
                // Debug.Log("Defenders skill is: " + diggerSkills.defence);
                AdefenceNumber = AattackDefence.GetDefenceNumber();

                messageText.text = "AI attacking against a " + AblockQuality / 2 + " person block";
                yield return new WaitForSeconds(0.1f);

                diggersXDistance = GetXDistanceFromBall(diggingPawn, aiBallIndicator, playerGridManager);
                diggersYDistance = GetYDistanceFromBall(diggingPawn, aiBallIndicator, playerGridManager);

                // compare the attack values to the defence values
                resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber, diggersXDistance, diggersYDistance);
                if ((resultNumber == 0 || resultNumber == 1 || resultNumber == 2 || resultNumber == 3) && (aiAttackLocation.x > 8 || aiAttackLocation.x < 0 || aiAttackLocation.y > 8 || aiAttackLocation.y < 0))
                {
                    messageText.text = "AI attacks it just out of bounds";
                    Vector3Int modVector = Vector3Int.zero;
                    if (aiAttackLocation.y > 4)
                        modVector = aiOutOfBoundsVectorUp;
                    else modVector = aiOutOfBoundsVectorDown;
                    // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, playerGridManager.GetGridPosition(aiAttackLocation.x, aiAttackLocation.y) + modVector, 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    StartCoroutine(ballScript.SetPosition(playerGridManager, aiAttackLocation.x + modVector.x, aiAttackLocation.y + modVector.y, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    // Debug.Log("B Hitting Error");
                    yield return new WaitForSeconds(1.001f);
                    AICoach.Instance.StatAIHittingError();
                    trueCallback();
                    yield return true;
                    yield break;
                }
                if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
                {
                    StartCoroutine(Movement.MoveFromAtoBWithStartSound(ballScript.transform, ballScript.transform.position, ballScript.transform.position + new Vector3(-0.5f, 0, 0), 0.2f, SoundManager.Instance.volleyballSpikeSounds));
                    yield return new WaitForSeconds(0.2f + .001f);
                    bool result4 = CompareResultsB(resultNumber, aiAttackLocation);
                    yield return new WaitForSeconds(1 + .001f);
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
        ballScript.SetPosition(aiGridManager, 8, 8);
        // set the players team skills to whats on the sliders
        skillManager.SetPlayersTeamSkills();
        isAteamServing = false;
        playerPawnManager.EnablePawnMove(false);
        //AIPawnManager.SetPositions(AIPawnManager.allPositionSets[0].positions);
        rotationManager.SetAIServicePositions(0);
        
        
        // Debug.Log("Ball currently at " + ballScript.GetGridPosition());

        // set all the sprites to neutral
        AIPawnManager.SetAllPawnSprites(Pawn.Sprites.neutral);
        playerPawnManager.SetAllPawnSprites(Pawn.Sprites.neutral);

        // PLAYER INTERACTION
        waitingForPlayerInteraction = true;
        playerInteractionButton.SetActive(true);
        playerPawnManager.EnablePawnMove(true);
        messageText.text = "Player has a chance to set up their reception positions";
        // playerPawnManager.SetPositions(playerPawnManager.allPositionSets[2].positions);
        rotationManager.SetPlayerRecievePositions(0);
        playerPawnManager.serveRecieve = true;
        yield return new WaitUntil(() => !waitingForPlayerInteraction);
        playerPawnManager.serveRecieve = false;
        playerInteractionButton.SetActive(false);
        playerPawnManager.EnablePawnMove(false);

        // SERVE PASS
        messageText.text = "AI up to serve";
        int x = Mathf.CeilToInt((UnityEngine.Random.Range(0, 6)));
        int y = Mathf.CeilToInt((UnityEngine.Random.Range(0, 7)));
        Vector2Int serveLocation = new Vector2Int(x, y);
        aiBallIndicator.SetPosition(playerGridManager, x, y);
        yield return new WaitForSeconds(0.1f);

        // Debug.Log("B serves");
        BservePass.SetServeAbility(skillManager.AIM2.serve);
        // Debug.Log("Servers skill is: " + skillManager.AIM2.serve);
        BserveNumber = BservePass.GetServeNumber();
        // serve location may change based on serve quality
        serveLocation = UpdateServeLocationWithQuality(BserveNumber, serveLocation);
        aiBallIndicator.SetPosition(playerGridManager, serveLocation.x, serveLocation.y);
        messageText.text = "AI serve goes here";
        yield return new WaitForSeconds(0.1f);


        // also need to impact the pass value based on distance from the ball
        // Pawn passingPawn = playerPawnManager.GetClosestPawn(aiBallIndicator.transform.position, false, playerBlockingColumn);
        Pawn passingPawn = playerPawnManager.GetClosestPawn(playerGridManager.GetGridPosition(serveLocation.x, serveLocation.y), false, playerBlockingColumn);
        SkillManager.PlayerSkills passerSkills = GetPlayerSkillFromPlayerPawn(passingPawn);
        AservePass.SetPassAbility(passerSkills.pass);
        // Debug.Log("Passers skill is: " + skillManager.PlayerP2.pass);
        // account for passers distance from the ball
        int passersXDistance = GetXDistanceFromBall(passingPawn, aiBallIndicator, playerGridManager);
        int passersYDistance = GetYDistanceFromBall(passingPawn, aiBallIndicator, playerGridManager);
        ApassNumber = AservePass.GetPassNumber(BserveNumber, passersXDistance, passersYDistance);

        // AIPawnManager.SetPositions(AIPawnManager.allPositionSets[1].positions);


        // check for aces or misses
        if (serveLocation.x > 8 || serveLocation.x < 0 || serveLocation.y > 8 || serveLocation.y < 0)
        {
            messageText.text = "AI crushed it just out of bounds";
            Vector3Int modVector = Vector3Int.zero;
            if (serveLocation.y > 4)
                modVector = aiOutOfBoundsVectorUp;
            else modVector = aiOutOfBoundsVectorDown;
            // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, playerGridManager.GetGridPosition(serveLocation.x, serveLocation.y) + modVector, 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            StartCoroutine(ballScript.SetPosition(playerGridManager, serveLocation.x + modVector.x, serveLocation.y + modVector.y, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            // Debug.Log("B Miss Serve");
            yield return new WaitForSeconds(1.001f);
            AICoach.Instance.StatAIServiceError();
            falseCallback();
            yield return false;
            yield break;
        }
        if (ApassNumber == 4)
        { 
            messageText.text = "AI crushed it into the net";
            // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, playerGridManager.GetGridPosition(0, 4), 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            StartCoroutine(ballScript.SetPosition(aiGridManager, 0, 4, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            // Debug.Log("B Miss Serve");
            yield return new WaitForSeconds(1.001f);
            AICoach.Instance.StatAIServiceError();
            falseCallback();
            yield return false;
            yield break;
        }
        else if (ApassNumber == 0)
        {
            messageText.text = "AI rips an ace";
            // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, playerGridManager.GetGridPosition(serveLocation.x, serveLocation.y), 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballBounceSounds));
            StartCoroutine(ballScript.SetPosition(playerGridManager, serveLocation.x, serveLocation.y, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballBounceSounds));
            // Debug.Log("B Ace");
            yield return new WaitForSeconds(1.001f);
            AICoach.Instance.StatAIAce();
            trueCallback();
            yield return true;
            yield break;
        }
        float AIServeTravelTime = 1;
        rotationManager.SetAIDefensivePositions(AIServeTravelTime);
        playerGridManager.SetCellOccupied(passingPawn.transform.position, false); // setting the passing pawn's tile occupation to false before moving towards the ball
        StartCoroutine(Movement.MoveFromAtoB(passingPawn.transform, passingPawn.transform.position, playerGridManager.ForceGetGridPosition(serveLocation.x, serveLocation.y), AIServeTravelTime));
        // StartCoroutine(Movement.MoveFromAtoBWithStartSound(ballScript.transform, ballScript.transform.position, playerGridManager.GetGridPosition(serveLocation.x, serveLocation.y), AIServeTravelTime, SoundManager.Instance.volleyballSpikeSounds));
        StartCoroutine(ballScript.SetPosition(playerGridManager, serveLocation.x, serveLocation.y, AIServeTravelTime, SoundManager.Instance.volleyballSpikeSounds));
        yield return new WaitForSeconds(AIServeTravelTime + .001f);
        // Debug.Log("A Passed " + ApassNumber);
        messageText.text = "Player passes it up";
        AICoach.Instance.StatPlayerPass(ApassNumber);
        float playerPassDigTravelTime = 1;
        SetBallPositionOffDigPlayer(ApassNumber, playerPassDigTravelTime, passingPawn);
        passingPawn.SetSprite(Pawn.Sprites.dig);
        yield return new WaitForSeconds(0.2f);
        passingPawn.SetSprite(Pawn.Sprites.neutral);
        // rotationManager.SetPlayerOffensePositions(ApassNumber, playerPassDigTravelTime);
        yield return new WaitForSeconds(playerPassDigTravelTime);
        



        // PASS SET
        // PLAYER INTERACTION
        waitingForPlayerInteraction = true;
        playerInteractionButton.SetActive(true);
        playerPawnManager.EnablePawnMoveMinusSetter(true, passingPawn);
        messageText.text = "Player can transition their players to offensive positions";
        yield return new WaitUntil(() => !waitingForPlayerInteraction);
        playerInteractionButton.SetActive(false);
        playerPawnManager.EnablePawnMove(false);

        float blockersReactTime = 0.2f;
        StartCoroutine(rotationManager.LineUpBlockers(blockersReactTime));
        yield return new WaitForSeconds(blockersReactTime);

        // SET CHOICE
        // PLAYER INTERACTION
        playerSetDecision = false;
        PlayerSetChoiceButtonsActivate(ApassNumber, passingPawn);
        messageText.text = "Player choose who to set";
        yield return new WaitUntil(() => playerSetDecision);
        rotationManager.TurnOffAllSetButtons();
        yield return new WaitForSeconds(1);
        attackersRow = ballScript.GetGridPosition().y;
        attackerPosition = ballScript.transform.position;
        //setLeftSideButton.SetActive(false);
        //setMiddleButton.SetActive(false);
        //setRightSideButton.SetActive(false);
        playerPawnManager.GetClosestPawn(ballScript.transform.position, false, 10).SetSprite(Pawn.Sprites.spike);

        // get the set quality based on the pass
        ApassSet.SetSettingAbility(skillManager.PlayerS.set);
        // Debug.Log("Setters Skill is: " + skillManager.PlayerS.set);
        AsetNumber = ApassSet.GetSetNumber(ApassNumber);
        //  Debug.Log("A Set " + AsetNumber);

        // SET ATTACK
        messageText.text = "Player sets it up";
        yield return new WaitForSeconds(0.001f);

        blockersReactTime = 0.2f;
        StartCoroutine(AIMovements.BlockersReactToPlayerSetChoice(aiGridManager.ForceGetGridPosition(Mathf.RoundToInt(aiGridManager.GetGridXYPosition(ballScript.transform.position).x), Mathf.RoundToInt(aiGridManager.GetGridXYPosition(attackerPosition).y)), aiGridManager, playerGridManager, AIPawnManager, rotationManager, blockersReactTime));
        yield return new WaitForSeconds(blockersReactTime);

        // PLAYER INTERACTION
        waitingForPlayerInteraction = true;
        playerInteractionButton.SetActive(true);
        messageText.text = "Player chooses where to attack";
        playerBallIndicator.SetPosition(aiGridManager, 4, 4);
        Vector2Int playerAttackLocation = Vector2Int.zero;
        playerAttackLocation = Vector2Int.RoundToInt(aiGridManager.GetGridXYPosition(playerBallIndicator.transform.position));
        playerBallIndicator.gameObject.SetActive(true);
        while (waitingForPlayerInteraction)
        {
            if (Input.GetMouseButtonUp(0))
            {
                playerBallIndicator.SetPosition(aiGridManager, VBallTools.GetCursorPosition());
                playerAttackLocation = Vector2Int.RoundToInt(aiGridManager.GetGridXYPosition(playerBallIndicator.transform.position));
            }
            yield return null;
        }
        playerInteractionButton.SetActive(false);
        playerBallIndicator.gameObject.SetActive(false);
        messageText.text = "Attack location selected";
        yield return new WaitForSeconds(0.1f);

        // get the attack quality based on the set
        AsetAttack.SetAttackAbility(setChoiceSkills.attack);
        //Debug.Log("Attacker skill is: " + setChoiceSkills.attack);
        AattackNumber = AsetAttack.GetAttackNumber(AsetNumber, playerPawnManager.GetClosestPawn(playerBallIndicator.transform.position));
        AattackQuality = AsetAttack.GetAttackQuality(AsetNumber, AattackNumber);
        //Debug.Log("A Hit " + AattackQuality);
        if (AattackQuality == 1)
        {
            messageText.text = "Player hits it into the bottom of the net";
            // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, playerGridManager.GetGridPosition(8, 4), 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            StartCoroutine(ballScript.SetPosition(playerGridManager, 8, 4, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            //Debug.Log("A Hitting Error");
            yield return new WaitForSeconds(1.001f);
            AICoach.Instance.StatPlayerHittingError();
            trueCallback();
            yield return true;
            yield break;
        }
        // player attack changes location based on attack quality
        playerAttackLocation = UpdateAttackLocationWithQuality(AattackQuality, playerAttackLocation);
        playerBallIndicator.SetPosition(aiGridManager, playerAttackLocation.x, playerAttackLocation.y);
        messageText.text = "Player attack going towards here";
        yield return new WaitForSeconds(0.1f);

        // ATTACK DEFENCE
        // get the block and defence values
        BattackDefence.SetBlockAbility(skillManager.AIM1.block);
        //Debug.Log("Blockers skill is: " + skillManager.AIM1.block);
        BblockNumber = BattackDefence.GetBlockNumber();
        BblockQuality = BattackDefence.GetBlockQuality(AIPawnManager, attackerPosition, aiGridManager.GetGridPosition(playerAttackLocation.x, playerAttackLocation.y), false);

        // Pawn diggingPawn = AIPawnManager.GetClosestPawn(playerBallIndicator.transform.position, true,aiBlockingColumn);
        Pawn diggingPawn = AIPawnManager.GetClosestPawn(aiGridManager.GetGridPosition(playerAttackLocation.x, playerAttackLocation.y), true, aiBlockingColumn);
        SkillManager.PlayerSkills diggerSkills = GetPlayerSkillFromAIPawn(diggingPawn);
        BattackDefence.SetDefenceAbility(diggerSkills.defence);
        //Debug.Log("Defenders skill is: " + skillManager.AIP2.defence);
        BdefenceNumber = BattackDefence.GetDefenceNumber();

        messageText.text = "Player attacking against a " + BblockQuality / 2 + " person block";
        yield return new WaitForSeconds(0.1f);

        int diggersXDistance = GetXDistanceFromBall(diggingPawn, playerBallIndicator, aiGridManager);
        int diggersYDistance = GetYDistanceFromBall(diggingPawn, playerBallIndicator, aiGridManager);

        // compare the attack values to the defence values
        resultNumber = AattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber, diggersXDistance, diggersYDistance);
        if ((resultNumber == 0 || resultNumber == 1 || resultNumber == 2 || resultNumber == 3) && (playerAttackLocation.x > 8 || playerAttackLocation.x < 0 || playerAttackLocation.y > 8 || playerAttackLocation.y < 0))
        {
            messageText.text = "Player hits it out of bounds";
            Vector3Int modVector = Vector3Int.zero;
            if (playerAttackLocation.y > 4)
                modVector = playerOutOfBoundsVectorUp;
            else modVector = playerOutOfBoundsVectorDown;
            // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, aiGridManager.GetGridPosition(playerAttackLocation.x, playerAttackLocation.y) + modVector, 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            StartCoroutine(ballScript.SetPosition(aiGridManager, playerAttackLocation.x + modVector.x, playerAttackLocation.y + modVector.y, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
            //Debug.Log("A Hitting Error");
            yield return new WaitForSeconds(1.001f);
            AICoach.Instance.StatPlayerHittingError();
            trueCallback();
            yield return true;
            yield break;
        }
        if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
        {
            StartCoroutine(Movement.MoveFromAtoBWithStartSound(ballScript.transform, ballScript.transform.position, ballScript.transform.position + new Vector3(0.5f, 0, 0), 0.2f, SoundManager.Instance.volleyballSpikeSounds));
            yield return new WaitForSeconds(0.2f + .001f);
            bool result = CompareResultsA(resultNumber, playerAttackLocation, playerAttackLocation);
            yield return new WaitForSeconds(1 + .001f);
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
                aiGridManager.SetCellOccupied(diggingPawn.transform.position, false); // setting the digging pawn's tile occupation to false before moving towards the ball
                StartCoroutine(Movement.MoveFromAtoB(diggingPawn.transform, diggingPawn.transform.position, aiGridManager.ForceGetGridPosition(playerAttackLocation.x, playerAttackLocation.y), 1));
                // StartCoroutine(Movement.MoveFromAtoBWithStartSound(ballScript.transform, ballScript.transform.position, playerBallIndicator.transform.position, 1, SoundManager.Instance.volleyballSpikeSounds));
                StartCoroutine(ballScript.SetPosition(aiGridManager, playerAttackLocation.x, playerAttackLocation.y, 1, null));
                yield return new WaitForSeconds(1 + .001f);

                int digNumber = resultNumber;
                // A SIDE WITH A DIG
                //Debug.Log("B Dug " + digNumber);
                messageText.text = "AI digs it up";
                AICoach.Instance.StatAIDig();
                diggingPawn.SetSprite(Pawn.Sprites.dig);
                float AIPassDigTime = 1;
                SetBallPositionOffDigAI(digNumber, AIPassDigTime);
                yield return new WaitForSeconds(0.2f);
                AIPawnManager.SetAllPawnSprites(Pawn.Sprites.neutral);
                SetAIPositionsBasedOffPass(digNumber, AIPassDigTime, diggingPawn);
                rotationManager.SetPlayerDefensivePositions(AIPassDigTime);
                yield return new WaitForSeconds(AIPassDigTime);
                playerPawnManager.SetBlockersAndDefendersSprites(playerBlockingColumn);
                


                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                playerPawnManager.EnablePawnMove(true);
                messageText.text = "Player has a chance to have transition to defensive positions";
                // playerPawnManager.SetPositions(playerPawnManager.allPositionSets[1].positions);

                // yield return new WaitForSeconds(playerDefenceMoveTime);
                yield return new WaitUntil(() => !waitingForPlayerInteraction);
                playerInteractionButton.SetActive(false);
                playerPawnManager.EnablePawnMove(false);
                playerPawnManager.SetBlockersAndDefendersSprites(playerBlockingColumn);

                // PASS SET
                // SET CHOICE
                AIsetChoiceSkills = AISetSelection(digNumber, diggingPawn);
                yield return new WaitForSeconds(1); // ball travel time wait
                attackersRow = ballScript.GetGridPosition().y;
                attackerPosition = ballScript.transform.position;
                messageText.text = "AI making a set choice";
                AIPawnManager.GetClosestPawn(ballScript.transform.position, false, 10).SetSprite(Pawn.Sprites.spike);
                yield return new WaitForSeconds(0.2f);


                // get the set quality based on the pass
                BpassSet.SetSettingAbility(skillManager.AIS.set);
                //Debug.Log("Setters Skill is: " + skillManager.AIS.set);
                BsetNumber = BpassSet.GetSetNumber(digNumber);
                //Debug.Log("B Set " + AsetNumber);

                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                playerPawnManager.EnableLimitedMove(1, 1);
                playerPawnManager.EnablePawnMove(true);
                messageText.text = "Player has a chance to have their blockers react";
                yield return new WaitUntil(() => !waitingForPlayerInteraction);
                playerPawnManager.SetBlockersAndDefendersSprites(playerBlockingColumn);
                playerInteractionButton.SetActive(false);
                playerPawnManager.DisableLimitedMove();
                playerPawnManager.EnablePawnMove(false);

                // SET ATTACK
                // get the attack quality based on the set
                BsetAttack.SetAttackAbility(AIsetChoiceSkills.attack);
                //Debug.Log("Attackers skill is: " + AIsetChoiceSkills.attack);
                BattackNumber = BsetAttack.GetAttackNumber(BsetNumber, AIPawnManager.GetClosestPawn(ballScript.transform.position));
                BattackQuality = BsetAttack.GetAttackQuality(BsetNumber, BattackNumber);
                //Debug.Log("B Hit " + BattackQuality);
                // AI chooses where to attack
                messageText.text = "AI swinging to attack here";
                int aix = Mathf.CeilToInt((UnityEngine.Random.Range(0, 7)));
                int aiy = Mathf.CeilToInt((UnityEngine.Random.Range(0, 7)));
                Vector2Int aiAttackLocation = new Vector2Int(aix, aiy);
                aiBallIndicator.SetPosition(playerGridManager, aix, aiy);
                yield return new WaitForSeconds(0.1f);

                if (BattackQuality == 1)
                {
                    //Debug.Log("B Hitting Error");
                    messageText.text = "AI pummels it into the net";
                    // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, aiGridManager.GetGridPosition(0, 4), 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    StartCoroutine(ballScript.SetPosition(aiGridManager, 0, 4, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    yield return new WaitForSeconds(1.001f);
                    AICoach.Instance.StatAIHittingError();
                    falseCallback();
                    yield return false;
                    yield break;
                }

                // AI attack changes location based on attack quality
                aiAttackLocation = UpdateAttackLocationWithQuality(BattackQuality, aiAttackLocation);
                aiBallIndicator.SetPosition(playerGridManager, aiAttackLocation.x, aiAttackLocation.y);
                messageText.text = "AI attack going towards here";
                yield return new WaitForSeconds(0.2f);

                // ATTACK DEFENCE
                // get the block and defence values
                AattackDefence.SetBlockAbility(skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                //Debug.Log("Blockers skill is: " + skillManager.M1Sliders.transform.Find("BlockSlider").GetComponent<Slider>().value);
                AblockNumber = AattackDefence.GetBlockNumber();
                AblockQuality = AattackDefence.GetBlockQuality(playerPawnManager, attackerPosition, playerGridManager.GetGridPosition(aiAttackLocation.x, aiAttackLocation.y), true);

                // diggingPawn = playerPawnManager.GetClosestPawn(aiBallIndicator.transform.position, true, playerBlockingColumn);
                diggingPawn = playerPawnManager.GetClosestPawn(playerGridManager.GetGridPosition(aiAttackLocation.x, aiAttackLocation.y), true, playerBlockingColumn);
                diggerSkills = GetPlayerSkillFromPlayerPawn(diggingPawn);
                AattackDefence.SetDefenceAbility(diggerSkills.defence);
                //Debug.Log("Defenders skill is: " + diggerSkills.defence);
                AdefenceNumber = AattackDefence.GetDefenceNumber();

                messageText.text = "AI attacking against a " + AblockQuality / 2 + " person block";
                yield return new WaitForSeconds(0.1f);

                diggersXDistance = GetXDistanceFromBall(diggingPawn, aiBallIndicator, playerGridManager);
                diggersYDistance = GetYDistanceFromBall(diggingPawn, aiBallIndicator, playerGridManager);

                // compare the attack values to the defence values
                resultNumber = BattackDefence.GetResultNumber(BattackNumber, BattackQuality, AblockNumber, AblockQuality, AdefenceNumber, diggersXDistance, diggersYDistance);
                if ((resultNumber == 0 || resultNumber == 1 || resultNumber == 2 || resultNumber == 3) && (aiAttackLocation.x > 8 || aiAttackLocation.x < 0 || aiAttackLocation.y > 8 || aiAttackLocation.y < 0))
                {
                    //Debug.Log("B Hitting Error");
                    messageText.text = "AI attacks it just out of bounds";
                    Vector3Int modVector = Vector3Int.zero;
                    if (aiAttackLocation.y > 4)
                        modVector = aiOutOfBoundsVectorUp;
                    else modVector = aiOutOfBoundsVectorDown;
                    // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, playerGridManager.GetGridPosition(aiAttackLocation.x, aiAttackLocation.y) + modVector, 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    StartCoroutine(ballScript.SetPosition(playerGridManager, aiAttackLocation.x + modVector.x, aiAttackLocation.y + modVector.y, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    yield return new WaitForSeconds(1.001f);
                    AICoach.Instance.StatAIHittingError();
                    falseCallback();
                    yield return false;
                    yield break;
                }
                if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
                {
                    StartCoroutine(Movement.MoveFromAtoBWithStartSound(ballScript.transform, ballScript.transform.position, ballScript.transform.position + new Vector3(-0.5f, 0, 0), 0.2f, SoundManager.Instance.volleyballSpikeSounds));
                    yield return new WaitForSeconds(0.2f + .001f);
                    bool result3 = CompareResultsB(resultNumber, aiAttackLocation);
                    yield return new WaitForSeconds(1 + .001f);
                    if (result3) trueCallback();
                    else falseCallback();
                    yield return result3;
                    yield break;
                }
                playerGridManager.SetCellOccupied(diggingPawn.transform.position, false); // setting the digging pawn's tile occupation to false before moving towards the ball
                StartCoroutine(Movement.MoveFromAtoB(diggingPawn.transform, diggingPawn.transform.position, playerGridManager.ForceGetGridPosition(aiAttackLocation.x, aiAttackLocation.y), 1));
                // StartCoroutine(Movement.MoveFromAtoBWithStartSound(ballScript.transform, ballScript.transform.position, aiBallIndicator.transform.position, 1, SoundManager.Instance.volleyballSpikeSounds));
                StartCoroutine(ballScript.SetPosition(playerGridManager, aiAttackLocation.x, aiAttackLocation.y, 1, null));
                yield return new WaitForSeconds(1 + .001f);

                digNumber = resultNumber;
                // B SIDE WITH A DIG
                //Debug.Log("A Dug " + digNumber);
                messageText.text = "Player digs it up";
                AICoach.Instance.StatPlayerDig();
                SetBallPositionOffDigPlayer(digNumber, playerPassDigTravelTime, diggingPawn);
                diggingPawn.SetSprite(Pawn.Sprites.dig);
                yield return new WaitForSeconds(0.2f);
                playerPawnManager.SetAllPawnSprites(Pawn.Sprites.neutral);
                rotationManager.SetAIDefensivePositions(playerPassDigTravelTime);
                yield return new WaitForSeconds(playerPassDigTravelTime);
                AIPawnManager.SetBlockersAndDefendersSprites(aiBlockingColumn);
                

                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                playerPawnManager.EnablePawnMoveMinusSetter(true, diggingPawn);
                messageText.text = "Player can transition their players to offensive positions";
                yield return new WaitUntil(() => !waitingForPlayerInteraction);
                playerInteractionButton.SetActive(false);
                playerPawnManager.EnablePawnMove(false);

                blockersReactTime = 0.2f;
                StartCoroutine(rotationManager.LineUpBlockers(blockersReactTime));
                yield return new WaitForSeconds(blockersReactTime);

                // PASS SET
                // SET CHOICE
                playerSetDecision = false;
                PlayerSetChoiceButtonsActivate(ApassNumber, diggingPawn);
                messageText.text = "Player choose who to set";
                yield return new WaitUntil(() => playerSetDecision);
                rotationManager.TurnOffAllSetButtons();
                yield return new WaitForSeconds(1);
                attackersRow = ballScript.GetGridPosition().y;
                attackerPosition = ballScript.transform.position;
                //setLeftSideButton.SetActive(false);
                //setMiddleButton.SetActive(false);
                //setRightSideButton.SetActive(false);
                playerPawnManager.GetClosestPawn(ballScript.transform.position, false, 10).SetSprite(Pawn.Sprites.spike);

                // get the set quality based on the pass
                ApassSet.SetSettingAbility(skillManager.SetterSliders.transform.Find("SetSlider").GetComponent<Slider>().value);
                //Debug.Log("Setters Skill is: " + skillManager.SetterSliders.transform.Find("SetSlider").GetComponent<Slider>().value);
                AsetNumber = ApassSet.GetSetNumber(digNumber);
                //Debug.Log("A Set " + AsetNumber);

                messageText.text = "Player sets it up";
                yield return new WaitForSeconds(0.001f);

                blockersReactTime = 0.2f;
                StartCoroutine(AIMovements.BlockersReactToPlayerSetChoice(aiGridManager.ForceGetGridPosition(Mathf.RoundToInt(aiGridManager.GetGridXYPosition(ballScript.transform.position).x), Mathf.RoundToInt(aiGridManager.GetGridXYPosition(attackerPosition).y)), aiGridManager, playerGridManager, AIPawnManager, rotationManager, blockersReactTime));
                yield return new WaitForSeconds(blockersReactTime);

                // PLAYER INTERACTION
                waitingForPlayerInteraction = true;
                playerInteractionButton.SetActive(true);
                messageText.text = "Player chooses where to attack";
                playerBallIndicator.SetPosition(aiGridManager, 4, 4);
                playerAttackLocation = Vector2Int.zero;
                playerAttackLocation = Vector2Int.RoundToInt(aiGridManager.GetGridXYPosition(playerBallIndicator.transform.position));
                playerBallIndicator.gameObject.SetActive(true);
                while (waitingForPlayerInteraction)
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        playerBallIndicator.SetPosition(aiGridManager, VBallTools.GetCursorPosition());
                        playerAttackLocation = Vector2Int.RoundToInt(aiGridManager.GetGridXYPosition(playerBallIndicator.transform.position));
                    }
                    yield return null;
                }
                playerInteractionButton.SetActive(false);
                playerBallIndicator.gameObject.SetActive(false);
                messageText.text = "Attack location selected";
                yield return new WaitForSeconds(0.1f);

                // SET ATTACK
                // get the attack quality based on the set
                AsetAttack.SetAttackAbility(setChoiceSkills.attack);
                //Debug.Log("Attacker skill is: " + setChoiceSkills.attack);
                AattackNumber = AsetAttack.GetAttackNumber(AsetNumber, playerPawnManager.GetClosestPawn(ballScript.transform.position));
                AattackQuality = AsetAttack.GetAttackQuality(AsetNumber, AattackNumber);
                //Debug.Log("A Hit " + AattackQuality);
                if (AattackQuality == 1)
                {
                    //Debug.Log("A Hitting Error");
                    messageText.text = "Player hits it into the bottom of the net";
                    // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, playerGridManager.GetGridPosition(8, 4), 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    StartCoroutine(ballScript.SetPosition(playerGridManager, 8, 4, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    yield return new WaitForSeconds(1.001f);
                    AICoach.Instance.StatPlayerHittingError();
                    trueCallback();
                    yield return true;
                    yield break;
                }
                // player attack changes location based on attack quality
                playerAttackLocation = UpdateAttackLocationWithQuality(AattackQuality, playerAttackLocation);
                playerBallIndicator.SetPosition(aiGridManager, playerAttackLocation.x, playerAttackLocation.y);
                messageText.text = "Player attack going towards here";
                yield return new WaitForSeconds(0.2f);

                // ATTACK DEFENCE
                // get the block and defence values
                BattackDefence.SetBlockAbility(skillManager.AIM1.block);
                //Debug.Log("Blockers skill is: " + skillManager.AIM1.block);
                BblockNumber = BattackDefence.GetBlockNumber();
                BblockQuality = BattackDefence.GetBlockQuality(AIPawnManager, attackerPosition, aiGridManager.GetGridPosition(playerAttackLocation.x, playerAttackLocation.y), false);

                //  diggingPawn = AIPawnManager.GetClosestPawn(playerBallIndicator.transform.position, true, aiBlockingColumn);
                diggingPawn = AIPawnManager.GetClosestPawn(aiGridManager.GetGridPosition(playerAttackLocation.x, playerAttackLocation.y), true, aiBlockingColumn);
                diggerSkills = GetPlayerSkillFromAIPawn(diggingPawn);
                BattackDefence.SetDefenceAbility(diggerSkills.defence);
                //Debug.Log("Defenders skill is: " + diggerSkills.defence);
                BdefenceNumber = BattackDefence.GetDefenceNumber();

                messageText.text = "Player attacking against a " + BblockQuality / 2 + " person block";
                yield return new WaitForSeconds(0.1f);

                diggersXDistance = GetXDistanceFromBall(diggingPawn, playerBallIndicator, aiGridManager);
                diggersYDistance = GetYDistanceFromBall(diggingPawn, playerBallIndicator, aiGridManager);

                // compare the attack values to the defence values
                resultNumber = AattackDefence.GetResultNumber(AattackNumber, AattackQuality, BblockNumber, BblockQuality, BdefenceNumber, diggersXDistance, diggersYDistance);
                if ((resultNumber == 0 || resultNumber == 1 || resultNumber == 2 || resultNumber == 3) && (playerAttackLocation.x > 8 || playerAttackLocation.x < 0 || playerAttackLocation.y > 8 || playerAttackLocation.y < 0))
                {
                    messageText.text = "Player hits it out of bounds";
                    Vector3Int modVector = Vector3Int.zero;
                    if (playerAttackLocation.y > 4)
                        modVector = playerOutOfBoundsVectorUp;
                    else modVector = playerOutOfBoundsVectorDown;
                    // StartCoroutine(Movement.MoveFromAtoBWithStartAndEndSound(ballScript.transform, ballScript.transform.position, aiGridManager.GetGridPosition(playerAttackLocation.x, playerAttackLocation.y) + modVector, 1f, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    StartCoroutine(ballScript.SetPosition(aiGridManager, playerAttackLocation.x + modVector.x, playerAttackLocation.y + modVector.y, 1, SoundManager.Instance.volleyballSpikeSounds, SoundManager.Instance.volleyballToolSounds));
                    //Debug.Log("A Hitting Error");
                    yield return new WaitForSeconds(1.001f);
                    AICoach.Instance.StatPlayerHittingError();
                    trueCallback();
                    yield return true;
                    yield break;
                }
                if (resultNumber != 1 && resultNumber != 2 && resultNumber != 3)
                {
                    StartCoroutine(Movement.MoveFromAtoBWithStartSound(ballScript.transform, ballScript.transform.position, ballScript.transform.position + new Vector3(0.5f, 0, 0), 0.2f, SoundManager.Instance.volleyballSpikeSounds));
                    yield return new WaitForSeconds(0.2f + .001f);
                    bool result4 = CompareResultsA(resultNumber, playerAttackLocation, playerAttackLocation);
                    yield return new WaitForSeconds(1 + .001f);
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

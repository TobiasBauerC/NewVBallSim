using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICoach : MonoBehaviour
{
    private static AICoach _instance;
    public static AICoach Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AICoach>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }



    [SerializeField] private SetManager setManager;

    private int playerTotalAttacks = 0;
    private int playerStraightKills = 0;
    private int playerTools = 0;
    private int playerHittingErrors = 0;
    private int playerBlocks = 0;
    private int playerDigs = 0;
    private int playerAces = 0;
    private int playerServiceErrors = 0;
    private int playerTotalPasses = 0;
    private float playerPassingAverage = 0;
    private float playerHittingAverage = 0;

    private int aiTotalAttacks = 0;
    private int aiStraightKills = 0;
    private int aiTools = 0;
    private int aiHittingErrors = 0;
    private int aiBlocks = 0;
    private int aiDigs = 0;
    private int aiAces = 0;
    private int aiServiceErrors = 0;
    private int aiTotalPasses = 0;
    private float aiPassingAverage = 0;
    private float aiHittingAverage = 0;


    // variables to keep track of hitting zones that the player scores from
    private int topZoneScoreCount = 0;
    private int midZoneScoreCount = 0;
    private int bottomZoneScoreCount = 0;
    private Vector3Int[] allDefensiveStrategies = new Vector3Int[] { new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0), new Vector3Int(2, 0, 0), new Vector3Int(3, 0, 0), new Vector3Int(4, 0, 0),
    new Vector3Int(5, 0, 0), new Vector3Int(6, 0, 0), new Vector3Int(7, 0, 0), new Vector3Int(8, 0, 0), new Vector3Int(9, 0, 0),
    new Vector3Int(10, 0, 0), new Vector3Int(11, 0, 0), new Vector3Int(12, 0, 0), new Vector3Int(13, 0, 0), new Vector3Int(14, 0, 0),
    new Vector3Int(15, 0, 0), new Vector3Int(16, 0, 0), new Vector3Int(17, 0, 0), new Vector3Int(18, 0, 0), new Vector3Int(19, 0, 0) };
    private int lastDefensiveStrategyUsedIndex = -1;

    // Vectors to store lists of defensive strategies
    // x = defensive strategy number
    // y = number of times strategy has been used
    // z = number of times the strategy has been successful
    private Vector3Int[] keyLeftHitterCloseGameBucket = new Vector3Int[] { new Vector3Int(6, 0, 0), new Vector3Int(9, 0, 0), new Vector3Int(11, 0, 0), new Vector3Int(14, 0, 0) };
    private Vector3Int[] keyMidHitterCloseGameBucket = new Vector3Int[] { new Vector3Int(0, 0, 0), new Vector3Int(2, 0, 0), new Vector3Int(8, 0, 0), new Vector3Int(12, 0, 0) };
    private Vector3Int[] keyRightHitterCloseGameBucket = new Vector3Int[] { new Vector3Int(7, 0, 0), new Vector3Int(10, 0, 0), new Vector3Int(13, 0, 0), new Vector3Int(16, 0, 0) };
    private Vector3Int[] neutralCloseGameBucket = new Vector3Int[] { new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0), new Vector3Int(4, 0, 0), new Vector3Int(5, 0, 0), new Vector3Int(8, 0, 0) };


    public void StatPlayerKill(int attackerYValue)
    {
        if (attackerYValue < 3)
            bottomZoneScoreCount++;
        else if (attackerYValue < 6)
            midZoneScoreCount++;
        else topZoneScoreCount++;

        playerTotalAttacks++;
        playerStraightKills += 1;
        playerHittingAverage = (playerStraightKills + playerTools - playerHittingErrors)/ playerTotalAttacks;
    }

    public void StatPlayerTool(int attackerYValue)
    {
        if (attackerYValue < 3)
            bottomZoneScoreCount++;
        else if (attackerYValue < 6)
            midZoneScoreCount++;
        else topZoneScoreCount++;

        playerTotalAttacks++;
        playerTools += 1;
        playerHittingAverage = (playerStraightKills + playerTools - playerHittingErrors) / playerTotalAttacks;
    }
    public void StatPlayerHittingError()
    {
        allDefensiveStrategies[lastDefensiveStrategyUsedIndex].z++;

        playerTotalAttacks++;
        playerHittingErrors += 1;
        playerHittingAverage = (playerStraightKills + playerTools - playerHittingErrors) / playerTotalAttacks;
    }
    public void StatPlayerBlock()
    {
        playerBlocks += 1;
        StatAIHittingError();
    }
    public void StatPlayerDig()
    {
        aiTotalAttacks++;
        playerDigs += 1;
        aiHittingAverage = (aiStraightKills + aiTools - aiHittingErrors) / aiTotalAttacks;
    }
    public void StatPlayerAce()
    {
        aiTotalPasses++;
        playerAces += 1;
        StatAIPass(0);
    }
    public void StatPlayerServiceError()
    {
        playerServiceErrors++;
    }
    public void StatPlayerPass(int passNumber)
    {
        float tempPassingTotals = playerPassingAverage * playerTotalPasses;
        playerTotalPasses++;
        playerPassingAverage = (tempPassingTotals + passNumber) / playerTotalPasses;
    }

    public void ResetAllStats()
    {
        playerTotalAttacks = 0;
        playerStraightKills = 0;
        playerTools = 0;
        playerHittingErrors = 0;
        playerBlocks = 0;
        playerDigs = 0;
        playerAces = 0;
        playerServiceErrors = 0;
        playerTotalPasses = 0;
        playerPassingAverage = 0;
        playerHittingAverage = 0;

        aiTotalAttacks = 0;
        aiStraightKills = 0;
        aiTools = 0;
        aiHittingErrors = 0;
        aiBlocks = 0;
        aiDigs = 0;
        aiAces = 0;
        aiServiceErrors = 0;
        aiTotalPasses = 0;
        aiPassingAverage = 0;
        aiHittingAverage = 0;

        topZoneScoreCount = 0;
        midZoneScoreCount = 0;
        bottomZoneScoreCount = 0;
    }
    public void StatAIKill()
    {
        aiTotalAttacks++;
        aiStraightKills += 1;
        aiHittingAverage = (aiStraightKills + aiTools - aiHittingErrors) / aiTotalAttacks;
    }

    public void StatAITool()
    {
        aiTotalAttacks++;
        aiTools += 1;
        aiHittingAverage = (aiStraightKills + aiTools - aiHittingErrors) / aiTotalAttacks;
    }
    public void StatAIHittingError()
    {
        aiTotalAttacks++;
        aiHittingErrors += 1;
        aiHittingAverage = (aiStraightKills + aiTools - aiHittingErrors) / aiTotalAttacks;
    }
    public void StatAIBlock()
    {
        allDefensiveStrategies[lastDefensiveStrategyUsedIndex].z++;

        aiBlocks += 1;
        StatPlayerHittingError();
    }
    public void StatAIDig()
    {
        allDefensiveStrategies[lastDefensiveStrategyUsedIndex].z++;

        playerTotalAttacks++;
        aiDigs += 1;
        playerHittingAverage = (playerStraightKills + playerTools - playerHittingErrors) / playerTotalAttacks;
    }
    public void StatAIAce()
    {
        playerTotalPasses++;
        aiAces += 1;
        StatPlayerPass(0);
    }
    public void StatAIServiceError()
    {
        aiServiceErrors++;
    }
    public void StatAIPass(int passNumber)
    {
        float tempPassingTotals = aiPassingAverage * aiTotalPasses;
        aiTotalPasses++;
        aiPassingAverage = (tempPassingTotals + passNumber) / aiTotalPasses;
    }




    public int GetDefensiveStrategy()
    {
        int defensiveStrategy = 0;

        int scoreDifference = setManager.GetAITeamScore() - setManager.GetPlayerTeamScore();

        // enter various functions depending on the score
        if (scoreDifference >= 4)
            defensiveStrategy = BehaviourUpBig();
        else if (scoreDifference <= -4)
            defensiveStrategy = BehaviourDownBig();
        else defensiveStrategy = BehaviourCloseGame();

        

        return defensiveStrategy;
    }

    private int BehaviourCloseGame()
    {
        // check for a consistant hitting zone
        if (topZoneScoreCount - midZoneScoreCount >= 4 && topZoneScoreCount - bottomZoneScoreCount >= 4)
            BehaviourCloseGameKeyHitter(1);
        else if (midZoneScoreCount - topZoneScoreCount >= 4 && midZoneScoreCount - bottomZoneScoreCount >= 4)
            BehaviourCloseGameKeyHitter(2);
        else if (bottomZoneScoreCount - topZoneScoreCount >= 4 && bottomZoneScoreCount - midZoneScoreCount >= 4)
            BehaviourCloseGameKeyHitter(3);

        // else check for a strategy that hasn't been tried yet
        List<Vector3Int> newStratsIndex = new List<Vector3Int>();
        for(int i = 0; i < neutralCloseGameBucket.Length; i++)
        {
            if(neutralCloseGameBucket[i].y == 0)
            {
                newStratsIndex.Add(neutralCloseGameBucket[i]);
            }
        }
        if(newStratsIndex.Count > 0)
        {
            Debug.LogWarning("Trying a new strategy: " + newStratsIndex.Count + " to choose from");
            int number = Random.Range(0, newStratsIndex.Count);
            for(int i = 0; i < neutralCloseGameBucket.Length; i++)
            {
                if(newStratsIndex[number].x == neutralCloseGameBucket[i].x)
                    neutralCloseGameBucket[i].y += 1;
            }
            lastDefensiveStrategyUsedIndex = newStratsIndex[number].x;
            return newStratsIndex[number].x;
        }

        // else randomly select from the neutral close game pool
        Debug.LogWarning("Randomly selecting a neutral close game strategy");
        int thing = Random.Range(0, neutralCloseGameBucket.Length);
        neutralCloseGameBucket[thing].y += 1;
        lastDefensiveStrategyUsedIndex = neutralCloseGameBucket[thing].x;
        return neutralCloseGameBucket[thing].x;
    }
    private int BehaviourCloseGameKeyHitter(int hitterToKey)
    {
        int number = -1;

        if(hitterToKey == 1)
        {
            number = Random.Range(0, keyLeftHitterCloseGameBucket.Length);
            keyLeftHitterCloseGameBucket[number].y += 1;
            lastDefensiveStrategyUsedIndex = keyLeftHitterCloseGameBucket[number].x;
            return keyLeftHitterCloseGameBucket[number].x;
        }
        else if(hitterToKey == 3)
        {
            number = Random.Range(0, keyRightHitterCloseGameBucket.Length);
            keyRightHitterCloseGameBucket[number].y += 1;
            lastDefensiveStrategyUsedIndex = keyRightHitterCloseGameBucket[number].x;
            return keyRightHitterCloseGameBucket[number].x;
        }
        else
        {
            number = Random.Range(0, keyMidHitterCloseGameBucket.Length);
            keyMidHitterCloseGameBucket[number].y += 1;
            lastDefensiveStrategyUsedIndex = keyMidHitterCloseGameBucket[number].x;
            return keyMidHitterCloseGameBucket[number].x;
        }
    }




    private int BehaviourDownBig()
    {
        return 0;
    }







    private int BehaviourUpBig()
    {
        return 0;
    }


}

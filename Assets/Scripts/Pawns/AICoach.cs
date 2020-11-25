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



    [SerializeField] private SetManager setManager = null;

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
    private bool didLastDefensiveStrategyWork = false;

    // Vectors to store lists of defensive strategies
    // x = defensive strategy number
    // y = number of times strategy has been used
    // z = number of times the strategy has been successful
    private Vector3Int[] keyLeftHitterCloseGameBucket = new Vector3Int[] { new Vector3Int(6, 0, 0), new Vector3Int(9, 0, 0), new Vector3Int(11, 0, 0), new Vector3Int(14, 0, 0) };
    private Vector3Int[] keyMidHitterCloseGameBucket = new Vector3Int[] { new Vector3Int(0, 0, 0), new Vector3Int(2, 0, 0), new Vector3Int(8, 0, 0), new Vector3Int(12, 0, 0) };
    private Vector3Int[] keyRightHitterCloseGameBucket = new Vector3Int[] { new Vector3Int(7, 0, 0), new Vector3Int(10, 0, 0), new Vector3Int(13, 0, 0), new Vector3Int(16, 0, 0) };
    private Vector3Int[] neutralCloseGameBucket = new Vector3Int[] { new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0), new Vector3Int(2, 0, 0), new Vector3Int(4, 0, 0), new Vector3Int(5, 0, 0), 
        new Vector3Int(6, 0, 0), new Vector3Int(7, 0, 0), new Vector3Int(8, 0, 0) };

    private Vector3Int[] conservativeBucket = new Vector3Int[] { new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0), new Vector3Int(3, 0, 0), new Vector3Int(4, 0, 0), new Vector3Int(5, 0, 0),
    new Vector3Int(8, 0, 0), new Vector3Int(12, 0, 0), };


    public void StatPlayerKill(int attackerYValue)
    {
        if (attackerYValue < 3)
            bottomZoneScoreCount++;
        else if (attackerYValue < 6)
            midZoneScoreCount++;
        else topZoneScoreCount++;

        didLastDefensiveStrategyWork = false;

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

        didLastDefensiveStrategyWork = false;

        playerTotalAttacks++;
        playerTools += 1;
        playerHittingAverage = (playerStraightKills + playerTools - playerHittingErrors) / playerTotalAttacks;
    }
    public void StatPlayerHittingError()
    {
        allDefensiveStrategies[lastDefensiveStrategyUsedIndex].z++;

        didLastDefensiveStrategyWork = true;

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
        Debug.Log("statting AI outcome");
        aiTotalAttacks++;
        aiStraightKills += 1;
        aiHittingAverage = (aiStraightKills + aiTools - aiHittingErrors) / aiTotalAttacks;
    }

    public void StatAITool()
    {
        Debug.Log("statting AI outcome");
        aiTotalAttacks++;
        aiTools += 1;
        aiHittingAverage = (aiStraightKills + aiTools - aiHittingErrors) / aiTotalAttacks;
    }
    public void StatAIHittingError()
    {
        Debug.Log("statting AI outcome");
        aiTotalAttacks++;
        aiHittingErrors += 1;
        aiHittingAverage = (aiStraightKills + aiTools - aiHittingErrors) / aiTotalAttacks;
    }
    public void StatAIBlock()
    {
        aiBlocks += 1;
        StatPlayerHittingError();
    }
    public void StatAIDig()
    {
        allDefensiveStrategies[lastDefensiveStrategyUsedIndex].z++;
        didLastDefensiveStrategyWork = true;

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


    private int ReturnDefensiveStrategy(int defensiveStrategyToReturn)
    {
        // Debug.Log("Returning defensive strategy properly");
        lastDefensiveStrategyUsedIndex = defensiveStrategyToReturn;
        allDefensiveStrategies[defensiveStrategyToReturn].y++;
        return defensiveStrategyToReturn;
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
        // Debug.LogWarning("Close game behaviour");
        // check if the last defensive strategy had worked, if it did, use it again
        if (didLastDefensiveStrategyWork)
        {
            // Debug.LogWarning("Repeating successful last defensive strategy");
            return ReturnDefensiveStrategy(lastDefensiveStrategyUsedIndex);
        }

        // check for a consistant hitting zone
        if (topZoneScoreCount - midZoneScoreCount >= 3 && topZoneScoreCount - bottomZoneScoreCount >= 3)
            return BehaviourCloseGameKeyHitter(1);
        else if (midZoneScoreCount - topZoneScoreCount >= 3 && midZoneScoreCount - bottomZoneScoreCount >= 3)
            return BehaviourCloseGameKeyHitter(2);
        else if (bottomZoneScoreCount - topZoneScoreCount >= 3 && bottomZoneScoreCount - midZoneScoreCount >= 3)
            return BehaviourCloseGameKeyHitter(3);

        // else check for a strategy that hasn't been tried yet
        List<Vector3Int> newStratsIndex = new List<Vector3Int>();
        for(int i = 0; i < neutralCloseGameBucket.Length; i++)
        {
            if(allDefensiveStrategies[neutralCloseGameBucket[i].x].y == 0)
            {
                newStratsIndex.Add(allDefensiveStrategies[neutralCloseGameBucket[i].x]);
            }
        }
        if(newStratsIndex.Count > 0)
        {
           // Debug.LogWarning("Trying a new strategy: " + newStratsIndex.Count + " to choose from");
            int number = Random.Range(0, newStratsIndex.Count);
            //for(int i = 0; i < neutralCloseGameBucket.Length; i++)
            //{
            //    if(newStratsIndex[number].x == neutralCloseGameBucket[i].x)
            //        allDefensiveStrategies[neutralCloseGameBucket[i].x].y += 1;
            //}
            //lastDefensiveStrategyUsedIndex = newStratsIndex[number].x;
            return ReturnDefensiveStrategy(newStratsIndex[number].x);
        }

        // else randomly select from the neutral close game pool
        // Debug.LogWarning("Randomly selecting a neutral close game strategy");
        int thing = Random.Range(0, neutralCloseGameBucket.Length);
        return ReturnDefensiveStrategy(neutralCloseGameBucket[thing].x);
    }
    private int BehaviourCloseGameKeyHitter(int hitterToKey)
    {
        int number = -1;

        if(hitterToKey == 1)
        {
           // Debug.LogWarning("Keying on the left side hitter");
            number = Random.Range(0, keyLeftHitterCloseGameBucket.Length);
            return ReturnDefensiveStrategy(keyLeftHitterCloseGameBucket[number].x);
        }
        else if(hitterToKey == 3)
        {
            //Debug.LogWarning("Keying on the right side hitter");
            number = Random.Range(0, keyRightHitterCloseGameBucket.Length);
            return ReturnDefensiveStrategy(keyRightHitterCloseGameBucket[number].x);
        }
        else
        {
            //Debug.LogWarning("Keying on the middle hitter");
            number = Random.Range(0, keyMidHitterCloseGameBucket.Length);
            return ReturnDefensiveStrategy(keyMidHitterCloseGameBucket[number].x);
        }
    }

    private int BehaviourDoMostSuccessfulStrategy()
    {
        // will grab the highest successful strategy, if there is a tie will grab the earliest in the array (tends to be more conservative)
        // if nothing has worked, will return 0 (default strategy)
        //Debug.LogWarning("doing most successful strategy");
        int strategy = 0;
        for(int i = 0; i < allDefensiveStrategies.Length; i++)
        {
            if(allDefensiveStrategies[i].z > allDefensiveStrategies[strategy].z)
            {
                strategy = allDefensiveStrategies[i].x;
            }
        }
        return ReturnDefensiveStrategy(strategy);
    }

    private int BehaviourDoMostSuccessfulConservativeStrategy()
    {
        //Debug.LogWarning("doing most successful conservative strategy");
        int strategy = 0;
        for (int i = 0; i < conservativeBucket.Length; i++)
        {
            if (allDefensiveStrategies[conservativeBucket[i].x].z > allDefensiveStrategies[strategy].z)
            {
                strategy = allDefensiveStrategies[conservativeBucket[i].x].x;
            }
        }
        return ReturnDefensiveStrategy(strategy);
    }




    private int BehaviourDownBig()
    {
        //Debug.LogWarning("Down big behaviour");
        // check if the last defensive strategy had worked, if it did, use it again
        if (didLastDefensiveStrategyWork)
        {
            //Debug.LogWarning("repeating last defensive strategy");
            return ReturnDefensiveStrategy(lastDefensiveStrategyUsedIndex);
        }

        // otherwise, try something new
        for(int i= 0; i < allDefensiveStrategies.Length; i++)
        {
            if (allDefensiveStrategies[i].y == 0)
            {
               // Debug.LogWarning("Trying something new");
                return ReturnDefensiveStrategy(allDefensiveStrategies[i].x);
            }
        }

        // if everything has been tried at least once, check for a key hitter
        if (topZoneScoreCount - midZoneScoreCount >= 3 && topZoneScoreCount - bottomZoneScoreCount >= 3)
            return BehaviourCloseGameKeyHitter(1);
        else if (midZoneScoreCount - topZoneScoreCount >= 3 && midZoneScoreCount - bottomZoneScoreCount >= 3)
            return BehaviourCloseGameKeyHitter(2);
        else if (bottomZoneScoreCount - topZoneScoreCount >= 3 && bottomZoneScoreCount - midZoneScoreCount >= 3)
            return BehaviourCloseGameKeyHitter(3);

        // if no key hitter, do what has worked
       // Debug.Log("No key hitter and nothing new to try");
        return BehaviourDoMostSuccessfulStrategy();
    }







    private int BehaviourUpBig()
    {
        // check if the last defensive strategy had worked, if it did, use it again
        if (didLastDefensiveStrategyWork)
        {
            //Debug.LogWarning("repeating last defensive strategy");
            return ReturnDefensiveStrategy(lastDefensiveStrategyUsedIndex);
        }

        // see if the most successful strategy is clear (at laest x more than the next highest)
        int mostSuccessfulStrategy = 0;
        // get the most successful strategy
        for (int i = 0; i < allDefensiveStrategies.Length; i++)
        {
            if (allDefensiveStrategies[i].z > allDefensiveStrategies[mostSuccessfulStrategy].z)
            {
                mostSuccessfulStrategy = allDefensiveStrategies[i].x;
            }
        }
        // then check if the most successful strategy is x more than the next
        bool isThereClearStrategy = true;
        for (int i = 0; i < allDefensiveStrategies.Length; i++)
        {
            if(allDefensiveStrategies[mostSuccessfulStrategy].x != allDefensiveStrategies[i].x)
            {
                if (allDefensiveStrategies[mostSuccessfulStrategy].y - allDefensiveStrategies[i].y <= 2 )
                {
                    isThereClearStrategy = false;
                }
            }
        }
        if (isThereClearStrategy)
        {
            //Debug.LogWarning("clearly superior defensive strategy present");
            return ReturnDefensiveStrategy(allDefensiveStrategies[mostSuccessfulStrategy].x);
        }

        // if no clear strategy, get the most successful from the very conservative bucket
        return BehaviourDoMostSuccessfulConservativeStrategy();
    }







    public Vector2Int[] GetFrontRowHittersLineup(int passValue)
    {
        Vector2Int[] frontRowHittersLocations = new Vector2Int[3];

        if (passValue == 1) // BAD PASS BEHAVIOUR
        {
            Debug.Log("Hitters move to cover left side and stay off the net on a bad pass");
            frontRowHittersLocations[0] = new Vector2Int(2, 3);
            frontRowHittersLocations[1] = new Vector2Int(3, 2);
            frontRowHittersLocations[2] = new Vector2Int(1, 0);
        }
        else if (passValue == 2) // OK PASS BEHAVIOUR
        {
            Debug.Log("Hitters Stay off the net and spread on an ok pass");
            frontRowHittersLocations[0] = new Vector2Int(1, 8);
            frontRowHittersLocations[1] = new Vector2Int(0, 4);
            frontRowHittersLocations[2] = new Vector2Int(1, 0);
        }
        else if (passValue == 3) // GOOD PASS BEHAVIOUR
        {
            aiHittingAverage = (aiStraightKills + aiTools - aiHittingErrors) / aiTotalAttacks;
            Debug.Log("Hitting average is " + aiHittingAverage);
            if(aiHittingAverage >= 0)
            {
                int offensiveChoice = Random.Range(0, 2);
                if(offensiveChoice == 0)
                {
                    frontRowHittersLocations = BackQuickOffence();
                }
                else
                {
                    frontRowHittersLocations = ShootOffence();
                }
            }
            else
            {
                frontRowHittersLocations = SpreadOffense();
            }
        }
        else Debug.LogError("Something went wrong");

        return frontRowHittersLocations;
    }

    private Vector2Int[] SpreadOffense()
    {
        Vector2Int[] spreadOffense = new Vector2Int[3];
        Debug.Log("Hitters spread across the court evenly");
        spreadOffense[0] = new Vector2Int(0, 8);
        spreadOffense[1] = new Vector2Int(1, 4);
        spreadOffense[2] = new Vector2Int(0, 0);
        return spreadOffense;
    }

    private Vector2Int[] BackQuickOffence()
    {
        Vector2Int[] backQuickOffense = new Vector2Int[3];
        Debug.Log("Hitters spread + a back quick");
        backQuickOffense[0] = new Vector2Int(0, 8);
        backQuickOffense[1] = new Vector2Int(1, 6);
        backQuickOffense[2] = new Vector2Int(0, 0);
        return backQuickOffense;
    }

    private Vector2Int[] ShootOffence()
    {
        Vector2Int[] shootOffense = new Vector2Int[3];
        Debug.Log("Hitters spread + a shoot");
        shootOffense[0] = new Vector2Int(0, 8);
        shootOffense[1] = new Vector2Int(1, 2);
        shootOffense[2] = new Vector2Int(0, 0);
        return shootOffense;
    }


}

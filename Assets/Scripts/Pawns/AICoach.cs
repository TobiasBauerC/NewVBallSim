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


    public void StatPlayerKill()
    {
        playerTotalAttacks++;
        playerStraightKills += 1;
        playerHittingAverage = (playerStraightKills + playerTools - playerHittingErrors)/ playerTotalAttacks;
    }

    public void StatPlayerTool()
    {
        playerTotalAttacks++;
        playerTools += 1;
        playerHittingAverage = (playerStraightKills + playerTools - playerHittingErrors) / playerTotalAttacks;
    }
    public void StatPlayerHittingError()
    {
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
        aiBlocks += 1;
        StatPlayerHittingError();
    }
    public void StatAIDig()
    {
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



        return defensiveStrategy;
    }

}

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



    private const int BIG_LEAD_DIFFERENCE = 5;

    


}

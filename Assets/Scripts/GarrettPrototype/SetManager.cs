using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetManager : MonoBehaviour
{
    private int AteamScore;
    private int BteamScore;

    private bool AteamServing;
    private bool setOver = false;

    private bool rallyStarted = false;

    [SerializeField] private RallyManager rallyManager;
    [SerializeField] private RallyManagerV2 rallyManagerV2;

    [SerializeField] private Text aWinsText;
    [SerializeField] private Text bWinsText;
    private int aWins;
    private int bWins;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text messageText;


    private int coroutineResult = 3;


    // Start is called before the first frame update
    void Start()
    {
        setOver = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Simulate100Sets()
    {
        aWins = 0;
        bWins = 0;
        int counter = 0;
        while (counter < 100)
        {
            SimulateSet();
            counter++;
        }

        aWinsText.text = "A Wins: " + aWins;
        bWinsText.text = "B Wins: " + bWins;
    }

    public void PlaySet()
    {
        StartCoroutine(SimulateSet());
    }

    public void SimulateOneRallyPlayerServing()
    {
        StartCoroutine(rallyManagerV2.SimulateRallyAServing(ReturnTrue, ReturnFalse));
    }

    public void SimulateOneRallyAIServing()
    {
        StartCoroutine(rallyManagerV2.SimulateRallyBServing(ReturnTrue, ReturnFalse));
    }

    private IEnumerator SimulateSet()
    {
        setOver = false;
        // A team serves first
        AteamServing = true;
        AteamScore = 0;
        BteamScore = 0;
        coroutineResult = 3;

        while(setOver == false)
        {
            

            // simulate the rally to see if the serving team won the rally
            bool didServingTeamWinTheRally = false;
            if (AteamServing)
            {
                Debug.Log("A team should be serving");
                // didServingTeamWinTheRally = StartCoroutine(rallyManagerV2.SimulateRallyAServing());

                StartCoroutine(rallyManagerV2.SimulateRallyAServing(ReturnTrue, ReturnFalse));
                yield return new WaitUntil(() => coroutineResult != 3);
                if (coroutineResult == 0)
                    didServingTeamWinTheRally = false;
                else if (coroutineResult == 1)
                    didServingTeamWinTheRally = true;

                coroutineResult = 3;
            }
            else if (!AteamServing)
            {
                Debug.Log("B team should be serving");
                // didServingTeamWinTheRally = rallyManagerV2.SimulateRallyBServing();
                StartCoroutine(rallyManagerV2.SimulateRallyBServing(ReturnTrue, ReturnFalse));
                yield return new WaitUntil(() => coroutineResult != 3);
                if (coroutineResult == 0)
                    didServingTeamWinTheRally = false;
                else if (coroutineResult == 1)
                    didServingTeamWinTheRally = true;

                coroutineResult = 3;
            }

            // serving team won the rally, A serving
            if (didServingTeamWinTheRally && AteamServing)
            {
                Debug.Log("A wins the point, continues serving");
                AteamScore++;
            }

            // serving team won the rally, B serving
            else if(didServingTeamWinTheRally && !AteamServing)
            {
                Debug.Log("B wins the point, continues serving");
                BteamScore++;
            }

            // serving team lost the rally, A serving
            else if(!didServingTeamWinTheRally && AteamServing)
            {
                Debug.Log("B wins the point, takes over the serve");
                BteamScore++;
                AteamServing = !AteamServing;
            }

            // serving team lost the rally, B serving
            else if (!didServingTeamWinTheRally && !AteamServing)
            {
                Debug.Log("A wins the point, takes over the serve");
                AteamScore++;
                AteamServing = !AteamServing;
            }

            Debug.Log("Score: " + AteamScore + " - " + BteamScore);
            scoreText.text = "Score \n" + AteamScore + " - " + BteamScore;

            yield return new WaitForSeconds(2);
            messageText.text = "Rally over";
            yield return new WaitForSeconds(1);

            setOver = SetOverCheck();
        }
        Debug.Log("Set Finished");
        // messageText.text = "Set over";
        yield return new WaitForSeconds(1);
        yield break;
    }

    private bool SetOverCheck()
    {
        if(AteamScore >= 25 && AteamScore > BteamScore && AteamScore - BteamScore > 1)
        {
            Debug.Log("A TEAM WINS");
            messageText.text = "You Win!";
            aWins++;
            return true;
        }
        else if (BteamScore >= 25 && BteamScore > AteamScore && BteamScore - AteamScore > 1)
        {
            Debug.Log("B TEAM WINS");
            messageText.text = "AI Wins";
            bWins++;
            return true;
        }
        else
            return false;
    }

    public void ReturnTrue() { coroutineResult = 1; }
    public void ReturnFalse() { coroutineResult = 0; }
}

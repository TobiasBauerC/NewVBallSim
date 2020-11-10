using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditorInternal;

public class RotationManager : MonoBehaviour
{
    [SerializeField] private GridManager playerGridManager = null;
    [SerializeField] private GridManager aiGridManager = null;

    public Pawn[] playerPositionsArray;
    public Pawn playerSetter;
    public Pawn playerRightSide;
    public Pawn playerMiddle1;
    public Pawn playerMiddle2;
    public Pawn playerLeftSide1;
    public Pawn playerLeftSide2;

    public Pawn[] aiPositionsArray;
    public Pawn aiSetter;
    public Pawn aiRightSide;
    public Pawn aiMiddle1;
    public Pawn aiMiddle2;
    public Pawn aiLeftSide1;
    public Pawn aiLeftSide2;

    private static Vector2 playerPosition1ReceiveLocation = new Vector2(3, 1);
    private static Vector2 playerPosition6ReceiveLocation = new Vector2(3, 4);
    private static Vector2 playerPosition5ReceiveLocation = new Vector2(3, 7);
    private static Vector2 playerPosition4ReceiveLocation = new Vector2(6, 8);
    private static Vector2 playerPosition3ReceiveLocation = new Vector2(8, 4);
    private static Vector2 playerPosition2ReceiveLocation = new Vector2(6, 0);
    private static Vector2[] playerRecievePositions = new Vector2[] { playerPosition6ReceiveLocation, playerPosition1ReceiveLocation, playerPosition2ReceiveLocation, playerPosition3ReceiveLocation, playerPosition4ReceiveLocation, playerPosition5ReceiveLocation };

    private static Vector2 playerPosition1OffenseLocation = new Vector2(5, 0);
    private static Vector2 playerPosition6OffenseLocation = new Vector2(5, 4);
    private static Vector2 playerPosition5OffenseLocation = new Vector2(5, 7);
    private static Vector2 playerPosition4OffenseLocation = new Vector2(8, 8);
    private static Vector2 playerPosition3OffenseLocation = new Vector2(8, 4);
    private static Vector2 playerPosition2OffenseLocation = new Vector2(8, 0);
    private static Vector2[] playerOffensePositions = new Vector2[] { playerPosition6OffenseLocation, playerPosition1OffenseLocation, playerPosition2OffenseLocation, playerPosition3OffenseLocation, playerPosition4OffenseLocation, playerPosition5OffenseLocation };

    private static Vector2 playerSetterPosition1ReceiveLocation = new Vector2(4, 0);
    private static Vector2 playerSetterPosition2ReceiveLocation = new Vector2(8, 2);
    private static Vector2 playerSetterPosition3ReceiveLocation = new Vector2(8, 3);
    private static Vector2 playerSetterPosition4ReceiveLocation = new Vector2(8, 5);
    private static Vector2 playerSetterPosition5ReceiveLocation = new Vector2(5, 8);
    private static Vector2 playerSetterPosition6ReceiveLocation = new Vector2(7, 4);
    private static Vector2[] playerSetterRecievePositions = new Vector2[] { playerSetterPosition6ReceiveLocation, playerSetterPosition1ReceiveLocation, playerSetterPosition2ReceiveLocation, playerSetterPosition3ReceiveLocation, playerSetterPosition4ReceiveLocation, playerSetterPosition5ReceiveLocation };
    private static Vector2 playerSetterPosition1Pass = new Vector2(4, 4);
    private static Vector2 playerSetterPosition2Pass = new Vector2(6, 3);
    private static Vector2 playerSetterPosition3Pass = new Vector2(8, 3);

    private static Vector2 playerDefensivePosition1Location = new Vector2(4, 1);
    private static Vector2 playerDefensivePosition2Location = new Vector2(8, 2);
    private static Vector2 playerDefensivePosition3Location = new Vector2(8, 4);
    private static Vector2 playerDefensivePosition4Location = new Vector2(8, 6);
    private static Vector2 playerDefensivePosition5Location = new Vector2(4, 7);
    private static Vector2 playerDefensivePosition6Location = new Vector2(1, 4);
    private static Vector2 playerDefensivePosition1LocationServing = new Vector2(0, 0);
    private static Vector2[] playerDefensivePositions = new Vector2[] { playerDefensivePosition6Location, playerDefensivePosition1Location, playerDefensivePosition2Location, playerDefensivePosition3Location, playerDefensivePosition4Location, playerDefensivePosition5Location };
    private static Vector2[] playerDefensivePositionsServing = new Vector2[] { playerDefensivePosition6Location, playerDefensivePosition1LocationServing, playerDefensivePosition2Location, playerDefensivePosition3Location, playerDefensivePosition4Location, playerDefensivePosition5Location };

    private static Vector2 aiPosition1ReceiveLocation = new Vector2(5, 6);
    private static Vector2 aiPosition6ReceiveLocation = new Vector2(5, 4);
    private static Vector2 aiPosition5ReceiveLocation = new Vector2(5, 2);
    private static Vector2 aiPosition4ReceiveLocation = new Vector2(2, 0);
    private static Vector2 aiPosition3ReceiveLocation = new Vector2(0, 4);
    private static Vector2 aiPosition2ReceiveLocation = new Vector2(3, 8);
    private static Vector2[] aiRecievePositions = new Vector2[] { aiPosition6ReceiveLocation, aiPosition1ReceiveLocation, aiPosition2ReceiveLocation, aiPosition3ReceiveLocation, aiPosition4ReceiveLocation, aiPosition5ReceiveLocation };

    private static Vector2 aiPosition1OffenseLocation = new Vector2(3, 8);
    private static Vector2 aiPosition6OffenseLocation = new Vector2(3, 4);
    private static Vector2 aiPosition5OffenseLocation = new Vector2(3, 0);
    private static Vector2 aiPosition4OffenseLocation = new Vector2(0, 1);
    private static Vector2 aiPosition3OffenseLocation = new Vector2(0, 4);
    private static Vector2 aiPosition2OffenseLocation = new Vector2(0, 7);
    private static Vector2[] aiOffensePositions = new Vector2[] { aiPosition6OffenseLocation, aiPosition1OffenseLocation, aiPosition2OffenseLocation, aiPosition3OffenseLocation, aiPosition4OffenseLocation, aiPosition5OffenseLocation };

    private static Vector2 aiSetterPosition1ReceiveLocation = new Vector2(4, 8);
    private static Vector2 aiSetterPosition2ReceiveLocation = new Vector2(0, 6);
    private static Vector2 aiSetterPosition3ReceiveLocation = new Vector2(0, 5);
    private static Vector2 aiSetterPosition4ReceiveLocation = new Vector2(0, 3);
    private static Vector2 aiSetterPosition5ReceiveLocation = new Vector2(3, 0);
    private static Vector2 aiSetterPosition6ReceiveLocation = new Vector2(1, 4);
    private static Vector2[] aiSetterRecievePositions = new Vector2[] { aiSetterPosition6ReceiveLocation, aiSetterPosition1ReceiveLocation, aiSetterPosition2ReceiveLocation, aiSetterPosition3ReceiveLocation, aiSetterPosition4ReceiveLocation, aiSetterPosition5ReceiveLocation };
    private static Vector2 aiSetterPosition1Pass = new Vector2(4, 4);
    private static Vector2 aiSetterPosition2Pass = new Vector2(2, 6);
    private static Vector2 aiSetterPosition3Pass = new Vector2(0, 6);

    private static Vector2 aiDefensivePosition1Location = new Vector2(4, 7);
    private static Vector2 aiDefensivePosition2Location = new Vector2(0, 6);
    private static Vector2 aiDefensivePosition3Location = new Vector2(0, 4);
    private static Vector2 aiDefensivePosition4Location = new Vector2(0, 2);
    private static Vector2 aiDefensivePosition5Location = new Vector2(4, 1);
    private static Vector2 aiDefensivePosition6Location = new Vector2(7, 4);
    private static Vector2 aiDefensivePosition1LocationServing = new Vector2(8, 8);
    private static Vector2[] aiDefensivePositions = new Vector2[] { aiDefensivePosition6Location, aiDefensivePosition1Location, aiDefensivePosition2Location, aiDefensivePosition3Location, aiDefensivePosition4Location, aiDefensivePosition5Location };
    private static Vector2[] aiDefensivePositionsServing = new Vector2[] { aiDefensivePosition6Location, aiDefensivePosition1LocationServing, aiDefensivePosition2Location, aiDefensivePosition3Location, aiDefensivePosition4Location, aiDefensivePosition5Location };

    private Pawn[] playerStartingRotation;
    private Pawn[] aiStartingRotation;



    // Start is called before the first frame update
    void Start()
    {
        playerStartingRotation = playerPositionsArray;
        aiStartingRotation = aiPositionsArray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // position 0 in the array represents position 6 on the court
    public Pawn[] GetCurrentPositions()
    {
        return playerPositionsArray;
    }

    private IEnumerator SetPlayerPawnPositions(Vector2[] positions, float travelTime)
    {
        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, false);
            StartCoroutine(Movement.MoveFromAtoB(playerPositionsArray[i].transform, playerPositionsArray[i].transform.position, playerGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y), travelTime));
        }
        yield return new WaitForSeconds(travelTime);
        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, false);
            playerPositionsArray[i].transform.position = playerGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y);
            playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, true);
            Debug.Log("Setting cell occupied at " + positions[i]);
        }
        yield break;
    }

    private IEnumerator SetAIPawnPositions(Vector2[] positions, float travelTime)
    {
        for (int i = 0; i < aiPositionsArray.Length; i++)
        {
            aiGridManager.SetCellOccupied(aiPositionsArray[i].transform.position, false);
            StartCoroutine(Movement.MoveFromAtoB(aiPositionsArray[i].transform, aiPositionsArray[i].transform.position, aiGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y), travelTime));
        }

        yield return new WaitForSeconds(travelTime);

        for (int i = 0; i < aiPositionsArray.Length; i++)
        {
            aiGridManager.SetCellOccupied(aiPositionsArray[i].transform.position, false);
            aiPositionsArray[i].transform.position = aiGridManager.GetGridPosition((int)positions[i].x, (int)positions[i].y);
            aiGridManager.SetCellOccupied(aiPositionsArray[i].transform.position, true);
            Debug.Log("Setting cell occupied at " + positions[i]);
        }

        yield break;
    }

    public void ResetRotations()
    {
        playerPositionsArray = playerStartingRotation;
        aiPositionsArray = aiStartingRotation;
        // Debug.LogWarning("resetting rotations");
    }

    public void RotatePlayer()
    {
        Pawn position1 = playerPositionsArray[2];
        Pawn position2 = playerPositionsArray[3];
        Pawn position3 = playerPositionsArray[4];
        Pawn position4 = playerPositionsArray[5];
        Pawn position5 = playerPositionsArray[0];
        Pawn position6 = playerPositionsArray[1];

        playerPositionsArray[1] = position1;
        playerPositionsArray[2] = position2;
        playerPositionsArray[3] = position3;
        playerPositionsArray[4] = position4;
        playerPositionsArray[5] = position5;
        playerPositionsArray[0] = position6;

        // Debug.LogWarning("Rotating Players");
    }
    public void RotateAI()
    {
        Pawn position1 = aiPositionsArray[2];
        Pawn position2 = aiPositionsArray[3];
        Pawn position3 = aiPositionsArray[4];
        Pawn position4 = aiPositionsArray[5];
        Pawn position5 = aiPositionsArray[0];
        Pawn position6 = aiPositionsArray[1];

        aiPositionsArray[1] = position1;
        aiPositionsArray[2] = position2;
        aiPositionsArray[3] = position3;
        aiPositionsArray[4] = position4;
        aiPositionsArray[5] = position5;
        aiPositionsArray[0] = position6;

        // Debug.LogWarning("Rotating AI's");
    }

    public void SetPlayerRecievePositions(float travelTime)
    {
        Vector2[] positions = new Vector2[] { playerPosition6ReceiveLocation, playerPosition1ReceiveLocation, playerPosition2ReceiveLocation, playerPosition3ReceiveLocation, playerPosition4ReceiveLocation, playerPosition5ReceiveLocation };

        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            if(playerPositionsArray[i].pawnRole == PawnRole.Setter)
            {
                positions[i] = playerSetterRecievePositions[i];
            }
        }

        StartCoroutine(SetPlayerPawnPositions(positions, travelTime));
    }

    public void SetPlayerOffensePositions(int passValue, float travelTime)
    {
        Vector2[] positions = new Vector2[] { playerPosition6OffenseLocation, playerPosition1OffenseLocation, playerPosition2OffenseLocation, playerPosition3OffenseLocation, playerPosition4OffenseLocation, playerPosition5OffenseLocation };

        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            if (playerPositionsArray[i].pawnRole == PawnRole.Setter)
            {
                if (passValue == 1)
                    positions[i] = playerSetterPosition1Pass;
                else if (passValue == 2)
                    positions[i] = playerSetterPosition2Pass;
                else if (passValue == 3)
                    positions[i] = playerSetterPosition3Pass;
            }
        }

        StartCoroutine(SetPlayerPawnPositions(positions, travelTime));
    }
    public void SetPlayerOffensePositions(int passValue, float travelTime, Pawn diggingPawn)
    {
        Vector2[] positions = new Vector2[] { playerPosition6OffenseLocation, playerPosition1OffenseLocation, playerPosition2OffenseLocation, playerPosition3OffenseLocation, playerPosition4OffenseLocation, playerPosition5OffenseLocation };
        bool setterDug = false;
        if (diggingPawn.pawnRole == PawnRole.Setter)
            setterDug = true;

        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            if ((playerPositionsArray[i].pawnRole == PawnRole.Setter && !setterDug) || (playerPositionsArray[i].pawnRole == PawnRole.RightSide && setterDug))
            {
                if (passValue == 1)
                    positions[i] = playerSetterPosition1Pass;
                else if (passValue == 2)
                    positions[i] = playerSetterPosition2Pass;
                else if (passValue == 3)
                    positions[i] = playerSetterPosition3Pass;
            }
        }

        StartCoroutine(SetPlayerPawnPositions(positions, travelTime));
    }

    public void SetPlayerDefensivePositions(float travelTime)
    {
        StartCoroutine(SetPlayerPawnPositions(playerDefensivePositions, travelTime));
    }

    public void SetPlayerServicePositions(float travelTime)
    {
        StartCoroutine(SetPlayerPawnPositions(playerDefensivePositionsServing, travelTime));
    }

    public void SetAIRecievePositions(float travelTime)
    {
        Vector2[] positions = new Vector2[] { aiPosition6ReceiveLocation, aiPosition1ReceiveLocation, aiPosition2ReceiveLocation, aiPosition3ReceiveLocation, aiPosition4ReceiveLocation, aiPosition5ReceiveLocation };

        for (int i = 0; i < aiPositionsArray.Length; i++)
        {
            if (aiPositionsArray[i].pawnRole == PawnRole.Setter)
            {
                positions[i] = aiSetterRecievePositions[i];
            }
        }

        StartCoroutine(SetAIPawnPositions(positions, travelTime));
    }

    public void SetAIOffensePositions(int passValue, float travelTime)
    {
        Vector2[] positions = new Vector2[] { aiPosition6OffenseLocation, aiPosition1OffenseLocation, aiPosition2OffenseLocation, aiPosition3OffenseLocation, aiPosition4OffenseLocation, aiPosition5OffenseLocation };

        for (int i = 0; i < aiPositionsArray.Length; i++)
        {
            if (aiPositionsArray[i].pawnRole == PawnRole.Setter)
            {
                if (passValue == 1)
                    positions[i] = aiSetterPosition1Pass;
                else if (passValue == 2)
                    positions[i] = aiSetterPosition2Pass;
                else if (passValue == 3)
                    positions[i] = aiSetterPosition3Pass;
            }
        }

        StartCoroutine(SetAIPawnPositions(positions, travelTime));
    }

    public void SetAIOffensePositions(int passValue, float travelTime, Pawn diggingPawn)
    {
        Vector2[] positions = new Vector2[] { aiPosition6OffenseLocation, aiPosition1OffenseLocation, aiPosition2OffenseLocation, aiPosition3OffenseLocation, aiPosition4OffenseLocation, aiPosition5OffenseLocation };
        bool setterDug = false;
        if (diggingPawn.pawnRole == PawnRole.Setter)
            setterDug = true;

        for (int i = 0; i < aiPositionsArray.Length; i++)
        {
            if ((aiPositionsArray[i].pawnRole == PawnRole.Setter && !setterDug) || (aiPositionsArray[i].pawnRole == PawnRole.RightSide && setterDug))
            {
                if (passValue == 1)
                    positions[i] = aiSetterPosition1Pass;
                else if (passValue == 2)
                    positions[i] = aiSetterPosition2Pass;
                else if (passValue == 3)
                    positions[i] = aiSetterPosition3Pass;
            }
        }

        StartCoroutine(SetAIPawnPositions(positions, travelTime));
    }

    public void SetAIDefensivePositions(float travelTime)
    {
        StartCoroutine(SetAIPawnPositions(aiDefensivePositions, travelTime));
    }

    public void SetAIServicePositions(float travelTime)
    {
        StartCoroutine(SetAIPawnPositions(aiDefensivePositionsServing, travelTime));
    }

    public bool IsPawnRotationFrontRow(Pawn pawn)
    {
        if (pawn == playerPositionsArray[0] || pawn == playerPositionsArray[5] || pawn == playerPositionsArray[1])
        {
            return false;
        }
        else return true;
    }

    public bool IsPlayerPawnLocatedInBackRow(Pawn pawn)
    {
        if (playerGridManager.GetGridXYPosition(pawn.transform.position).x > 5)
        {
            return false;
        }
        else return true;
    }

    public void ActivateSetButtonsBasedOnPassDigNumber(int passDigNumber, Pawn diggingPawn)
    {
        if (passDigNumber > 0)
        {
            // left side and C ball are available on a 1 pass
            if (playerPositionsArray[4].pawnRole != PawnRole.Setter)
                playerPositionsArray[4].setButton.transform.gameObject.SetActive(true);
            if (playerPositionsArray[1].pawnRole != PawnRole.Setter && IsPlayerPawnLocatedInBackRow(playerPositionsArray[1]))
                playerPositionsArray[1].setButton.transform.gameObject.SetActive(true);
        }
        if (passDigNumber > 1)
        {
            // right side, Pipe and A ball are available on a 2 pass
            if (playerPositionsArray[2].pawnRole != PawnRole.Setter)
                playerPositionsArray[2].setButton.transform.gameObject.SetActive(true);
            if (playerPositionsArray[5].pawnRole != PawnRole.Setter && IsPlayerPawnLocatedInBackRow(playerPositionsArray[5]))
                playerPositionsArray[5].setButton.transform.gameObject.SetActive(true);
            if (playerPositionsArray[0].pawnRole != PawnRole.Setter && IsPlayerPawnLocatedInBackRow(playerPositionsArray[0]))
                playerPositionsArray[0].setButton.transform.gameObject.SetActive(true);
        }
        if (passDigNumber > 2)
        {
            // middle and dump are available on a 3 pass
            if (playerPositionsArray[3].pawnRole != PawnRole.Setter)
                playerPositionsArray[3].setButton.transform.gameObject.SetActive(true);
            for (int i = 0; i < playerPositionsArray.Length; i++)
            {
                if (playerPositionsArray[i].pawnRole == PawnRole.Setter)
                {
                    if (IsPawnRotationFrontRow(playerPositionsArray[i]))
                    {
                        playerPositionsArray[i].setButton.transform.gameObject.SetActive(true);
                    }
                }
            }
        }

        // disable the setter and right side hitters if the setter dug the ball
        if(diggingPawn.pawnRole == PawnRole.Setter)
        {
            for (int i = 0; i < playerPositionsArray.Length; i++)
            {
                if (playerPositionsArray[i].pawnRole == PawnRole.Setter || playerPositionsArray[i].pawnRole == PawnRole.RightSide)
                {
                    playerPositionsArray[i].setButton.transform.gameObject.SetActive(false);
                }
            }
        }

        // check to make sure at least one set option is available
        int setOptions = 0;
        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            if (playerPositionsArray[i].setButton.IsActive())
                setOptions++;
        }
        if(setOptions < 1)
        {
            if(playerPositionsArray[0].pawnRole != PawnRole.Setter && playerPositionsArray[0] != diggingPawn)
                playerPositionsArray[0].setButton.transform.gameObject.SetActive(true);
            else if (playerPositionsArray[2].pawnRole != PawnRole.Setter && playerPositionsArray[2] != diggingPawn)
                playerPositionsArray[2].setButton.transform.gameObject.SetActive(true);
            else if (playerPositionsArray[5].pawnRole != PawnRole.Setter && playerPositionsArray[5] != diggingPawn)
                playerPositionsArray[5].setButton.transform.gameObject.SetActive(true);
        }

        Debug.Log("Activated the buttons fine");
    }

    public void TurnOffAllSetButtons()
    {
        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            playerPositionsArray[i].setButton.transform.gameObject.SetActive(false);
        }
    }
}

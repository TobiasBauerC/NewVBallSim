using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotationManager : MonoBehaviour
{
    [SerializeField] private GridManager playerGridManager;
    [SerializeField] private GridManager aiGridManager;

    [SerializeField] private Pawn[] playerPositionsArray;
    public Pawn playerSetter;
    public Pawn playerRightSide;
    public Pawn playerMiddle1;
    public Pawn playerMiddle2;
    public Pawn playerLeftSide1;
    public Pawn playerLeftSide2;

    [SerializeField] private Pawn[] aiPositionsArray;
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

    private static Vector2 playerSetterPosition1ReceiveLocation = new Vector2(4, 0);
    private static Vector2 playerSetterPosition2ReceiveLocation = new Vector2(8, 2);
    private static Vector2 playerSetterPosition3ReceiveLocation = new Vector2(8, 3);
    private static Vector2 playerSetterPosition4ReceiveLocation = new Vector2(8, 5);
    private static Vector2 playerSetterPosition5ReceiveLocation = new Vector2(5, 8);
    private static Vector2 playerSetterPosition6ReceiveLocation = new Vector2(7, 4);
    private static Vector2[] playerSetterRecievePositions = new Vector2[] { playerSetterPosition6ReceiveLocation, playerSetterPosition1ReceiveLocation, playerSetterPosition2ReceiveLocation, playerSetterPosition3ReceiveLocation, playerSetterPosition4ReceiveLocation, playerSetterPosition5ReceiveLocation };

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

    private static Vector2 aiSetterPosition1ReceiveLocation = new Vector2(4, 8);
    private static Vector2 aiSetterPosition2ReceiveLocation = new Vector2(0, 6);
    private static Vector2 aiSetterPosition3ReceiveLocation = new Vector2(0, 5);
    private static Vector2 aiSetterPosition4ReceiveLocation = new Vector2(0, 3);
    private static Vector2 aiSetterPosition5ReceiveLocation = new Vector2(3, 0);
    private static Vector2 aiSetterPosition6ReceiveLocation = new Vector2(1, 4);
    private static Vector2[] aiSetterRecievePositions = new Vector2[] { aiSetterPosition6ReceiveLocation, aiSetterPosition1ReceiveLocation, aiSetterPosition2ReceiveLocation, aiSetterPosition3ReceiveLocation, aiSetterPosition4ReceiveLocation, aiSetterPosition5ReceiveLocation };

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

    private void SetPlayerPawnPositions(Vector2[] positions)
    {
        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, false);
            playerPositionsArray[i].transform.position = playerGridManager.GetGridPosition((int)positions[i].x, (int)positions[i].y);
            playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, true);
        }
    }

    private void SetAIPawnPositions(Vector2[] positions)
    {
        for (int i = 0; i < aiPositionsArray.Length; i++)
        {
            aiGridManager.SetCellOccupied(aiPositionsArray[i].transform.position, false);
            aiPositionsArray[i].transform.position = aiGridManager.GetGridPosition((int)positions[i].x, (int)positions[i].y);
            aiGridManager.SetCellOccupied(aiPositionsArray[i].transform.position, true);
        }
    }

    public void ResetRotations()
    {
        playerPositionsArray = playerStartingRotation;
        aiPositionsArray = aiStartingRotation;
        Debug.LogWarning("resetting rotations");
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

        Debug.LogWarning("Rotating Players");
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

        Debug.LogWarning("Rotating AI's");
    }

    public void SetPlayerRecievePositions()
    {
        Vector2[] positions = new Vector2[] { playerPosition6ReceiveLocation, playerPosition1ReceiveLocation, playerPosition2ReceiveLocation, playerPosition3ReceiveLocation, playerPosition4ReceiveLocation, playerPosition5ReceiveLocation };

        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            if(playerPositionsArray[i].pawnRole == PawnRole.Setter)
            {
                positions[i] = playerSetterRecievePositions[i];
            }
        }

        SetPlayerPawnPositions(positions);
    }

    public void SetPlayerDefensivePositions()
    {
        SetPlayerPawnPositions(playerDefensivePositions);
    }

    public void SetPlayerServicePositions()
    {
        SetPlayerPawnPositions(playerDefensivePositionsServing);
    }

    public void SetAIRecievePositions()
    {
        Vector2[] positions = new Vector2[] { aiPosition6ReceiveLocation, aiPosition1ReceiveLocation, aiPosition2ReceiveLocation, aiPosition3ReceiveLocation, aiPosition4ReceiveLocation, aiPosition5ReceiveLocation };

        for (int i = 0; i < aiPositionsArray.Length; i++)
        {
            if (aiPositionsArray[i].pawnRole == PawnRole.Setter)
            {
                positions[i] = aiSetterRecievePositions[i];
            }
        }

        SetAIPawnPositions(positions);
    }

    public void SetAIDefensivePositions()
    {
        SetAIPawnPositions(aiDefensivePositions);
    }

    public void SetAIServicePositions()
    {
        SetAIPawnPositions(aiDefensivePositionsServing);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// using UnityEditorInternal;

public class RotationManager : MonoBehaviour
{
    public GridManager playerGridManager = null;
    public GridManager aiGridManager = null;

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
    private static Vector2 aiPosition4ReceiveLocation = new Vector2(3, 1);
    private static Vector2 aiPosition3ReceiveLocation = new Vector2(1, 4);
    private static Vector2 aiPosition2ReceiveLocation = new Vector2(4, 7);
    private static Vector2[] aiRecievePositions = new Vector2[] { aiPosition6ReceiveLocation, aiPosition1ReceiveLocation, aiPosition2ReceiveLocation, aiPosition3ReceiveLocation, aiPosition4ReceiveLocation, aiPosition5ReceiveLocation };

    private static Vector2 aiPosition1OffenseLocation = new Vector2(3, 8);
    private static Vector2 aiPosition6OffenseLocation = new Vector2(3, 4);
    private static Vector2 aiPosition5OffenseLocation = new Vector2(3, 0);
    private static Vector2 aiPosition4OffenseLocation = new Vector2(0, 1);
    private static Vector2 aiPosition3OffenseLocation = new Vector2(0, 4);
    private static Vector2 aiPosition2OffenseLocation = new Vector2(0, 7);
    private static Vector2[] aiOffensePositions = new Vector2[] { aiPosition6OffenseLocation, aiPosition1OffenseLocation, aiPosition2OffenseLocation, aiPosition3OffenseLocation, aiPosition4OffenseLocation, aiPosition5OffenseLocation };

    private static Vector2 aiSetterPosition1ReceiveLocation = new Vector2(5, 8);
    private static Vector2 aiSetterPosition2ReceiveLocation = new Vector2(0, 6);
    private static Vector2 aiSetterPosition3ReceiveLocation = new Vector2(0, 5);
    private static Vector2 aiSetterPosition4ReceiveLocation = new Vector2(0, 3);
    private static Vector2 aiSetterPosition5ReceiveLocation = new Vector2(3, 0);
    private static Vector2 aiSetterPosition6ReceiveLocation = new Vector2(2, 4);
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

    private void ResetAIDefensivePositions()
    {
        aiDefensivePositions = new Vector2[] { aiDefensivePosition6Location, aiDefensivePosition1Location, aiDefensivePosition2Location, aiDefensivePosition3Location, aiDefensivePosition4Location, aiDefensivePosition5Location };
    }

    private IEnumerator SetPlayerPawnPositions(Vector2[] positions, float travelTime)
    {
        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            if(Vector3.Distance(playerPositionsArray[i].transform.position, playerGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y)) > 0.3f)
                playerPositionsArray[i].GetMyManager().SetAnimation(playerPositionsArray[i], 1, playerGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y));
            playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, false);
            StartCoroutine(Movement.MoveFromAtoB(playerPositionsArray[i].transform, playerPositionsArray[i].transform.position, playerGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y), travelTime));
        }
        yield return new WaitForSeconds(travelTime);
        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            playerPositionsArray[i].GetMyManager().SetAnimation(playerPositionsArray[i], 0);
            playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, false);
            playerPositionsArray[i].transform.position = playerGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y);
            playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, true);
            // Debug.Log("Setting cell occupied at " + positions[i]);
        }
        yield break;
    }

    private IEnumerator SetPlayerPawnServicePositions(Vector2[] positions, float travelTime)
    {
        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            if (Vector3.Distance(playerPositionsArray[i].transform.position, playerGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y)) > 0.3f)
                playerPositionsArray[i].GetMyManager().SetAnimation(playerPositionsArray[i], 1, playerGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y));
            if (i != 1)
            {
                playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, false);
                StartCoroutine(Movement.MoveFromAtoB(playerPositionsArray[i].transform, playerPositionsArray[i].transform.position, playerGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y) , travelTime));
            }
            else
            {
                playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, false);
                StartCoroutine(Movement.MoveFromAtoB(playerPositionsArray[i].transform, playerPositionsArray[i].transform.position, playerGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y) + new Vector3(-1, 0, 0), travelTime));
            }
        }
        yield return new WaitForSeconds(travelTime);
        for (int i = 0; i < playerPositionsArray.Length; i++)
        {

            playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, false);
            playerPositionsArray[i].transform.position = playerGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y);
            playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, true);
            if (i != 1)
            {
                playerPositionsArray[i].GetMyManager().SetAnimation(playerPositionsArray[i], 0);
                playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, false);
                playerPositionsArray[i].transform.position = playerGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y);
                playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, true);
            }
            else
            {
                playerPositionsArray[i].GetMyManager().SetAnimation(playerPositionsArray[i], -1);
                playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, false);
                playerPositionsArray[i].transform.position = playerGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y) + new Vector3(-1, 0, 0);
                playerGridManager.SetCellOccupied(playerPositionsArray[i].transform.position, true);
            }
        }
        yield break;
    }

    private IEnumerator SetAIPawnPositions(Vector2[] positions, float travelTime)
    {
        for (int i = 0; i < aiPositionsArray.Length; i++)
        {
            if (Vector3.Distance(aiPositionsArray[i].transform.position, aiGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y)) > 0.3f)
                aiPositionsArray[i].GetMyManager().SetAnimation(aiPositionsArray[i], 1, aiGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y));
            aiGridManager.SetCellOccupied(aiPositionsArray[i].transform.position, false);
            StartCoroutine(Movement.MoveFromAtoB(aiPositionsArray[i].transform, aiPositionsArray[i].transform.position, aiGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y), travelTime));
        }

        yield return new WaitForSeconds(travelTime);

        for (int i = 0; i < aiPositionsArray.Length; i++)
        {
            aiPositionsArray[i].GetMyManager().SetAnimation(aiPositionsArray[i], 0);
            aiGridManager.SetCellOccupied(aiPositionsArray[i].transform.position, false);
            aiPositionsArray[i].transform.position = aiGridManager.GetGridPosition((int)positions[i].x, (int)positions[i].y);
            aiGridManager.SetCellOccupied(aiPositionsArray[i].transform.position, true);
        }

        yield break;
    }

    private IEnumerator SetAIPawnServicePositions(Vector2[] positions, float travelTime)
    {
        for (int i = 0; i < aiPositionsArray.Length; i++)
        {
            if (Vector3.Distance(aiPositionsArray[i].transform.position, aiGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y)) > 0.3f)
                aiPositionsArray[i].GetMyManager().SetAnimation(aiPositionsArray[i], 1, aiGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y));
            if (i != 1)
            {
                
                aiGridManager.SetCellOccupied(aiPositionsArray[i].transform.position, false);
                StartCoroutine(Movement.MoveFromAtoB(aiPositionsArray[i].transform, aiPositionsArray[i].transform.position, aiGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y), travelTime));
            }
            else
            {
                
                aiGridManager.SetCellOccupied(aiPositionsArray[i].transform.position, false);
                StartCoroutine(Movement.MoveFromAtoB(aiPositionsArray[i].transform, aiPositionsArray[i].transform.position, aiGridManager.ForceGetGridPosition((int)positions[i].x, (int)positions[i].y) + new Vector3(1, 0, 0), travelTime));

            }
        }

        yield return new WaitForSeconds(travelTime);

        for (int i = 0; i < aiPositionsArray.Length; i++)
        {
            if (i != 1)
            {
                aiPositionsArray[i].GetMyManager().SetAnimation(aiPositionsArray[i], 0);
                aiGridManager.SetCellOccupied(aiPositionsArray[i].transform.position, false);
                aiPositionsArray[i].transform.position = aiGridManager.GetGridPosition((int)positions[i].x, (int)positions[i].y);
                aiGridManager.SetCellOccupied(aiPositionsArray[i].transform.position, true);
            }
            else
            {
                aiPositionsArray[i].GetMyManager().SetAnimation(aiPositionsArray[i], -1);
                aiGridManager.SetCellOccupied(aiPositionsArray[i].transform.position, false);
                aiPositionsArray[i].transform.position = aiGridManager.GetGridPosition((int)positions[i].x, (int)positions[i].y) + new Vector3(1, 0, 0);
                aiGridManager.SetCellOccupied(aiPositionsArray[i].transform.position, true);
            }
        }

        yield break;
    }

    public IEnumerator LineUpBlockers(float travelTime)
    {
        Vector2[] newPositions = aiDefensivePositions;
        Vector2[] playerCurrentPositions = new Vector2[] { playerGridManager.GetGridXYPosition(playerPositionsArray[0].transform.position),
        playerGridManager.GetGridXYPosition(playerPositionsArray[1].transform.position),
        playerGridManager.GetGridXYPosition(playerPositionsArray[2].transform.position),
        playerGridManager.GetGridXYPosition(playerPositionsArray[3].transform.position),
        playerGridManager.GetGridXYPosition(playerPositionsArray[4].transform.position),
        playerGridManager.GetGridXYPosition(playerPositionsArray[5].transform.position)};

        int blockingStrategy = AICoach.Instance.GetDefensiveStrategy();
        // blockingStrategy = 1;

        // STRATEGY 0
        // block strategy 0 -> just line up in base defense spots and react
        if (blockingStrategy == 0)
        {
           //  Debug.LogWarning("Executing 'neutral' strategy");
            // do nothing, default positions are correct
        }

        // Strategy 1
        // block strategy 1 -> line the blockers up with their attackers, 6 moves up
        else if (blockingStrategy == 1)
        {
           // Debug.LogWarning("Executing 'line up' strategy");
            newPositions[2].y = playerCurrentPositions[4].y;
            newPositions[3].y = playerCurrentPositions[3].y;
            newPositions[4].y = playerCurrentPositions[2].y;
            newPositions[0].x -= 1;

            // check if all three attackers are stacked
            if(playerCurrentPositions[4].y == playerCurrentPositions[3].y && playerCurrentPositions[4].y == playerCurrentPositions[2].y)
            {
                // check if position is at the top of the grid
                if(playerCurrentPositions[4].y == 8)
                {
                    newPositions[2].y = 8;
                    newPositions[3].y = 7;
                    newPositions[4].y = 6;
                }
                // check if position is at the bottom of the grid
                else if(playerCurrentPositions[4].y == 0)
                {
                    newPositions[2].y = 2;
                    newPositions[3].y = 1;
                    newPositions[4].y = 0;
                }
                // otherwise can stack the triple on the stack
                else
                {
                    newPositions[2].y = playerCurrentPositions[4].y + 1;
                    newPositions[3].y = playerCurrentPositions[3].y;
                    newPositions[4].y = playerCurrentPositions[2].y - 1;
                }
            }
            // check if any two of the positions match
            else if (playerCurrentPositions[4].y == playerCurrentPositions[3].y || playerCurrentPositions[4].y == playerCurrentPositions[2].y || playerCurrentPositions[3].y == playerCurrentPositions[2].y)
            {
                if(playerCurrentPositions[4].y == playerCurrentPositions[3].y)
                {
                    // check if position is at the top of the grid
                    if (playerCurrentPositions[4].y == 8)
                    {
                        newPositions[2].y = 8;
                        newPositions[3].y = 7;
                    }
                    // check if position is at the bottom of the grid
                    else if (playerCurrentPositions[4].y == 0)
                    {
                        newPositions[2].y = 1;
                        newPositions[3].y = 0;
                    }
                    // otherwise can stack the double on the stack
                    else
                    {
                        newPositions[3].y = playerCurrentPositions[3].y;
                        if (newPositions[2].y > 4)
                            newPositions[2].y = playerCurrentPositions[4].y - 1;
                        else newPositions[2].y = playerCurrentPositions[4].y + 1;
                    }
                }
                else if(playerCurrentPositions[4].y == playerCurrentPositions[2].y)
                {
                    // check if position is at the top of the grid
                    if (playerCurrentPositions[4].y == 8)
                    {
                        newPositions[2].y = 8;
                        newPositions[4].y = 7;
                    }
                    // check if position is at the bottom of the grid
                    else if (playerCurrentPositions[4].y == 0)
                    {
                        newPositions[2].y = 1;
                        newPositions[4].y = 0;
                    }
                    // otherwise can stack the double on the stack
                    else
                    {
                        newPositions[4].y = playerCurrentPositions[2].y;

                        if (newPositions[2].y > 4)
                            newPositions[2].y = playerCurrentPositions[4].y - 1;
                        else newPositions[2].y = playerCurrentPositions[4].y + 1;
                    }
                }
                else if(playerCurrentPositions[3].y == playerCurrentPositions[2].y)
                {
                    // check if position is at the top of the grid
                    if (playerCurrentPositions[3].y == 8)
                    {
                        newPositions[3].y = 8;
                        newPositions[4].y = 7;
                    }
                    // check if position is at the bottom of the grid
                    else if (playerCurrentPositions[3].y == 0)
                    {
                        newPositions[3].y = 1;
                        newPositions[4].y = 0;
                    }
                    // otherwise can stack the double on the stack
                    else
                    {
                        newPositions[3].y = playerCurrentPositions[3].y + 1;
                        newPositions[4].y = playerCurrentPositions[2].y;

                        if (newPositions[3].y > 4)
                            newPositions[3].y = playerCurrentPositions[3].y - 1;
                        else newPositions[3].y = playerCurrentPositions[3].y + 1;
                    }
                }
            }

            bool positionStillOverlapping = false;
            int indexOfOverlappingPosition = -1;
            int indexOfOtherPosition = -1;
            

            if(newPositions[2].y == newPositions[3].y || newPositions[2].y == newPositions[4].y)
            {
                positionStillOverlapping = true;
                indexOfOverlappingPosition = 2;
                indexOfOtherPosition = 4;
            }
            else if(newPositions[4].y == newPositions[2].y || newPositions[3].y == newPositions[3].y)
            {
                positionStillOverlapping = true;
                indexOfOverlappingPosition = 4;
                indexOfOtherPosition = 2;
            }

            if(positionStillOverlapping && indexOfOverlappingPosition >= 0 && indexOfOtherPosition >= 0)
            {
                // Debug.Log("blockers positions were still overlapping, adjusting now");
                if(newPositions[indexOfOverlappingPosition].y > 4)
                {
                    while(newPositions[indexOfOverlappingPosition].y == newPositions[3].y || newPositions[indexOfOverlappingPosition].y == newPositions[indexOfOtherPosition].y)
                    {
                        newPositions[indexOfOverlappingPosition].y -= 1;
                    }
                }
                else
                {
                    while (newPositions[indexOfOverlappingPosition].y == newPositions[3].y || newPositions[indexOfOverlappingPosition].y == newPositions[indexOfOtherPosition].y)
                    {
                        newPositions[indexOfOverlappingPosition].y += 1;
                    }
                }
            }
        }

        // STRATEGY 2
        // block strategy 2 -> blockers bunch close together in the middle, six moves up 
        else if(blockingStrategy == 2)
        {
           // Debug.LogWarning("Executing 'middle triple' strategy");
            newPositions[2].y = 5;
            newPositions[3].y = 4;
            newPositions[4].y = 3;
            newPositions[0].x -= 1;
        }

        // STRATEGY 3
        // block strategy 3 -> blockers peel and play defense
        else if(blockingStrategy == 3)
        {
          //  Debug.LogWarning("Executing 'peel' strategy");
            newPositions[2].x += 2;
            newPositions[3].x += 1;
            newPositions[4].x += 2;
            newPositions[0].x -= 1;
            newPositions[1].x += 1;
            newPositions[5].x += 1;
        }

        // Strategy 4
        // Wide Spread
        else if(blockingStrategy == 4)
        {
          //  Debug.LogWarning("Executing 'Wide Spread' strategy");
            newPositions[2].y = 8;
            newPositions[3].y = 4;
            newPositions[4].y = 0;
            newPositions[1].y -= 1;
            newPositions[0].x -= 1;
            newPositions[5].y += 1;
        }

        // Strategy 5
        // Mid Spread
        else if (blockingStrategy == 5)
        {
          //  Debug.LogWarning("Executing 'Mid Spread w/ 6 up' strategy");
            newPositions[2].y = 7;
            newPositions[3].y = 4;
            newPositions[4].y = 1;
            newPositions[1].x += 2;
            newPositions[0].x = 3;
            newPositions[5].x += 2;
        }

        // Strategy 6
        // Left Heavy
        else if (blockingStrategy == 6)
        {
           // Debug.LogWarning("Executing 'Left Heavy' strategy");
            newPositions[2].y = 8;
            newPositions[3].y = 6;
            newPositions[4].y = 4;
            newPositions[0].y -= 1;
            newPositions[0].x -= 1;
        }

        // Strategy 7
        // Right Heavy
        else if (blockingStrategy == 7)
        {
          //  Debug.LogWarning("Executing 'Right Heavy' strategy");
            newPositions[2].y = 4;
            newPositions[3].y = 2;
            newPositions[4].y = 0;
            newPositions[0].y += 1;
            newPositions[0].x -= 1;
        }

        // Strategy 8
        // 2 Blockers
        else if (blockingStrategy == 8)
        {
         //   Debug.LogWarning("Executing '2 Blockers' strategy");
            newPositions[2].y = 6;
            newPositions[3].x += 2;
            newPositions[4].y = 2;
        }

        // Strategy 9
        // Left Shift
        else if (blockingStrategy == 9)
        {
           // Debug.LogWarning("Executing 'Left Shift' strategy");
            newPositions[2] = new Vector2(0, 7);
            newPositions[3] = new Vector2(0, 5);
            newPositions[4] = new Vector2(4, 1);
            newPositions[1].x -= 2;
            newPositions[0].y += 3;
            newPositions[5].x += 3;
            newPositions[5].y += 1;
        }

        // Strategy 10
        // Right Shift
        else if (blockingStrategy == 10)
        {
           // Debug.LogWarning("Executing 'Right Shift' strategy");
            newPositions[2] = new Vector2(4, 7);
            newPositions[3] = new Vector2(0, 3);
            newPositions[4] = new Vector2(0, 1);
            newPositions[5].x -= 2;
            newPositions[0].y -= 3;
            newPositions[1].x += 3;
            newPositions[1].y -= 1;
        }

        // Strategy 11
        // Solo Up
        else if (blockingStrategy == 11)
        {
          //  Debug.LogWarning("Executing 'Solo Up' strategy");
            // newPositions[2].x += 2;
            newPositions[3].x += 2;
            newPositions[4].x += 2;
            newPositions[0].x -= 1;
            newPositions[1].x += 1;
            newPositions[5].x += 1;
        }

        // Strategy 12
        // Solo Mid
        else if (blockingStrategy == 12)
        {
          //  Debug.LogWarning("Executing 'Solo Mid' strategy");
            newPositions[2].x += 2;
            // newPositions[3].x += 2;
            newPositions[4].x += 2;
            newPositions[0].x -= 1;
            newPositions[1].x += 1;
            newPositions[5].x += 1;
        }

        // Strategy 13
        // Solo Down
        else if (blockingStrategy == 13)
        {
           // Debug.LogWarning("Executing 'Solo Down' strategy");
            newPositions[2].x += 2;
            newPositions[3].x += 2;
            // newPositions[4].x += 2;
            newPositions[0].x -= 1;
            newPositions[1].x += 1;
            newPositions[5].x += 1;
        }

        // Strategy 14
        // Double Up - Easy
        else if (blockingStrategy == 14)
        {
           // Debug.LogWarning("Executing 'Double Up - Easy' strategy");
            newPositions[2].y = 6;
            newPositions[3].y = 5;
            newPositions[4] = new Vector2(3, 4);
            newPositions[1].x += 1;
        }

        // Strategy 15
        // Double Mid - Easy
        else if (blockingStrategy == 15)
        {
          //  Debug.LogWarning("Executing 'Double Mid - Easy' strategy");
            newPositions[2].y = 5;
            newPositions[3].y = 4;
            newPositions[4] = new Vector2(3, 3);
            newPositions[1].x -= 1;
        }

        // Strategy 16
        // Double Down - Easy
        else if (blockingStrategy == 16)
        {
           // Debug.LogWarning("Executing 'Double Down - Easy' strategy");
            newPositions[2] = new Vector2(3, 4);
            newPositions[3].y = 3;
            newPositions[4].y = 2;
        }

        // Strategy 17
        // Double Up - Hard
        else if (blockingStrategy == 17)
        {
           // Debug.LogWarning("Executing 'Double Up - Hard' strategy");
            newPositions[2].y = 7;
            newPositions[3].y = 6;
            newPositions[4] = new Vector2(3, 5);
            newPositions[1].x += 1;
            newPositions[5].x -= 1;
            newPositions[0].y -= 1;
        }

        // Strategy 18
        // Double Mid - Hard
        else if (blockingStrategy == 18)
        {
          //  Debug.LogWarning("Executing 'Double Mid - Hard' strategy");
            newPositions[2].y = 5;
            newPositions[3].y = 4;
            newPositions[4] = new Vector2(3, 4);
            newPositions[1].x -= 1;
            newPositions[5].x -= 1;
            newPositions[0].x -= 1;
        }

        // Strategy 19
        // Double Down - Hard
        else if (blockingStrategy == 19)
        {
          //  Debug.LogWarning("Executing 'Double Down - Hard' strategy");
            newPositions[2] = new Vector2(3, 4);
            newPositions[3].y = 2;
            newPositions[4].y = 1;
            newPositions[1].x -= 1;
            newPositions[5].x += 1;
            newPositions[0].y += 1;
        }

        StartCoroutine(SetAIPawnPositions(newPositions, travelTime));
        ResetAIDefensivePositions();
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
        StartCoroutine(SetPlayerPawnServicePositions(playerDefensivePositionsServing, travelTime));
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

        // update the positions with values taken from the coach
        Vector2Int[] updatedFrontRowPositions = AICoach.Instance.GetFrontRowHittersLineup(passValue);

        if((aiPositionsArray[2].pawnRole != PawnRole.Setter && !setterDug) && !(aiPositionsArray[2].pawnRole == PawnRole.RightSide && setterDug))
            positions[2] = updatedFrontRowPositions[0];
        if ((aiPositionsArray[3].pawnRole != PawnRole.Setter && !setterDug) && !(aiPositionsArray[3].pawnRole == PawnRole.RightSide && setterDug))
            positions[3] = updatedFrontRowPositions[1];
        if ((aiPositionsArray[4].pawnRole != PawnRole.Setter && !setterDug) && !(aiPositionsArray[4].pawnRole == PawnRole.RightSide && setterDug))
            positions[4] = updatedFrontRowPositions[2];

        StartCoroutine(SetAIPawnPositions(positions, travelTime));
    }

    public void SetAIDefensivePositions(float travelTime)
    {
        StartCoroutine(SetAIPawnPositions(aiDefensivePositions, travelTime));
    }

    public void SetAIServicePositions(float travelTime)
    {
        StartCoroutine(SetAIPawnServicePositions(aiDefensivePositionsServing, travelTime));
    }

    public bool IsPawnRotationFrontRow(Pawn pawn)
    {
        if (pawn == playerPositionsArray[0] || pawn == playerPositionsArray[5] || pawn == playerPositionsArray[1])
        {
            return false;
        }
        else return true;
    }

    public bool IsAnyPawnRotationFrontRow(Pawn pawn)
    {
        if (pawn == playerPositionsArray[0] || pawn == playerPositionsArray[5] || pawn == playerPositionsArray[1] || pawn == aiPositionsArray[0] || pawn == aiPositionsArray[5] || pawn == aiPositionsArray[1])
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

        // Debug.Log("Activated the buttons fine");
    }

    public void TurnOffAllSetButtons()
    {
        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            playerPositionsArray[i].setButton.transform.gameObject.SetActive(false);
        }
    }

    public bool CheckIfPawnInRotation(Pawn pawnToCheck)
    {
        int positionOfPawnToCheck = -1;

        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            if (playerPositionsArray[i] == pawnToCheck)
            {
                positionOfPawnToCheck = i;
                // Debug.Log("Checking rotation of pawn in position " + positionOfPawnToCheck);
            }
        }


        if (positionOfPawnToCheck < 0 || positionOfPawnToCheck > 6)
        {
            Debug.LogError("Pawn position is a number that doesn't make sense");
            return true;
        }

        Vector2 pawnToMoveGridPosition = playerGridManager.GetGridXYPosition(pawnToCheck.GetMyManager().GetCursorPosition());
        Vector2 pos0GridPosition = playerGridManager.GetGridXYPosition(playerPositionsArray[0].transform.position);
        Vector2 pos1GridPosition = playerGridManager.GetGridXYPosition(playerPositionsArray[1].transform.position);
        Vector2 pos2GridPosition = playerGridManager.GetGridXYPosition(playerPositionsArray[2].transform.position);
        Vector2 pos3GridPosition = playerGridManager.GetGridXYPosition(playerPositionsArray[3].transform.position);
        Vector2 pos4GridPosition = playerGridManager.GetGridXYPosition(playerPositionsArray[4].transform.position);
        Vector2 pos5GridPosition = playerGridManager.GetGridXYPosition(playerPositionsArray[5].transform.position);

        switch (positionOfPawnToCheck)
        {
            case 0:
                // player x position must be lower or equal to pos 3
                // player y position must be higher or equal to pos 1 AND lower or equal to pos 5
                if (pawnToMoveGridPosition.x <= pos3GridPosition.x && pawnToMoveGridPosition.y >= pos1GridPosition.y && pawnToMoveGridPosition.y <= pos5GridPosition.y)
                    return true;
                else return false;
            case 1:
                // player x position must be lower or equal than pos 2
                // player y position must be lower or equal to pos 0
                if (pawnToMoveGridPosition.x <= pos2GridPosition.x && pawnToMoveGridPosition.y <= pos0GridPosition.y)
                    return true;
                else return false;
            case 2:
                // player x position must be higher or equal to pos 1
                // player y position must be lower or equal to pos 3
                if (pawnToMoveGridPosition.x >= pos1GridPosition.x && pawnToMoveGridPosition.y <= pos3GridPosition.y)
                    return true;
                else return false;
            case 3:
                // player x pos must be higher or equal to pos 0
                // player y pos must be higher or equal to pos 2 AND lower or equal to pos 4
                if (pawnToMoveGridPosition.x >= pos0GridPosition.x && pawnToMoveGridPosition.y >= pos2GridPosition.y && pawnToMoveGridPosition.y <= pos4GridPosition.y)
                    return true;
                else return false;
            case 4:
                // player x pos must be higher or equal to pos 5
                // player y post must be higher or equal to pos 3
                if (pawnToMoveGridPosition.x >= pos5GridPosition.x && pawnToMoveGridPosition.y >= pos3GridPosition.y)
                    return true;
                else return false;
            case 5:
                // player x pos must be lower or equal to pos 4
                // player y pos must be greater or equal to pos 0
                if (pawnToMoveGridPosition.x <= pos4GridPosition.x && pawnToMoveGridPosition.y >= pos0GridPosition.y)
                    return true;
                else return false;

        }

        Debug.LogError("Didn't want to get here");
        return true;
    }

    public bool CheckIfLocationInRotation(Vector2 gridLocation, Pawn pawnToCheck)
    {
        int positionOfPawnToCheck = -1;

        for (int i = 0; i < playerPositionsArray.Length; i++)
        {
            if (playerPositionsArray[i] == pawnToCheck)
            {
                positionOfPawnToCheck = i;
                // Debug.Log("Checking rotation of pawn in position " + positionOfPawnToCheck);
            }
        }

        Vector2 pawnToMoveGridPosition = gridLocation;
        Vector2 pos0GridPosition = playerGridManager.GetGridXYPosition(playerPositionsArray[0].transform.position);
        Vector2 pos1GridPosition = playerGridManager.GetGridXYPosition(playerPositionsArray[1].transform.position);
        Vector2 pos2GridPosition = playerGridManager.GetGridXYPosition(playerPositionsArray[2].transform.position);
        Vector2 pos3GridPosition = playerGridManager.GetGridXYPosition(playerPositionsArray[3].transform.position);
        Vector2 pos4GridPosition = playerGridManager.GetGridXYPosition(playerPositionsArray[4].transform.position);
        Vector2 pos5GridPosition = playerGridManager.GetGridXYPosition(playerPositionsArray[5].transform.position);

        switch (positionOfPawnToCheck)
        {
            case 0:
                // player x position must be lower or equal to pos 3
                // player y position must be higher or equal to pos 1 AND lower or equal to pos 5
                if (pawnToMoveGridPosition.x <= pos3GridPosition.x && pawnToMoveGridPosition.y >= pos1GridPosition.y && pawnToMoveGridPosition.y <= pos5GridPosition.y)
                    return true;
                else return false;
            case 1:
                // player x position must be lower or equal than pos 2
                // player y position must be lower or equal to pos 0
                if (pawnToMoveGridPosition.x <= pos2GridPosition.x && pawnToMoveGridPosition.y <= pos0GridPosition.y)
                    return true;
                else return false;
            case 2:
                // player x position must be higher or equal to pos 1
                // player y position must be lower or equal to pos 3
                if (pawnToMoveGridPosition.x >= pos1GridPosition.x && pawnToMoveGridPosition.y <= pos3GridPosition.y)
                    return true;
                else return false;
            case 3:
                // player x pos must be higher or equal to pos 0
                // player y pos must be higher or equal to pos 2 AND lower or equal to pos 4
                if (pawnToMoveGridPosition.x >= pos0GridPosition.x && pawnToMoveGridPosition.y >= pos2GridPosition.y && pawnToMoveGridPosition.y <= pos4GridPosition.y)
                    return true;
                else return false;
            case 4:
                // player x pos must be higher or equal to pos 5
                // player y post must be higher or equal to pos 3
                if (pawnToMoveGridPosition.x >= pos5GridPosition.x && pawnToMoveGridPosition.y >= pos3GridPosition.y)
                    return true;
                else return false;
            case 5:
                // player x pos must be lower or equal to pos 4
                // player y pos must be greater or equal to pos 0
                if (pawnToMoveGridPosition.x <= pos4GridPosition.x && pawnToMoveGridPosition.y >= pos0GridPosition.y)
                    return true;
                else return false;

        }


        Debug.Log("Didn't want to get here");
        return true;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditorInternal;

public static class AIMovements
{
    public static IEnumerator MoveSingleAITowardsTarget(Pawn aiToMove, int xLimit, int yLimit, Vector3 targetPosition, GridManager aiGridManager, PawnManager aiPawnManager, RotationManager rotationManager, float time)
    {
        Vector2 targetGridPositionVector2 = aiGridManager.GetGridXYPosition(targetPosition);
        Vector2Int targetGridPosition = new Vector2Int(Mathf.RoundToInt(targetGridPositionVector2.x), Mathf.RoundToInt(targetGridPositionVector2.y));

        Vector2 pawnGridPositionVector2 = aiGridManager.GetGridXYPosition(aiToMove.transform.position);
        Vector2Int pawnGridPosition = new Vector2Int(Mathf.RoundToInt(pawnGridPositionVector2.x), Mathf.RoundToInt(pawnGridPositionVector2.y));

        Vector3 targetPositionWithLimit = aiGridManager.GetGridPosition(targetPosition, pawnGridPosition, xLimit, yLimit);
        Debug.Log("Target grid position with limit is " + aiGridManager.GetGridXYPosition(targetPositionWithLimit));

        Debug.Log("Target grid position is " + targetGridPosition);
        Debug.Log("pawn's starting grid position is " + pawnGridPosition);

        int aiToMovePosition = -1;
        for(int i = 0; i < rotationManager.aiPositionsArray.Length; i++)
        {
            if (rotationManager.aiPositionsArray[i] == aiToMove)
                aiToMovePosition = i;
        }

        Debug.Log(" Comparing " + targetGridPosition.y + " to " + pawnGridPosition.y);
        if (targetGridPosition.y != pawnGridPosition.y && !aiGridManager.GetCellOccupied(targetPositionWithLimit))
        {
            aiGridManager.SetCellOccupied(rotationManager.aiPositionsArray[aiToMovePosition].transform.position, false);
            Debug.Log("About to start moving pawn from " + aiToMove.transform.position);
            Debug.Log("and moving towards " + targetPositionWithLimit + " which is also " + aiGridManager.GetGridXYPosition(targetPositionWithLimit));
            aiGridManager.StartCoroutine(Movement.MoveFromAtoB(aiToMove.transform, aiToMove.transform.position, targetPositionWithLimit, time));
            aiGridManager.SetCellOccupied(targetPositionWithLimit, true);

            yield return new WaitForSeconds(time);

            aiGridManager.SetCellOccupied(rotationManager.aiPositionsArray[aiToMovePosition].transform.position, false);
            Debug.Log("Done moving, about to set pawns position to " + targetPositionWithLimit + " which is also " + aiGridManager.GetGridXYPosition(targetPositionWithLimit));
            rotationManager.aiPositionsArray[aiToMovePosition].transform.position = targetPositionWithLimit;
            aiGridManager.SetCellOccupied(rotationManager.aiPositionsArray[aiToMovePosition].transform.position, true);
        }

        yield break;
    }

    public static IEnumerator BlockersReactToPlayerSetChoice(Vector3 targetPosition, GridManager aiGridManager, GridManager playerGridManager, PawnManager aiPawnManager, RotationManager rotationManager, float time)
    {
        List<Pawn> blockers = new List<Pawn>();
        blockers.Add(rotationManager.aiPositionsArray[2]);
        blockers.Add(rotationManager.aiPositionsArray[3]);
        blockers.Add(rotationManager.aiPositionsArray[4]);

        Vector2 targetGridPositionVector2 = playerGridManager.GetGridXYPosition(targetPosition);
        Debug.Log("target grid position vector 2 calculated at " + targetGridPositionVector2);
        Vector2Int targetGridPosition = new Vector2Int(Mathf.RoundToInt(targetGridPositionVector2.x), Mathf.RoundToInt(targetGridPositionVector2.y));
        Debug.Log("Calculating targetGridPosition at " + targetGridPosition);
        targetGridPosition = new Vector2Int(0, targetGridPosition.y);
        Vector3 properTargetPosition = aiGridManager.ForceGetGridPosition(0, targetGridPosition.y);
        Debug.LogWarning("Proper target location set at " + properTargetPosition + " which is also " + aiGridManager.GetGridXYPosition(properTargetPosition));

        for(int i = 0; i < 3; i++)
        {
            // get the closest pawn in the list
            Pawn closestBlocker = aiPawnManager.GetClosestPawn(properTargetPosition, blockers);
            Debug.Log("closest pawn found to be " + closestBlocker.name);

            // move that pawn closer to the target location
            if(aiGridManager.GetGridXYPosition(properTargetPosition) != aiGridManager.GetGridXYPosition(closestBlocker.transform.position))
                aiGridManager.StartCoroutine(AIMovements.MoveSingleAITowardsTarget(closestBlocker, 0, 1, properTargetPosition, aiGridManager, aiPawnManager, rotationManager, time));

            // pop that blocker from the list
            int count = blockers.Count;
            bool foundTheClosestBlocker = false;
            for (int j = 0; j < count; j++)
            {
                if (j <= blockers.Count && !foundTheClosestBlocker)
                {
                    if (blockers[j] == closestBlocker)
                    {
                        Debug.Log("popping this pawn " + blockers[j].name);
                        blockers.RemoveAt(j);
                        foundTheClosestBlocker = true;
                    }
                }
            }
        }
        yield return new WaitForSeconds(time);


        yield break;
    }


}

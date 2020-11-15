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
        Vector3 targetPositionWithLimit = aiGridManager.ForceGetGridPosition(targetPosition, pawnGridPosition, xLimit, yLimit);

        int aiToMovePosition = -1;
        for(int i = 0; i < rotationManager.aiPositionsArray.Length; i++)
        {
            if (rotationManager.aiPositionsArray[i] == aiToMove)
                aiToMovePosition = i;
        }

        if (targetGridPosition.y != pawnGridPosition.y && !aiGridManager.GetCellOccupied(targetPositionWithLimit))
        {
            aiGridManager.SetCellOccupied(rotationManager.aiPositionsArray[aiToMovePosition].transform.position, false);
            aiGridManager.StartCoroutine(Movement.MoveFromAtoB(aiToMove.transform, aiToMove.transform.position, targetPositionWithLimit, time));
            aiGridManager.SetCellOccupied(targetPositionWithLimit, true);

            yield return new WaitForSeconds(time);

            aiGridManager.SetCellOccupied(rotationManager.aiPositionsArray[aiToMovePosition].transform.position, false);
            rotationManager.aiPositionsArray[aiToMovePosition].transform.position = targetPositionWithLimit;
            aiGridManager.SetCellOccupied(rotationManager.aiPositionsArray[aiToMovePosition].transform.position, true);
        }

        yield break;
    }

    public static IEnumerator BlockersReactToPlayerSetChoice(Vector3 targetPosition, GridManager aiGridManager, GridManager playerGridManager, PawnManager aiPawnManager, RotationManager rotationManager, float time)
    {
        List<Pawn> blockers = new List<Pawn>();
        blockers.Add(rotationManager.aiPositionsArray[3]);
        blockers.Add(rotationManager.aiPositionsArray[2]);
        blockers.Add(rotationManager.aiPositionsArray[4]);

        Vector2 targetGridPositionVector2 = playerGridManager.GetGridXYPosition(targetPosition);
        Vector2Int targetGridPosition = new Vector2Int(Mathf.RoundToInt(targetGridPositionVector2.x), Mathf.RoundToInt(targetGridPositionVector2.y));
        targetGridPosition = new Vector2Int(0, targetGridPosition.y);
        Vector3 properTargetPosition = aiGridManager.ForceGetGridPosition(0, targetGridPosition.y);

        for(int i = 0; i < 3; i++)
        {
            // get the closest pawn in the list
            Pawn closestBlocker = aiPawnManager.GetClosestPawn(properTargetPosition, blockers);

            // move that pawn closer to the target location
            if (aiGridManager.GetGridXYPosition(properTargetPosition) != aiGridManager.GetGridXYPosition(closestBlocker.transform.position))
            {
                // check if the blocker is too far to block, then move them off the net
                int aiXMovementLimit = 0;
                int aiYDifference = Mathf.RoundToInt(Mathf.Abs(aiGridManager.GetGridXYPosition(closestBlocker.transform.position).y - aiGridManager.GetGridXYPosition(properTargetPosition).y));
                // Debug.Log("Y distance calculated at " + aiYDifference);
                if(aiYDifference > 3)
                {
                    aiXMovementLimit = 1;
                    properTargetPosition = aiGridManager.ForceGetGridPosition(1, targetGridPosition.y);
                }
                aiGridManager.StartCoroutine(AIMovements.MoveSingleAITowardsTarget(closestBlocker, aiXMovementLimit, 1, properTargetPosition, aiGridManager, aiPawnManager, rotationManager, time));
            }

            // pop that blocker from the list
            int count = blockers.Count;
            bool foundTheClosestBlocker = false;
            for (int j = 0; j < count; j++)
            {
                if (j <= blockers.Count && !foundTheClosestBlocker)
                {
                    if (blockers[j] == closestBlocker)
                    {
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

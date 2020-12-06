// ******************************************************** ///
/// ** This script is owned and monitored by Tobias Bauer ** /// 
/// ******************************************************** ///

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
// using System.Numerics;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager = null;
    public GridManager gridManager { get { return _gridManager; } }

    [SerializeField] private Pawn[] pawns = null;

    [SerializeField] private bool _snapToGrid = true;
    public bool snapToGrid { get { return _snapToGrid; } }

    [SerializeField] private float _pickupDist = 0.4f;
    public float pickupDist { get { return _pickupDist; } }

    [SerializeField] private PositionSets[] _allPositionSets = null;
    public PositionSets[] allPositionSets { get { return _allPositionSets; } }

    [SerializeField] private LayerMask blockingLayerMask = 0;

    public RotationManager rotationManager;

    private bool _serveRecieve = false;
    public bool serveRecieve
    {
        get { return _serveRecieve; }
        set { _serveRecieve = value; }
    }

    /// <summary>
    /// Gets the cursor's position in world space
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCursorPosition()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0.0f;
        return cursorPos;
    }

    void Start()
    {
        foreach(Pawn pawn in pawns)
        {
            pawn.Init(this);
            pawn.enabled = pickupDist != -1;
        }    
    }

    /// <summary>
    /// Places one pawn at each location
    /// </summary>
    /// <param name="positions"></param>
    public void SetPositions(Vector2[] positions)
    {
        for(int i = 0; i < pawns.Length; i++)
        {
            gridManager.SetCellOccupied(pawns[i].transform.position, false);
            pawns[i].transform.position = gridManager.GetGridPosition((int)positions[i].x, (int)positions[i].y);
            gridManager.SetCellOccupied(pawns[i].transform.position, true);
        }
    }

    /// <summary>
    /// Enables/Disables all the passed in pawns. If none are passed in, all will be set to the desired state.
    /// </summary>
    /// <param name="enabled"></param>
    /// <param name="pawns"></param>
    public void EnablePawnMove(bool enabled, Pawn[] pawns = null)
    {
        if (pawns == null || pawns.Length == 0)
            pawns = this.pawns;
        foreach (Pawn p in pawns)
            p.enabled = enabled;
    }

    public void EnablePawnMoveMinusSetter(bool enabled, Pawn[] pawns = null)
    {
        if (pawns == null || pawns.Length == 0)
            pawns = this.pawns;
        foreach (Pawn p in pawns)
        {
            if (p.pawnRole != PawnRole.Setter)
                p.enabled = enabled;
            else p.enabled = false;
        }
    }

    public void EnablePawnMoveMinusSetter(bool enabled, Pawn diggingPawn, Pawn[] pawns = null)
    {
        if (pawns == null || pawns.Length == 0)
            pawns = this.pawns;
        foreach (Pawn p in pawns)
        {
            if(diggingPawn.pawnRole == PawnRole.Setter)
            {
                if (p.pawnRole != PawnRole.RightSide)
                    p.enabled = enabled;
                else p.enabled = false;
            }
            else
            {
                if (p.pawnRole != PawnRole.Setter)
                    p.enabled = enabled;
                else p.enabled = false;
            }
        }
    }

    public void EnableLimitedMove(int limitX, int limitY, Pawn[] pawns = null)
    {
        if (pawns == null || pawns.Length == 0)
            pawns = this.pawns;
        foreach (Pawn pawn in pawns)
        {
            pawn.SetMoveLimits(limitX, limitY);
        }
    }

    public void DisableLimitedMove(Pawn[] pawns = null)
    {
        if (pawns == null || pawns.Length == 0)
            pawns = this.pawns;
        foreach (Pawn pawn in pawns)
        {
            pawn.ResetMoveLimits();
        }
    }

    /// <summary>
    /// Returns the X and Y grid position in Vector2 format
    /// </summary>
    /// <param name="pawnRole"></param>
    /// <returns></returns>
    public Vector2 GetPawnGridPositon(PawnRole pawnRole)
    {
        foreach(Pawn pawn in pawns)
        {
            if(pawn.pawnRole == pawnRole)
            {
                return gridManager.GetGridXYPosition(pawn.transform.position);
            }
        }

        // Should never reach here
        return Vector2.zero;
    }

    /// <summary>
    /// Returns the X and Y grid position in Vector2 format
    /// </summary>
    /// <param name="pawnRole"></param>
    /// <returns></returns>
    public Vector2 GetPawnGridPositon(Pawn pawn)
    {
        return gridManager.GetGridXYPosition(pawn.transform.position);
    }

    /// <summary>
    /// Returns the closest pawn to the given grid location
    /// </summary>
    public Pawn GetClosestPawn(Vector2 ballWorldPosition, bool isDigging, int blockingRow)
    {
        Pawn closestPawn = null;
        foreach(Pawn p in pawns)
        {
            if(closestPawn == null)
            {
                if ((isDigging || p.pawnRole != PawnRole.Setter) && GetPawnGridPositon(p).x != blockingRow)
                {
                    closestPawn = p;
                    continue;
                }
            }
            else if (closestPawn != null)
            {
                if (Vector2.Distance(p.transform.position, ballWorldPosition) < Vector2.Distance(closestPawn.transform.position, ballWorldPosition))
                {
                    if ((isDigging || p.pawnRole != PawnRole.Setter) && GetPawnGridPositon(p).x != blockingRow)
                        closestPawn = p;
                }
            }
        }
        if (closestPawn == null)
            closestPawn = pawns[5];
        // Debug.Log("Closest pawn calculated at " + closestPawn.name);
        return closestPawn;
    }

    public Pawn GetClosestPawn(Vector2 ballWorldPosition, List<Pawn> pawnList)
    {
        Pawn closestPawn = null;
        for(int i = 0; i < pawnList.Count; i++)
        {
            if (closestPawn == null)
            {
                closestPawn = pawnList[i];
            }
            else /*if (closestPawn != null)*/
            {
                if (Vector2.Distance(pawnList[i].transform.position, ballWorldPosition) < Vector2.Distance(closestPawn.transform.position, ballWorldPosition))
                {
                    closestPawn = pawnList[i];
                }
            }
        }
        if (closestPawn == null)
        {
            closestPawn = pawns[4];
            Debug.LogError("Something went wrong, didn't find the closest pawn");
        }
        return closestPawn;
    }

    public Pawn GetClosestPawn(Vector2 ballWorldPosition)
    {
        Pawn closestPawn = null;
        foreach (Pawn p in pawns)
        {
            if (closestPawn == null)
            {
                closestPawn = p;
                continue;
            }
            else if (closestPawn != null)
            {
                if (Vector2.Distance(p.transform.position, ballWorldPosition) < Vector2.Distance(closestPawn.transform.position, ballWorldPosition))
                {
                    closestPawn = p;
                }
            }
        }
        if (closestPawn == null)
            closestPawn = pawns[5];
        // Debug.Log("Closest pawn calculated at " + closestPawn.name);
        return closestPawn;
    }

    public int GetNumberOfBlockersHandsNearby(int attackersRow, bool isPlayersBlockers)
    {
        int numberOfBlockersHands = 0;
        int blockersColumn = 0;
        if (isPlayersBlockers)
            blockersColumn = 8;
        else blockersColumn = 0;

        foreach(Pawn p in pawns)
        {
            if (GetPawnGridPositon(p).x == blockersColumn && Mathf.Abs(GetPawnGridPositon(p).y - attackersRow) <= 1)
                numberOfBlockersHands += 2;
            if (GetPawnGridPositon(p).x == blockersColumn && Mathf.Abs(GetPawnGridPositon(p).y - attackersRow) == 2)
                numberOfBlockersHands += 1;
        }
        // Debug.LogWarning(" There are " + numberOfBlockersHands + " blockers hands nearby");


        return numberOfBlockersHands;
    }

    public int GetNumberOfBlockersHandsNearby(Vector3 attackerPosition, Vector3 attackDirection, bool isPlayersBlockers)
    {
        int numberOfBlockersHands = 0;
        int blockersColumn = 0;
        if (isPlayersBlockers)
            blockersColumn = 8;
        else blockersColumn = 0;

        LayerMask blockerLayer = 8;
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(attackerPosition, new Vector2(attackDirection.x - attackerPosition.x, attackDirection.y - attackerPosition.y), Vector2.Distance(attackerPosition, attackDirection), blockingLayerMask);
        // Debug.LogWarning("Raycast hit this many things: " + hits.Length);
        // Debug.DrawRay(attackerPosition, new Vector2(attackDirection.x - attackerPosition.x, attackDirection.y - attackerPosition.y), Color.red, Vector2.Distance(attackerPosition, attackDirection));
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.parent.gameObject.GetComponent<Pawn>())
            {
                foreach (Pawn p in pawns) // check all the pawns on this pawn manager
                {
                    if (hit.transform.parent.gameObject.GetComponent<Pawn>() == p) // if the hit pawn is part of this grid
                    {
                        // then check if its in the right column
                        if (GetPawnGridPositon(hit.transform.parent.gameObject.GetComponent<Pawn>()).x == blockersColumn)
                        {
                            if (isPlayersBlockers)
                            {
                                if (rotationManager.IsPawnRotationFrontRow(hit.transform.parent.gameObject.GetComponent<Pawn>()))
                                {
                                    numberOfBlockersHands += 1;
                                    // Debug.LogWarning("Counting " + hit.transform.name + " part of the " + hit.transform.parent.name + " object as a raycast hit. It is in column " + GetPawnGridPositon(hit.transform.parent.gameObject.GetComponent<Pawn>()).x);
                                }
                            }
                            else numberOfBlockersHands += 1;
                            
                        }
                    }
                }  
            }
        }

        //Debug.Log(" There are " + numberOfBlockersHands + " blockers hands nearby");

        return numberOfBlockersHands;
    }

    public Vector2Int AIPickAttackDirection(Vector3 attackerPosition)
    {
        Vector2Int attackDirection = Vector2Int.zero;

        Vector2Int position1 = new Vector2Int(2, 1);
        Vector2Int position5 = new Vector2Int(2, 7);
        Vector2Int position2 = new Vector2Int(6, 1);
        Vector2Int position4 = new Vector2Int(6, 7);
        Vector2Int shortMiddle = new Vector2Int(5, 4);
        Vector2Int deepMiddle = new Vector2Int(1, 4);
        Vector2Int shortTip = new Vector2Int(7, 4);
        Vector2Int sideline1 = new Vector2Int(4, 0);
        Vector2Int sideline5 = new Vector2Int(4, 8);
        Vector2Int veryMiddle = new Vector2Int(4, 4);
        Vector2Int[] attackLocations = new Vector2Int[10];
        Vector3[] attackLocationsWorldPosition = new Vector3[10];

        attackLocations[2] = position1;
        attackLocations[1] = position5;
        attackLocations[4] = position2;
        attackLocations[3] = position4;
        attackLocations[6] = shortMiddle;
        attackLocations[5] = deepMiddle;
        attackLocations[9] = shortTip;
        attackLocations[7] = sideline1;
        attackLocations[8] = sideline5;
        attackLocations[0] = veryMiddle;

        for (int i = 0; i < attackLocations.Length; i++)
        {
            attackLocationsWorldPosition[i] = rotationManager.playerGridManager.ForceGetGridPosition(attackLocations[i].x, attackLocations[i].y);
            // Debug.Log(i + " world position being set at " + attackLocationsWorldPosition[i]);
        }

        // check how many blockers are present at each shot location
        int[] attackLocationsOpponentBlockersInTheWay = new int[10];
        for(int i = 0; i < attackLocations.Length; i++)
        {
            attackLocationsOpponentBlockersInTheWay[i] = GetNumberOfBlockersHandsNearby(attackerPosition, attackLocationsWorldPosition[i], true);
            // Debug.Log(i + " has " + attackLocationsOpponentBlockersInTheWay[i] + " blockers in the way");
        }

        // go through them all, track the lowest number's index
        int bestShotOptionIndex = -1;
        for(int i = 0; i < attackLocationsOpponentBlockersInTheWay.Length; i++)
        {
            if(i == 0)
            {
                bestShotOptionIndex = i;
            }
            else
            {
                if(attackLocationsOpponentBlockersInTheWay[i] < attackLocationsOpponentBlockersInTheWay[bestShotOptionIndex])
                {
                    bestShotOptionIndex = i;
                }
            }
        }
        // Debug.Log("Best shop option index selected at " + bestShotOptionIndex);

        // when a location is selected, generate a random location that is within 1 square of that spot
        int aix = Mathf.CeilToInt((UnityEngine.Random.Range(attackLocations[bestShotOptionIndex].x - 1, attackLocations[bestShotOptionIndex].x + 1)));
        int aiy = Mathf.CeilToInt((UnityEngine.Random.Range(attackLocations[bestShotOptionIndex].y - 1, attackLocations[bestShotOptionIndex].y + 1)));

        attackDirection.x = aix;
        attackDirection.y = aiy;

        Debug.Log("Attack location decided to be " + attackDirection);

        return attackDirection;
    }

    public void MoveSetter(int x, int y)
    {
        foreach (Pawn p in pawns)
        {
            if(p.pawnRole == PawnRole.Setter)
            {
                gridManager.SetCellOccupied(p.transform.position, false);
                p.transform.position = gridManager.GetGridPosition(x, y);
                gridManager.SetCellOccupied(p.transform.position, true);
            }
        }
    }
    public void SetAllPawnSprites(Pawn.Sprites sprite)
    {
        foreach(Pawn p in pawns)
        {
            p.SetSprite(sprite);
        }
    }

    public void SetAllPawnAnimatorInteger(int animationNumber)
    {
        foreach (Pawn p in pawns)
        {
            SetAnimation(p, animationNumber);
        }
    }

    public void SetAnimation(Pawn pawn, int animationNumber)
    {
        pawn.transform.GetComponentInChildren<Animator>().SetInteger("animationNumber", animationNumber);
    }

    public void SetAnimation(Pawn pawn, int animationNumber, Vector3 targetLocationPosition)
    {
        int returnNumber = animationNumber;

        if (animationNumber == 1)
        {
            Vector2 startingGridPositionVector2 = gridManager.GetGridXYPosition(pawn.transform.position);
            Vector2Int startingGridPositionVector2Int = new Vector2Int(Mathf.RoundToInt(startingGridPositionVector2.x), Mathf.RoundToInt(startingGridPositionVector2.y));
            Vector2 targetGridPositionVector2 = gridManager.GetGridXYPosition(targetLocationPosition);
            Vector2Int targetGridPositionVector2Int = new Vector2Int(Mathf.RoundToInt(targetGridPositionVector2.x), Mathf.RoundToInt(targetGridPositionVector2.y));
            returnNumber = GetRunDirectionInteger(startingGridPositionVector2Int, targetGridPositionVector2Int);
        }


        pawn.transform.GetComponentInChildren<Animator>().SetInteger("animationNumber", returnNumber);
    }


    public void SetBlockersAndDefendersSprites(int blockersColumn, int animationNumber = 5)
    {
        foreach(Pawn p in pawns)
        {
            if (GetPawnGridPositon(p).x == blockersColumn && rotationManager.IsPawnRotationFrontRow(p))
                SetAnimation(p, animationNumber);
            //p.SetSprite(Pawn.Sprites.block);
            else
            {
                SetAnimation(p, 0);
            }
        }
    }

    private int GetRunDirectionInteger(Vector2Int startingGridPosition, Vector2Int targetLocationGridPosition)
    {
        int startingX = startingGridPosition.x;
        int startingY = startingGridPosition.y;

        int targetX = targetLocationGridPosition.x;
        int targetY = targetLocationGridPosition.y;

        int xDifference = startingX - targetX;
        int yDifference = startingY - targetY;

        if(xDifference > 0)  // moving to the left
        {
            if(yDifference > 0) // moving down
            {
                return -6;
            }
            else if(yDifference < 0)  // moving up
            {
                return -3;
            }
            else if(yDifference == 0) // no up or down movement
            {
                return -5;
            }
        }
        else if (xDifference < 0)  // moving to the right
        {
            if (yDifference > 0)   // moving down
            {
                return -7;
            }
            else if (yDifference < 0)   // moving up
            {
                return -4;
            }
            else if (yDifference == 0)
            {
                return 1;
            }
        }
        else if (xDifference == 0)   // no left or right movement
        {
            if (yDifference > 0)  // moving down
            {
                return -8;
            }
            else if (yDifference < 0)   // moving up
            {
                return -2;
            }
            else if (yDifference == 0)   // no up or down movement
            {
                return 0;
            }
        }

        return 1;
    }

}







/// <summary>
/// A class for holding several Vector2 positions
/// </summary>
[Serializable]
public class PositionSets
{
    public Vector2[] positions;
}

public enum PawnRole
{
    Power1, 
    Power2, 
    Middle1,
    Middle2,
    RightSide,
    Setter
}

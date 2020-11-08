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
        Debug.LogWarning(" There are " + numberOfBlockersHands + " blockers hands nearby");


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
                            numberOfBlockersHands += 1;
                            // Debug.LogWarning("Counting " + hit.transform.name + " part of the " + hit.transform.parent.name + " object as a raycast hit. It is in column " + GetPawnGridPositon(hit.transform.parent.gameObject.GetComponent<Pawn>()).x);
                        }
                    }
                }  
            }
        }

        //Debug.Log(" There are " + numberOfBlockersHands + " blockers hands nearby");

        return numberOfBlockersHands;
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

    public void SetBlockersAndDefendersSprites(int blockersColumn)
    {
        foreach(Pawn p in pawns)
        {
            if (GetPawnGridPositon(p).x == blockersColumn)
                p.SetSprite(Pawn.Sprites.block);
            else p.SetSprite(Pawn.Sprites.neutral);
        }
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

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
// using System.Numerics;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    public GridManager gridManager { get { return _gridManager; } }

    [SerializeField] private Pawn[] pawns;

    [SerializeField] private bool _snapToGrid;
    public bool snapToGrid { get { return _snapToGrid; } }

    [SerializeField] private float _pickupDist = 0.4f;
    public float pickupDist { get { return _pickupDist; } }

    [SerializeField] private PositionSets[] _allPositionSets;
    public PositionSets[] allPositionSets { get { return _allPositionSets; } }

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
            pawns[i].transform.position = gridManager.GetGridPosition((int)positions[i].x, (int)positions[i].y);
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
        Debug.Log("Closest pawn calculated at " + closestPawn.name);
        return closestPawn;
    }

    public void MoveSetter(int x, int y)
    {
        foreach (Pawn p in pawns)
        {
            if(p.pawnRole == PawnRole.Setter)
            {
                p.transform.position = gridManager.GetGridPosition(x, y);
            }
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

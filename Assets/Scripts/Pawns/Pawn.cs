﻿/// ******************************************************** ///
/// ** This script is owned and monitored by Tobias Bauer ** /// 
/// ******************************************************** ///


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : MonoBehaviour
{
    private PawnManager pawnManager;
    private bool selected = false;
    [Header("Role")]
    [SerializeField] private PawnRole _pawnRole;
    public PawnRole pawnRole { get { return _pawnRole; } }

    [Header("Starting Pos")]
    [SerializeField] private int x;
    [SerializeField] private int y;

    //public bool limitedMovement = false;
    //private float limitedMoveDistance = 1.5f;

    Vector2 pickupOrigin;
    int limitX = -1;
    int limitY = -1;

    public Button setButton;

    public enum Sprites
    {
        neutral,
        spike,
        dig,
        block
    }
    public Sprite[] sprites;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SetSprite(Sprites sprite)
    {
        spriteRenderer.sprite = sprites[(int)sprite];
    }

    [Space]
    // Stats
    [Header("Player Stats")]
    [Range(-75, 125)] [SerializeField] private float serveMod; 
    [Range(-75, 125)] [SerializeField] private float passMod; 
    [Range(-75, 125)] [SerializeField] private float setMod; 
    [Range(-75, 125)] [SerializeField] private float attackMod; 
    [Range(-75, 125)] [SerializeField] private float blockMod; 
    [Range(-75, 125)] [SerializeField] private float defenceMod; 

    public void Init(PawnManager pawnManager)
    {
        this.pawnManager = pawnManager;
        transform.position = pawnManager.gridManager.GetGridPosition(x, y);
        pawnManager.gridManager.SetCellOccupied(transform.position, true);
    }

    void Update()
    {
        // If left mosue is clicked on pawn, set it to selected and set cell to unoccupied
        if (Input.GetMouseButtonDown(0) && Vector3.Distance(transform.position, pawnManager.GetCursorPosition()) < pawnManager.pickupDist)
        {
            pickupOrigin = pawnManager.gridManager.GetGridXYPosition(transform.position);
            selected = true;
            pawnManager.gridManager.SetCellOccupied(transform.position, false);
        }
        // if selected, have it follow mouse
        else if (selected)
        {
            transform.position = pawnManager.snapToGrid ? pawnManager.gridManager.GetGridPosition(pawnManager.GetCursorPosition(), pickupOrigin, GetXLimitForGrid(), GetYLimitForGrid()) : pawnManager.GetCursorPosition();
         
            if (Input.GetMouseButtonUp(0))
            {
                selected = false;
                transform.position = pawnManager.gridManager.GetGridPosition(pawnManager.GetCursorPosition(), pickupOrigin, GetXLimitForGrid(), GetYLimitForGrid());
                pawnManager.gridManager.SetCellOccupied(transform.position, true);
            }
        }
    }

    /// <summary>
    /// Sets the limit to how many tiles pawn can move on x and y axis
    /// </summary>
    /// <param name="limitX"></param>
    /// <param name="limitY"></param>
    public void SetMoveLimits(int limitX = -1, int limitY = -1)
    {
        this.limitX = limitX;
        this.limitY = limitY;
    }

    /// <summary>
    /// Resets pawn movement limits to default (No limit) 
    /// </summary>
    public void ResetMoveLimits()
    {
        SetMoveLimits();
    }

    int GetXLimitForGrid()
    {
        return limitX < 0 ? pawnManager.gridManager.GetGridWidth() : limitX;
    }

    int GetYLimitForGrid()
    {
        return limitX < 0 ? pawnManager.gridManager.GetGridHeight() : limitY;
    }
}

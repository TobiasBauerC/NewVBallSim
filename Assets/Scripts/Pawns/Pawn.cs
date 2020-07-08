using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    private PawnManager pawnManager;
    private bool selected = false;

    [Header("Starting Pos")]
    [SerializeField] private int x;
    [SerializeField] private int y;

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
            selected = true;
            pawnManager.gridManager.SetCellOccupied(transform.position, false);
        }
        // if selected, have it follow mouse
        else if (selected)
        {
            transform.position = pawnManager.snapToGrid ? pawnManager.gridManager.GetGridPosition(pawnManager.GetCursorPosition()) : pawnManager.GetCursorPosition();
            // If left mouse is released, stop following mouse, got to cell position, set cell to occupied
            if (Input.GetMouseButtonUp(0))
            {
                selected = false;
                pawnManager.gridManager.PrintGridOccupied(transform.position);
                transform.position = pawnManager.gridManager.GetGridPosition(pawnManager.GetCursorPosition());
                pawnManager.gridManager.SetCellOccupied(transform.position, true);
                //worldGrid.grid.PrintGridOccupied(transform.position);
            }
        }
    }
}

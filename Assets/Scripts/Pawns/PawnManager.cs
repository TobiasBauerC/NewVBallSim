using System;
using System.Collections;
using System.Collections.Generic;
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

    public void SetPositions(List<Vector2> positions)
    {
        for(int i = 0; i < pawns.Length; i++)
        {
            pawns[i].transform.position = gridManager.GetGridPosition((int)positions[i].x, (int)positions[i].y);
        }
    }
}

[Serializable]
public class PositionSets
{
    public Vector2[] positions;
}

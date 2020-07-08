using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Grid grid;

    [SerializeField] private int width = 5;
    [SerializeField] private int height = 5;
    [SerializeField] private float cellSize = 1f;

    private void Awake()
    {
        grid = new Grid(width, height, cellSize, transform.position);
    }

    // Returns world position for center of Cell
    public Vector2 GetGridXYPosition(Vector3 worldPosition)
    {
        return grid.GetGridXYPosition(worldPosition);
    }

    // Returns world position for center of Cell
    public Vector3 GetGridPosition(Vector3 worldPosition)
    {
        return grid.GetGridPosition(worldPosition);
    }

    public Vector3 GetGridPosition(int x, int y)
    {
        return grid.GetGridPosition(x, y);
    }

    // Returns world position for center of Cell
    public Vector3 ForceGetGridPosition(Vector3 worldPosition)
    {
        return grid.ForceGetGridPosition(worldPosition);
    }

    public Vector3 ForceGetGridPosition(int x, int y)
    {
        return grid.ForceGetGridPosition(x, y);
    }

    // Changes occupied status of 2D array element 
    public void SetCellOccupied(Vector3 worldPosition, bool occupied)
    {
        grid.SetCellOccupied(worldPosition, occupied);
    }

    public float GetDefenceScore(int attackerRow, int defenderColumn)
    {
        float result = 0;

        for(int i = attackerRow -2; i <= attackerRow + 2; i++)
        {
            if(grid.GetGridOccupied(i, defenderColumn))
            {
                result += Mathf.Abs(i - attackerRow) > 1 ? 1 : 2;
            }
        }

        return result;
    }
}

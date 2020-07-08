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
    public Vector3 GetGridPosition(Vector3 worldPosition)
    {
        return grid.GetGridPosition(worldPosition);
    }

    public Vector3 GetGridPosition(int x, int y)
    {
        return grid.GetGridPosition(x, y);
    }

    // Changes occupied status of 2D array element 
    public void SetCellOccupied(Vector3 worldPosition, bool occupied)
    {
        grid.SetCellOccupied(worldPosition, occupied);
    }

    public void PrintGridOccupied(Vector3 worldPosition)
    {
        grid.PrintGridOccupied(worldPosition);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width, height;
    private bool[,] gridArray; // 2D array or bools as each cell needs to know if its occupied or not
    private float cellSize;
    private Vector3 origin;

    // Constructor
    public Grid(int width, int height, float cellSize, Vector3 origin)
    {
        // Assign values
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;

        // Init grid array
        gridArray = new bool[width, height];
    }

    // Returns a Vector3 representing a world space position
    Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + origin;
    }

    // Returns a Vector3 representing world space position of the center of the cell
    Vector3 GetCellCenter(int x, int y)
    {
        Vector3 result = new Vector3(x, y) * cellSize + origin;
        result.x += cellSize * 0.5f;
        result.y += cellSize * 0.5f;
        return result;
    }

    // Converts world position into [x, y] position in 2D array
    void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        // Modify world pos by origin point and clamp return values to the grid
        worldPosition -= origin;
        x = Mathf.Clamp(Mathf.FloorToInt(worldPosition.x / cellSize), 0, width - 1);
        y = Mathf.Clamp(Mathf.FloorToInt(worldPosition.y / cellSize), 0, height - 1);
    }

    void GetAvailableXY(Vector3 worldPosition, out int x, out int y)
    {
        // Modify world pos by origin point
        worldPosition -= origin;
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);

        GetAvailableXY(x, y, out x, out y);
    }

    void GetAvailableXY(int xPos, int yPos, out int x, out int y)
    {
        // Modify world pos by origin point and clamp return values to the grid
        x = Mathf.Clamp(xPos, 0, width - 1);
        y = Mathf.Clamp(yPos, 0, height - 1);

        // Find closest available cell
        if (gridArray[x, y])
        {
            // Array where every two elements are an x, y location. So neighbors[0] = x, and neighbors[1] = y
            int[] neighbors = GetNeighbors(x, y);

            // Check to see if [i, i+1] is within grid, then check to see if its available
            for (int i = 0; i < neighbors.Length; i += 2)
            {
                if (neighbors[i] < 0 || neighbors[i] >= width || neighbors[i + 1] < 0 || neighbors[i + 1] >= height)
                    continue;
                if (!gridArray[neighbors[i], neighbors[i + 1]])
                {
                    x = neighbors[i];
                    y = neighbors[i + 1];
                    break;
                }
            }
        }
    }

    // This function has been written with 6 pawns in mind. Any more and this will need to be expanded
    int[] GetNeighbors(int x, int y)
    {
        // Imagine x, y == 2, 2
        return new int[] { x, y + 1, // 2, 3
            x, y - 1, // 2, 1
            x + 1, y, // 3, 2
            x - 1, y, // 1, 2
            x - 1, y + 1, // 1, 3 
            x + 1, y + 1, // 3, 3
            x - 1, y - 1, // 1, 1
            x + 1, y - 1, // 3, 1
            x, y + 2, // 2, 4
            x, y - 2, // 2, 0
            x + 2, y, // 4, 2
            x - 2, y // 0, 2
        };
    }

    // Returns world position for center of Cell
    public Vector3 GetGridPosition(Vector3 worldPosition)
    {
        int x, y;
        GetAvailableXY(worldPosition, out x, out y);
        return GetCellCenter(x, y);
    }

    // Returns world position for center of Cell
    public Vector3 GetGridPosition(int x, int y)
    {
        GetAvailableXY(x, y, out x, out y);
        return GetCellCenter(x, y);
    }

    // Returns world position for center of Cell
    public Vector3 ForceGetGridPosition(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetCellCenter(x, y);
    }

    // Returns world position for center of Cell
    public Vector3 ForceGetGridPosition(int x, int y)
    {
        x = Mathf.Clamp(Mathf.FloorToInt(x / cellSize), 0, width - 1);
        y = Mathf.Clamp(Mathf.FloorToInt(y / cellSize), 0, height - 1);
        return GetCellCenter(x, y);
    }

    // Changes occupied status of 2D array element 
    public void SetCellOccupied(Vector3 worldPosition, bool occupied)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        gridArray[x, y] = occupied;
    }

    // Debug to get info on cell at world position
    public void PrintGridOccupied(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        Debug.LogFormat("[{0}, {1}] is {2}", x, y, gridArray[x, y]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GridManager currentGrid;

    public void SetCurrentGrid(GridManager gridManager)
    {
        currentGrid = gridManager;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = currentGrid.GetGridPosition(position);
    }

    public void SetPosition(int x, int y)
    {
        transform.position = currentGrid.GetGridPosition(x, y);
    }

    public void SetPosition(GridManager gridManager, Vector3 position)
    {
        currentGrid = gridManager;
        SetPosition(position);
    }

    public void SetPosition(GridManager gridManager, int x, int y)
    {
        currentGrid = gridManager;
        SetPosition(x, y);
    }
}

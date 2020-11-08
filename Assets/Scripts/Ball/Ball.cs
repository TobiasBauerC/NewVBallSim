// ******************************************************** ///
/// ** This script is owned and monitored by Tobias Bauer ** /// 
/// ******************************************************** ///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Current Grid the ball responds to
    private GridManager currentGrid;


    /// <summary>
    /// Sets the Ball's active grid
    /// </summary>
    /// <param name="gridManager"></param>
    public void SetCurrentGrid(GridManager gridManager)
    {
        currentGrid = gridManager;
    }

    /// <summary>
    /// Sets Ball's current position
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Vector3 position)
    {
        transform.position = currentGrid.ForceGetGridPosition(position);
    }

    /// <summary>
    /// Sets Ball's current position
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetPosition(int x, int y)
    {
        transform.position = currentGrid.ForceGetGridPosition(x, y);
        Debug.Log("Setting " + gameObject.name +" position at " + x + " " + y);
    }

    /// <summary>
    /// Sets Ball's active grid, then sets ball's current position
    /// </summary>
    /// <param name="gridManager"></param>
    /// <param name="position"></param>
    public void SetPosition(GridManager gridManager, Vector3 position)
    {
        currentGrid = gridManager;
        SetPosition(position);
        Debug.Log("Setting " + gameObject.name + " position at " + gridManager.GetGridXYPosition(position));
    }

    /// <summary>
    /// Sets Ball's active grid, then sets ball's current position
    /// </summary>
    /// <param name="gridManager"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetPosition(GridManager gridManager, int x, int y)
    {
        currentGrid = gridManager;
        SetPosition(x, y);
    }

    public IEnumerator SetPosition(GridManager gridManager, int x, int y, float time, AudioClip[] startSounds)
    {
        Debug.Log("starting to move ball towards " + x + " " + y);
        // currentGrid = gridManager;
        StartCoroutine(Movement.MoveFromAtoB(transform, transform.position, gridManager.ForceGetGridPosition(x, y), time));
        if(startSounds != null)
            SoundManager.Instance.PlaySFX(startSounds);
        yield return new WaitForSeconds(time + .001f);
        SetPosition(gridManager, x, y);
        Debug.Log("done moving ball");
        // yield break;
    }

    public IEnumerator SetPosition(GridManager gridManager, int x, int y, float time, AudioClip[] startSounds, AudioClip[] endSounds)
    {
        Debug.Log("starting to move ball towards " + x + " " + y);
        // currentGrid = gridManager;
        StartCoroutine(Movement.MoveFromAtoB(transform, transform.position, gridManager.ForceGetGridPosition(x, y), time));
        if (startSounds != null)
            SoundManager.Instance.PlaySFX(startSounds);
        yield return new WaitForSeconds(time + .001f);
        if (endSounds != null)
            SoundManager.Instance.PlaySFX(endSounds);
        SetPosition(gridManager, x, y);
        Debug.Log("done moving ball");
        // yield break;
    }

    public Vector2Int GetGridPosition()
    {
        Vector2 position = currentGrid.GetGridXYPosition(transform.position);
        return Vector2Int.RoundToInt(position);
    }
}

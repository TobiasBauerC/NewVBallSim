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
        // Debug.Log("Setting " + gameObject.name +" position at " + x + " " + y);
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
        // Debug.Log("Setting " + gameObject.name + " position at " + gridManager.GetGridXYPosition(position));
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

    public IEnumerator SetPosition(GridManager gridManager, int x, int y, float time, AudioClip[] startSounds, float rotationValue, bool rotateRight)
    {
        // Debug.Log("starting to move ball towards " + x + " " + y);
        // currentGrid = gridManager;
        Debug.Log("Calling ball position function");
        if (rotationValue != 0)
        {
            Debug.Log("set position funtion if rotationValue != 0");
            StartCoroutine(Movement.Rotate(transform, time, rotationValue, rotateRight));
        }
        StartCoroutine(Movement.MoveFromAtoB(transform, transform.position, gridManager.ForceGetGridPosition(x, y), time));
        if(startSounds != null)
            SoundManager.Instance.PlaySFX(startSounds);
        yield return new WaitForSeconds(time + .001f);
        SetPosition(gridManager, x, y);
        // Debug.Log("done moving ball");
        // yield break;
    }

    public IEnumerator SetPosition(GridManager gridManager, int x, int y, float time, AudioClip[] startSounds, AudioClip[] endSounds, float rotationValue, bool rotateRight)
    {
        // Debug.Log("starting to move ball towards " + x + " " + y);
        // currentGrid = gridManager;
        if (rotationValue != 0)
            StartCoroutine(Movement.Rotate(transform, time, rotationValue, rotateRight));
        StartCoroutine(Movement.MoveFromAtoB(transform, transform.position, gridManager.ForceGetGridPosition(x, y), time));
        if (startSounds != null)
            SoundManager.Instance.PlaySFX(startSounds);
        yield return new WaitForSeconds(time + .001f);
        if (endSounds != null)
            SoundManager.Instance.PlaySFX(endSounds);
        SetPosition(gridManager, x, y);
        // Debug.Log("done moving ball");
        // yield break;
    }

    public IEnumerator SetPositionOutOfBounds(GridManager gridManager, int x, int y, float time, AudioClip[] startSounds, AudioClip[] endSounds, bool ballOnOpposite, float rotationValue, bool rotateRight)
    {
        Vector3 targetPosition = GetPositionOutOfBounds(gridManager.ForceGetGridPosition(x, y), ballOnOpposite);
        if (rotationValue != 0)
            StartCoroutine(Movement.Rotate(transform, time, rotationValue, rotateRight));
        StartCoroutine(Movement.MoveFromAtoB(transform, transform.position, targetPosition, time));
        if (startSounds != null)
            SoundManager.Instance.PlaySFX(startSounds);
        yield return new WaitForSeconds(time + .001f);
        if (endSounds != null)
            SoundManager.Instance.PlaySFX(endSounds);
    }

    public void StartRotationCoroutine(Transform objectToMove, float time, float rotationValue, bool rotateRight)
    {
        StartCoroutine(Movement.Rotate(objectToMove, time, rotationValue, rotateRight));
    }

    private Vector3 GetPositionOutOfBounds(Vector3 inBoundsPosition, bool ballOnOpposite)
    {
        Vector3 returnVector = inBoundsPosition;

        if (ballOnOpposite)
        {
            if (inBoundsPosition.x > 0)
            {
                returnVector.x += 1;
            }
            else returnVector.x -= 1;
            if (inBoundsPosition.y > 0)
            {
                returnVector.y += 1;
            }
            else returnVector.y -= 1;
        }
        else
        {
            if (inBoundsPosition.x > 0)
            {
                returnVector.x -= 2;
            }
            else returnVector.x += 1;
            if (inBoundsPosition.y > 0)
            {
                returnVector.y += 1;
            }
            else returnVector.y -= 1;
        }

        return returnVector;
    }

    public Vector2Int GetGridPosition()
    {
        Vector2 position = currentGrid.GetGridXYPosition(transform.position);
        return Vector2Int.RoundToInt(position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Movement
{

    public static IEnumerator MoveFromAtoB(Transform objectToMove, Vector3 worldStartPosition, Vector3 worldEndPosition, float time)
    {
        // Debug.Log("Trying to move " + objectToMove.name + " from " + worldStartPosition + " to " + worldEndPosition + " in " + time + " seconds.");
        float timeSpent = 0;
        float t = 0;
        while(objectToMove.position != worldEndPosition)
        {
            t += Time.deltaTime / time;
            objectToMove.position = Vector3.Lerp(worldStartPosition, worldEndPosition, t);
            timeSpent += Time.deltaTime;
            if (timeSpent > time)
                yield break;
            yield return null;
        }
        //Pawn pawnToMove = objectToMove.GetComponent<Pawn>();
        //if (pawnToMove != null)
        //{
        //    pawnToMove.GetMyManager().gridManager.SetCellOccupied(worldStartPosition, false);
        //    //objectToMove.position = pawnToMove.GetMyManager().gridManager.GetGridPosition(Mathf.RoundToInt(pawnToMove.GetMyManager().gridManager.GetGridXYPosition(worldEndPosition).x), Mathf.RoundToInt(pawnToMove.GetMyManager().gridManager.GetGridXYPosition(worldEndPosition).y));
        //    Vector2 finalLocation = pawnToMove.GetMyManager().gridManager.GetGridXYPosition(worldEndPosition);
        //    Vector2Int finalLocationInt = new Vector2Int(Mathf.RoundToInt(finalLocation.x), Mathf.RoundToInt(finalLocation.y));
        //    objectToMove.position = pawnToMove.GetMyManager().gridManager.GetGridPosition(finalLocationInt.x, finalLocationInt.y);
        //    pawnToMove.GetMyManager().gridManager.SetCellOccupied(objectToMove.position, true);
        //}
        //else objectToMove.position = worldEndPosition;
        // Debug.Log("Time " + timeSpent);
        // Debug.Log("Shouldn't get here until finished");
        yield break;
    }

    public static IEnumerator MoveFromAtoBWithStartSound(Transform objectToMove, Vector3 worldStartPosition, Vector3 worldEndPosition, float time, AudioClip[] sounds)
    {
        // Debug.Log("Trying to move " + objectToMove.name + " from " + worldStartPosition + " to " + worldEndPosition + " in " + time + " seconds.");
        float timeSpent = 0;
        float t = 0;
        SoundManager.Instance.PlaySFX(sounds);
        while (objectToMove.position != worldEndPosition)
        {
            t += Time.deltaTime / time;
            objectToMove.position = Vector3.Lerp(worldStartPosition, worldEndPosition, t);
            timeSpent += Time.deltaTime;
            if (timeSpent > time)
                yield break;
            yield return null;
        }
        //Pawn pawnToMove = objectToMove.GetComponent<Pawn>();
        //if (pawnToMove != null)
        //{
        //    pawnToMove.GetMyManager().gridManager.SetCellOccupied(worldStartPosition, false);
        //    //objectToMove.position = pawnToMove.GetMyManager().gridManager.GetGridPosition(Mathf.RoundToInt(pawnToMove.GetMyManager().gridManager.GetGridXYPosition(worldEndPosition).x), Mathf.RoundToInt(pawnToMove.GetMyManager().gridManager.GetGridXYPosition(worldEndPosition).y));
        //    Vector2 finalLocation = pawnToMove.GetMyManager().gridManager.GetGridXYPosition(worldEndPosition);
        //    Vector2Int finalLocationInt = new Vector2Int(Mathf.RoundToInt(finalLocation.x), Mathf.RoundToInt(finalLocation.y));
        //    objectToMove.position = pawnToMove.GetMyManager().gridManager.GetGridPosition(finalLocationInt.x, finalLocationInt.y);
        //    pawnToMove.GetMyManager().gridManager.SetCellOccupied(objectToMove.position, true);
        //}
        //else objectToMove.position = worldEndPosition;
        // Debug.Log("Time " + timeSpent);
        // Debug.Log("Shouldn't get here until finished");
        yield break;
    }

    public static IEnumerator MoveFromAtoBWithStartAndEndSound(Transform objectToMove, Vector3 worldStartPosition, Vector3 worldEndPosition, float time, AudioClip[] startSounds, AudioClip[] endSounds)
    {
        // Debug.Log("Trying to move " + objectToMove.name + " from " + worldStartPosition + " to " + worldEndPosition + " in " + time + " seconds.");
        float timeSpent = 0;
        float t = 0;
        SoundManager.Instance.PlaySFX(startSounds);
        while (objectToMove.position != worldEndPosition)
        {
            t += Time.deltaTime / time;
            objectToMove.position = Vector3.Lerp(worldStartPosition, worldEndPosition, t);
            timeSpent += Time.deltaTime;
            if (timeSpent > time)
                yield break;
            yield return null;
        }
        SoundManager.Instance.PlaySFX(endSounds);
        //Pawn pawnToMove = objectToMove.GetComponent<Pawn>();
        //if (pawnToMove != null)
        //{
        //    pawnToMove.GetMyManager().gridManager.SetCellOccupied(worldStartPosition, false);
        //    //objectToMove.position = pawnToMove.GetMyManager().gridManager.GetGridPosition(Mathf.RoundToInt(pawnToMove.GetMyManager().gridManager.GetGridXYPosition(worldEndPosition).x), Mathf.RoundToInt(pawnToMove.GetMyManager().gridManager.GetGridXYPosition(worldEndPosition).y));
        //    Vector2 finalLocation = pawnToMove.GetMyManager().gridManager.GetGridXYPosition(worldEndPosition);
        //    Vector2Int finalLocationInt = new Vector2Int(Mathf.RoundToInt(finalLocation.x), Mathf.RoundToInt(finalLocation.y));
        //    objectToMove.position = pawnToMove.GetMyManager().gridManager.GetGridPosition(finalLocationInt.x, finalLocationInt.y);
        //    pawnToMove.GetMyManager().gridManager.SetCellOccupied(objectToMove.position, true);
        //}
        //else objectToMove.position = worldEndPosition;
        // Debug.Log("Time " + timeSpent);
        // Debug.Log("Shouldn't get here until finished");
        yield break;
    }

    public static IEnumerator MoveFromAtoBWithEndSound(Transform objectToMove, Vector3 worldStartPosition, Vector3 worldEndPosition, float time, AudioClip[] sounds)
    {
        // Debug.Log("Trying to move " + objectToMove.name + " from " + worldStartPosition + " to " + worldEndPosition + " in " + time + " seconds.");
        float timeSpent = 0;
        float t = 0;
        
        while (objectToMove.position != worldEndPosition)
        {
            t += Time.deltaTime / time;
            objectToMove.position = Vector3.Lerp(worldStartPosition, worldEndPosition, t);
            timeSpent += Time.deltaTime;
            if (timeSpent > time)
                yield break;
            yield return null;
        }
        SoundManager.Instance.PlaySFX(sounds);
        //Pawn pawnToMove = objectToMove.GetComponent<Pawn>();
        //if (pawnToMove != null)
        //{
        //    pawnToMove.GetMyManager().gridManager.SetCellOccupied(worldStartPosition, false);
        //    //objectToMove.position = pawnToMove.GetMyManager().gridManager.GetGridPosition(Mathf.RoundToInt(pawnToMove.GetMyManager().gridManager.GetGridXYPosition(worldEndPosition).x), Mathf.RoundToInt(pawnToMove.GetMyManager().gridManager.GetGridXYPosition(worldEndPosition).y));
        //    Vector2 finalLocation = pawnToMove.GetMyManager().gridManager.GetGridXYPosition(worldEndPosition);
        //    Vector2Int finalLocationInt = new Vector2Int(Mathf.RoundToInt(finalLocation.x), Mathf.RoundToInt(finalLocation.y));
        //    objectToMove.position = pawnToMove.GetMyManager().gridManager.GetGridPosition(finalLocationInt.x, finalLocationInt.y);
        //    pawnToMove.GetMyManager().gridManager.SetCellOccupied(objectToMove.position, true);
        //}
        //else objectToMove.position = worldEndPosition;
        // Debug.Log("Time " + timeSpent);
        // Debug.Log("Shouldn't get here until finished");
        yield break;
    }

}

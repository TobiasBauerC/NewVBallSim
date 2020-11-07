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
            yield return null;
        }
        // Debug.Log("Time " + timeSpent);
        // Debug.Log("Shouldn't get here until finished");
        yield return true;
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
            yield return null;
        }
        // Debug.Log("Time " + timeSpent);
        // Debug.Log("Shouldn't get here until finished");
        yield return true;
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
            yield return null;
        }
        SoundManager.Instance.PlaySFX(endSounds);
        // Debug.Log("Time " + timeSpent);
        // Debug.Log("Shouldn't get here until finished");
        yield return true;
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
            yield return null;
        }
        SoundManager.Instance.PlaySFX(sounds);
        // Debug.Log("Time " + timeSpent);
        // Debug.Log("Shouldn't get here until finished");
        yield return true;
    }

}

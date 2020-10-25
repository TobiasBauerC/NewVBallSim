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

}

using UnityEngine;

public static class VBallTools
{
    /// <summary>
    /// Gets the cursor's position in world space
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetCursorPosition()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0.0f;
        return cursorPos;
    }
}

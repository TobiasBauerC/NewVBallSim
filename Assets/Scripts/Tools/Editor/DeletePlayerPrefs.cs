using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeletePlayerPrefs : Editor
{
    [MenuItem("File/Delete Player Prefs")]
    private static void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}

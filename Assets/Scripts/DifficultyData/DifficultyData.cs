using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "DifficultData", order = 1)]
public class DifficultyData : ScriptableObject
{
    public int player = 0;
    public int ai = 0;
}

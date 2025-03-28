using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Puzzle Room")]
public class PuzzleRoom : Room
{
    public bool hasPuzzle;
    public Puzzle roomPuzzle;
}

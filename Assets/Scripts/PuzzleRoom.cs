using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Objects/Puzzle Room")]
public class PuzzleRoom : Room
{
    public bool hasPuzzle;
    public bool hasRandomNum;
    public Puzzle_Manager.Puzzle puzzleNum;
}

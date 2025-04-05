
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Puzzle Room")]
public class PuzzleRoom : Room
{
    public bool hasPuzzle;
    public bool hasRandomNum;
    public Puzzle.PuzzleNum roomPuzzleNum;
}

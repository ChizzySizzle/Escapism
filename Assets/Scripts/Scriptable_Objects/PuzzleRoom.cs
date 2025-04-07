
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Rooms/Puzzle Room")]
public class PuzzleRoom : Room
{
    public bool hasPuzzle;
    public bool hasPuzzleOneNum;
    public bool hasPuzzleFiveNum;
    public Puzzle.PuzzleNum roomPuzzleNum;
}

///
/// Gabriel Heiser
/// 4/29/25
/// This is a child of the room object, meant to store puzzle room specific data
/// 

using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Rooms/Puzzle Room")]
public class PuzzleRoom : Room
{
    // Represents the state of the rooms puzzle
    public bool hasPuzzle;
    // The room contains puzzle one's hidden number
    public bool hasPuzzleOneNum;
    // The room contatains puzzle five's hidden number
    public bool hasPuzzleFiveNum;
    // Enumerator selection for this rooms puzzle number
    public Puzzle.PuzzleNum roomPuzzleNum;
}

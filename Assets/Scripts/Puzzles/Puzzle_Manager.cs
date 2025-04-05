
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Manager : MonoBehaviour
{
    // Public references
    public static Puzzle_Manager instance;
    public GameObject puzzleBackground;
    public List<PuzzleRoom> puzzleRooms= new List<PuzzleRoom>();
    public Dialog_Requirement hasDonePuzzle;

    private Puzzle[] puzzles;
    private int completedPuzzles;
    private PuzzleRoom currentPuzzleRoom;
    private Puzzle currentPuzzle;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        foreach (PuzzleRoom room in puzzleRooms) {
            room.hasPuzzle = true;
        }
        puzzles = FindObjectsOfType<Puzzle>();

        hasDonePuzzle.isSatisfied = false;

        completedPuzzles = 0;
    }

    public void GetRoomPuzzle(PuzzleRoom currentRoom) {
        currentPuzzleRoom = currentRoom;
 
        foreach (Puzzle puzzle in puzzles) {
            if (currentRoom.roomPuzzleNum == puzzle.puzzleNum) {
                currentPuzzle = puzzle;
                puzzle.BeginPuzzle();
                return;
            }
        }
    }

    public void AddCompletedPuzzle() {
        completedPuzzles++;
        if (completedPuzzles == 1) {
            hasDonePuzzle.isSatisfied = true;
        }
        currentPuzzleRoom.hasPuzzle = false;
        Navigation_Manager.instance.Unpack();
    }

    public void OnEscape() {
        currentPuzzle.EndPuzzle();
    }
}

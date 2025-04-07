
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
        puzzles = FindObjectsOfType<Puzzle>();

        Game_Manager.instance.onRestart += OnRestart;
        OnRestart();
    }

    void OnRestart() {
        foreach (PuzzleRoom room in puzzleRooms) {
            room.hasPuzzle = true;
        }
        hasDonePuzzle.isSatisfied = false;

        completedPuzzles = 0;

        OnEscape();
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
        else if (completedPuzzles > 4) {
            Game_Manager.instance.GameWon();
        }
        currentPuzzleRoom.hasPuzzle = false;
        Navigation_Manager.instance.Unpack();
    }

    public void SetCurrentPuzzle(Puzzle puzzle) {
        currentPuzzle = puzzle;
    }

    public void OnEscape() {
        if (currentPuzzle != null) {
            currentPuzzle.EndPuzzle();
        }
    }
}

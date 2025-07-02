///
/// Gabriel Heiser
/// 4/29/25
/// The puzzle manager handles all higher level puzzle logic, individual puzzles communicate with the manager
/// The manager tracks the puzzles completed, puzzle related dialog requirements, and loads the correct puzzle for each room
/// 

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puzzle_Manager : MonoBehaviour
{
    // Public references
    public static Puzzle_Manager instance;
    [Header("Dialog Requirements")]
    public Dialog_Requirement hasDonePuzzle;
    public Dialog_Requirement onLastPuzzle;
    [Header("Audio")]
    public AudioClip puzzleOpened;
    public AudioClip puzzleComplete;
    public AudioClip puzzleIncorrect;
    public AudioClip buttonPressed;
    [Header("Scene References")]
    public TMP_Text numPuzzlesDoneText;
    public GameObject puzzleBackground;
    public List<PuzzleRoom> puzzleRooms= new List<PuzzleRoom>();

    // Runtime variables
    public bool inPuzzle = false;
    // Private references
    private Puzzle[] puzzles;
    private int completedPuzzles;
    private PuzzleRoom currentPuzzleRoom;
    private Puzzle currentPuzzle;

    // Initialize public instance
    private void Awake()
    {
            instance = this;
    }

    // Initialize puzzle manager
    void Start()
    {
        // Get a list of every puzzle in the scene
        puzzles = FindObjectsOfType<Puzzle>();

        // Ensure that each room has its puzzle
        foreach (PuzzleRoom room in puzzleRooms) {
            room.hasPuzzle = true;
        }
        // Reset the puzzle dialog requirements
        hasDonePuzzle.isSatisfied = false;
        onLastPuzzle.isSatisfied = false;

        // Reset the number of puzzle the player has completed and update the puzzles done text
        completedPuzzles = 0;
        numPuzzlesDoneText.text = completedPuzzles.ToString();

        // Call the escape method to auto close a puzzle if the player is currently in one
        ClosePuzzle();
    }

    // Call to set up the current room puzzle
    public void GetRoomPuzzle(PuzzleRoom currentRoom) {
        // Capture the current puzzle room
        currentPuzzleRoom = currentRoom;

        // Play the puzzle opened audio
        Game_Manager.instance.effectSource.PlayOneShot(puzzleOpened);
 
        // Check each puzzle in the puzzle list and compare it to the current rooms puzzle
        foreach (Puzzle puzzle in puzzles) {
            // If the current room and the current puzzle share the same puzzle number, begin the current puzzle
            if (currentRoom.roomPuzzleNum == puzzle.puzzleNum) {
                currentPuzzle = puzzle;
                inPuzzle = true;
                puzzle.BeginPuzzle();
                return;
            }
        }
    }

    // Called when the player completes a puzzle
    public void AddCompletedPuzzle() {
        // Add one to the total number ofcompleted puzzle
        completedPuzzles++;
        // Update the puzzles done text
        numPuzzlesDoneText.text = completedPuzzles.ToString();
        // Play the puzzle completed audio
        Game_Manager.instance.effectSource.PlayOneShot(puzzleComplete);

        // Compare the number of puzzle with a few different cases
        switch (completedPuzzles) {
            // Player has completed one puzzle
            case 1:
                hasDonePuzzle.isSatisfied = true;
                break;
            // Player is on the last puzzle
            case 6:
                onLastPuzzle.isSatisfied = true;
                break;
            // Player has completed the last puzzle
            case 7:
                Game_Manager.instance.GameWon();
                break;
        }

        // Turn off the current rooms puzzle
        currentPuzzleRoom.hasPuzzle = false;
        // Remove the puzzle button
        Navigation_Manager.instance.puzzleButton.SetActive(false);

        ///////////////// OLD LOGIC ///////////////////
        // if (completedPuzzles == 1) {
        //     hasDonePuzzle.isSatisfied = true;
        // }
        // else if (completedPuzzles == 6) {
        //     onLastPuzzle.isSatisfied = true;
        // }
        // else if (completedPuzzles == 7) {
        //     Game_Manager.instance.GameWon();
        // }
    }

    // Method for other scripts to set the current puzzle
    public void SetCurrentPuzzle(Puzzle puzzle) {
        inPuzzle = true;
        currentPuzzle = puzzle;
    }

    // Method to exit the current puzzle
    public void ClosePuzzle() {
        if (currentPuzzle != null) {
            inPuzzle = false;
            currentPuzzle.EndPuzzle();
        }
    }
}

///
/// Gabriel Heiser
/// 4/29/25
/// This is a base class for all puzzles to inherit from. provides puzzle numbers, input check, begin, end, and completion methods
/// 

using TMPro;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    // PARENT PUZZLE CLASS

    // Enum to decide the identity of each puzzle
    public enum PuzzleNum { one, two, three, four, five, six, seven }
    public PuzzleNum puzzleNum;
    // Background for every puzzle
    public GameObject puzzleBackground;
    // Status text for each puzzle
    public TMP_Text puzzleStatus;

    // Answer key for each puzzle
    protected string keyString;

    // For game start
    public virtual void Start() {
        
    }

    // For every set/reset of the game
    public virtual void OnRestart() {

    }

    // Set up puzzle background and text fields for every puzzle
    public virtual void BeginPuzzle() {
        puzzleBackground.SetActive(true);
        puzzleStatus.gameObject.SetActive(true);
        puzzleStatus.text = "";
        // play puzzle opened sound
        Game_Manager.instance.effectSource.PlayOneShot(Puzzle_Manager.instance.puzzleOpened);
    }

    // Check if the players input is equal to the answer key
    protected bool CheckInput(string input) {
        // If it is correct, the puzzle is completed
        if (input == keyString) {
            PuzzleCompleted();
            return true;
        }
        // is it is not correct, return that the input was false and play incorrect sound
        else {
            Game_Manager.instance.effectSource.PlayOneShot(Puzzle_Manager.instance.puzzleIncorrect);
            return false;
        }
    }

    // Tell the puzzle manager when each puzzle has been completed
    public virtual void PuzzleCompleted() {
        Puzzle_Manager.instance.AddCompletedPuzzle();
    }

    // Turn off puzzle objects when the puzzle gets ended
    public virtual void EndPuzzle() {
        puzzleBackground.SetActive(false);
        puzzleStatus.gameObject.SetActive(false);
    }
}

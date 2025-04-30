///
/// Gabriel Heiser
/// 4/29/25
/// Puzzle four requires the player to find a hidden code within cabinets spread across the map.
/// Once found the puzzle will provide the player with the answer code.
/// The answer number is randomized each reset.
/// 

using System.Collections;
using TMPro;
using UnityEngine;

public class Puzzle_Four_Controller : Puzzle
{
    public TMP_Text puzzleText;
    public bool textDisplaying;
    public TMP_InputField playerInput;

    public override void Start() {
        // Call parent class start
        base.Start();

        playerInput.onSubmit.AddListener(GetInput);
        // Call the restart function
        OnRestart();
    }

    // Called on start/restart to set up puzzle cariables
    public override void OnRestart() {
        // Set this puzzles key to a random six digit number
        keyString = Random.Range(100000, 999999).ToString();
        // Clear the players input field
        playerInput.text = "";
    }

    // Called when the player opens the puzzle
    public override void BeginPuzzle() {
        base.BeginPuzzle();
        // If the player currently has the code, set the puzzle text to show the code number
        if (Cabinet_Manager.instance.playerHasCode) {
            puzzleText.text = keyString;
        }
        // If the player does not have it, prompt them to find the hidden code
        else {
            puzzleText.text = "Find the hidden code.";
        }
        // Set puzzle objects active
        puzzleText.gameObject.SetActive(true);
        playerInput.gameObject.SetActive(true);
    }

    // Called to check player input
    private void GetInput(string input) {
        if (CheckInput(input) == false) {
            // If the input is incorrect, reset the text field, reactivate it, and display an incorrect answer message
            playerInput.text = "";
            playerInput.ActivateInputField();
            puzzleStatus.text = "Incorrect\nTry Again";
        }
    }

    // Call to display text to the screen, taking the message as input
    // Used to show the cabinet and lock box messages
    public void DisplayText(string text) {
        // Set the backgoround and puzzle text active
        puzzleBackground.SetActive(true);
        puzzleText.gameObject.SetActive(true);
        // Set the puzzle text to the input text
        puzzleText.text = text;
        // Set the current puzzle on the puzzle manager to allow the player to exit
        Puzzle_Manager.instance.SetCurrentPuzzle(this);
        // Start a coroutine to automatically close the window after a set period of time
        StartCoroutine("EndText");
    }

    // Coroutine to close text window automatically after a set time
    IEnumerator EndText() {
        yield return new WaitForSecondsRealtime(5);
        EndPuzzle();
    }

    // Called when puzzle is successfully completed
    public override void PuzzleCompleted() {
        // End the puzzle and call the parent end puzzle function
        EndPuzzle();
        base.PuzzleCompleted();
    }

    // Called to close out the puzzle
    public override void EndPuzzle() {
        base.EndPuzzle();
        // Stop any current coroutines and turn off all puzzle objects
        StopAllCoroutines();
        puzzleText.gameObject.SetActive(false);
        playerInput.gameObject.SetActive(false);
    }
}

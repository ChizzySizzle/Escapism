
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Two_Controller : Puzzle
{
    // Define the length of the puzzle string
    public int simonLength = 10;

    // Array of buttons to be used in the puzzle
    private Simon_Button_Controller[] simonButtons;
    // String to keep track of player input
    private string inputString = "";

    public override void Start()
    {
        // Call parent class start
        base.Start();

        simonButtons = FindObjectsOfType<Simon_Button_Controller>();

        for (int i = 0; i < simonLength; i++) {
            // Append an integer 1-4 to the puzzles answer key to the specified simon length
            keyString += Random.Range(1, 5).ToString();
        }
    }

    public override void BeginPuzzle() {
        // Display every puzzle button in the array to the player when the puzzle begins
        foreach (Simon_Button_Controller button in simonButtons) {
            button.gameObject.GetComponent<Image>().enabled = true;
        }
        // Call parent begin puzzle function
        base.BeginPuzzle();
    }

    // Called when a simon button is pressed, takes in that buttons number
    public void AddInput(int input) {
        // Append the stringified input to the total input string
        inputString += input.ToString();
        // Display the current input to the player through the puzzle status textbox
        puzzleStatus.text = inputString;

        // Compare the last input to the corresponding number on the answer string
        if (inputString[inputString.Length - 1] == keyString[inputString.Length - 1]) {
            // Compare the current length of input to the lentgh of the answer key
            if (inputString.Length == keyString.Length) {
                // Check the total input
                GetInput(inputString);
            }
            return;
        }
        // if the last input does not match the answer key, end the input early
        else {
            GetInput(inputString);
        }
    }

    // Accept players current input
    private void GetInput(string input) {
        // If the input is incorrect, display incorrect message and refresh input string
        if (CheckInput(input) == false) {
            inputString = "";
            puzzleStatus.text = "Incorrect\nTry Again";
        }
    }

    public override void PuzzleCompleted() {
        // Call end puzzle and parent puzzle complete function
        EndPuzzle();
        base.PuzzleCompleted();
    }

    public override void EndPuzzle() {
        // Turn off every simon button
        foreach(Simon_Button_Controller button in simonButtons) {
            button.gameObject.GetComponent<Image>().enabled = false;
        }
        // Call parent end puzzle function
        base.EndPuzzle();
    }
}

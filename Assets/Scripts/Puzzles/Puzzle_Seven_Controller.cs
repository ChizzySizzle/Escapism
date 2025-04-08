
using TMPro;
using UnityEngine;

public class Puzzle_Seven_Controller : Puzzle
{
    // Input field for the players answer to the puzzle
    public TMP_InputField chizzyMessageInput;
    public TMP_Text puzzleText;
    public Dialog_Requirement onLastPuzzle;
    public string[] possibleMessages;

    // Generate puzzle data at game start
    public override void Start()
    {
        // Call parent start function
        base.Start();
        
        // Add a listener function for when the player submits their input
        chizzyMessageInput.onSubmit.AddListener(GetInput); 

        // Call the restart function
        OnRestart();
    }

    // Load in puzzle information on start/restart
    public override void OnRestart()
    {
        base.OnRestart();

        // Generate a random number within the bounds of possible messages
        int randomNum = Random.Range(0, possibleMessages.Length);

        // Assign the answer key to the random message
        keyString = possibleMessages[randomNum];
        // Assign chizzy's last message to the random message
        Dialog_Manager.instance.lastMessage = possibleMessages[randomNum];
    }

    public override void BeginPuzzle() {
        // Call parent begin puzzle function
        base.BeginPuzzle();

        if (onLastPuzzle.isSatisfied) {
            // Show the player input field
            chizzyMessageInput.gameObject.SetActive(true);
        }
        else {
            puzzleText.gameObject.SetActive(true);
            puzzleText.text = "This puzzle is unavailable";
        }
    }

    // Process player input
    private void GetInput(string Input) {
        // If the input is not equal to the answer key, reset the text box and display "incorrect" message
        if (CheckInput(Input) == false) {
            chizzyMessageInput.ActivateInputField();
            puzzleStatus.text = "Incorrect\nTry Again";
        }
        chizzyMessageInput.text = "";
    }

    public override void PuzzleCompleted() {
        // End the puzzle and call the parent end puzzle function
        EndPuzzle();
        base.PuzzleCompleted();
    }

    public override void EndPuzzle() {
        // Call the parent end puzzle function and turn off player input field
        base.EndPuzzle();
        puzzleText.gameObject.SetActive(false);
        chizzyMessageInput.gameObject.SetActive(false);
    }
}


///
/// Gabriel Heiser
/// 4/29/25
/// Puzzle one requires the player to spot a hidden four-digit number in puzzle room two.
/// The answer number is randomized each reset.
/// 

using UnityEngine;
using TMPro;

public class Puzzle_One_Controller : Puzzle
{
    // Input field for the players answer to the puzzle
    public TMP_InputField randomNumberInput;

    // Generate puzzle data at game start
    public override void Start()
    {
        // Call parent start function
        base.Start();
        
        // Add a listener function for when the player submits their input
        randomNumberInput.onSubmit.AddListener(GetInput); 

        // Call the restart function
        OnRestart();
    }

    // Load in puzzle information on start/restart
    public override void OnRestart()
    {
        base.OnRestart();

        // Generate a random four digit number
        int randomNum = Random.Range(1000, 9999);

        // Set the random number room text and answer key to the random number
        Navigation_Manager.instance.puzzleOneNumber.GetComponent<TMP_Text>().text = randomNum.ToString();
        keyString = randomNum.ToString();
    }

    public override void BeginPuzzle() {
        // Call parent begin puzzle function
        base.BeginPuzzle();
        
        // Show the player input field
        randomNumberInput.gameObject.SetActive(true);
    }

    // Process player input
    private void GetInput(string Input) {
        // If the input is not equal to the answer key, reset the text box and display "incorrect" message
        if (CheckInput(Input) == false) {
            randomNumberInput.ActivateInputField();
            puzzleStatus.text = "Incorrect\nTry Again";
        }
        randomNumberInput.text = "";
    }

    public override void PuzzleCompleted() {
        // End the puzzle and call the parent end puzzle function
        EndPuzzle();
        base.PuzzleCompleted();
    }

    public override void EndPuzzle() {
        // Call the parent end puzzle function and turn off player input field
        base.EndPuzzle();
        randomNumberInput.gameObject.SetActive(false);
    }
}

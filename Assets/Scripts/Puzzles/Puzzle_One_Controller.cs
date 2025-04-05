
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
        
        // Generate a random four digit number
        int randomNum = Random.Range(1000, 9999);

        // Set the random number room text and answer key to the random number
        Navigation_Manager.instance.randomNumberText.GetComponent<TMP_Text>().text = randomNum.ToString();
        keyString = randomNum.ToString();

        // Add a listener function for when the player submits their input
        randomNumberInput.onSubmit.AddListener(GetInput); 
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
            randomNumberInput.text = "";
            randomNumberInput.ActivateInputField();
            puzzleStatus.text = "Incorrect\nTry Again";
        }
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


using UnityEngine;
using TMPro;

public class Puzzle_Three_Controller : Puzzle
{
    // Public variables
    public TMP_InputField inputField;
    public TMP_Text puzzleText;
    // private variables
    string displayText = "";

    // Called on level start
    public override void Start()
    {
        base.Start();
        // Add a listener for when the plaeyr submits their input
        inputField.onSubmit.AddListener(OnInputFieldSubmit);

        OnRestart();
    }

    // Called on start / restart to set up puzzle variables
    public override void OnRestart() {
        // clear puzzle text box and input field
        displayText = "";
        inputField.text = "";

        // Get a random constant and variable value for equation 1
        int randomConstX = Random.Range(3, 7);
        int randomVarX = Random.Range(3, 10);

        // Get a random constant and variable value for equation 2
        int randomConstY = Random.Range(3, 7);
        int randomVarY = Random.Range(3, 10);

        // Get a random constant and variable value for equation 3
        int randomConstZ = Random.Range(3, 7);
        int randomVarZ = Random.Range(3, 10);

        // Display the constant and the hidden variable to the puzzle text for all three equations
        displayText += randomConstX.ToString() + "X" + " = " + (randomConstX * randomVarX).ToString() + "\n";
        displayText += randomConstY.ToString() + "Y" + " = " + (randomConstY * randomVarY).ToString() + "\n";
        displayText += randomConstZ.ToString() + "Z" + " = " + (randomConstZ * randomVarZ).ToString() + "\n";
        // Append question text to the bottom
        displayText += "XYZ = ?";

        // Set the key string equal to the equation answer
        keyString = (randomVarX * randomVarY * randomVarZ).ToString();
    }
    
    // On submit, check player input
    public void OnInputFieldSubmit(string inputText) {
        GetInput(inputText);
    }

    // Called when the player begins the puzzle
    public override void BeginPuzzle() {
        puzzleText.text = displayText;
        inputField.gameObject.SetActive(true);
        puzzleText.gameObject.SetActive(true);
        base.BeginPuzzle();
    }

    // Check the players input, display error text if wrong, complete puzzle through parent if correct
    private void GetInput(string input) {
        // Check input method provided by parent
        if (CheckInput(input) == false) {
            inputField.text = "";
            inputField.ActivateInputField();

            puzzleStatus.text = "Incorrect\nTry Again";
        }
    }

    // Called when the puzzle is successfully completed
    public override void PuzzleCompleted() {
        EndPuzzle();
        base.PuzzleCompleted();
    }

    // Called to close out puzzle UI
    public override void EndPuzzle() {
        inputField.gameObject.SetActive(false);
        puzzleText.gameObject.SetActive(false);
        base.EndPuzzle();
    }
}

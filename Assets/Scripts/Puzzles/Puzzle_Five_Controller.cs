
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Five_Controller : Puzzle
{
    // Scene references
    public Scrollbar[] scrollbars;
    public Button submitButton;
    public string playerInput;

    // Private variabeles
    private TMP_Text puzzleFiveNumber;
    private int lastValueChange = 0;

    // Called once on level start
    public override void Start() {
        base.Start();
        // Assign the puzzle five number text box
        puzzleFiveNumber = Navigation_Manager.instance.puzzleFiveNumber.GetComponent<TMP_Text>();

        // Add listener event for when the submit button is pressed
        submitButton.onClick.AddListener(SubmitInput);

        // Call to set up puzzle
        OnRestart();
    }

    // Sets randomized puzzle variables on game start/restart
    public override void OnRestart() {
        // Clear the random string variable
        string randomString = "";
        // Clear the puzzle five number text box
        puzzleFiveNumber.text = "";

        // For every scrollbar in the array
        for (int i = 0; i < scrollbars.Length; i++) {
            // Get a random number
            int randomNum = Random.Range(0,6);
            // Append the random number to the random string
            randomString += randomNum;
            // Append the random number to the puzzle five number text box
            puzzleFiveNumber.text += randomNum + "\n";
        }

        // The key for this puzzle is the random string
        keyString = randomString;
    }

    // Called when the player opens the puzzle
    public override void BeginPuzzle()
    {
        // Set each scrollbar active
        for (int i = 0; i < scrollbars.Length; i++) {
            scrollbars[i].gameObject.SetActive(true);
        }
        // Set the submit button active
        submitButton.gameObject.SetActive(true);
        base.BeginPuzzle();
    }

    // Called when a scrollbar's value is changed
    public void ChangeInput(Scrollbar scrollbar) {
        // Multiply the scrollbars value by six and convert it to an intger
        int value = (int)(scrollbar.value * 6);

        // If the new value is different than the last
        if (value != lastValueChange) {
            // Clear out the player's input
            playerInput = "";
            // Check each scrollbar for their values and append that value to the player input string
            foreach (Scrollbar bar in scrollbars) {
                playerInput += ((int)(bar.value * 6 - .01)).ToString();
            }
            // Set the last value changed to the new value
            lastValueChange = value;
            // Change the puzzle status text to reflect the player's current input
            puzzleStatus.text = playerInput;
        }
    }

    public void SubmitInput() {
        GetInput(playerInput);
        Debug.Log(playerInput);
        Debug.Log(keyString);
    }

    public void GetInput(string input) {
        if (CheckInput(input) == false) {
        }
    }

    public override void PuzzleCompleted()
    {
        EndPuzzle();
        base.PuzzleCompleted();
    }

    public override void EndPuzzle() {
        for (int i = 0; i < scrollbars.Length; i++) {
            scrollbars[i].value = 0;
            scrollbars[i].gameObject.SetActive(false);
        }
        submitButton.gameObject.SetActive(false);
        base.EndPuzzle();
    }
}
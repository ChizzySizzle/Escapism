
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Six_Controller : Puzzle
{
    private int[] possibleRotations = new int[4];
    private Map_Button_Controller[] pieces;

    public override void Start()
    {
        // Call parent start function
        base.Start();

        pieces = FindObjectsOfType<Map_Button_Controller>();

        possibleRotations[0] = 0;
        possibleRotations[1] = 90;
        possibleRotations[2] = 180;
        possibleRotations[3] = -90;

        // Call the restart function
        OnRestart();
    }

    // Load in puzzle information on start/restart
    public override void OnRestart()
    {
        base.OnRestart();

        foreach (var piece in pieces) {
            int randomNum = Random.Range(0,4);
            int randomRotation = possibleRotations[randomNum];

            while (new Vector3(0, 0, randomRotation) == piece.correctRotation) {
                randomNum = Random.Range(0,4);
                randomRotation = possibleRotations[randomNum];
            }

            piece.transform.rotation = Quaternion.Euler(0, 0, randomRotation);
        }
    }

    public override void BeginPuzzle() {
        // Call parent begin puzzle function
        base.BeginPuzzle();

        foreach (var piece in pieces) {
            piece.gameObject.GetComponent<Image>().enabled = true;
        }
    }

    // Process player input
    private void GetInput(string Input) {
        // If the input is not equal to the answer key, reset the text box and display "incorrect" message
        if (CheckInput(Input) == false) {
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
        foreach (var piece in pieces) {
            piece.gameObject.GetComponent<Image>().enabled = false;
        }
    }
}

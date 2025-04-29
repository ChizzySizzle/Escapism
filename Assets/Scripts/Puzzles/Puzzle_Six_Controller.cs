
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Puzzle_Six_Controller : Puzzle
{
    // Scene objects
    public Image mapBackground;
    // Runtime private variables
    private int[] possibleRotations = new int[4];
    private Map_Button_Controller[] pieces;
    private List<Map_Button_Controller> answerPieces = new List<Map_Button_Controller>();

    // Called on level load
    public override void Start()
    {
        // Call parent start function
        base.Start();

        // Load up the  pieces array with all of the puzzle piece "Buttons"
        pieces = FindObjectsOfType<Map_Button_Controller>();

        // Loop through the buttons and capture their correct rotations
        foreach (Map_Button_Controller piece in pieces) {
            piece.SetCorrectRotation();

            // If the piece affects the answer, save it into the answer piece list
            if (piece.affectsAnswer) {
                answerPieces.Add(piece);
            }
        }

        // Loop to set up all possible rotation values
        for (int i = 0; i < 4; i++)
            possibleRotations[i] = 90 * i;

        // Call the restart function
        OnRestart();
    }

    // Load in puzzle information on start/restart
    public override void OnRestart()
    {
        base.OnRestart();

        // Loop through each piece and set up their random rotations
        foreach (Map_Button_Controller piece in pieces) {

            piece.isInPlace = false;

            // Choose a random number and assign a random rotation based on that number
            int randomNum = Random.Range(0,4);
            int randomRotation = possibleRotations[randomNum];

            // If the piece is an answer peice
            if (piece.affectsAnswer) {
                // If the piece has two correct solutions
                if (piece.isTwoWay) {
                    // If the new random rotation value places the piece into the correct spot, choose a new rotation
                    while (new Vector3(0, 0, randomRotation) == piece.correctRotation 
                    || new Vector3 (0, 0, randomRotation - 180) == piece.correctRotation) {
                        randomNum = Random.Range(0,4);
                        randomRotation = possibleRotations[randomNum];
                    }
                }
                // If the piece only has one correct solution
                else {
                    // If the new random rotation value places the piece into the correct spot, choose a new rotation
                    while (new Vector3(0, 0, randomRotation) == piece.correctRotation) {
                        randomNum = Random.Range(0,4);
                        randomRotation = possibleRotations[randomNum];
                    }
                }

            }
            // If the piece does not affect the answer, place it in any orientation
            else {
                randomNum = Random.Range(0,4);
                randomRotation = possibleRotations[randomNum];
            }

            // Set the pieces actual rotation to the new rotation value
            piece.transform.rotation = Quaternion.Euler(0, 0, randomRotation);
        }
    }

    // Called to open the puzzle to the player
    public override void BeginPuzzle() {
        // Call parent begin puzzle function
        base.BeginPuzzle();
        mapBackground.gameObject.SetActive(true);

        // Show all button pieces to the player
        foreach (Map_Button_Controller piece in pieces) {
            piece.gameObject.GetComponent<Image>().enabled = true;
        }
    }

    // Process player input
    public void CheckRotations() {
        bool anyWrong = false;
        // Check each answer piece's current rotation to see if the puzzle is complete
        foreach (Map_Button_Controller piece in answerPieces) {
            // If the piece is not in place, mark that there is at least one wrong and close the function
            if (!piece.isInPlace) {
                anyWrong = true;
                return;
            }
        }
        // If all are correct, complete the puzzle
        if (!anyWrong) {
            PuzzleCompleted();
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
        mapBackground.gameObject.SetActive(false);

        // Set all puzzle pieces invisible to the player
        foreach (var piece in pieces) {
            piece.gameObject.GetComponent<Image>().enabled = false;
        }
    }
}

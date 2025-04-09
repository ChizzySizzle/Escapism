
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Puzzle_Six_Controller : Puzzle
{
    public Image mapBackground;
    private int[] possibleRotations = new int[4];
    private Map_Button_Controller[] pieces;
    private List<Map_Button_Controller> answerPieces = new List<Map_Button_Controller>();

    public override void Start()
    {
        // Call parent start function
        base.Start();

        pieces = FindObjectsOfType<Map_Button_Controller>();

        foreach (Map_Button_Controller piece in pieces) {
            if (piece.affectsAnswer) {
                answerPieces.Add(piece);
            }
        }

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

        foreach (Map_Button_Controller piece in pieces) {

            piece.correctRotation = piece.transform.rotation.eulerAngles;

            int randomNum = Random.Range(0,4);
            int randomRotation = possibleRotations[randomNum];

            if (piece.affectsAnswer) {
                if (piece.isTwoWay) {
                    while (new Vector3(0, 0, randomRotation) == piece.correctRotation 
                    || new Vector3 (0, 0, randomRotation - 180) == piece.correctRotation) {
                        randomNum = Random.Range(0,4);
                        randomRotation = possibleRotations[randomNum];
                    }
                }
                else {
                    while (new Vector3(0, 0, randomRotation) == piece.correctRotation) {
                        randomNum = Random.Range(0,4);
                        randomRotation = possibleRotations[randomNum];
                    }
                }

            }
            else {
                randomNum = Random.Range(0,4);
                randomRotation = possibleRotations[randomNum];
            }

            piece.transform.rotation = Quaternion.Euler(0, 0, randomRotation);
        }
    }

    public override void BeginPuzzle() {
        // Call parent begin puzzle function
        base.BeginPuzzle();
        mapBackground.gameObject.SetActive(true);

        foreach (Map_Button_Controller piece in pieces) {
            piece.gameObject.GetComponent<Image>().enabled = true;
        }
    }

    // Process player input
    public void CheckRotations() {
        bool anyWrong = false;
        int amountLeft = 0;
        foreach (Map_Button_Controller piece in answerPieces) {
            if (!piece.isInPlace) {
                anyWrong = true;
                amountLeft++;
            }
        }
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

        foreach (var piece in pieces) {
            piece.gameObject.GetComponent<Image>().enabled = false;
        }
    }
}

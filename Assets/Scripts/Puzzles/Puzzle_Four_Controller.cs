
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
        // Call the restart function
        OnRestart();
    }

    public override void OnRestart() {
        keyString = Random.Range(100000, 999999).ToString();
        playerInput.text = "";
    }

    public override void BeginPuzzle() {
        base.BeginPuzzle();
        if (Cabinet_Manager.instance.playerHasCode) {
            puzzleText.text = keyString;
        }
        else {
            puzzleText.text = "Find the hidden code.";
        }
        puzzleText.gameObject.SetActive(true);
        playerInput.gameObject.SetActive(true);

        playerInput.onSubmit.AddListener(GetInput);
    }

    private void GetInput(string input) {
        if (CheckInput(input) == false) {
            playerInput.text = "";
            playerInput.ActivateInputField();
            puzzleStatus.text = "Incorrect\nTry Again";
        }
    }

    public void DisplayText(string text) {
        puzzleBackground.SetActive(true);
        puzzleText.gameObject.SetActive(true);
        puzzleText.text = text;
        Puzzle_Manager.instance.SetCurrentPuzzle(this);
        StartCoroutine("EndText");
    }

    IEnumerator EndText() {
        yield return new WaitForSecondsRealtime(5);
        EndPuzzle();
    }

    public override void PuzzleCompleted() {
        // End the puzzle and call the parent end puzzle function
        EndPuzzle();
        base.PuzzleCompleted();
    }

    public override void EndPuzzle() {
        base.EndPuzzle();
        StopAllCoroutines();
        puzzleText.gameObject.SetActive(false);
        playerInput.gameObject.SetActive(false);
    }
}
